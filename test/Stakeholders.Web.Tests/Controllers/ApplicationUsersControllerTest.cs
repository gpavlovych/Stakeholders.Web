// ***********************************************************************
// Assembly         : Stakeholders.Web.Tests
// Author           : George
// Created          : 02-19-2017
//
// Last Modified By : George
// Last Modified On : 04-07-2017
// ***********************************************************************
// <copyright file="ApplicationUsersControllerTest.cs" company="">
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
using Stakeholders.Web.Models.ApplicationUserViewModels;
using Xunit;

namespace Stakeholders.Web.Tests.Controllers
{
    /// <summary>
    /// Class ApplicationUsersControllerTest.
    /// </summary>
    public class ApplicationUsersControllerTest
    {
        /// <summary>
        /// The entities for test
        /// </summary>
        private readonly EntitiesForTest entitiesForTest;

        /// <summary>
        /// The repository mock
        /// </summary>
        private readonly Mock<IRepository<ApplicationUser>> repositoryMock;

        /// <summary>
        /// The target
        /// </summary>
        private readonly ApplicationUsersController target;

        /// <summary>
        /// The mapper mock
        /// </summary>
        private readonly Mock<IMapper> mapperMock;

        /// <summary>
        /// The user manager mock
        /// </summary>
        private readonly Mock<IApplicationUserManager> userManagerMock;

        /// <summary>
        /// The repository role mock
        /// </summary>
        private readonly Mock<IRepository<Role>> repositoryRoleMock;

        /// <summary>
        /// The repository company mock
        /// </summary>
        private readonly Mock<IRepository<Company>> repositoryCompanyMock;

        /// <summary>
        /// The source mock
        /// </summary>
        private readonly Mock<IDataSource<ApplicationUser>> sourceMock;

        /// <summary>
        /// Initializes a new instance of the <see cref="ApplicationUsersControllerTest" /> class.
        /// </summary>
        public ApplicationUsersControllerTest()
        {
            this.entitiesForTest = new EntitiesForTest();
            this.sourceMock = new Mock<IDataSource<ApplicationUser>>();
            this.repositoryMock = new Mock<IRepository<ApplicationUser>>();
            this.repositoryCompanyMock = new Mock<IRepository<Company>>();
            this.repositoryRoleMock = new Mock<IRepository<Role>>();
            this.mapperMock = new Mock<IMapper>();
            this.userManagerMock = new Mock<IApplicationUserManager>();

            this.target = new ApplicationUsersController(
                this.sourceMock.Object,
                this.repositoryMock.Object,
                this.repositoryCompanyMock.Object,
                this.repositoryRoleMock.Object,
                this.mapperMock.Object,
                this.userManagerMock.Object);
        }

        #region GetApplicationUsers

        /// <summary>
        /// Gets the organization types test.
        /// </summary>
        [Fact]
        public void GetOrganizationTypesTest()
        {
            // arrange
            var entities = this.entitiesForTest.CreateCollection(4, this.entitiesForTest.CreateApplicationUser);
            var models = new List<ApplicationUserViewModel>();
            foreach (var entity in entities)
            {
                var model = this.entitiesForTest.CreateApplicationUserViewModel();
                this.mapperMock.Setup(it => it.Map<ApplicationUserViewModel>(entity)).Returns(model);
                models.Add(model);
            }

            var expectedResult = models.ToArray();
            var start = 2;
            var count = 3;
            this.repositoryMock.Setup(it => it.GetAll(start, count, It.IsAny<Func<ApplicationUser, bool>>())).Returns(entities);

            // act 
            var result = this.target.GetApplicationUsers(start, count);

            // assert
            result.ShouldBeEquivalentTo(expectedResult);
        }

        #endregion GetApplicationUsers

        #region GetApplicationUsersCount

        /// <summary>
        /// Gets the organization types count test.
        /// </summary>
        [Fact]
        public void GetApplicationUsersCountTest()
        {
            // arrange
            var expectedResult = 42L;
            this.repositoryMock.Setup(it => it.Count()).Returns(expectedResult);

            // act 
            var result = this.target.GetApplicationUsersCount();

            // assert
            result.ShouldBeEquivalentTo(expectedResult);
        }

        #endregion GetApplicationUsersCount

        #region GetApplicationUser

        /// <summary>
        /// Gets the ApplicationUser test invalid model.
        /// </summary>
        /// <returns>Task.</returns>
        [Fact]
        public async Task GetApplicationUserTestInvalidModel()
        {
            // arrange
            var id = 3L;
            this.target.ModelState.AddModelError("someerrorkey", "someerrormessage");

            // act
            var result = await this.target.GetApplicationUser(id) as BadRequestObjectResult;

            // assert
            result.Should().NotBeNull();
            result.Value.ShouldBeEquivalentTo(new SerializableError(this.target.ModelState));
        }

        /// <summary>
        /// Gets the ApplicationUser test not found.
        /// </summary>
        /// <returns>Task.</returns>
        [Fact]
        public async Task GetApplicationUserTestNotFound()
        {
            // arrange
            var id = 3L;
            this.repositoryMock.Setup(it => it.FindByIdAsync(id)).ReturnsAsync(default(ApplicationUser));

            // act
            var result = await this.target.GetApplicationUser(id) as NotFoundResult;

            // assert
            result.Should().NotBeNull();
        }

        /// <summary>
        /// Gets the ApplicationUser test.
        /// </summary>
        /// <returns>Task.</returns>
        [Fact]
        public async Task GetApplicationUserTest()
        {
            // arrange
            var id = 3L;
            var entity = this.entitiesForTest.CreateApplicationUser();
            this.repositoryMock.Setup(it => it.FindByIdAsync(id)).ReturnsAsync(entity);


            var expectedResult = this.entitiesForTest.CreateApplicationUserViewModel();
            this.mapperMock.Setup(it => it.Map<ApplicationUserViewModel>(entity)).Returns(expectedResult);

            // act
            var result = await this.target.GetApplicationUser(id) as OkObjectResult;

            // assert
            result.Should().NotBeNull();
            result.Value.ShouldBeEquivalentTo(expectedResult);
        }

        #endregion GetApplicationUser

        #region PutApplicationUser

        /// <summary>
        /// Puts the ApplicationUser test invalid model.
        /// </summary>
        /// <returns>Task.</returns>
        [Fact]
        public async Task PutApplicationUserTestInvalidModel()
        {
            // arrange
            var id = 3L;
            this.target.ModelState.AddModelError("someerrorkey", "someerrormessage");
            var viewModel = this.entitiesForTest.CreateApplicationUserViewModel();

            // act
            var result = await this.target.PutApplicationUser(id, viewModel) as BadRequestObjectResult;

            // assert
            result.Should().NotBeNull();
            result.Value.ShouldBeEquivalentTo(new SerializableError(this.target.ModelState));
        }

        /// <summary>
        /// Puts the ApplicationUser test not found.
        /// </summary>
        /// <returns>Task.</returns>
        [Fact]
        public async Task PutApplicationUserTestNotFound()
        {
            // arrange
            var id = 3L;
            var viewModel = this.entitiesForTest.CreateApplicationUserViewModel();
            this.repositoryMock.Setup(it => it.FindByIdAsync(id)).ReturnsAsync(default(ApplicationUser));

            // act
            var result = await this.target.PutApplicationUser(id, viewModel) as NotFoundResult;

            // assert
            result.Should().NotBeNull();
        }

        /// <summary>
        /// Puts the ApplicationUser test.
        /// </summary>
        /// <returns>Task.</returns>
        [Fact]
        public async Task PutApplicationUserTest()
        {
            // arrange
            var id = 3L;
            var entity = this.entitiesForTest.CreateApplicationUser();
            var viewModel = this.entitiesForTest.CreateApplicationUserViewModel();
            this.repositoryMock.Setup(it => it.FindByIdAsync(id)).ReturnsAsync(entity);

            // act
            var result = await this.target.PutApplicationUser(id, viewModel) as NoContentResult;

            // assert
            result.Should().NotBeNull();
            this.mapperMock.Verify(it => it.Map(viewModel, entity));
            this.repositoryMock.Verify(it => it.UpdateAsync(entity));
        }

        #endregion PutApplicationUser

        #region PostApplicationUser

        /// <summary>
        /// Posts the ApplicationUsertype test invalid model.
        /// </summary>
        /// <returns>Task.</returns>
        [Fact]
        public async Task PostApplicationUserTestInvalidModel()
        {
            // arrange
            this.target.ModelState.AddModelError("someerrorkey", "someerrormessage");
            var viewModel = this.entitiesForTest.CreateCreateApplicationUserViewModel();

            // act
            var result = await this.target.PostApplicationUser(viewModel) as BadRequestObjectResult;

            // assert
            result.Should().NotBeNull();
            result.Value.ShouldBeEquivalentTo(new SerializableError(this.target.ModelState));
        }

        /// <summary>
        /// Posts the ApplicationUsertype test.
        /// </summary>
        /// <returns>Task.</returns>
        [Fact]
        public async Task PostApplicationUserTest()
        {
            // arrange
            var model = this.entitiesForTest.CreateCreateApplicationUserViewModel();
            var entity = this.entitiesForTest.CreateApplicationUser();
            this.mapperMock.Setup(it => it.Map<ApplicationUser>(model)).Returns(entity);

            // act
            var result = await this.target.PostApplicationUser(model) as CreatedAtActionResult;

            // assert
            result.Should().NotBeNull();
            result.ActionName.Should().Be("GetApplicationUser");
            result.RouteValues["id"].Should().Be(entity.Id);
            this.userManagerMock.Verify(it => it.CreateAsync(entity, model.Password));
        }

        #endregion PostApplicationUser

        #region DeleteApplicationUser

        /// <summary>
        /// Deletes the ApplicationUserinvalid model.
        /// </summary>
        /// <returns>Task.</returns>
        [Fact]
        public async Task DeleteApplicationUserTestInvalidModel()
        {
            // arrange
            var id = 3L;
            this.target.ModelState.AddModelError("someerrorkey", "someerrormessage");

            // act
            var result = await this.target.DeleteApplicationUser(id) as BadRequestObjectResult;

            // assert
            result.Should().NotBeNull();
            result.Value.ShouldBeEquivalentTo(new SerializableError(this.target.ModelState));
        }

        /// <summary>
        /// Deletes the ApplicationUser test not found.
        /// </summary>
        /// <returns>Task.</returns>
        [Fact]
        public async Task DeleteApplicationUserTestNotFound()
        {
            // arrange
            var id = 3L;
            this.repositoryMock.Setup(it => it.FindByIdAsync(id)).ReturnsAsync(default(ApplicationUser));

            // act
            var result = await this.target.DeleteApplicationUser(id) as NotFoundResult;

            // assert
            result.Should().NotBeNull();
        }

        /// <summary>
        /// Deletes the ApplicationUser test.
        /// </summary>
        /// <returns>Task.</returns>
        [Fact]
        public async Task DeleteApplicationUserTest()
        {
            // arrange
            var id = 3L;
            var entity = this.entitiesForTest.CreateApplicationUser();
            var model = this.entitiesForTest.CreateApplicationUserViewModel();
            this.mapperMock.Setup(it => it.Map<ApplicationUserViewModel>(entity)).Returns(model);
            this.repositoryMock.Setup(it => it.FindByIdAsync(id)).ReturnsAsync(entity);

            // act
            var result = await this.target.DeleteApplicationUser(id) as OkObjectResult;

            // assert
            result.Should().NotBeNull();
            result.Value.ShouldBeEquivalentTo(model);
            this.repositoryMock.Verify(it => it.DeleteAsync(entity));
        }

        #endregion DeleteApplicationUser
    }
}