// ***********************************************************************
// Assembly         : Stakeholders.Web.Tests
// Author           : George
// Created          : 02-19-2017
//
// Last Modified By : George
// Last Modified On : 02-19-2017
// ***********************************************************************
// <copyright file="OrganizationTypesControllerTest.cs" company="">
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
using Stakeholders.Web.Models.OrganizationTypeViewModels;
using Xunit;

namespace Stakeholders.Web.Tests.Controllers
{
    /// <summary>
    /// Class OrganizationTypesControllerTest.
    /// </summary>
    public class OrganizationTypesControllerTest
    {
        private readonly EntitiesForTest entitiesForTest;

        /// <summary>
        /// The repository mock
        /// </summary>
        private readonly Mock<IRepository<OrganizationType>> repositoryMock;

        /// <summary>
        /// The target
        /// </summary>
        private readonly OrganizationTypesController target;

        /// <summary>
        /// The mapper mock
        /// </summary>
        private Mock<IMapper> mapperMock;

        /// <summary>
        /// Initializes a new instance of the <see cref="OrganizationTypesControllerTest"/> class.
        /// </summary>
        public OrganizationTypesControllerTest()
        {
            this.entitiesForTest = new EntitiesForTest();
            this.repositoryMock = new Mock<IRepository<OrganizationType>>();
            this.mapperMock = new Mock<IMapper>();
            this.target = new OrganizationTypesController(this.repositoryMock.Object, this.mapperMock.Object);
        }

        #region GetOrganizationTypes

        /// <summary>
        /// Gets the organization types test.
        /// </summary>
        [Fact]
        public void GetOrganizationTypesTest()
        {
            // arrange
            var entities = this.entitiesForTest.CreateCollection(4, this.entitiesForTest.CreateOrganizationType);
            var models = new List<OrganizationTypeViewModel>();
            foreach (var entity in entities)
            {
                var model = this.entitiesForTest.CreateOrganizationTypeViewModel();
                this.mapperMock.Setup(it => it.Map<OrganizationTypeViewModel>(entity)).Returns(model);
                models.Add(model);
            }

            var expectedResult = models.ToArray();

            var start = 2;
            var count = 3;
            this.repositoryMock.Setup(it => it.GetAll(start, count, It.IsAny<Func<OrganizationType, bool>>())).Returns(entities);

            // act 
            var result = this.target.GetOrganizationTypes(start, count);

            // assert
            result.ShouldBeEquivalentTo(expectedResult);
        }

        #endregion GetOrganizationTypes

        #region GetOrganizationTypesCount

        /// <summary>
        /// Gets the organization types count test.
        /// </summary>
        [Fact]
        public void GetOrganizationTypesCountTest()
        {
            // arrange
            var expectedResult = 42L;
            this.repositoryMock.Setup(it => it.Count()).Returns(expectedResult);

            // act 
            var result = this.target.GetOrganizationTypesCount();

            // assert
            result.ShouldBeEquivalentTo(expectedResult);
        }

        #endregion GetOrganizationTypes

        #region GetOrganizationType

        /// <summary>
        /// Gets the organization type test invalid model.
        /// </summary>
        /// <returns>Task.</returns>
        [Fact]
        public async Task GetOrganizationTypeTestInvalidModel()
        {
            // arrange
            var id = 3L;
            this.target.ModelState.AddModelError("someerrorkey", "someerrormessage");

            // act
            var result = await this.target.GetOrganizationType(id) as BadRequestObjectResult;

            // assert
            result.Should().NotBeNull();
            result.Value.ShouldBeEquivalentTo(new SerializableError(this.target.ModelState));
        }

        /// <summary>
        /// Gets the organization type test not found.
        /// </summary>
        /// <returns>Task.</returns>
        [Fact]
        public async Task GetOrganizationTypeTestNotFound()
        {
            // arrange
            var id = 3L;
            this.repositoryMock.Setup(it => it.FindByIdAsync(id)).ReturnsAsync(default(OrganizationType));

            // act
            var result = await this.target.GetOrganizationType(id) as NotFoundResult;

            // assert
            result.Should().NotBeNull();
        }

        /// <summary>
        /// Gets the organization type test.
        /// </summary>
        /// <returns>Task.</returns>
        [Fact]
        public async Task GetOrganizationTypeTest()
        {
            // arrange
            var id = 3L;
            var entity = this.entitiesForTest.CreateOrganizationType();
            this.repositoryMock.Setup(it => it.FindByIdAsync(id)).ReturnsAsync(entity);
            var expectedResult = this.entitiesForTest.CreateOrganizationTypeViewModel();
            this.mapperMock.Setup(it => it.Map<OrganizationTypeViewModel>(entity)).Returns(expectedResult);

            // act
            var result = await this.target.GetOrganizationType(id) as OkObjectResult;

            // assert
            result.Should().NotBeNull();
            result.Value.ShouldBeEquivalentTo(expectedResult);
        }

        #endregion GetOrganizationType

        #region PutOrganizationType

        /// <summary>
        /// Puts the organization type test invalid model.
        /// </summary>
        /// <returns>Task.</returns>
        [Fact]
        public async Task PutOrganizationTypeTestInvalidModel()
        {
            // arrange
            var id = 3L;
            this.target.ModelState.AddModelError("someerrorkey", "someerrormessage");
            var viewModel = this.entitiesForTest.CreateOrganizationTypeViewModel();

            // act
            var result = await this.target.PutOrganizationType(id, viewModel) as BadRequestObjectResult;

            // assert
            result.Should().NotBeNull();
            result.Value.ShouldBeEquivalentTo(new SerializableError(this.target.ModelState));
        }

        /// <summary>
        /// Puts the organization type test not found.
        /// </summary>
        /// <returns>Task.</returns>
        [Fact]
        public async Task PutOrganizationTypeTestNotFound()
        {
            // arrange
            var id = 3L;
            var viewModel = this.entitiesForTest.CreateOrganizationTypeViewModel();
            this.repositoryMock.Setup(it => it.FindByIdAsync(id)).ReturnsAsync(default(OrganizationType));

            // act
            var result = await this.target.PutOrganizationType(id, viewModel) as NotFoundResult;

            // assert
            result.Should().NotBeNull();
        }

        /// <summary>
        /// Puts the organization type test.
        /// </summary>
        /// <returns>Task.</returns>
        [Fact]
        public async Task PutOrganizationTypeTest()
        {
            // arrange
            var id = 3L;
            var entity = this.entitiesForTest.CreateOrganizationType();
            var viewModel = this.entitiesForTest.CreateOrganizationTypeViewModel();
            this.repositoryMock.Setup(it => it.FindByIdAsync(id)).ReturnsAsync(entity);

            // act
            var result = await this.target.PutOrganizationType(id, viewModel) as NoContentResult;

            // assert
            result.Should().NotBeNull();
            this.mapperMock.Verify(it => it.Map(viewModel, entity));
            this.repositoryMock.Verify(it => it.UpdateAsync(entity));
        }

        #endregion PutOrganizationType

        #region PostOrganizationType

        /// <summary>
        /// Posts the organization type type test invalid model.
        /// </summary>
        /// <returns>Task.</returns>
        [Fact]
        public async Task PostOrganizationTypeTypeTestInvalidModel()
        {
            // arrange
            this.target.ModelState.AddModelError("someerrorkey", "someerrormessage");
            var viewModel = this.entitiesForTest.CreateOrganizationTypeViewModel();

            // act
            var result = await this.target.PostOrganizationType(viewModel) as BadRequestObjectResult;

            // assert
            result.Should().NotBeNull();
            result.Value.ShouldBeEquivalentTo(new SerializableError(this.target.ModelState));
        }

        /// <summary>
        /// Posts the organization type type test.
        /// </summary>
        /// <returns>Task.</returns>
        [Fact]
        public async Task PostOrganizationTypeTypeTest()
        {
            // arrange
            var model = this.entitiesForTest.CreateOrganizationTypeViewModel();
            var entity = this.entitiesForTest.CreateOrganizationType();
            this.mapperMock.Setup(it => it.Map<OrganizationType>(model)).Returns(entity);

            // act
            var result = await this.target.PostOrganizationType(model) as CreatedAtActionResult;

            // assert
            result.Should().NotBeNull();
            result.ActionName.Should().Be("GetOrganizationType");
            result.RouteValues["id"].Should().Be(entity.Id);
            this.repositoryMock.Verify(it => it.InsertAsync(entity));
        }

        #endregion PostOrganizationType

        #region DeleteOrganizationType

        /// <summary>
        /// Deletes the organization type invalid model.
        /// </summary>
        /// <returns>Task.</returns>
        [Fact]
        public async Task DeleteOrganizationTypeInvalidModel()
        {
            // arrange
            var id = 3L;
            this.target.ModelState.AddModelError("someerrorkey", "someerrormessage");

            // act
            var result = await this.target.DeleteOrganizationType(id) as BadRequestObjectResult;

            // assert
            result.Should().NotBeNull();
            result.Value.ShouldBeEquivalentTo(new SerializableError(this.target.ModelState));
        }

        /// <summary>
        /// Deletes the organization type test not found.
        /// </summary>
        /// <returns>Task.</returns>
        [Fact]
        public async Task DeleteOrganizationTypeTestNotFound()
        {
            // arrange
            var id = 3L;
            this.repositoryMock.Setup(it => it.FindByIdAsync(id)).ReturnsAsync(default(OrganizationType));

            // act
            var result = await this.target.DeleteOrganizationType(id) as NotFoundResult;

            // assert
            result.Should().NotBeNull();
        }

        /// <summary>
        /// Deletes the organization type test.
        /// </summary>
        /// <returns>Task.</returns>
        [Fact]
        public async Task DeleteOrganizationTypeTest()
        {
            // arrange
            var id = 3L;
            var entity = this.entitiesForTest.CreateOrganizationType();
            var model = this.entitiesForTest.CreateOrganizationTypeViewModel();
            this.mapperMock.Setup(it => it.Map<OrganizationTypeViewModel>(entity)).Returns(model);
            this.repositoryMock.Setup(it => it.FindByIdAsync(id)).ReturnsAsync(entity);

            // act
            var result = await this.target.DeleteOrganizationType(id) as OkObjectResult;

            // assert
            result.Should().NotBeNull();
            result.Value.ShouldBeEquivalentTo(model);
            this.repositoryMock.Verify(it => it.DeleteAsync(entity));
        }

        #endregion DeleteOrganizationType
    }
}
