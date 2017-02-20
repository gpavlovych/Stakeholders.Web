// ***********************************************************************
// Assembly         : Stakeholders.Web
// Author           : George
// Created          : 02-19-2017
//
// Last Modified By : George
// Last Modified On : 02-20-2017
// ***********************************************************************
// <copyright file="ContactsController.cs" company="">
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
using Stakeholders.Web.Models.ContactViewModels;

namespace Stakeholders.Web.Controllers
{
    /// <summary>
    /// Class ContactsController.
    /// </summary>
    /// <seealso cref="Microsoft.AspNetCore.Mvc.Controller" />
    [Produces("application/json")]
    [Route("api/Contacts")]
    [Authorize]
    public class ContactsController : Controller
    {
        /// <summary>
        /// The context
        /// </summary>
        private readonly IRepository<Contact> repository;

        /// <summary>
        /// The mapper
        /// </summary>
        private readonly IMapper mapper;

        /// <summary>
        /// Initializes a new instance of the <see cref="ContactsController" /> class.
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
        public ContactsController(
            IRepository<Contact> repository,
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

        // GET: api/Contacts
        /// <summary>
        /// Gets the contacts.
        /// </summary>
        /// <param name="start">The start.</param>
        /// <param name="count">The count.</param>
        /// <returns>ContactInfoViewModel[].</returns>
        [HttpGet]
        public ContactViewModel[] GetContacts(int start = 0, int count = 10)
        {
            return this.repository.GetAll(start, count).Select(it => this.mapper.Map<ContactViewModel>(it)).ToArray();
        }

        // GET: api/Contacts/count
        /// <summary>
        /// Gets the contacts count.
        /// </summary>
        /// <returns>System.Int64.</returns>
        [HttpGet("count")]
        public long GetContactsCount()
        {
            return this.repository.Count();
        }

        // GET: api/Contacts/5
        /// <summary>
        /// Gets the contact.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns>Task&lt;IActionResult&gt;.</returns>
        [HttpGet("{id}")]
        public async Task<IActionResult> GetContact([FromRoute] long id)
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

            var result = this.mapper.Map<ContactViewModel>(entity);

            return this.Ok(result);
        }

        // PUT: api/Contacts/5
        /// <summary>
        /// Puts the contact.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <param name="model">the contact.</param>
        /// <returns>Task&lt;IActionResult&gt;.</returns>
        [HttpPut("{id}")]
        public async Task<IActionResult> PutContact(
            [FromRoute] long id,
            [FromBody] ContactViewModel model)
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

        // POST: api/Contacts
        /// <summary>
        /// Posts the contact.
        /// </summary>
        /// <param name="model">the contact.</param>
        /// <returns>Task&lt;IActionResult&gt;.</returns>
        [HttpPost]
        public async Task<IActionResult> PostContact([FromBody] ContactViewModel model)
        {
            if (!this.ModelState.IsValid)
            {
                return this.BadRequest(this.ModelState);
            }

            var entity = this.mapper.Map<Contact>(model);

            await this.repository.InsertAsync(entity);

            return this.CreatedAtAction(
                "GetContact",
                new
                {
                    id = entity.Id
                },
                model);
        }

        // DELETE: api/Contacts/5
        /// <summary>
        /// Deletes the contact.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns>Task&lt;IActionResult&gt;.</returns>
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteContact([FromRoute] long id)
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

            var result = this.mapper.Map<ContactViewModel>(entity);

            return this.Ok(result);
        }
    }
}