// ***********************************************************************
// Assembly         : Stakeholders.Web
// Author           : George
// Created          : 02-20-2017
//
// Last Modified By : George
// Last Modified On : 02-20-2017
// ***********************************************************************
// <copyright file="ActivityTaskStatusesController.cs" company="">
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
using Stakeholders.Web.Models.ActivityTaskStatusViewModels;

namespace Stakeholders.Web.Controllers
{
    /// <summary>
    /// Class ActivityTaskStatusesController.
    /// </summary>
    /// <seealso cref="Microsoft.AspNetCore.Mvc.Controller" />
    [Produces("application/json")]
    [Route("api/ActivityTaskStatuses")]
    public class ActivityTaskStatusesController : Controller
    {
        /// <summary>
        /// The repository
        /// </summary>
        private readonly IRepository<ActivityTaskStatus> repository;

        /// <summary>
        /// Initializes a new instance of the <see cref="ActivityTaskStatusesController" /> class.
        /// </summary>
        /// <param name="repository">The repository.</param>
        public ActivityTaskStatusesController(
            IRepository<ActivityTaskStatus> repository)
        {
            if (repository == null)
            {
                throw new ArgumentNullException(nameof(repository));
            }
            
            this.repository = repository;
        }

        // GET: api/ActivityTaskStatuses
        /// <summary>
        /// Gets the activity task statuses.
        /// </summary>
        /// <param name="start">The start.</param>
        /// <param name="count">The count.</param>
        /// <returns>ActivityTaskStatusInfoViewModel[].</returns>
        [HttpGet]
        public ActivityTaskStatusInfoViewModel[] GetActivityTaskStatuses(int start = 0, int count = 10)
        {
            return
                this.repository.GetAll(start, count).Select(
                    it =>
                    {
                        var result = new ActivityTaskStatusInfoViewModel()
                        {
                            Id = it.Id
                        };
                        ActivityTaskStatusesController.UpdateViewModel(result, it);
                        return result;
                    }).ToArray();
        }

        // GET: api/ActivityTaskStatuses/count
        /// <summary>
        /// Gets the activity task statuses count.
        /// </summary>
        /// <returns>System.Int64.</returns>
        [HttpGet("count")]
        public long GetActivityTaskStatusesCount()
        {
            return this.repository.Count();
        }

        // GET: api/ActivityTaskStatuses/5
        /// <summary>
        /// Gets the activity task status.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns>Task&lt;IActionResult&gt;.</returns>
        [HttpGet("{id}")]
        public async Task<IActionResult> GetActivityTaskStatus([FromRoute] long id)
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

            var result = new ActivityTaskStatusViewModel();
            ActivityTaskStatusesController.UpdateViewModel(result, entity);

            return this.Ok(result);
        }

        // PUT: api/ActivityTasksStatuses/5
        /// <summary>
        /// Puts the activity task status.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <param name="model">The model.</param>
        /// <returns>Task&lt;IActionResult&gt;.</returns>
        [HttpPut("{id}")]
        public async Task<IActionResult> PutActivityTaskStatus(
            [FromRoute] long id,
            [FromBody] ActivityTaskStatusViewModel model)
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

        // POST: api/ActivityTaskStatuses
        /// <summary>
        /// Posts the activity task status.
        /// </summary>
        /// <param name="model">The model.</param>
        /// <returns>Task&lt;IActionResult&gt;.</returns>
        [HttpPost]
        public async Task<IActionResult> PostActivityTaskStatus([FromBody] ActivityTaskStatusViewModel model)
        {
            if (!this.ModelState.IsValid)
            {
                return this.BadRequest(this.ModelState);
            }

            var entity = new ActivityTaskStatus();

            this.UpdateEntityFromViewModel(model, entity);

            await this.repository.InsertAsync(entity);

            return this.CreatedAtAction(
                "GetActivityTaskStatus",
                new
                {
                    id = entity.Id
                },
                model);
        }

        // DELETE: api/ActivityTaskStatuses/5
        /// <summary>
        /// Deletes the activity task status.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns>Task&lt;IActionResult&gt;.</returns>
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteActivityTaskStatus([FromRoute] long id)
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

            var result = new ActivityTaskStatusViewModel();
            ActivityTaskStatusesController.UpdateViewModel(result, entity);

            return this.Ok(result);
        }

        /// <summary>
        /// Updates the view model.
        /// </summary>
        /// <param name="model">The result.</param>
        /// <param name="entity">The entity.</param>
        private static void UpdateViewModel(ActivityTaskStatusViewModel model, ActivityTaskStatus entity)
        {
            model.Name = entity.Name;
            model.NameEn = entity.NameEn;
        }

        /// <summary>
        /// Updates the entity from view model.
        /// </summary>
        /// <param name="model">The activity view model.</param>
        /// <param name="entity">The entity.</param>
        /// <returns>Task.</returns>
        private void UpdateEntityFromViewModel(ActivityTaskStatusViewModel model, ActivityTaskStatus entity)
        {
            entity.Name = model.Name;
            entity.NameEn = model.NameEn;
        }
    }
}