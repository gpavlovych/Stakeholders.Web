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
        /// The repository tasks
        /// </summary>
        private readonly IRepository<ActivityTask> repositoryTasks;

        /// <summary>
        /// The repository companies
        /// </summary>
        private readonly IRepository<Company> repositoryCompanies;

        /// <summary>
        /// The repository users
        /// </summary>
        private readonly IRepository<ApplicationUser> repositoryUsers;

        /// <summary>
        /// The repository activity types
        /// </summary>
        private readonly IRepository<ActivityType> repositoryActivityTypes;

        private readonly IRepository<Contact> repositoryContacts;

        /// <summary>
        /// Initializes a new instance of the <see cref="ActivitiesController" /> class.
        /// </summary>
        /// <param name="repository">The repository.</param>
        /// <param name="repositoryTasks">The repository tasks.</param>
        /// <param name="repositoryCompanies">The repository companies.</param>
        /// <param name="repositoryUsers">The repository users.</param>
        /// <param name="repositoryActivityTypes">The repository activity types.</param>
        /// <param name="repositoryContacts">The repository contacts.</param>
        public ActivitiesController(
            IRepository<Activity> repository,
            IRepository<ActivityTask> repositoryTasks,
            IRepository<Company> repositoryCompanies,
            IRepository<ApplicationUser> repositoryUsers,
            IRepository<ActivityType> repositoryActivityTypes,
            IRepository<Contact> repositoryContacts)
        {
            if (repository == null)
            {
                throw new ArgumentNullException(nameof(repository));
            }

            if (repositoryTasks == null)
            {
                throw new ArgumentNullException(nameof(repositoryTasks));
            }

            if (repositoryCompanies == null)
            {
                throw new ArgumentNullException(nameof(repositoryCompanies));
            }

            if (repositoryUsers == null)
            {
                throw new ArgumentNullException(nameof(repositoryUsers));
            }

            if (repositoryActivityTypes == null)
            {
                throw new ArgumentNullException(nameof(repositoryActivityTypes));
            }

            if (repositoryContacts == null)
            {
                throw new ArgumentNullException(nameof(repositoryContacts));
            }

            this.repository = repository;
            this.repositoryTasks = repositoryTasks;
            this.repositoryCompanies = repositoryCompanies;
            this.repositoryUsers = repositoryUsers;
            this.repositoryActivityTypes = repositoryActivityTypes;
            this.repositoryContacts = repositoryContacts;
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
            return
                this.repository.GetAll(start, count).Select(
                    it =>
                    {
                        var result = new ActivityViewModel()
                        {
                            Id = it.Id
                        };
                        ActivitiesController.UpdateViewModel(result, it);
                        return result;
                    }).ToArray();
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

            var result = new ActivityViewModel();
            ActivitiesController.UpdateViewModel(result, entity);

            return this.Ok(result);
        }

        // PUT: api/Activities/5
        /// <summary>
        /// Puts the activity.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <param name="activityViewModel">The activity.</param>
        /// <returns>Task&lt;IActionResult&gt;.</returns>
        [HttpPut("{id}")]
        public async Task<IActionResult> PutActivity(
            [FromRoute] long id,
            [FromBody] ActivityViewModel activityViewModel)
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

            this.UpdateEntityFromViewModel(activityViewModel, entity);

            await this.repository.UpdateAsync(entity);

            return this.NoContent();
        }

        // POST: api/Activities
        /// <summary>
        /// Posts the activity.
        /// </summary>
        /// <param name="activityViewModel">The activity.</param>
        /// <returns>Task&lt;IActionResult&gt;.</returns>
        [HttpPost]
        public async Task<IActionResult> PostActivity([FromBody] ActivityViewModel activityViewModel)
        {
            if (!this.ModelState.IsValid)
            {
                return this.BadRequest(this.ModelState);
            }

            var entity = new Activity();

            this.UpdateEntityFromViewModel(activityViewModel, entity);

            await this.repository.InsertAsync(entity);

            return this.CreatedAtAction(
                "GetActivity",
                new
                {
                    id = entity.Id
                },
                activityViewModel);
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

            var result = new ActivityViewModel();
            ActivitiesController.UpdateViewModel(result, entity);

            return this.Ok(result);
        }

        /// <summary>
        /// Updates the view model.
        /// </summary>
        /// <param name="result">The result.</param>
        /// <param name="entity">The entity.</param>
        private static void UpdateViewModel(ActivityViewModel result, Activity entity)
        {
            result.TaskId = entity.Task?.Id;
            result.CompanyId = entity.Company?.Id;
            result.ContactId = entity.Contact?.Id;
            result.DateActivity = entity.DateActivity;
            result.DateCreated = entity.DateCreated.ToNullable();
            result.Description = entity.Description;
            result.ObserverCompanyIds =
                entity.ObserverUsersCompanies?.Where(it => it.Company != null).Select(it => it.Company.Id).ToArray();
            result.ObserverUserIds =
                entity.ObserverUsersCompanies?.Where(it => it.User != null).Select(it => it.User.Id).ToArray();
            result.Subject = entity.Subject;
            result.TypeId = entity.Type?.Id;
            result.UserId = entity.User?.Id;
        }

        /// <summary>
        /// Updates the entity from view model.
        /// </summary>
        /// <param name="activityViewModel">The activity view model.</param>
        /// <param name="entity">The entity.</param>
        /// <returns>Task.</returns>
        private void UpdateEntityFromViewModel(ActivityViewModel activityViewModel, Activity entity)
        {
            entity.Subject = activityViewModel.Subject;
            entity.Description = activityViewModel.Description;
            var contactId = activityViewModel.ContactId;
            entity.Contact = contactId != null ? this.repositoryContacts.FindById(contactId.Value) : null;

            var taskId = activityViewModel.TaskId;
            entity.Task = taskId != null ? this.repositoryTasks.FindById(taskId.Value) : null;

            entity.DateActivity = activityViewModel.DateActivity ?? DateTime.UtcNow;

            var activityObserverCompanies = activityViewModel.ObserverCompanyIds?
                .Select(
                    observerCompanyId => new ActivityObserverUserCompany()
                    {
                        Company = this.repositoryCompanies.FindById(observerCompanyId)
                    }) ?? Enumerable.Empty<ActivityObserverUserCompany>();

            var activityObserverUsers = activityViewModel.ObserverUserIds?.Select(
                observerUserId => new ActivityObserverUserCompany()
                {
                    User = this.repositoryUsers.FindById(observerUserId)
                }) ?? Enumerable.Empty<ActivityObserverUserCompany>();

            entity.ObserverUsersCompanies = activityObserverCompanies.Concat(activityObserverUsers).ToList();

            var typeId = activityViewModel.TypeId;
            entity.Type = typeId != null ? this.repositoryActivityTypes.FindById(typeId.Value) : null;

            var userId = activityViewModel.UserId;
            entity.User = userId != null ? this.repositoryUsers.FindById(userId.Value) : null;

            var companyId = activityViewModel.CompanyId;
            entity.Company = companyId != null ? this.repositoryCompanies.FindById(companyId.Value) : null;
        }
    }
}