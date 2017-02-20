// ***********************************************************************
// Assembly         : Stakeholders.Web
// Author           : George
// Created          : 02-19-2017
//
// Last Modified By : George
// Last Modified On : 02-19-2017
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
    public class ActivitiesController : Controller
    {
        /// <summary>
        /// The context
        /// </summary>
        private readonly IRepository<Activity> repository;

        /// <summary>
        /// The mapper
        /// </summary>
        private readonly IMapper mapper;

        /// <summary>
        /// Initializes a new instance of the <see cref="ActivitiesController"/> class.
        /// </summary>
        /// <param name="repository">The repository.</param>
        /// <param name="mapper">The mapper.</param>
        /// <exception cref="ArgumentNullException">
        /// repository
        /// or
        /// mapper
        /// </exception>
        public ActivitiesController(
            IRepository<Activity> repository,
            IMapper mapper)
        {
            if (repository == null)
            {
                throw new ArgumentNullException(nameof(repository));
            }

            if (mapper == null)
            {
                throw new ArgumentNullException(nameof(mapper));
            }

            this.repository = repository;
            this.mapper = mapper;
        }

        // GET: api/Activities
        /// <summary>
        /// Gets the activities.
        /// </summary>
        /// <param name="start">The start.</param>
        /// <param name="count">The count.</param>
        /// <returns>ActivityInfoViewModel[].</returns>
        [HttpGet]
        public ActivityViewModel[] GetActivities(int start = 0, int count = 10)
        {
            return this.repository.GetAll(start, count).Select(it => this.mapper.Map<ActivityViewModel>(it)).ToArray();
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

        ///// <summary>
        ///// Updates the view model.
        ///// </summary>
        ///// <param name="result">The result.</param>
        ///// <param name="entity">The entity.</param>
        //private static void UpdateViewModel(ActivityViewModel result, Activity entity)
        //{
        //    result.TaskId = entity.Task?.Id;
        //    result.CompanyId = entity.Company?.Id;
        //    result.ContactId = entity.Contact?.Id;
        //    result.DateActivity = entity.DateActivity;
        //    result.DateCreated = entity.DateCreated.ToNullable();
        //    result.Description = entity.Description;
        //    result.ObserverCompanyIds =
        //        entity.ObserverUsersCompanies?.Where(it => it.Company != null).Select(it => it.Company.Id).ToArray();
        //    result.ObserverUserIds =
        //        entity.ObserverUsersCompanies?.Where(it => it.User != null).Select(it => it.User.Id).ToArray();
        //    result.Subject = entity.Subject;
        //    result.TypeId = entity.Type?.Id;
        //    result.UserId = entity.User?.Id;
        //}

        ///// <summary>
        ///// Updates the entity from view model.
        ///// </summary>
        ///// <param name="activityViewModel">The activity view model.</param>
        ///// <param name="entity">The entity.</param>
        ///// <returns>Task.</returns>
        //private void UpdateEntityFromViewModel(ActivityViewModel activityViewModel, Activity entity)
        //{
        //    entity.Subject = activityViewModel.Subject;
        //    entity.Description = activityViewModel.Description;
        //    var contactId = activityViewModel.ContactId;
        //    entity.Contact = contactId != null ? this.repositoryContacts.FindById(contactId.Value) : null;

        //    var taskId = activityViewModel.TaskId;
        //    entity.Task = taskId != null ? this.repositoryTasks.FindById(taskId.Value) : null;

        //    entity.DateActivity = activityViewModel.DateActivity ?? DateTime.UtcNow;

        //    var activityObserverCompanies = activityViewModel.ObserverCompanyIds?
        //        .Select(
        //            observerCompanyId => new ActivityObserverUserCompany()
        //            {
        //                Company = this.repositoryCompanies.FindById(observerCompanyId)
        //            }) ?? Enumerable.Empty<ActivityObserverUserCompany>();

        //    var activityObserverUsers = activityViewModel.ObserverUserIds?.Select(
        //        observerUserId => new ActivityObserverUserCompany()
        //        {
        //            User = this.repositoryUsers.FindById(observerUserId)
        //        }) ?? Enumerable.Empty<ActivityObserverUserCompany>();

        //    entity.ObserverUsersCompanies = activityObserverCompanies.Concat(activityObserverUsers).ToList();

        //    var typeId = activityViewModel.TypeId;
        //    entity.Type = typeId != null ? this.repositoryActivityTypes.FindById(typeId.Value) : null;

        //    var userId = activityViewModel.UserId;
        //    entity.User = userId != null ? this.repositoryUsers.FindById(userId.Value) : null;

        //    var companyId = activityViewModel.CompanyId;
        //    entity.Company = companyId != null ? this.repositoryCompanies.FindById(companyId.Value) : null;
        //}
    }
}