// ***********************************************************************
// Assembly         : Stakeholders.Web
// Author           : George
// Created          : 02-19-2017
//
// Last Modified By : George
// Last Modified On : 02-20-2017
// ***********************************************************************
// <copyright file="GoalsController.cs" company="">
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
using Stakeholders.Web.Models.GoalViewModels;

namespace Stakeholders.Web.Controllers
{
    /// <summary>
    /// Class GoalsController.
    /// </summary>
    /// <seealso cref="Microsoft.AspNetCore.Mvc.Controller" />
    [Produces("application/json")]
    [Route("api/Goals")]
    [Authorize]
    public class GoalsController : Controller
    {
        /// <summary>
        /// The context
        /// </summary>
        private readonly IRepository<Goal> repository;

        /// <summary>
        /// The mapper
        /// </summary>
        private readonly IMapper mapper;

        /// <summary>
        /// Initializes a new instance of the <see cref="GoalsController" /> class.
        /// </summary>
        /// <param name="repository">The repository.</param>
        /// <param name="mapper">The mapper.</param>
        /// <exception cref="System.ArgumentNullException">repository
        /// or
        /// mapper</exception>
        /// <exception cref="ArgumentNullException">repository
        /// or
        /// mapper</exception>
        public GoalsController(
            IRepository<Goal> repository,
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

        // GET: api/Goals
        /// <summary>
        /// Gets the goals.
        /// </summary>
        /// <param name="start">The start.</param>
        /// <param name="count">The count.</param>
        /// <returns>GoalInfoViewModel[].</returns>
        [HttpGet]
        public GoalViewModel[] GetGoals(int start = 0, int count = 10)
        {
            return this.repository.GetAll(start, count).Select(it => this.mapper.Map<GoalViewModel>(it)).ToArray();
        }

        // GET: api/Goals/count
        /// <summary>
        /// Gets the goals count.
        /// </summary>
        /// <returns>System.Int64.</returns>
        [HttpGet("count")]
        public long GetGoalsCount()
        {
            return this.repository.Count();
        }

        // GET: api/Goals/5
        /// <summary>
        /// Gets the goal.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns>Task&lt;IActionResult&gt;.</returns>
        [HttpGet("{id}")]
        public async Task<IActionResult> GetGoal([FromRoute] long id)
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

            var result = this.mapper.Map<GoalViewModel>(entity);

            return this.Ok(result);
        }

        // PUT: api/Goals/5
        /// <summary>
        /// Puts the goal.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <param name="model">the goal.</param>
        /// <returns>Task&lt;IActionResult&gt;.</returns>
        [HttpPut("{id}")]
        public async Task<IActionResult> PutGoal(
            [FromRoute] long id,
            [FromBody] GoalViewModel model)
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

        // POST: api/Goals
        /// <summary>
        /// Posts the goal.
        /// </summary>
        /// <param name="model">the goal.</param>
        /// <returns>Task&lt;IActionResult&gt;.</returns>
        [HttpPost]
        public async Task<IActionResult> PostGoal([FromBody] GoalViewModel model)
        {
            if (!this.ModelState.IsValid)
            {
                return this.BadRequest(this.ModelState);
            }

            var entity = this.mapper.Map<Goal>(model);

            await this.repository.InsertAsync(entity);

            return this.CreatedAtAction(
                "GetGoal",
                new
                {
                    id = entity.Id
                },
                model);
        }

        // DELETE: api/Goals/5
        /// <summary>
        /// Deletes the goal.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns>Task&lt;IActionResult&gt;.</returns>
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteGoal([FromRoute] long id)
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

            var result = this.mapper.Map<GoalViewModel>(entity);

            return this.Ok(result);
        }
    }
}