// ***********************************************************************
// Assembly         : Stakeholders.Web
// Author           : George
// Created          : 02-19-2017
//
// Last Modified By : George
// Last Modified On : 02-20-2017
// ***********************************************************************
// <copyright file="RolesController.cs" company="">
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
using Stakeholders.Web.Models.RoleViewModels;

namespace Stakeholders.Web.Controllers
{
    /// <summary>
    /// Class RolesController.
    /// </summary>
    /// <seealso cref="Microsoft.AspNetCore.Mvc.Controller" />
    [Produces("application/json")]
    [Route("api/Roles")]
    [Authorize]
    public class RolesController : Controller
    {
        /// <summary>
        /// The context
        /// </summary>
        private readonly IRepository<Role> repository;

        /// <summary>
        /// The mapper
        /// </summary>
        private readonly IMapper mapper;

        /// <summary>
        /// Initializes a new instance of the <see cref="RolesController" /> class.
        /// </summary>
        /// <param name="repository">The repository.</param>
        /// <param name="mapper">The mapper.</param>
        /// <exception cref="System.ArgumentNullException">
        /// repository
        /// or
        /// mapper
        /// </exception>
        /// <exception cref="ArgumentNullException">repository
        /// or
        /// mapper</exception>
        public RolesController(
            IRepository<Role> repository,
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

        // GET: api/Roles
        /// <summary>
        /// Gets the roles.
        /// </summary>
        /// <param name="start">The start.</param>
        /// <param name="count">The count.</param>
        /// <returns>RoleInfoViewModel[].</returns>
        [HttpGet]
        public RoleViewModel[] GetRoles(int start = 0, int count = 10)
        {
            return this.repository.GetAll(start, count).Select(it => this.mapper.Map<RoleViewModel>(it)).ToArray();
        }

        // GET: api/Roles/count
        /// <summary>
        /// Gets the roles count.
        /// </summary>
        /// <returns>System.Int64.</returns>
        [HttpGet("count")]
        public long GetRolesCount()
        {
            return this.repository.Count();
        }

        // GET: api/Roles/5
        /// <summary>
        /// Gets the role.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns>Task&lt;IActionResult&gt;.</returns>
        [HttpGet("{id}")]
        public async Task<IActionResult> GetRole([FromRoute] long id)
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

            var result = this.mapper.Map<RoleViewModel>(entity);

            return this.Ok(result);
        }

        // PUT: api/Roles/5
        /// <summary>
        /// Puts the role.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <param name="model">the role.</param>
        /// <returns>Task&lt;IActionResult&gt;.</returns>
        [HttpPut("{id}")]
        public async Task<IActionResult> PutRole(
            [FromRoute] long id,
            [FromBody] RoleViewModel model)
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

        // POST: api/Roles
        /// <summary>
        /// Posts the role.
        /// </summary>
        /// <param name="model">the role.</param>
        /// <returns>Task&lt;IActionResult&gt;.</returns>
        [HttpPost]
        public async Task<IActionResult> PostRole([FromBody] RoleViewModel model)
        {
            if (!this.ModelState.IsValid)
            {
                return this.BadRequest(this.ModelState);
            }

            var entity = this.mapper.Map<Role>(model);

            await this.repository.InsertAsync(entity);

            return this.CreatedAtAction(
                "GetRole",
                new
                {
                    id = entity.Id
                },
                model);
        }

        // DELETE: api/Roles/5
        /// <summary>
        /// Deletes the role.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns>Task&lt;IActionResult&gt;.</returns>
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteRole([FromRoute] long id)
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

            var result = this.mapper.Map<RoleViewModel>(entity);

            return this.Ok(result);
        }
    }
}