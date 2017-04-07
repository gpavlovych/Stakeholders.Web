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
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
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
    [Authorize]
    public class ActivityTaskStatusesController : Controller
    {
        /// <summary>
        /// The repository
        /// </summary>
        private readonly IRepository<ActivityTaskStatus> repository;

        /// <summary>
        /// The mapper
        /// </summary>
        private readonly IMapper mapper;

        /// <summary>
        /// Initializes a new instance of the <see cref="ActivityTaskStatusesController" /> class.
        /// </summary>
        /// <param name="repository">The repository.</param>
        /// <param name="mapper">The mapper.</param>
        /// <exception cref="ArgumentNullException">
        /// repository
        /// or
        /// mapper
        /// </exception>
        public ActivityTaskStatusesController(
            IRepository<ActivityTaskStatus> repository,
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

        // GET: api/ActivityTaskStatuses
        /// <summary>
        /// Gets the activity task statuses.
        /// </summary>
        /// <param name="start">The start.</param>
        /// <param name="count">The count.</param>
        /// <returns>ActivityTaskStatusInfoViewModel[].</returns>
        [HttpGet]
        public ActivityTaskStatusViewModel[] GetActivityTaskStatuses(int start = 0, int count = 10, string search = "")
        {
            return
                this.repository.GetAll(start, count, it=>string.IsNullOrEmpty(search) || it.Name.Contains(search)).Select(it => this.mapper.Map<ActivityTaskStatusViewModel>(it)).ToArray();
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

            var result = this.mapper.Map<ActivityTaskStatusViewModel>(entity);

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

            this.mapper.Map(model, entity);

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

            var entity = this.mapper.Map<ActivityTaskStatus>(model);

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

            var result = this.mapper.Map<ActivityTaskStatusViewModel>(entity);

            return this.Ok(result);
        }
    }
}