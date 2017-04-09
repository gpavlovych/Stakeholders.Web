// ***********************************************************************
// Assembly         : Stakeholders.Web
// Author           : George
// Created          : 02-19-2017
//
// Last Modified By : George
// Last Modified On : 04-09-2017
// ***********************************************************************
// <copyright file="OrganizationCategoriesController.cs" company="">
//     Copyright (c) . All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************
using System;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Stakeholders.Web.Data;
using Stakeholders.Web.Models;
using Stakeholders.Web.Models.OrganizationCategoryViewModels;

namespace Stakeholders.Web.Controllers
{
    /// <summary>
    /// Class OrganizationCategoriesController.
    /// </summary>
    /// <seealso cref="Microsoft.AspNetCore.Mvc.Controller" />
    [Produces("application/json")]
    [Route("api/OrganizationCategories")]
    [Authorize]
    public class OrganizationCategoriesController : Controller
    {
        /// <summary>
        /// The source
        /// </summary>
        private readonly IDataSource<OrganizationCategory> source;

        /// <summary>
        /// The context
        /// </summary>
        private readonly IRepository<OrganizationCategory> repository;

        /// <summary>
        /// The mapper
        /// </summary>
        private readonly IMapper mapper;

        /// <summary>
        /// Initializes a new instance of the <see cref="OrganizationCategoriesController" /> class.
        /// </summary>
        /// <param name="source">The source.</param>
        /// <param name="repository">The repository.</param>
        /// <param name="mapper">The mapper.</param>
        /// <exception cref="ArgumentNullException">repository
        /// or
        /// mapper</exception>
        /// <exception cref="System.ArgumentNullException">repository
        /// or
        /// mapper</exception>
        public OrganizationCategoriesController(
            IDataSource<OrganizationCategory> source, 
            IRepository<OrganizationCategory> repository,
            IMapper mapper)
        {
            if (source == null)
            {
                throw new ArgumentNullException(nameof(source));
            }

            if (repository == null)
            {
                throw new ArgumentNullException(nameof(repository));
            }

            if (mapper == null)
            {
                throw new ArgumentNullException(nameof(mapper));
            }

            this.source = source;
            this.repository = repository;
            this.mapper = mapper;
        }

        // GET: api/OrganizationCategories
        /// <summary>
        /// Gets the organization categories.
        /// </summary>
        /// <param name="start">The start.</param>
        /// <param name="count">The count.</param>
        /// <param name="search">The search.</param>
        /// <param name="period">The period.</param>
        /// <returns>OrganizationCategoryViewModel[].</returns>
        [HttpGet]
        public OrganizationCategoryViewModel[] GetOrganizationCategories(int start = 0, int count = 10, string search = "", int? period = null, int? includeStats = 0)
        {
            DateTime? startPeriod = null;
            DateTime? endPeriod = null;
            switch (period)
            {
                case 1:

                    //this year
                    startPeriod = DateTime.UtcNow.AddYears(-1);
                    endPeriod = DateTime.UtcNow;
                    break;
                case 2:

                    //this quarter
                    startPeriod = DateTime.UtcNow.AddMonths(-3);
                    endPeriod = DateTime.UtcNow;
                    break;
                case 3:

                    //this month
                    startPeriod = DateTime.UtcNow.AddMonths(-1);
                    endPeriod = DateTime.UtcNow;
                    break;
                case 4:

                    //this week
                    startPeriod = DateTime.UtcNow.AddDays(-7);
                    endPeriod = DateTime.UtcNow;
                    break;
            }

            var query = this.source.GetDataQueryable()
                .Where(it => string.IsNullOrEmpty(search) || it.Name.Contains(search))
                .Skip(start)
                .Take(count);
            if (includeStats == 1)
            {
                return query.Select(
                        it => new Tuple<OrganizationCategory, long, long, long>(

                            //Item1 - organizationCategory
                            it,

                            //Item2 - activites
                            it.Organizations.SelectMany(org => org.Contacts)
                                .SelectMany(cont => cont.Activities)
                                .Distinct()
                                .Count(
                                    activity =>
                                        ((startPeriod == null) || (activity.DateActivity >= startPeriod)) &&
                                        ((endPeriod == null) || (activity.DateActivity <= endPeriod))),

                            //Item3 - tasksTotal
                            it.Organizations.SelectMany(org => org.Contacts)
                                .SelectMany(cont => cont.Tasks)
                                .Select(taskContact => taskContact.Task)
                                .Distinct()
                                .Count(
                                    task =>
                                        ((startPeriod == null) || (task.DateDeadline >= startPeriod)) &&
                                        ((endPeriod == null) || (task.DateDeadline <= endPeriod))),

                            //Item4 - tasksDone
                            it.Organizations.SelectMany(org => org.Contacts)
                                .SelectMany(cont => cont.Tasks)
                                .Select(taskContact => taskContact.Task)
                                .Distinct()
                                .Count(
                                    task =>
                                        (task.Status.Alias == "Done") &&
                                        ((startPeriod == null) || (task.DateDeadline >= startPeriod)) &&
                                        ((endPeriod == null) || (task.DateDeadline <= endPeriod)))
                        ))
                    .ToList()
                    .Select(
                        it =>
                        {
                            var organizationCategory = it.Item1;
                            var activities = it.Item2;
                            var tasksTotal = it.Item3;
                            var tasksDone = it.Item4;
                            var result = this.mapper.Map<OrganizationCategoryViewModel>(organizationCategory);
                            result.TasksCompletedPercentage = tasksTotal != 0 ? tasksDone*100.0/tasksTotal : 0;
                            result.ActivitiesNumber = activities;
                            result.TasksNumber = tasksTotal;
                            return result;
                        })
                    .ToArray();
            }
            else
            {
                return query.ToList().Select(it => this.mapper.Map<OrganizationCategoryViewModel>(it)).ToArray();
            }
        }

        // GET: api/OrganizationCategories/count
        /// <summary>
        /// Gets the organization categories count.
        /// </summary>
        /// <returns>System.Int64.</returns>
        [HttpGet("count")]
        public long GetOrganizationCategoriesCount()
        {
            return this.repository.Count();
        }

        // GET: api/OrganizationCategories/5
        /// <summary>
        /// Gets the organization category.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns>Task&lt;IActionResult&gt;.</returns>
        [HttpGet("{id}")]
        public async Task<IActionResult> GetOrganizationCategory([FromRoute] long id)
        {
            if (!this.ModelState.IsValid)
            {
                return this.BadRequest(this.ModelState);
            }

            var entity = await this.repository.FindByIdAsync(id);
            if (entity == null)
            {
                return this.NotFound();
            }

            var result = this.mapper.Map<OrganizationCategoryViewModel>(entity);

            return this.Ok(result);
        }

        // PUT: api/OrganizationCategories/5
        /// <summary>
        /// Puts the organization category.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <param name="model">the organization category.</param>
        /// <returns>Task&lt;IActionResult&gt;.</returns>
        [HttpPut("{id}")]
        public async Task<IActionResult> PutOrganizationCategory(
            [FromRoute] long id,
            [FromBody] OrganizationCategoryViewModel model)
        {
            if (!this.ModelState.IsValid)
            {
                return this.BadRequest(this.ModelState);
            }

            var entity = await this.repository.FindByIdAsync(id);
            if (entity == null)
            {
                return this.NotFound();
            }

            this.mapper.Map(model, entity);

            await this.repository.UpdateAsync(entity);

            return this.NoContent();
        }

        // POST: api/OrganizationCategories
        /// <summary>
        /// Posts the organization category.
        /// </summary>
        /// <param name="model">the organization category.</param>
        /// <returns>Task&lt;IActionResult&gt;.</returns>
        [HttpPost]
        public async Task<IActionResult> PostOrganizationCategory([FromBody] OrganizationCategoryViewModel model)
        {
            if (!this.ModelState.IsValid)
            {
                return this.BadRequest(this.ModelState);
            }

            var entity = this.mapper.Map<OrganizationCategory>(model);

            await this.repository.InsertAsync(entity);

            return this.CreatedAtAction(
                "GetOrganizationCategory",
                new
                {
                    id = entity.Id
                },
                model);
        }

        // DELETE: api/OrganizationCategories/5
        /// <summary>
        /// Deletes the organization category.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns>Task&lt;IActionResult&gt;.</returns>
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteOrganizationCategory([FromRoute] long id)
        {
            if (!this.ModelState.IsValid)
            {
                return this.BadRequest(this.ModelState);
            }

            var entity = await this.repository.FindByIdAsync(id);
            if (entity == null)
            {
                return this.NotFound();
            }

            await this.repository.DeleteAsync(entity);

            var result = this.mapper.Map<OrganizationCategoryViewModel>(entity);

            return this.Ok(result);
        }
    }
}