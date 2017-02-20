// ***********************************************************************
// Assembly         : Stakeholders.Web
// Author           : George
// Created          : 02-19-2017
//
// Last Modified By : George
// Last Modified On : 02-20-2017
// ***********************************************************************
// <copyright file="CompaniesController.cs" company="">
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
using Stakeholders.Web.Models.CompanyViewModels;

namespace Stakeholders.Web.Controllers
{
    /// <summary>
    /// Class CompaniesController.
    /// </summary>
    /// <seealso cref="Microsoft.AspNetCore.Mvc.Controller" />
    [Produces("application/json")]
    [Route("api/Companies")]
    public class CompaniesController : Controller
    {
        /// <summary>
        /// The context
        /// </summary>
        private readonly IRepository<Company> repository;

        /// <summary>
        /// The mapper
        /// </summary>
        private readonly IMapper mapper;

        /// <summary>
        /// Initializes a new instance of the <see cref="CompaniesController" /> class.
        /// </summary>
        /// <param name="repository">The repository.</param>
        /// <param name="mapper">The mapper.</param>
        /// <exception cref="ArgumentNullException">
        /// repository
        /// or
        /// mapper
        /// </exception>
        public CompaniesController(
            IRepository<Company> repository,
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

        // GET: api/Companies
        /// <summary>
        /// Gets the Companies.
        /// </summary>
        /// <param name="start">The start.</param>
        /// <param name="count">The count.</param>
        /// <returns>CompanyInfoViewModel[].</returns>
        [HttpGet]
        public CompanyInfoViewModel[] GetCompanies(int start = 0, int count = 10)
        {
            return this.repository.GetAll(start, count).Select(it => this.mapper.Map<CompanyInfoViewModel>(it)).ToArray();
        }

        // GET: api/Companies/count
        /// <summary>
        /// Gets the Companies count.
        /// </summary>
        /// <returns>System.Int64.</returns>
        [HttpGet("count")]
        public long GetCompaniesCount()
        {
            return this.repository.Count();
        }

        // GET: api/Companies/5
        /// <summary>
        /// Gets the Company.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns>Task&lt;IActionResult&gt;.</returns>
        [HttpGet("{id}")]
        public async Task<IActionResult> GetCompany([FromRoute] long id)
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

            var result = this.mapper.Map<CompanyViewModel>(entity);

            return this.Ok(result);
        }

        // PUT: api/Companies/5
        /// <summary>
        /// Puts the Company.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <param name="model">The model.</param>
        /// <returns>Task&lt;IActionResult&gt;.</returns>
        [HttpPut("{id}")]
        public async Task<IActionResult> PutCompany(
            [FromRoute] long id,
            [FromBody] CompanyViewModel model)
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

        // POST: api/Companies
        /// <summary>
        /// Posts the Company.
        /// </summary>
        /// <param name="model">The model.</param>
        /// <returns>Task&lt;IActionResult&gt;.</returns>
        [HttpPost]
        public async Task<IActionResult> PostCompany([FromBody] CompanyViewModel model)
        {
            if (!this.ModelState.IsValid)
            {
                return this.BadRequest(this.ModelState);
            }

            var entity = this.mapper.Map<Company>(model);

            await this.repository.InsertAsync(entity);

            return this.CreatedAtAction(
                "GetCompany",
                new
                {
                    id = entity.Id
                },
                model);
        }

        // DELETE: api/Companies/5
        /// <summary>
        /// Deletes the Company.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns>Task&lt;IActionResult&gt;.</returns>
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCompany([FromRoute] long id)
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

            var result = this.mapper.Map<CompanyViewModel>(entity);

            return this.Ok(result);
        }

        ///// <summary>
        ///// Updates the view model.
        ///// </summary>
        ///// <param name="model">The result.</param>
        ///// <param name="entity">The entity.</param>
        //private static void UpdateViewModel(CompanyViewModel model, Company entity)
        //{
        //    model.Address = entity.Address;
        //    model.City = entity.City;
        //    model.CompanyCode = entity.CompanyCode;
        //    model.Name = entity.Name;
        //    model.Influencing = entity.Influencing;
        //    model.InfluencedBy = entity.InfluencedBy;
        //    model.Email = entity.Email;
        //    model.Phone = entity.Phone;
        //    model.ObserverActivityIds = entity.ObserverActivities.Select(it => it.Activity.Id).ToArray();
        //    model.LogoUrl = entity.LogoUrl;
        //}

        ///// <summary>
        ///// Updates the entity from view model.
        ///// </summary>
        ///// <param name="model">The entity.</param>
        ///// <param name="entity">The result.</param>
        ///// <returns>Task.</returns>
        //private void UpdateEntityFromViewModel(CompanyViewModel model, Company entity)
        //{
        //    entity.Address = model.Address;
        //    entity.City = model.City;
        //    entity.CompanyCode = model.CompanyCode;
        //    entity.Name = model.Name;
        //    entity.Influencing = model.Influencing;
        //    entity.InfluencedBy = model.InfluencedBy;
        //    entity.Email = model.Email;
        //    entity.Phone = model.Phone;
        //    entity.ObserverActivities = model.ObserverActivityIds?.Select(
        //        observerUserId => new ActivityObserverUserCompany()
        //        {
        //            Activity = this.repositoryActivities.FindById(observerUserId)
        //        }).ToList();
        //    entity.LogoUrl = model.LogoUrl;
        //}
    }
}