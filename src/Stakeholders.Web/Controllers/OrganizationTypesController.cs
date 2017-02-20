// ***********************************************************************
// Assembly         : Stakeholders.Web
// Author           : George
// Created          : 02-19-2017
//
// Last Modified By : George
// Last Modified On : 02-19-2017
// ***********************************************************************
// <copyright file="OrganizationTypesController.cs" company="">
//     Copyright (c) . All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************

using System;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Stakeholders.Web.Data;
using Stakeholders.Web.Models;
using Stakeholders.Web.Models.OrganizationTypeViewModels;

namespace Stakeholders.Web.Controllers
{
    /// <summary>
    /// Class OrganizationTypesController.
    /// </summary>
    /// <seealso cref="Microsoft.AspNetCore.Mvc.Controller" />
    [Produces("application/json")]
    [Route("api/OrganizationTypes")]
    public class OrganizationTypesController : Controller
    {
        /// <summary>
        /// The context
        /// </summary>
        private readonly IRepository<OrganizationType> repository;

        private readonly IMapper mapper;

        /// <summary>
        /// Initializes a new instance of the <see cref="OrganizationTypesController" /> class.
        /// </summary>
        /// <param name="repository">The repository.</param>
        /// <param name="mapper">The mapper.</param>
        /// <exception cref="ArgumentNullException">
        /// repository
        /// or
        /// mapper
        /// </exception>
        public OrganizationTypesController(IRepository<OrganizationType> repository, IMapper mapper)
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

        // GET: api/OrganizationTypes
        /// <summary>
        /// Gets the organization types.
        /// </summary>
        /// <param name="start">The start.</param>
        /// <param name="count">The count.</param>
        [HttpGet]
        public OrganizationTypeViewModel[] GetOrganizationTypes(int start = 0, int count = 10)
        {
            return this.repository.GetAll(start, count).Select(
                    it => this.mapper.Map<OrganizationTypeViewModel>(it)).ToArray();
        }

        /// <summary>
        /// Gets the organization types count.
        /// </summary>
        /// <returns>System.Int64.</returns>
        [HttpGet("count")]
        public long GetOrganizationTypesCount()
        {
            return this.repository.Count();
        }

        // GET: api/OrganizationTypes/5
        /// <summary>
        /// Gets the type of the organization.
        /// </summary>
        /// <param name="id">The identifier.</param>
        [HttpGet("{id}")]
        public async Task<IActionResult> GetOrganizationType([FromRoute] long id)
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

            var result = this.mapper.Map<OrganizationTypeViewModel>(entity);

            return this.Ok(result);
        }

        // PUT: api/OrganizationTypes/5
        /// <summary>
        /// Puts the type of the organization.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <param name="organizationType">Type of the organization.</param>
        [HttpPut("{id}")]
        public async Task<IActionResult> PutOrganizationType(
            [FromRoute] long id,
            [FromBody] OrganizationTypeViewModel model)
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

        // POST: api/OrganizationTypes
        /// <summary>
        /// Posts the type of the organization.
        /// </summary>
        /// <param name="model">The model.</param>
        [HttpPost]
        public async Task<IActionResult> PostOrganizationType([FromBody] OrganizationTypeViewModel model)
        {
            if (!this.ModelState.IsValid)
            {
                return this.BadRequest(this.ModelState);
            }

            var entity = this.mapper.Map<OrganizationType>(model);
            await this.repository.InsertAsync(entity);

            return this.CreatedAtAction(
                "GetOrganizationType",
                new
                {
                    id = entity.Id
                },
                model);
        }

        // DELETE: api/OrganizationTypes/5
        /// <summary>
        /// Deletes the type of the organization.
        /// </summary>
        /// <param name="id">The identifier.</param>
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteOrganizationType([FromRoute] long id)
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

            var result = this.mapper.Map<OrganizationTypeViewModel>(entity);

            return this.Ok(result);
        }
    }
}