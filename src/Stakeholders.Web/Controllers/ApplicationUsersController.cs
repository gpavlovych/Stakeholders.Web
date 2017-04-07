// ***********************************************************************
// Assembly         : Stakeholders.Web
// Author           : George
// Created          : 02-19-2017
//
// Last Modified By : George
// Last Modified On : 02-20-2017
// ***********************************************************************
// <copyright file="ApplicationUsersController.cs" company="">
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
using Stakeholders.Web.Models.ApplicationUserViewModels;

namespace Stakeholders.Web.Controllers
{
    /// <summary>
    /// Class ApplicationUsersController.
    /// </summary>
    /// <seealso cref="Microsoft.AspNetCore.Mvc.Controller" />
    [Produces("application/json")]
    [Route("api/ApplicationUsers")]
    [Authorize]
    public class ApplicationUsersController : Controller
    {
        /// <summary>
        /// The context
        /// </summary>
        private readonly IRepository<ApplicationUser> repository;

        /// <summary>
        /// The mapper
        /// </summary>
        private readonly IMapper mapper;

        private readonly IApplicationUserManager userManager;

        /// <summary>
        /// Initializes a new instance of the <see cref="ApplicationUsersController" /> class.
        /// </summary>
        /// <param name="repository">The repository.</param>
        /// <param name="mapper">The mapper.</param>
        /// <exception cref="System.ArgumentNullException">repository
        /// or
        /// mapper</exception>
        /// <exception cref="ArgumentNullException">repository
        /// or
        /// mapper</exception>
        public ApplicationUsersController(
            IRepository<ApplicationUser> repository,
            IMapper mapper,
            IApplicationUserManager userManager)
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
            this.userManager = userManager;
        }

        // GET: api/ApplicationUsers
        /// <summary>
        /// Gets the application users.
        /// </summary>
        /// <param name="start">The start.</param>
        /// <param name="count">The count.</param>
        /// <returns>ApplicationUserInfoViewModel[].</returns>
        [HttpGet]
        public ApplicationUserViewModel[] GetApplicationUsers(int start = 0, int count = 10, string search="")
        {
            return this.repository.GetAll(start, count, it=>string.IsNullOrEmpty(search) || it.Name.Contains(search)).Select(it => this.mapper.Map<ApplicationUserViewModel>(it)).ToArray();
        }

        // GET: api/ApplicationUsers/count
        /// <summary>
        /// Gets the application users count.
        /// </summary>
        /// <returns>System.Int64.</returns>
        [HttpGet("count")]
        public long GetApplicationUsersCount()
        {
            return this.repository.Count();
        }

        // GET: api/ApplicationUsers/5
        /// <summary>
        /// Gets the application user.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns>Task&lt;IActionResult&gt;.</returns>
        [HttpGet("{id}")]
        public async Task<IActionResult> GetApplicationUser([FromRoute] long id)
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

            var result = this.mapper.Map<ApplicationUserViewModel>(entity);

            return this.Ok(result);
        }

        // PUT: api/ApplicationUsers/5
        /// <summary>
        /// Puts the application user.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <param name="model">the application user.</param>
        /// <returns>Task&lt;IActionResult&gt;.</returns>
        [HttpPut("{id}")]
        public async Task<IActionResult> PutApplicationUser(
            [FromRoute] long id,
            [FromBody] ApplicationUserViewModel model)
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

        // POST: api/ApplicationUsers
        // does not require authorization
        /// <summary>
        /// Posts the application user.
        /// </summary>
        /// <param name="model">the application user.</param>
        /// <returns>Task&lt;IActionResult&gt;.</returns>
        [HttpPost]
        [AllowAnonymous]
        public async Task<IActionResult> PostApplicationUser([FromBody] CreateUserViewModel model)
        {
            if (!this.ModelState.IsValid)
            {
                return this.BadRequest(this.ModelState);
            }

            var entity = this.mapper.Map<ApplicationUser>(model);
            try
            {
                await this.userManager.CreateAsync(entity, model.Password);

                return this.CreatedAtAction(
                    "GetApplicationUser",
                    new
                    {
                        id = entity.Id
                    },
                    model);
            }
            catch (Exception ex)
            {
                return this.BadRequest(ex);
            }
        }

        // DELETE: api/ApplicationUsers/5
        /// <summary>
        /// Deletes the application user.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns>Task&lt;IActionResult&gt;.</returns>
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteApplicationUser([FromRoute] long id)
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

            var result = this.mapper.Map<ApplicationUserViewModel>(entity);

            return this.Ok(result);
        }
    }
}