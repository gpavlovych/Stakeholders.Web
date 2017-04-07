// ***********************************************************************
// Assembly         : Stakeholders.Web.Tests
// Author           : George
// Created          : 02-19-2017
//
// Last Modified By : George
// Last Modified On : 04-03-2017
// ***********************************************************************
// <copyright file="ContactsControllerTest.cs" company="">
//     Copyright (c) . All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************

using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using Moq;
using Stakeholders.Web.Controllers;
using Stakeholders.Web.Data;
using Stakeholders.Web.Models;
using Stakeholders.Web.Models.ContactViewModels;
using Xunit;

namespace Stakeholders.Web.Tests.Controllers
{
    /// <summary>
    /// Class ContactsControllerTest.
    /// </summary>
    public class ContactsControllerTest
    {
        /// <summary>
        /// The entities for test
        /// </summary>
        private readonly EntitiesForTest entitiesForTest;

        /// <summary>
        /// The repository mock
        /// </summary>
        private readonly Mock<IRepository<Contact>> repositoryMock;

        /// <summary>
        /// The target
        /// </summary>
        private readonly ContactsController target;

        /// <summary>
        /// The mapper mock
        /// </summary>
        private readonly Mock<IMapper> mapperMock;

        /// <summary>
        /// Initializes a new instance of the <see cref="ContactsControllerTest" /> class.
        /// </summary>
        public ContactsControllerTest()
        {
            this.entitiesForTest = new EntitiesForTest();
            this.repositoryMock = new Mock<IRepository<Contact>>();
            this.mapperMock = new Mock<IMapper>();

            this.target = new ContactsController(
                this.repositoryMock.Object,
                this.mapperMock.Object);
        }

        #region GetContacts

        /// <summary>
        /// Gets the organization types test.
        /// </summary>
        [Fact]
        public void GetOrganizationTypesTest()
        {
            // arrange
            var entities = this.entitiesForTest.CreateCollection(4, this.entitiesForTest.CreateContact);
            var models = new List<ContactViewModel>();
            foreach (var entity in entities)
            {
                var model = this.entitiesForTest.CreateContactViewModel();
                this.mapperMock.Setup(it => it.Map<ContactViewModel>(entity)).Returns(model);
                models.Add(model);
            }

            var expectedResult = models.ToArray();
            var start = 2;
            var count = 3;
            this.repositoryMock.Setup(it => it.GetAll(start, count, It.IsAny<Func<Contact, bool>>())).Returns(entities);

            // act 
            var result = this.target.GetContacts(start, count);

            // assert
            result.ShouldBeEquivalentTo(expectedResult);
        }

        #endregion GetContacts

        #region GetContactsCount

        /// <summary>
        /// Gets the organization types count test.
        /// </summary>
        [Fact]
        public void GetContactsCountTest()
        {
            // arrange
            var expectedResult = 42L;
            this.repositoryMock.Setup(it => it.Count()).Returns(expectedResult);

            // act 
            var result = this.target.GetContactsCount();

            // assert
            result.ShouldBeEquivalentTo(expectedResult);
        }

        #endregion GetContactsCount

        #region GetContact

        /// <summary>
        /// Gets the Contact test invalid model.
        /// </summary>
        /// <returns>Task.</returns>
        [Fact]
        public async Task GetContactTestInvalidModel()
        {
            // arrange
            var id = 3L;
            this.target.ModelState.AddModelError("someerrorkey", "someerrormessage");

            // act
            var result = await this.target.GetContact(id) as BadRequestObjectResult;

            // assert
            result.Should().NotBeNull();
            result.Value.ShouldBeEquivalentTo(new SerializableError(this.target.ModelState));
        }

        /// <summary>
        /// Gets the Contact test not found.
        /// </summary>
        /// <returns>Task.</returns>
        [Fact]
        public async Task GetContactTestNotFound()
        {
            // arrange
            var id = 3L;
            this.repositoryMock.Setup(it => it.FindByIdAsync(id)).ReturnsAsync(default(Contact));

            // act
            var result = await this.target.GetContact(id) as NotFoundResult;

            // assert
            result.Should().NotBeNull();
        }

        /// <summary>
        /// Gets the Contact test.
        /// </summary>
        /// <returns>Task.</returns>
        [Fact]
        public async Task GetContactTest()
        {
            // arrange
            var id = 3L;
            var entity = this.entitiesForTest.CreateContact();
            this.repositoryMock.Setup(it => it.FindByIdAsync(id)).ReturnsAsync(entity);


            var expectedResult = this.entitiesForTest.CreateContactViewModel();
            this.mapperMock.Setup(it => it.Map<ContactViewModel>(entity)).Returns(expectedResult);

            // act
            var result = await this.target.GetContact(id) as OkObjectResult;

            // assert
            result.Should().NotBeNull();
            result.Value.ShouldBeEquivalentTo(expectedResult);
        }

        #endregion GetContact

        #region PutContact

        /// <summary>
        /// Puts the Contact test invalid model.
        /// </summary>
        /// <returns>Task.</returns>
        [Fact]
        public async Task PutContactTestInvalidModel()
        {
            // arrange
            var id = 3L;
            this.target.ModelState.AddModelError("someerrorkey", "someerrormessage");
            var viewModel = this.entitiesForTest.CreateContactViewModel();

            // act
            var result = await this.target.PutContact(id, viewModel) as BadRequestObjectResult;

            // assert
            result.Should().NotBeNull();
            result.Value.ShouldBeEquivalentTo(new SerializableError(this.target.ModelState));
        }

        /// <summary>
        /// Puts the Contact test not found.
        /// </summary>
        /// <returns>Task.</returns>
        [Fact]
        public async Task PutContactTestNotFound()
        {
            // arrange
            var id = 3L;
            var viewModel = this.entitiesForTest.CreateContactViewModel();
            this.repositoryMock.Setup(it => it.FindByIdAsync(id)).ReturnsAsync(default(Contact));

            // act
            var result = await this.target.PutContact(id, viewModel) as NotFoundResult;

            // assert
            result.Should().NotBeNull();
        }

        /// <summary>
        /// Puts the Contact test.
        /// </summary>
        /// <returns>Task.</returns>
        [Fact]
        public async Task PutContactTest()
        {
            // arrange
            var id = 3L;
            var entity = this.entitiesForTest.CreateContact();
            var viewModel = this.entitiesForTest.CreateContactViewModel();
            this.repositoryMock.Setup(it => it.FindByIdAsync(id)).ReturnsAsync(entity);

            // act
            var result = await this.target.PutContact(id, viewModel) as NoContentResult;

            // assert
            result.Should().NotBeNull();
            this.mapperMock.Verify(it => it.Map(viewModel, entity));
            this.repositoryMock.Verify(it => it.UpdateAsync(entity));
        }

        #endregion PutContact

        #region PostContact

        /// <summary>
        /// Posts the Contacttype test invalid model.
        /// </summary>
        /// <returns>Task.</returns>
        [Fact]
        public async Task PostContactTestInvalidModel()
        {
            // arrange
            this.target.ModelState.AddModelError("someerrorkey", "someerrormessage");
            var viewModel = this.entitiesForTest.CreateContactViewModel();

            // act
            var result = await this.target.PostContact(viewModel) as BadRequestObjectResult;

            // assert
            result.Should().NotBeNull();
            result.Value.ShouldBeEquivalentTo(new SerializableError(this.target.ModelState));
        }

        /// <summary>
        /// Posts the Contacttype test.
        /// </summary>
        /// <returns>Task.</returns>
        [Fact]
        public async Task PostContactTest()
        {
            // arrange
            var model = this.entitiesForTest.CreateContactViewModel();
            var entity = this.entitiesForTest.CreateContact();
            this.mapperMock.Setup(it => it.Map<Contact>(model)).Returns(entity);

            // act
            var result = await this.target.PostContact(model) as CreatedAtActionResult;

            // assert
            result.Should().NotBeNull();
            result.ActionName.Should().Be("GetContact");
            result.RouteValues["id"].Should().Be(entity.Id);
            this.repositoryMock.Verify(it => it.InsertAsync(entity));
        }

        #endregion PostContact

        #region DeleteContact

        /// <summary>
        /// Deletes the Contactinvalid model.
        /// </summary>
        /// <returns>Task.</returns>
        [Fact]
        public async Task DeleteContactTestInvalidModel()
        {
            // arrange
            var id = 3L;
            this.target.ModelState.AddModelError("someerrorkey", "someerrormessage");

            // act
            var result = await this.target.DeleteContact(id) as BadRequestObjectResult;

            // assert
            result.Should().NotBeNull();
            result.Value.ShouldBeEquivalentTo(new SerializableError(this.target.ModelState));
        }

        /// <summary>
        /// Deletes the Contact test not found.
        /// </summary>
        /// <returns>Task.</returns>
        [Fact]
        public async Task DeleteContactTestNotFound()
        {
            // arrange
            var id = 3L;
            this.repositoryMock.Setup(it => it.FindByIdAsync(id)).ReturnsAsync(default(Contact));

            // act
            var result = await this.target.DeleteContact(id) as NotFoundResult;

            // assert
            result.Should().NotBeNull();
        }

        /// <summary>
        /// Deletes the Contact test.
        /// </summary>
        /// <returns>Task.</returns>
        [Fact]
        public async Task DeleteContactTest()
        {
            // arrange
            var id = 3L;
            var entity = this.entitiesForTest.CreateContact();
            var model = this.entitiesForTest.CreateContactViewModel();
            this.mapperMock.Setup(it => it.Map<ContactViewModel>(entity)).Returns(model);
            this.repositoryMock.Setup(it => it.FindByIdAsync(id)).ReturnsAsync(entity);

            // act
            var result = await this.target.DeleteContact(id) as OkObjectResult;

            // assert
            result.Should().NotBeNull();
            result.Value.ShouldBeEquivalentTo(model);
            this.repositoryMock.Verify(it => it.DeleteAsync(entity));
        }

        #endregion DeleteContact
    }
}