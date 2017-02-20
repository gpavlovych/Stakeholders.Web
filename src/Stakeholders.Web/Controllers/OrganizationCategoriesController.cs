// ***********************************************************************
// Assembly         : Stakeholders.Web
// Author           : George
// Created          : 02-19-2017
//
// Last Modified By : George
// Last Modified On : 02-20-2017
// ***********************************************************************
// <copyright file="OrganizationCategoriesController.cs" company="">
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
using Stakeholders.Web.Models.OrganizationCategoryViewModels;

namespace Stakeholders.Web.Controllers
{
    /// <summary>
    /// Class OrganizationCategoriesController.
    /// </summary>
    /// <seealso cref="Microsoft.AspNetCore.Mvc.Controller" />
    [Produces("application/json")]
    [Route("api/OrganizationCategories")]
    public class OrganizationCategoriesController : Controller
    {
        /// <summary>
        /// The context
        /// </summary>
        private readonly IRepository<OrganizationCategory> repository;

        /// <summary>
        /// The mapper
        /// </summary>
        private readonly IMapper mapper;

        /// <summary>
        /// Initializes a new instance of the <see cref="OrganizationCategoriesController" /> class.
        /// </summary>
        /// <param name="repository">The repository.</param>
        /// <param name="mapper">The mapper.</param>
        /// <exception cref="System.ArgumentNullException">repository
        /// or
        /// mapper</exception>
        /// <exception cref="ArgumentNullException">repository
        /// or
        /// mapper</exception>
        public OrganizationCategoriesController(
            IRepository<OrganizationCategory> repository,
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

        // GET: api/OrganizationCategories
        /// <summary>
        /// Gets the organization categories.
        /// </summary>
        /// <param name="start">The start.</param>
        /// <param name="count">The count.</param>
        /// <returns>OrganizationCategoryViewModel[].</returns>
        [HttpGet]
        public OrganizationCategoryViewModel[] GetOrganizationCategories(int start = 0, int count = 10)
        {
            return
                this.repository.GetAll(start, count)
                    .Select(it => this.mapper.Map<OrganizationCategoryViewModel>(it))
                    .ToArray();
        }

        // GET: api/OrganizationCategories/count
        /// <summary>
        /// Gets the organization categories count.
        /// </summary>
        /// <returns>System.Int64.</returns>
        [HttpGet("count")]
        public long GetOrganizationCategoriesCount()
        {
            return this.repository.Count();
        }

        // GET: api/OrganizationCategories/5
        /// <summary>
        /// Gets the organization category.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns>Task&lt;IActionResult&gt;.</returns>
        [HttpGet("{id}")]
        public async Task<IActionResult> GetOrganizationCategory([FromRoute] long id)
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

            var result = this.mapper.Map<OrganizationCategoryViewModel>(entity);

            return this.Ok(result);
        }

        // PUT: api/OrganizationCategories/5
        /// <summary>
        /// Puts the organization category.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <param name="model">the organization category.</param>
        /// <returns>Task&lt;IActionResult&gt;.</returns>
        [HttpPut("{id}")]
        public async Task<IActionResult> PutOrganizationCategory(
            [FromRoute] long id,
            [FromBody] OrganizationCategoryViewModel model)
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

        // POST: api/OrganizationCategories
        /// <summary>
        /// Posts the organization category.
        /// </summary>
        /// <param name="model">the organization category.</param>
        /// <returns>Task&lt;IActionResult&gt;.</returns>
        [HttpPost]
        public async Task<IActionResult> PostOrganizationCategory([FromBody] OrganizationCategoryViewModel model)
        {
            if (!this.ModelState.IsValid)
            {
                return this.BadRequest(this.ModelState);
            }

            var entity = this.mapper.Map<OrganizationCategory>(model);

            await this.repository.InsertAsync(entity);

            return this.CreatedAtAction(
                "GetOrganizationCategory",
                new
                {
                    id = entity.Id
                },
                model);
        }

        // DELETE: api/OrganizationCategories/5
        /// <summary>
        /// Deletes the organization category.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns>Task&lt;IActionResult&gt;.</returns>
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteOrganizationCategory([FromRoute] long id)
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

            var result = this.mapper.Map<OrganizationCategoryViewModel>(entity);

            return this.Ok(result);
        }
    }
}