// ***********************************************************************
// Assembly         : Stakeholders.Web
// Author           : George
// Created          : 02-20-2017
//
// Last Modified By : George
// Last Modified On : 02-20-2017
// ***********************************************************************
// <copyright file="ActivityTypesController.cs" company="">
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
using Stakeholders.Web.Models.ActivityTypeViewModels;

namespace Stakeholders.Web.Controllers
{
    /// <summary>
    /// Class ActivityTypesController.
    /// </summary>
    /// <seealso cref="Microsoft.AspNetCore.Mvc.Controller" />
    [Produces("application/json")]
    [Route("api/ActivityTypes")]
    public class ActivityTypesController : Controller
    {
        /// <summary>
        /// The context
        /// </summary>
        private readonly IRepository<ActivityType> repository;

        /// <summary>
        /// Initializes a new instance of the <see cref="ActivityTypesController" /> class.
        /// </summary>
        /// <param name="repository">The repository.</param>
        /// <exception cref="ArgumentNullException">repository</exception>
        public ActivityTypesController(
            IRepository<ActivityType> repository)
        {
            if (repository == null)
            {
                throw new ArgumentNullException(nameof(repository));
            }

            this.repository = repository;
        }

        // GET: api/ActivityTypes
        /// <summary>
        /// Gets the activity types.
        /// </summary>
        /// <param name="start">The start.</param>
        /// <param name="count">The count.</param>
        /// <returns>ActivityTypeInfoViewModel[].</returns>
        [HttpGet]
        public ActivityTypeInfoViewModel[] GetActivityTypes(int start = 0, int count = 10)
        {
            return
                this.repository.GetAll(start, count).Select(
                    it =>
                    {
                        var result = new ActivityTypeInfoViewModel()
                        {
                            Id = it.Id
                        };
                        ActivityTypesController.UpdateViewModel(result, it);
                        return result;
                    }).ToArray();
        }

        // GET: api/ActivityTypes/count
        /// <summary>
        /// Gets the activity types count.
        /// </summary>
        /// <returns>System.Int64.</returns>
        [HttpGet("count")]
        public long GetActivityTypesCount()
        {
            return this.repository.Count();
        }

        // GET: api/ActivityTypes/5
        /// <summary>
        /// Gets the activity type.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns>Task&lt;IActionResult&gt;.</returns>
        [HttpGet("{id}")]
        public async Task<IActionResult> GetActivityType([FromRoute] long id)
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

            var result = new ActivityTypeViewModel();
            ActivityTypesController.UpdateViewModel(result, entity);

            return this.Ok(result);
        }

        // PUT: api/ActivityTypes/5
        /// <summary>
        /// Puts the activity type.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <param name="model">The model.</param>
        /// <returns>Task&lt;IActionResult&gt;.</returns>
        [HttpPut("{id}")]
        public async Task<IActionResult> PutActivityType(
            [FromRoute] long id,
            [FromBody] ActivityTypeViewModel model)
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

        // POST: api/ActivityTypes
        /// <summary>
        /// Posts the activity type.
        /// </summary>
        /// <param name="model">The model.</param>
        /// <returns>Task&lt;IActionResult&gt;.</returns>
        [HttpPost]
        public async Task<IActionResult> PostActivityType([FromBody] ActivityTypeViewModel model)
        {
            if (!this.ModelState.IsValid)
            {
                return this.BadRequest(this.ModelState);
            }

            var entity = new ActivityType();

            this.UpdateEntityFromViewModel(model, entity);

            await this.repository.InsertAsync(entity);

            return this.CreatedAtAction(
                "GetActivityType",
                new
                {
                    id = entity.Id
                },
                model);
        }

        // DELETE: api/ActivityTypes/5
        /// <summary>
        /// Deletes the activity type.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns>Task&lt;IActionResult&gt;.</returns>
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteActivityType([FromRoute] long id)
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

            var result = new ActivityTypeViewModel();
            ActivityTypesController.UpdateViewModel(result, entity);

            return this.Ok(result);
        }

        /// <summary>
        /// Updates the view model.
        /// </summary>
        /// <param name="model">The result.</param>
        /// <param name="entity">The entity.</param>
        private static void UpdateViewModel(ActivityTypeViewModel model, ActivityType entity)
        {
            model.Name = entity.Name;
        }

        /// <summary>
        /// Updates the entity from view model.
        /// </summary>
        /// <param name="model">The activity view model.</param>
        /// <param name="entity">The entity.</param>
        /// <returns>Task.</returns>
        private void UpdateEntityFromViewModel(ActivityTypeViewModel model, ActivityType entity)
        {
            entity.Name = model.Name;
        }
    }
}