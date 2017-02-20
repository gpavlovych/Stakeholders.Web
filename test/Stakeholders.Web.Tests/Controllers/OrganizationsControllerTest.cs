// ***********************************************************************
// Assembly         : Stakeholders.Web.Tests
// Author           : George
// Created          : 02-19-2017
//
// Last Modified By : George
// Last Modified On : 02-20-2017
// ***********************************************************************
// <copyright file="OrganizationsControllerTest.cs" company="">
//     Copyright (c) . All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using Moq;
using Stakeholders.Web.Controllers;
using Stakeholders.Web.Data;
using Stakeholders.Web.Models;
using Stakeholders.Web.Models.OrganizationViewModels;
using Xunit;

namespace Stakeholders.Web.Tests.Controllers
{
    /// <summary>
    /// Class OrganizationsControllerTest.
    /// </summary>
    public class OrganizationsControllerTest
    {
        /// <summary>
        /// The entities for test
        /// </summary>
        private readonly EntitiesForTest entitiesForTest;

        /// <summary>
        /// The repository mock
        /// </summary>
        private readonly Mock<IRepository<Organization>> repositoryMock;

        /// <summary>
        /// The target
        /// </summary>
        private readonly OrganizationsController target;

        /// <summary>
        /// The mapper mock
        /// </summary>
        private readonly Mock<IMapper> mapperMock;

        /// <summary>
        /// Initializes a new instance of the <see cref="OrganizationsControllerTest" /> class.
        /// </summary>
        public OrganizationsControllerTest()
        {
            this.entitiesForTest = new EntitiesForTest();
            this.repositoryMock = new Mock<IRepository<Organization>>();
            this.mapperMock = new Mock<IMapper>();

            this.target = new OrganizationsController(
                this.repositoryMock.Object,
                this.mapperMock.Object);
        }

        #region GetOrganizations

        /// <summary>
        /// Gets the organization types test.
        /// </summary>
        [Fact]
        public void GetOrganizationTypesTest()
        {
            // arrange
            var entities = this.entitiesForTest.CreateCollection(4, this.entitiesForTest.CreateOrganization);
            var models = new List<OrganizationViewModel>();
            foreach (var entity in entities)
            {
                var model = this.entitiesForTest.CreateOrganizationViewModel();
                this.mapperMock.Setup(it => it.Map<OrganizationViewModel>(entity)).Returns(model);
                models.Add(model);
            }

            var expectedResult = models.ToArray();
            var start = 2;
            var count = 3;
            this.repositoryMock.Setup(it => it.GetAll(start, count)).Returns(entities);

            // act 
            var result = this.target.GetOrganizations(start, count);

            // assert
            result.ShouldBeEquivalentTo(expectedResult);
        }

        #endregion GetOrganizations

        #region GetOrganizationsCount

        /// <summary>
        /// Gets the organization types count test.
        /// </summary>
        [Fact]
        public void GetOrganizationsCountTest()
        {
            // arrange
            var expectedResult = 42L;
            this.repositoryMock.Setup(it => it.Count()).Returns(expectedResult);

            // act 
            var result = this.target.GetOrganizationsCount();

            // assert
            result.ShouldBeEquivalentTo(expectedResult);
        }

        #endregion GetOrganizationsCount

        #region GetOrganization

        /// <summary>
        /// Gets the Organization test invalid model.
        /// </summary>
        /// <returns>Task.</returns>
        [Fact]
        public async Task GetOrganizationTestInvalidModel()
        {
            // arrange
            var id = 3L;
            this.target.ModelState.AddModelError("someerrorkey", "someerrormessage");

            // act
            var result = await this.target.GetOrganization(id) as BadRequestObjectResult;

            // assert
            result.Should().NotBeNull();
            result.Value.ShouldBeEquivalentTo(new SerializableError(this.target.ModelState));
        }

        /// <summary>
        /// Gets the Organization test not found.
        /// </summary>
        /// <returns>Task.</returns>
        [Fact]
        public async Task GetOrganizationTestNotFound()
        {
            // arrange
            var id = 3L;
            this.repositoryMock.Setup(it => it.FindByIdAsync(id)).ReturnsAsync(default(Organization));

            // act
            var result = await this.target.GetOrganization(id) as NotFoundResult;

            // assert
            result.Should().NotBeNull();
        }

        /// <summary>
        /// Gets the Organization test.
        /// </summary>
        /// <returns>Task.</returns>
        [Fact]
        public async Task GetOrganizationTest()
        {
            // arrange
            var id = 3L;
            var entity = this.entitiesForTest.CreateOrganization();
            this.repositoryMock.Setup(it => it.FindByIdAsync(id)).ReturnsAsync(entity);


            var expectedResult = this.entitiesForTest.CreateOrganizationViewModel();
            this.mapperMock.Setup(it => it.Map<OrganizationViewModel>(entity)).Returns(expectedResult);

            // act
            var result = await this.target.GetOrganization(id) as OkObjectResult;

            // assert
            result.Should().NotBeNull();
            result.Value.ShouldBeEquivalentTo(expectedResult);
        }

        #endregion GetOrganization

        #region PutOrganization

        /// <summary>
        /// Puts the Organization test invalid model.
        /// </summary>
        /// <returns>Task.</returns>
        [Fact]
        public async Task PutOrganizationTestInvalidModel()
        {
            // arrange
            var id = 3L;
            this.target.ModelState.AddModelError("someerrorkey", "someerrormessage");
            var viewModel = this.entitiesForTest.CreateOrganizationViewModel();

            // act
            var result = await this.target.PutOrganization(id, viewModel) as BadRequestObjectResult;

            // assert
            result.Should().NotBeNull();
            result.Value.ShouldBeEquivalentTo(new SerializableError(this.target.ModelState));
        }

        /// <summary>
        /// Puts the Organization test not found.
        /// </summary>
        /// <returns>Task.</returns>
        [Fact]
        public async Task PutOrganizationTestNotFound()
        {
            // arrange
            var id = 3L;
            var viewModel = this.entitiesForTest.CreateOrganizationViewModel();
            this.repositoryMock.Setup(it => it.FindByIdAsync(id)).ReturnsAsync(default(Organization));

            // act
            var result = await this.target.PutOrganization(id, viewModel) as NotFoundResult;

            // assert
            result.Should().NotBeNull();
        }

        /// <summary>
        /// Puts the Organization test.
        /// </summary>
        /// <returns>Task.</returns>
        [Fact]
        public async Task PutOrganizationTest()
        {
            // arrange
            var id = 3L;
            var entity = this.entitiesForTest.CreateOrganization();
            var viewModel = this.entitiesForTest.CreateOrganizationViewModel();
            this.repositoryMock.Setup(it => it.FindByIdAsync(id)).ReturnsAsync(entity);

            // act
            var result = await this.target.PutOrganization(id, viewModel) as NoContentResult;

            // assert
            result.Should().NotBeNull();
            this.mapperMock.Verify(it => it.Map(viewModel, entity));
            this.repositoryMock.Verify(it => it.UpdateAsync(entity));
        }

        #endregion PutOrganization

        #region PostOrganization

        /// <summary>
        /// Posts the Organizationtype test invalid model.
        /// </summary>
        /// <returns>Task.</returns>
        [Fact]
        public async Task PostOrganizationTestInvalidModel()
        {
            // arrange
            this.target.ModelState.AddModelError("someerrorkey", "someerrormessage");
            var viewModel = this.entitiesForTest.CreateOrganizationViewModel();

            // act
            var result = await this.target.PostOrganization(viewModel) as BadRequestObjectResult;

            // assert
            result.Should().NotBeNull();
            result.Value.ShouldBeEquivalentTo(new SerializableError(this.target.ModelState));
        }

        /// <summary>
        /// Posts the Organizationtype test.
        /// </summary>
        /// <returns>Task.</returns>
        [Fact]
        public async Task PostOrganizationTest()
        {
            // arrange
            var model = this.entitiesForTest.CreateOrganizationViewModel();
            var entity = this.entitiesForTest.CreateOrganization();
            this.mapperMock.Setup(it => it.Map<Organization>(model)).Returns(entity);

            // act
            var result = await this.target.PostOrganization(model) as CreatedAtActionResult;

            // assert
            result.Should().NotBeNull();
            result.ActionName.Should().Be("GetOrganization");
            result.RouteValues["id"].Should().Be(entity.Id);
            this.repositoryMock.Verify(it => it.InsertAsync(entity));
        }

        #endregion PostOrganization

        #region DeleteOrganization

        /// <summary>
        /// Deletes the Organizationinvalid model.
        /// </summary>
        /// <returns>Task.</returns>
        [Fact]
        public async Task DeleteOrganizationTestInvalidModel()
        {
            // arrange
            var id = 3L;
            this.target.ModelState.AddModelError("someerrorkey", "someerrormessage");

            // act
            var result = await this.target.DeleteOrganization(id) as BadRequestObjectResult;

            // assert
            result.Should().NotBeNull();
            result.Value.ShouldBeEquivalentTo(new SerializableError(this.target.ModelState));
        }

        /// <summary>
        /// Deletes the Organization test not found.
        /// </summary>
        /// <returns>Task.</returns>
        [Fact]
        public async Task DeleteOrganizationTestNotFound()
        {
            // arrange
            var id = 3L;
            this.repositoryMock.Setup(it => it.FindByIdAsync(id)).ReturnsAsync(default(Organization));

            // act
            var result = await this.target.DeleteOrganization(id) as NotFoundResult;

            // assert
            result.Should().NotBeNull();
        }

        /// <summary>
        /// Deletes the Organization test.
        /// </summary>
        /// <returns>Task.</returns>
        [Fact]
        public async Task DeleteOrganizationTest()
        {
            // arrange
            var id = 3L;
            var entity = this.entitiesForTest.CreateOrganization();
            var model = this.entitiesForTest.CreateOrganizationViewModel();
            this.mapperMock.Setup(it => it.Map<OrganizationViewModel>(entity)).Returns(model);
            this.repositoryMock.Setup(it => it.FindByIdAsync(id)).ReturnsAsync(entity);

            // act
            var result = await this.target.DeleteOrganization(id) as OkObjectResult;

            // assert
            result.Should().NotBeNull();
            result.Value.ShouldBeEquivalentTo(model);
            this.repositoryMock.Verify(it => it.DeleteAsync(entity));
        }

        #endregion DeleteOrganization
    }
}