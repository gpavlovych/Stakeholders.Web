// ***********************************************************************
// Assembly         : Stakeholders.Web
// Author           : George
// Created          : 02-19-2017
//
// Last Modified By : George
// Last Modified On : 04-15-2017
// ***********************************************************************
// <copyright file="OrganizationsController.cs" company="">
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
using Stakeholders.Web.Models.OrganizationViewModels;

namespace Stakeholders.Web.Controllers
{
    /// <summary>
    /// Class OrganizationsController.
    /// </summary>
    /// <seealso cref="Microsoft.AspNetCore.Mvc.Controller" />
    [Produces("application/json")]
    [Route("api/Organizations")]
    [Authorize]
    public class OrganizationsController : Controller
    {
        /// <summary>
        /// The context
        /// </summary>
        private readonly IRepository<Organization> repository;

        /// <summary>
        /// The mapper
        /// </summary>
        private readonly IMapper mapper;

        /// <summary>
        /// Initializes a new instance of the <see cref="OrganizationsController" /> class.
        /// </summary>
        /// <param name="repository">The repository.</param>
        /// <param name="mapper">The mapper.</param>
        /// <exception cref="ArgumentNullException">
        /// repository
        /// or
        /// mapper
        /// </exception>
        public OrganizationsController(
            IRepository<Organization> repository,
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

        // GET: api/Organizations
        /// <summary>
        /// Gets the organizations.
        /// </summary>
        /// <param name="start">The start.</param>
        /// <param name="count">The count.</param>
        /// <param name="search">The search.</param>
        /// <param name="organizationCategoryId">The organization category identifier.</param>
        /// <returns>OrganizationInfoViewModel[].</returns>
        [HttpGet]
        public OrganizationViewModel[] GetOrganizations(
            int start = 0,
            int count = 10,
            string search = "",
            long? organizationCategoryId = null)
        {
            return
                this.repository.GetAll(
                        start,
                        count,
                        it =>
                            (string.IsNullOrEmpty(search) || it.Name.Contains(search)) &&
                            ((organizationCategoryId == null) || (it.Category.Id == organizationCategoryId)))
                    .Select(it => this.mapper.Map<OrganizationViewModel>(it))
                    .ToArray();
        }

        // GET: api/Organizations/count
        /// <summary>
        /// Gets the organizations count.
        /// </summary>
        /// <returns>System.Int64.</returns>
        [HttpGet("count")]
        public long GetOrganizationsCount()
        {
            return this.repository.Count();
        }

        // GET: api/Organizations/5
        /// <summary>
        /// Gets the organization.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns>Task&lt;IActionResult&gt;.</returns>
        [HttpGet("{id}")]
        public async Task<IActionResult> GetOrganization([FromRoute] long id)
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

            var result = this.mapper.Map<OrganizationViewModel>(entity);

            return this.Ok(result);
        }

        // PUT: api/Organizations/5
        /// <summary>
        /// Puts the organization.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <param name="model">the organization.</param>
        /// <returns>Task&lt;IActionResult&gt;.</returns>
        [HttpPut("{id}")]
        public async Task<IActionResult> PutOrganization(
            [FromRoute] long id,
            [FromBody] OrganizationViewModel model)
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

        // POST: api/Organizations
        /// <summary>
        /// Posts the organization.
        /// </summary>
        /// <param name="model">the organization.</param>
        /// <returns>Task&lt;IActionResult&gt;.</returns>
        [HttpPost]
        public async Task<IActionResult> PostOrganization([FromBody] OrganizationViewModel model)
        {
            if (!this.ModelState.IsValid)
            {
                return this.BadRequest(this.ModelState);
            }

            var entity = this.mapper.Map<Organization>(model);

            await this.repository.InsertAsync(entity);

            return this.CreatedAtAction(
                "GetOrganization",
                new
                {
                    id = entity.Id
                },
                model);
        }

        // DELETE: api/Organizations/5
        /// <summary>
        /// Deletes the organization.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns>Task&lt;IActionResult&gt;.</returns>
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteOrganization([FromRoute] long id)
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

            var result = this.mapper.Map<OrganizationViewModel>(entity);

            return this.Ok(result);
        }
    }
}