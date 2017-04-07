// ***********************************************************************
// Assembly         : Stakeholders.Web.Tests
// Author           : George
// Created          : 02-19-2017
//
// Last Modified By : George
// Last Modified On : 04-03-2017
// ***********************************************************************
// <copyright file="OrganizationCategoriesControllerTest.cs" company="">
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
using Stakeholders.Web.Models.OrganizationCategoryViewModels;
using Xunit;

namespace Stakeholders.Web.Tests.Controllers
{
    /// <summary>
    /// Class OrganizationCategoriesControllerTest.
    /// </summary>
    public class OrganizationCategoriesControllerTest
    {
        /// <summary>
        /// The entities for test
        /// </summary>
        private readonly EntitiesForTest entitiesForTest;

        /// <summary>
        /// The repository mock
        /// </summary>
        private readonly Mock<IRepository<OrganizationCategory>> repositoryMock;

        /// <summary>
        /// The target
        /// </summary>
        private readonly OrganizationCategoriesController target;

        /// <summary>
        /// The mapper mock
        /// </summary>
        private readonly Mock<IMapper> mapperMock;

        /// <summary>
        /// Initializes a new instance of the <see cref="OrganizationCategoriesControllerTest" /> class.
        /// </summary>
        public OrganizationCategoriesControllerTest()
        {
            this.entitiesForTest = new EntitiesForTest();
            this.repositoryMock = new Mock<IRepository<OrganizationCategory>>();
            this.mapperMock = new Mock<IMapper>();

            this.target = new OrganizationCategoriesController(
                this.repositoryMock.Object,
                this.mapperMock.Object);
        }

        #region GetOrganizationCategories

        /// <summary>
        /// Gets the organization types test.
        /// </summary>
        [Fact]
        public void GetOrganizationTypesTest()
        {
            // arrange
            var entities = this.entitiesForTest.CreateCollection(4, this.entitiesForTest.CreateOrganizationCategory);
            var models = new List<OrganizationCategoryViewModel>();
            foreach (var entity in entities)
            {
                var model = this.entitiesForTest.CreateOrganizationCategoryViewModel();
                this.mapperMock.Setup(it => it.Map<OrganizationCategoryViewModel>(entity)).Returns(model);
                models.Add(model);
            }

            var expectedResult = models.ToArray();
            var start = 2;
            var count = 3;
            this.repositoryMock.Setup(it => it.GetAll(start, count, It.IsAny<Func<OrganizationCategory, bool>>())).Returns(entities);

            // act 
            var result = this.target.GetOrganizationCategories(start, count);

            // assert
            result.ShouldBeEquivalentTo(expectedResult);
        }

        #endregion GetOrganizationCategories

        #region GetOrganizationCategoriesCount

        /// <summary>
        /// Gets the organization types count test.
        /// </summary>
        [Fact]
        public void GetOrganizationCategoriesCountTest()
        {
            // arrange
            var expectedResult = 42L;
            this.repositoryMock.Setup(it => it.Count()).Returns(expectedResult);

            // act 
            var result = this.target.GetOrganizationCategoriesCount();

            // assert
            result.ShouldBeEquivalentTo(expectedResult);
        }

        #endregion GetOrganizationCategoriesCount

        #region GetOrganizationCategory

        /// <summary>
        /// Gets the OrganizationCategory test invalid model.
        /// </summary>
        /// <returns>Task.</returns>
        [Fact]
        public async Task GetOrganizationCategoryTestInvalidModel()
        {
            // arrange
            var id = 3L;
            this.target.ModelState.AddModelError("someerrorkey", "someerrormessage");

            // act
            var result = await this.target.GetOrganizationCategory(id) as BadRequestObjectResult;

            // assert
            result.Should().NotBeNull();
            result.Value.ShouldBeEquivalentTo(new SerializableError(this.target.ModelState));
        }

        /// <summary>
        /// Gets the OrganizationCategory test not found.
        /// </summary>
        /// <returns>Task.</returns>
        [Fact]
        public async Task GetOrganizationCategoryTestNotFound()
        {
            // arrange
            var id = 3L;
            this.repositoryMock.Setup(it => it.FindByIdAsync(id)).ReturnsAsync(default(OrganizationCategory));

            // act
            var result = await this.target.GetOrganizationCategory(id) as NotFoundResult;

            // assert
            result.Should().NotBeNull();
        }

        /// <summary>
        /// Gets the OrganizationCategory test.
        /// </summary>
        /// <returns>Task.</returns>
        [Fact]
        public async Task GetOrganizationCategoryTest()
        {
            // arrange
            var id = 3L;
            var entity = this.entitiesForTest.CreateOrganizationCategory();
            this.repositoryMock.Setup(it => it.FindByIdAsync(id)).ReturnsAsync(entity);


            var expectedResult = this.entitiesForTest.CreateOrganizationCategoryViewModel();
            this.mapperMock.Setup(it => it.Map<OrganizationCategoryViewModel>(entity)).Returns(expectedResult);

            // act
            var result = await this.target.GetOrganizationCategory(id) as OkObjectResult;

            // assert
            result.Should().NotBeNull();
            result.Value.ShouldBeEquivalentTo(expectedResult);
        }

        #endregion GetOrganizationCategory

        #region PutOrganizationCategory

        /// <summary>
        /// Puts the OrganizationCategory test invalid model.
        /// </summary>
        /// <returns>Task.</returns>
        [Fact]
        public async Task PutOrganizationCategoryTestInvalidModel()
        {
            // arrange
            var id = 3L;
            this.target.ModelState.AddModelError("someerrorkey", "someerrormessage");
            var viewModel = this.entitiesForTest.CreateOrganizationCategoryViewModel();

            // act
            var result = await this.target.PutOrganizationCategory(id, viewModel) as BadRequestObjectResult;

            // assert
            result.Should().NotBeNull();
            result.Value.ShouldBeEquivalentTo(new SerializableError(this.target.ModelState));
        }

        /// <summary>
        /// Puts the OrganizationCategory test not found.
        /// </summary>
        /// <returns>Task.</returns>
        [Fact]
        public async Task PutOrganizationCategoryTestNotFound()
        {
            // arrange
            var id = 3L;
            var viewModel = this.entitiesForTest.CreateOrganizationCategoryViewModel();
            this.repositoryMock.Setup(it => it.FindByIdAsync(id)).ReturnsAsync(default(OrganizationCategory));

            // act
            var result = await this.target.PutOrganizationCategory(id, viewModel) as NotFoundResult;

            // assert
            result.Should().NotBeNull();
        }

        /// <summary>
        /// Puts the OrganizationCategory test.
        /// </summary>
        /// <returns>Task.</returns>
        [Fact]
        public async Task PutOrganizationCategoryTest()
        {
            // arrange
            var id = 3L;
            var entity = this.entitiesForTest.CreateOrganizationCategory();
            var viewModel = this.entitiesForTest.CreateOrganizationCategoryViewModel();
            this.repositoryMock.Setup(it => it.FindByIdAsync(id)).ReturnsAsync(entity);

            // act
            var result = await this.target.PutOrganizationCategory(id, viewModel) as NoContentResult;

            // assert
            result.Should().NotBeNull();
            this.mapperMock.Verify(it => it.Map(viewModel, entity));
            this.repositoryMock.Verify(it => it.UpdateAsync(entity));
        }

        #endregion PutOrganizationCategory

        #region PostOrganizationCategory

        /// <summary>
        /// Posts the OrganizationCategorytype test invalid model.
        /// </summary>
        /// <returns>Task.</returns>
        [Fact]
        public async Task PostOrganizationCategoryTestInvalidModel()
        {
            // arrange
            this.target.ModelState.AddModelError("someerrorkey", "someerrormessage");
            var viewModel = this.entitiesForTest.CreateOrganizationCategoryViewModel();

            // act
            var result = await this.target.PostOrganizationCategory(viewModel) as BadRequestObjectResult;

            // assert
            result.Should().NotBeNull();
            result.Value.ShouldBeEquivalentTo(new SerializableError(this.target.ModelState));
        }

        /// <summary>
        /// Posts the OrganizationCategorytype test.
        /// </summary>
        /// <returns>Task.</returns>
        [Fact]
        public async Task PostOrganizationCategoryTest()
        {
            // arrange
            var model = this.entitiesForTest.CreateOrganizationCategoryViewModel();
            var entity = this.entitiesForTest.CreateOrganizationCategory();
            this.mapperMock.Setup(it => it.Map<OrganizationCategory>(model)).Returns(entity);

            // act
            var result = await this.target.PostOrganizationCategory(model) as CreatedAtActionResult;

            // assert
            result.Should().NotBeNull();
            result.ActionName.Should().Be("GetOrganizationCategory");
            result.RouteValues["id"].Should().Be(entity.Id);
            this.repositoryMock.Verify(it => it.InsertAsync(entity));
        }

        #endregion PostOrganizationCategory

        #region DeleteOrganizationCategory

        /// <summary>
        /// Deletes the OrganizationCategoryinvalid model.
        /// </summary>
        /// <returns>Task.</returns>
        [Fact]
        public async Task DeleteOrganizationCategoryTestInvalidModel()
        {
            // arrange
            var id = 3L;
            this.target.ModelState.AddModelError("someerrorkey", "someerrormessage");

            // act
            var result = await this.target.DeleteOrganizationCategory(id) as BadRequestObjectResult;

            // assert
            result.Should().NotBeNull();
            result.Value.ShouldBeEquivalentTo(new SerializableError(this.target.ModelState));
        }

        /// <summary>
        /// Deletes the OrganizationCategory test not found.
        /// </summary>
        /// <returns>Task.</returns>
        [Fact]
        public async Task DeleteOrganizationCategoryTestNotFound()
        {
            // arrange
            var id = 3L;
            this.repositoryMock.Setup(it => it.FindByIdAsync(id)).ReturnsAsync(default(OrganizationCategory));

            // act
            var result = await this.target.DeleteOrganizationCategory(id) as NotFoundResult;

            // assert
            result.Should().NotBeNull();
        }

        /// <summary>
        /// Deletes the OrganizationCategory test.
        /// </summary>
        /// <returns>Task.</returns>
        [Fact]
        public async Task DeleteOrganizationCategoryTest()
        {
            // arrange
            var id = 3L;
            var entity = this.entitiesForTest.CreateOrganizationCategory();
            var model = this.entitiesForTest.CreateOrganizationCategoryViewModel();
            this.mapperMock.Setup(it => it.Map<OrganizationCategoryViewModel>(entity)).Returns(model);
            this.repositoryMock.Setup(it => it.FindByIdAsync(id)).ReturnsAsync(entity);

            // act
            var result = await this.target.DeleteOrganizationCategory(id) as OkObjectResult;

            // assert
            result.Should().NotBeNull();
            result.Value.ShouldBeEquivalentTo(model);
            this.repositoryMock.Verify(it => it.DeleteAsync(entity));
        }

        #endregion DeleteOrganizationCategory
    }
}