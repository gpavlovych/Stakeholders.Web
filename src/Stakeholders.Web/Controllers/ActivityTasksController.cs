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
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
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
    [Authorize]
    public class ActivityTasksController : Controller
    {
        /// <summary>
        /// The context
        /// </summary>
        private readonly IRepository<ActivityTask> repository;

        /// <summary>
        /// The mapper
        /// </summary>
        private readonly IMapper mapper;

        /// <summary>
        /// Initializes a new instance of the <see cref="ActivityTasksController" /> class.
        /// </summary>
        /// <param name="repository">The repository.</param>
        /// <param name="mapper">The mapper.</param>
        /// <exception cref="ArgumentNullException">
        /// repository
        /// or
        /// mapper
        /// </exception>
        public ActivityTasksController(
            IRepository<ActivityTask> repository,
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

        // GET: api/ActivityTasks
        /// <summary>
        /// Gets the activity tasks.
        /// </summary>
        /// <param name="start">The start.</param>
        /// <param name="count">The count.</param>
        /// <returns>ActivityTaskInfoViewModel[].</returns>
        [HttpGet]
        public ActivityTaskViewModel[] GetActivityTasks(int start = 0, int count = 10)
        {
            return this.repository.GetAll(start, count).Select(it => this.mapper.Map<ActivityTaskViewModel>(it)).ToArray();
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

            var result = this.mapper.Map<ActivityTaskViewModel>(entity);

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

            this.mapper.Map(model, entity);

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

            var entity = this.mapper.Map<ActivityTask>(model);

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

            var result = this.mapper.Map<ActivityTaskViewModel>(entity);

            return this.Ok(result);
        }
    }
}