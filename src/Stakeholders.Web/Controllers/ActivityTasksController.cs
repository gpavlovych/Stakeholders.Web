// ***********************************************************************
// Assembly         : Stakeholders.Web
// Author           : George
// Created          : 02-19-2017
//
// Last Modified By : George
// Last Modified On : 02-20-2017
// ***********************************************************************
// <copyright file="ActivityTasksController.cs" company="">
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
using Stakeholders.Web.Models.ActivityTaskViewModels;

namespace Stakeholders.Web.Controllers
{
    /// <summary>
    /// Class ActivityTasksController.
    /// </summary>
    /// <seealso cref="Microsoft.AspNetCore.Mvc.Controller" />
    [Produces("application/json")]
    [Route("api/ActivityTasks")]
    public class ActivityTasksController : Controller
    {
        /// <summary>
        /// The context
        /// </summary>
        private readonly IRepository<ActivityTask> repository;

        /// <summary>
        /// The repository users
        /// </summary>
        private readonly IRepository<ApplicationUser> repositoryUsers;

        /// <summary>
        /// The repository contacts
        /// </summary>
        private readonly IRepository<Contact> repositoryContacts;

        /// <summary>
        /// The repository goals
        /// </summary>
        private readonly IRepository<Goal> repositoryGoals;

        /// <summary>
        /// The repository statuses
        /// </summary>
        private readonly IRepository<ActivityTaskStatus> repositoryStatuses;

        /// <summary>
        /// Initializes a new instance of the <see cref="ActivityTasksController" /> class.
        /// </summary>
        /// <param name="repository">The repository.</param>
        /// <param name="repositoryUsers">The repository users.</param>
        /// <param name="repositoryContacts">The repository contacts.</param>
        /// <param name="repositoryGoals">The repository goals.</param>
        /// <param name="repositoryStatuses">The repository statuses.</param>
        /// <exception cref="ArgumentNullException">repository</exception>
        public ActivityTasksController(
            IRepository<ActivityTask> repository,
            IRepository<ApplicationUser> repositoryUsers,
            IRepository<Contact> repositoryContacts,
            IRepository<Goal> repositoryGoals,
            IRepository<ActivityTaskStatus> repositoryStatuses)
        {
            if (repository == null)
            {
                throw new ArgumentNullException(nameof(repository));
            }

            if (repositoryUsers == null)
            {
                throw new ArgumentNullException(nameof(repositoryUsers));
            }

            if (repositoryContacts == null)
            {
                throw new ArgumentNullException(nameof(repositoryContacts));
            }

            if (repositoryGoals == null)
            {
                throw new ArgumentNullException(nameof(repositoryGoals));
            }

            if (repositoryStatuses == null)
            {
                throw new ArgumentNullException(nameof(repositoryStatuses));
            }

            this.repository = repository;
            this.repositoryUsers = repositoryUsers;
            this.repositoryContacts = repositoryContacts;
            this.repositoryGoals = repositoryGoals;
            this.repositoryStatuses = repositoryStatuses;
        }

        // GET: api/ActivityTasks
        /// <summary>
        /// Gets the activity tasks.
        /// </summary>
        /// <param name="start">The start.</param>
        /// <param name="count">The count.</param>
        /// <returns>ActivityTaskInfoViewModel[].</returns>
        [HttpGet]
        public ActivityTaskInfoViewModel[] GetActivityTasks(int start = 0, int count = 10)
        {
            return
                this.repository.GetAll(start, count).Select(
                    it =>
                    {
                        var result = new ActivityTaskInfoViewModel()
                        {
                            Id = it.Id
                        };
                        ActivityTasksController.UpdateViewModel(result, it);
                        return result;
                    }).ToArray();
        }

        // GET: api/ActivityTasks/count
        /// <summary>
        /// Gets the activity tasks count.
        /// </summary>
        /// <returns>System.Int64.</returns>
        [HttpGet("count")]
        public long GetActivityTasksCount()
        {
            return this.repository.Count();
        }

        // GET: api/ActivityTasks/5
        /// <summary>
        /// Gets the activity task.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns>Task&lt;IActionResult&gt;.</returns>
        [HttpGet("{id}")]
        public async Task<IActionResult> GetActivityTask([FromRoute] long id)
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

            var result = new ActivityTaskViewModel();
            ActivityTasksController.UpdateViewModel(result, entity);

            return this.Ok(result);
        }

        // PUT: api/ActivityTasks/5
        /// <summary>
        /// Puts the activity task.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <param name="model">The model.</param>
        /// <returns>Task&lt;IActionResult&gt;.</returns>
        [HttpPut("{id}")]
        public async Task<IActionResult> PutActivityTask(
            [FromRoute] long id,
            [FromBody] ActivityTaskViewModel model)
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

            this.UpdateEntityFromViewModel(model, entity);

            await this.repository.UpdateAsync(entity);

            return this.NoContent();
        }

        // POST: api/ActivityTasks
        /// <summary>
        /// Posts the activity task.
        /// </summary>
        /// <param name="model">The model.</param>
        /// <returns>Task&lt;IActionResult&gt;.</returns>
        [HttpPost]
        public async Task<IActionResult> PostActivityTask([FromBody] ActivityTaskViewModel model)
        {
            if (!this.ModelState.IsValid)
            {
                return this.BadRequest(this.ModelState);
            }

            var entity = new ActivityTask();

            this.UpdateEntityFromViewModel(model, entity);

            await this.repository.InsertAsync(entity);

            return this.CreatedAtAction(
                "GetActivityTask",
                new
                {
                    id = entity.Id
                },
                model);
        }

        // DELETE: api/ActivityTasks/5
        /// <summary>
        /// Deletes the activity task.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns>Task&lt;IActionResult&gt;.</returns>
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteActivityTask([FromRoute] long id)
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

            var result = new ActivityTaskViewModel();
            ActivityTasksController.UpdateViewModel(result, entity);

            return this.Ok(result);
        }

        /// <summary>
        /// Updates the view model.
        /// </summary>
        /// <param name="model">The result.</param>
        /// <param name="entity">The entity.</param>
        private static void UpdateViewModel(ActivityTaskViewModel model, ActivityTask entity)
        {
            model.DateCreated = entity.DateCreated.ToNullable();
            model.AssignToId = entity.AssignTo?.Id;
            model.ContactIds = entity.Contacts?.Select(it => it.Contact.Id).ToArray();
            model.CreatedById = entity.CreatedBy?.Id;
            model.GoalId = entity.Goal?.Id;
            model.ObserverUserIds = entity.ObserverUsers.Select(it => it.User.Id).ToArray();
            model.Subject = entity.Subject;
            model.DateDeadline = entity.DateDeadline;
            model.DateEnd = entity.DateEnd;
            model.Description = entity.Description;
            model.IsImportant = entity.IsImportant;
            model.StatusId = entity.Status?.Id;
            model.SuccessFactor = entity.SuccessFactor;
        }

        /// <summary>
        /// Updates the entity from view model.
        /// </summary>
        /// <param name="model">The activity view model.</param>
        /// <param name="entity">The entity.</param>
        /// <returns>Task.</returns>
        private void UpdateEntityFromViewModel(ActivityTaskViewModel model, ActivityTask entity)
        {
            var assignToId = model.AssignToId;
            entity.AssignTo = assignToId != null ? this.repositoryUsers.FindById(assignToId.Value) : null;

            entity.Contacts =
                model.ContactIds?.Select(
                    contactId => new ActivityTaskContact()
                    {
                        Contact = this.repositoryContacts.FindById(contactId)
                    }).ToList();
            var createdById = model.CreatedById; //TODO: to be auto-filled
            entity.CreatedBy = createdById != null ? this.repositoryUsers.FindById(createdById.Value) : null;
            var goalId = model.GoalId;
            entity.Goal = goalId != null ? this.repositoryGoals.FindById(goalId.Value) : null;
            entity.ObserverUsers =
                model.ObserverUserIds?.Select(
                    contactId => new ActivityTaskObserverUser()
                    {
                        User = this.repositoryUsers.FindById(contactId)
                    }).ToList();
            entity.Subject = model.Subject;
            entity.Description = model.Description;
            entity.DateDeadline = model.DateDeadline;
            entity.DateEnd = model.DateEnd;
            entity.IsImportant = model.IsImportant;

            var statusId = model.StatusId;
            entity.Status = statusId != null ? this.repositoryStatuses.FindById(statusId.Value) : null;

            entity.SuccessFactor = model.SuccessFactor;
        }
    }
}