// ***********************************************************************
// Assembly         : Stakeholders.Web
// Author           : George
// Created          : 02-19-2017
//
// Last Modified By : George
// Last Modified On : 04-07-2017
// ***********************************************************************
// <copyright file="ActivitiesController.cs" company="">
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
using Stakeholders.Web.Models.ActivityViewModels;

namespace Stakeholders.Web.Controllers
{
    /// <summary>
    /// Class ActivitiesController.
    /// </summary>
    /// <seealso cref="Microsoft.AspNetCore.Mvc.Controller" />
    [Produces("application/json")]
    [Route("api/Activities")]
    [Authorize]
    public class ActivitiesController : Controller
    {
        private readonly IPeriodProvider periodProvider;

        /// <summary>
        /// The context
        /// </summary>
        private readonly IRepository<Activity> repository;

        /// <summary>
        /// The mapper
        /// </summary>
        private readonly IMapper mapper;

        /// <summary>
        /// Initializes a new instance of the <see cref="ActivitiesController" /> class.
        /// </summary>
        /// <param name="repository">The repository.</param>
        /// <param name="mapper">The mapper.</param>
        /// <exception cref="ArgumentNullException">repository
        /// or
        /// mapper</exception>
        public ActivitiesController(
            IPeriodProvider periodProvider,
            IRepository<Activity> repository,
            IMapper mapper)
        {
            if (periodProvider == null)
            {
                throw new ArgumentNullException(nameof(periodProvider));
            }

            if (repository == null)
            {
                throw new ArgumentNullException(nameof(repository));
            }

            if (mapper == null)
            {
                throw new ArgumentNullException(nameof(mapper));
            }

            this.periodProvider = periodProvider;
            this.repository = repository;
            this.mapper = mapper;
        }

        // GET: api/Activities
        /// <summary>
        /// Gets the activities.
        /// </summary>
        /// <param name="start">The start.</param>
        /// <param name="count">The count.</param>
        /// <param name="search">The search.</param>
        /// <param name="period">The period (1-this year, 2-this quarter, 3-this month, 4-this week).</param>
        /// <param name="organizationId">The organization identifier.</param>
        /// <param name="organizationCategoryId">The organization category identifier.</param>
        /// <param name="contactId">The contact identifier.</param>
        /// <returns>ActivityInfoViewModel[].</returns>
        [HttpGet]
        public ActivityViewModel[] GetActivities(
            int start = 0,
            int count = 10,
            string search = "",
            int? period = null,
            long? organizationId = null,
            long? organizationCategoryId = null,
            long? contactId = null)
        {
            DateRange periodRange = null;
            switch (period)
            {
                case 1:
                    //this year
                    periodRange = this.periodProvider.GetThisYearRange();
                    break;
                case 2:
                    //this quarter
                    periodRange = this.periodProvider.GetThisQuarterRange();
                    break;
                case 3:
                    //this month
                    periodRange = this.periodProvider.GetThisMonthRange();
                    break;
                case 4:
                    //this week
                    periodRange = this.periodProvider.GetThisWeekRange();
                    break;
            }

            DateTime? startPeriod = periodRange?.MinDate;
            DateTime? endPeriod = periodRange?.MaxDate;

            return
                this.repository.GetAll(
                        start,
                        count,
                        activity =>
                            (string.IsNullOrEmpty(search) || activity.Subject.Contains(search)) &&
                            ((contactId == null) || (activity.Contact.Id == contactId)) &&
                            ((organizationId == null) || (activity.Contact.Organization.Id == organizationId)) &&
                            ((organizationCategoryId == null) ||
                             (activity.Contact.Organization.Category.Id == organizationCategoryId)) &&
                            (((startPeriod == null) || (startPeriod <= activity.DateActivity)) &&
                             ((endPeriod == null) || (activity.DateActivity <= endPeriod))))
                    .Select(it => this.mapper.Map<ActivityViewModel>(it))
                    .ToArray();
        }

        // GET: api/Activities/count
        /// <summary>
        /// Gets the activities count.
        /// </summary>
        /// <returns>System.Int64.</returns>
        [HttpGet("count")]
        public long GetActivitiesCount()
        {
            return this.repository.Count();
        }

        // GET: api/Activities/5
        /// <summary>
        /// Gets the activity.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns>Task&lt;IActionResult&gt;.</returns>
        [HttpGet("{id}")]
        public async Task<IActionResult> GetActivity([FromRoute] long id)
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

            var result = this.mapper.Map<ActivityViewModel>(entity);

            return this.Ok(result);
        }

        // PUT: api/Activities/5
        /// <summary>
        /// Puts the activity.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <param name="model">The activity.</param>
        /// <returns>Task&lt;IActionResult&gt;.</returns>
        [HttpPut("{id}")]
        public async Task<IActionResult> PutActivity(
            [FromRoute] long id,
            [FromBody] ActivityViewModel model)
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

        // POST: api/Activities
        /// <summary>
        /// Posts the activity.
        /// </summary>
        /// <param name="model">The activity.</param>
        /// <returns>Task&lt;IActionResult&gt;.</returns>
        [HttpPost]
        public async Task<IActionResult> PostActivity([FromBody] ActivityViewModel model)
        {
            if (!this.ModelState.IsValid)
            {
                return this.BadRequest(this.ModelState);
            }

            var entity = this.mapper.Map<Activity>(model);
            entity.DateCreated = DateTime.UtcNow;
            await this.repository.InsertAsync(entity);

            return this.CreatedAtAction(
                "GetActivity",
                new
                {
                    id = entity.Id
                },
                model);
        }

        // DELETE: api/Activities/5
        /// <summary>
        /// Deletes the activity.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns>Task&lt;IActionResult&gt;.</returns>
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteActivity([FromRoute] long id)
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

            var result = this.mapper.Map<ActivityViewModel>(entity);

            return this.Ok(result);
        }
    }
}