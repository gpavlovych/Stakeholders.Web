// ***********************************************************************
// Assembly         : Stakeholders.Web.Tests
// Author           : George
// Created          : 02-19-2017
//
// Last Modified By : George
// Last Modified On : 02-20-2017
// ***********************************************************************
// <copyright file="RolesControllerTest.cs" company="">
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
using Stakeholders.Web.Models.RoleViewModels;
using Xunit;

namespace Stakeholders.Web.Tests.Controllers
{
    /// <summary>
    /// Class RolesControllerTest.
    /// </summary>
    public class RolesControllerTest
    {
        /// <summary>
        /// The entities for test
        /// </summary>
        private readonly EntitiesForTest entitiesForTest;

        /// <summary>
        /// The repository mock
        /// </summary>
        private readonly Mock<IRepository<Role>> repositoryMock;

        /// <summary>
        /// The target
        /// </summary>
        private readonly RolesController target;

        /// <summary>
        /// The mapper mock
        /// </summary>
        private readonly Mock<IMapper> mapperMock;

        /// <summary>
        /// Initializes a new instance of the <see cref="RolesControllerTest" /> class.
        /// </summary>
        public RolesControllerTest()
        {
            this.entitiesForTest = new EntitiesForTest();
            this.repositoryMock = new Mock<IRepository<Role>>();
            this.mapperMock = new Mock<IMapper>();

            this.target = new RolesController(
                this.repositoryMock.Object,
                this.mapperMock.Object);
        }

        #region GetRoles

        /// <summary>
        /// Gets the organization types test.
        /// </summary>
        [Fact]
        public void GetOrganizationTypesTest()
        {
            // arrange
            var entities = this.entitiesForTest.CreateCollection(4, this.entitiesForTest.CreateRole);
            var models = new List<RoleViewModel>();
            foreach (var entity in entities)
            {
                var model = this.entitiesForTest.CreateRoleViewModel();
                this.mapperMock.Setup(it => it.Map<RoleViewModel>(entity)).Returns(model);
                models.Add(model);
            }

            var expectedResult = models.ToArray();
            var start = 2;
            var count = 3;
            this.repositoryMock.Setup(it => it.GetAll(start, count)).Returns(entities);

            // act 
            var result = this.target.GetRoles(start, count);

            // assert
            result.ShouldBeEquivalentTo(expectedResult);
        }

        #endregion GetRoles

        #region GetRolesCount

        /// <summary>
        /// Gets the organization types count test.
        /// </summary>
        [Fact]
        public void GetRolesCountTest()
        {
            // arrange
            var expectedResult = 42L;
            this.repositoryMock.Setup(it => it.Count()).Returns(expectedResult);

            // act 
            var result = this.target.GetRolesCount();

            // assert
            result.ShouldBeEquivalentTo(expectedResult);
        }

        #endregion GetRolesCount

        #region GetRole

        /// <summary>
        /// Gets the Role test invalid model.
        /// </summary>
        /// <returns>Task.</returns>
        [Fact]
        public async Task GetRoleTestInvalidModel()
        {
            // arrange
            var id = 3L;
            this.target.ModelState.AddModelError("someerrorkey", "someerrormessage");

            // act
            var result = await this.target.GetRole(id) as BadRequestObjectResult;

            // assert
            result.Should().NotBeNull();
            result.Value.ShouldBeEquivalentTo(new SerializableError(this.target.ModelState));
        }

        /// <summary>
        /// Gets the Role test not found.
        /// </summary>
        /// <returns>Task.</returns>
        [Fact]
        public async Task GetRoleTestNotFound()
        {
            // arrange
            var id = 3L;
            this.repositoryMock.Setup(it => it.FindByIdAsync(id)).ReturnsAsync(default(Role));

            // act
            var result = await this.target.GetRole(id) as NotFoundResult;

            // assert
            result.Should().NotBeNull();
        }

        /// <summary>
        /// Gets the Role test.
        /// </summary>
        /// <returns>Task.</returns>
        [Fact]
        public async Task GetRoleTest()
        {
            // arrange
            var id = 3L;
            var entity = this.entitiesForTest.CreateRole();
            this.repositoryMock.Setup(it => it.FindByIdAsync(id)).ReturnsAsync(entity);


            var expectedResult = this.entitiesForTest.CreateRoleViewModel();
            this.mapperMock.Setup(it => it.Map<RoleViewModel>(entity)).Returns(expectedResult);

            // act
            var result = await this.target.GetRole(id) as OkObjectResult;

            // assert
            result.Should().NotBeNull();
            result.Value.ShouldBeEquivalentTo(expectedResult);
        }

        #endregion GetRole

        #region PutRole

        /// <summary>
        /// Puts the Role test invalid model.
        /// </summary>
        /// <returns>Task.</returns>
        [Fact]
        public async Task PutRoleTestInvalidModel()
        {
            // arrange
            var id = 3L;
            this.target.ModelState.AddModelError("someerrorkey", "someerrormessage");
            var viewModel = this.entitiesForTest.CreateRoleViewModel();

            // act
            var result = await this.target.PutRole(id, viewModel) as BadRequestObjectResult;

            // assert
            result.Should().NotBeNull();
            result.Value.ShouldBeEquivalentTo(new SerializableError(this.target.ModelState));
        }

        /// <summary>
        /// Puts the Role test not found.
        /// </summary>
        /// <returns>Task.</returns>
        [Fact]
        public async Task PutRoleTestNotFound()
        {
            // arrange
            var id = 3L;
            var viewModel = this.entitiesForTest.CreateRoleViewModel();
            this.repositoryMock.Setup(it => it.FindByIdAsync(id)).ReturnsAsync(default(Role));

            // act
            var result = await this.target.PutRole(id, viewModel) as NotFoundResult;

            // assert
            result.Should().NotBeNull();
        }

        /// <summary>
        /// Puts the Role test.
        /// </summary>
        /// <returns>Task.</returns>
        [Fact]
        public async Task PutRoleTest()
        {
            // arrange
            var id = 3L;
            var entity = this.entitiesForTest.CreateRole();
            var viewModel = this.entitiesForTest.CreateRoleViewModel();
            this.repositoryMock.Setup(it => it.FindByIdAsync(id)).ReturnsAsync(entity);

            // act
            var result = await this.target.PutRole(id, viewModel) as NoContentResult;

            // assert
            result.Should().NotBeNull();
            this.mapperMock.Verify(it => it.Map(viewModel, entity));
            this.repositoryMock.Verify(it => it.UpdateAsync(entity));
        }

        #endregion PutRole

        #region PostRole

        /// <summary>
        /// Posts the Roletype test invalid model.
        /// </summary>
        /// <returns>Task.</returns>
        [Fact]
        public async Task PostRoleTestInvalidModel()
        {
            // arrange
            this.target.ModelState.AddModelError("someerrorkey", "someerrormessage");
            var viewModel = this.entitiesForTest.CreateRoleViewModel();

            // act
            var result = await this.target.PostRole(viewModel) as BadRequestObjectResult;

            // assert
            result.Should().NotBeNull();
            result.Value.ShouldBeEquivalentTo(new SerializableError(this.target.ModelState));
        }

        /// <summary>
        /// Posts the Roletype test.
        /// </summary>
        /// <returns>Task.</returns>
        [Fact]
        public async Task PostRoleTest()
        {
            // arrange
            var model = this.entitiesForTest.CreateRoleViewModel();
            var entity = this.entitiesForTest.CreateRole();
            this.mapperMock.Setup(it => it.Map<Role>(model)).Returns(entity);

            // act
            var result = await this.target.PostRole(model) as CreatedAtActionResult;

            // assert
            result.Should().NotBeNull();
            result.ActionName.Should().Be("GetRole");
            result.RouteValues["id"].Should().Be(entity.Id);
            this.repositoryMock.Verify(it => it.InsertAsync(entity));
        }

        #endregion PostRole

        #region DeleteRole

        /// <summary>
        /// Deletes the Roleinvalid model.
        /// </summary>
        /// <returns>Task.</returns>
        [Fact]
        public async Task DeleteRoleTestInvalidModel()
        {
            // arrange
            var id = 3L;
            this.target.ModelState.AddModelError("someerrorkey", "someerrormessage");

            // act
            var result = await this.target.DeleteRole(id) as BadRequestObjectResult;

            // assert
            result.Should().NotBeNull();
            result.Value.ShouldBeEquivalentTo(new SerializableError(this.target.ModelState));
        }

        /// <summary>
        /// Deletes the Role test not found.
        /// </summary>
        /// <returns>Task.</returns>
        [Fact]
        public async Task DeleteRoleTestNotFound()
        {
            // arrange
            var id = 3L;
            this.repositoryMock.Setup(it => it.FindByIdAsync(id)).ReturnsAsync(default(Role));

            // act
            var result = await this.target.DeleteRole(id) as NotFoundResult;

            // assert
            result.Should().NotBeNull();
        }

        /// <summary>
        /// Deletes the Role test.
        /// </summary>
        /// <returns>Task.</returns>
        [Fact]
        public async Task DeleteRoleTest()
        {
            // arrange
            var id = 3L;
            var entity = this.entitiesForTest.CreateRole();
            var model = this.entitiesForTest.CreateRoleViewModel();
            this.mapperMock.Setup(it => it.Map<RoleViewModel>(entity)).Returns(model);
            this.repositoryMock.Setup(it => it.FindByIdAsync(id)).ReturnsAsync(entity);

            // act
            var result = await this.target.DeleteRole(id) as OkObjectResult;

            // assert
            result.Should().NotBeNull();
            result.Value.ShouldBeEquivalentTo(model);
            this.repositoryMock.Verify(it => it.DeleteAsync(entity));
        }

        #endregion DeleteRole
    }
}