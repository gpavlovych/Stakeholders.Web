// ***********************************************************************
// Assembly         : Stakeholders.Web.Tests
// Author           : George
// Created          : 02-19-2017
//
// Last Modified By : George
// Last Modified On : 02-20-2017
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
using Stakeholders.Web.Models.ActivityViewModels;
using Xunit;

namespace Stakeholders.Web.Tests.Controllers
{
    /// <summary>
    /// Class ActivitiesControllerTest.
    /// </summary>
    public class ActivitiesControllerTest
    {
        /// <summary>
        /// The entities for test
        /// </summary>
        private readonly EntitiesForTest entitiesForTest;

        /// <summary>
        /// The repository mock
        /// </summary>
        private readonly Mock<IRepository<Activity>> repositoryMock;

        /// <summary>
        /// The target
        /// </summary>
        private readonly ActivitiesController target;

        /// <summary>
        /// The mapper mock
        /// </summary>
        private readonly Mock<IMapper> mapperMock;

        /// <summary>
        /// Initializes a new instance of the <see cref="ActivitiesControllerTest" /> class.
        /// </summary>
        public ActivitiesControllerTest()
        {
            this.entitiesForTest = new EntitiesForTest();
            this.repositoryMock = new Mock<IRepository<Activity>>();
            this.mapperMock = new Mock<IMapper>();

            this.target = new ActivitiesController(
                this.repositoryMock.Object,
                this.mapperMock.Object);
        }

        #region GetActivities

        /// <summary>
        /// Gets the organization types test.
        /// </summary>
        [Fact]
        public void GetOrganizationTypesTest()
        {
            // arrange
            var entities = this.entitiesForTest.CreateCollection(4, this.entitiesForTest.CreateActivity);
            var models = new List<ActivityViewModel>();
            foreach (var entity in entities)
            {
                var model = this.entitiesForTest.CreateActivityViewModel();
                this.mapperMock.Setup(it => it.Map<ActivityViewModel>(entity)).Returns(model);
                models.Add(model);
            }

            var expectedResult = models.ToArray();
            var start = 2;
            var count = 3;
            this.repositoryMock.Setup(it => it.GetAll(start, count, It.IsAny<Func<Activity, bool>>())).Returns(entities);

            // act 
            var result = this.target.GetActivities(start, count);

            // assert
            result.ShouldBeEquivalentTo(expectedResult);
        }

        #endregion GetActivities

        #region GetActivitiesCount

        /// <summary>
        /// Gets the organization types count test.
        /// </summary>
        [Fact]
        public void GetActivitiesCountTest()
        {
            // arrange
            var expectedResult = 42L;
            this.repositoryMock.Setup(it => it.Count()).Returns(expectedResult);

            // act 
            var result = this.target.GetActivitiesCount();

            // assert
            result.ShouldBeEquivalentTo(expectedResult);
        }

        #endregion GetActivitiesCount

        #region GetActivity

        /// <summary>
        /// Gets the activity test invalid model.
        /// </summary>
        /// <returns>Task.</returns>
        [Fact]
        public async Task GetActivityTestInvalidModel()
        {
            // arrange
            var id = 3L;
            this.target.ModelState.AddModelError("someerrorkey", "someerrormessage");

            // act
            var result = await this.target.GetActivity(id) as BadRequestObjectResult;

            // assert
            result.Should().NotBeNull();
            result.Value.ShouldBeEquivalentTo(new SerializableError(this.target.ModelState));
        }

        /// <summary>
        /// Gets the activity test not found.
        /// </summary>
        /// <returns>Task.</returns>
        [Fact]
        public async Task GetActivityTestNotFound()
        {
            // arrange
            var id = 3L;
            this.repositoryMock.Setup(it => it.FindByIdAsync(id)).ReturnsAsync(default(Activity));

            // act
            var result = await this.target.GetActivity(id) as NotFoundResult;

            // assert
            result.Should().NotBeNull();
        }

        /// <summary>
        /// Gets the activity test.
        /// </summary>
        /// <returns>Task.</returns>
        [Fact]
        public async Task GetActivityTest()
        {
            // arrange
            var id = 3L;
            var entity = this.entitiesForTest.CreateActivity();
            this.repositoryMock.Setup(it => it.FindByIdAsync(id)).ReturnsAsync(entity);


            var expectedResult = this.entitiesForTest.CreateActivityViewModel();
            this.mapperMock.Setup(it => it.Map<ActivityViewModel>(entity)).Returns(expectedResult);

            // act
            var result = await this.target.GetActivity(id) as OkObjectResult;

            // assert
            result.Should().NotBeNull();
            result.Value.ShouldBeEquivalentTo(expectedResult);
        }

        #endregion GetActivity

        #region PutActivity

        /// <summary>
        /// Puts the activity test invalid model.
        /// </summary>
        /// <returns>Task.</returns>
        [Fact]
        public async Task PutActivityTestInvalidModel()
        {
            // arrange
            var id = 3L;
            this.target.ModelState.AddModelError("someerrorkey", "someerrormessage");
            var viewModel = this.entitiesForTest.CreateActivityViewModel();

            // act
            var result = await this.target.PutActivity(id, viewModel) as BadRequestObjectResult;

            // assert
            result.Should().NotBeNull();
            result.Value.ShouldBeEquivalentTo(new SerializableError(this.target.ModelState));
        }

        /// <summary>
        /// Puts the activity test not found.
        /// </summary>
        /// <returns>Task.</returns>
        [Fact]
        public async Task PutActivityTestNotFound()
        {
            // arrange
            var id = 3L;
            var viewModel = this.entitiesForTest.CreateActivityViewModel();
            this.repositoryMock.Setup(it => it.FindByIdAsync(id)).ReturnsAsync(default(Activity));

            // act
            var result = await this.target.PutActivity(id, viewModel) as NotFoundResult;

            // assert
            result.Should().NotBeNull();
        }

        /// <summary>
        /// Puts the activity test.
        /// </summary>
        /// <returns>Task.</returns>
        [Fact]
        public async Task PutActivityTest()
        {
            // arrange
            var id = 3L;
            var entity = this.entitiesForTest.CreateActivity();
            var viewModel = this.entitiesForTest.CreateActivityViewModel();
            this.repositoryMock.Setup(it => it.FindByIdAsync(id)).ReturnsAsync(entity);

            // act
            var result = await this.target.PutActivity(id, viewModel) as NoContentResult;

            // assert
            result.Should().NotBeNull();
            this.mapperMock.Verify(it => it.Map(viewModel, entity));
            this.repositoryMock.Verify(it => it.UpdateAsync(entity));
        }

        #endregion PutActivity

        #region PostActivity

        /// <summary>
        /// Posts the activitytype test invalid model.
        /// </summary>
        /// <returns>Task.</returns>
        [Fact]
        public async Task PostActivityTestInvalidModel()
        {
            // arrange
            this.target.ModelState.AddModelError("someerrorkey", "someerrormessage");
            var viewModel = this.entitiesForTest.CreateActivityViewModel();

            // act
            var result = await this.target.PostActivity(viewModel) as BadRequestObjectResult;

            // assert
            result.Should().NotBeNull();
            result.Value.ShouldBeEquivalentTo(new SerializableError(this.target.ModelState));
        }

        /// <summary>
        /// Posts the activitytype test.
        /// </summary>
        /// <returns>Task.</returns>
        [Fact]
        public async Task PostActivityTest()
        {
            // arrange
            var model = this.entitiesForTest.CreateActivityViewModel();
            var entity = this.entitiesForTest.CreateActivity();
            this.mapperMock.Setup(it => it.Map<Activity>(model)).Returns(entity);

            // act
            var result = await this.target.PostActivity(model) as CreatedAtActionResult;

            // assert
            result.Should().NotBeNull();
            result.ActionName.Should().Be("GetActivity");
            result.RouteValues["id"].Should().Be(entity.Id);
            this.repositoryMock.Verify(it => it.InsertAsync(entity));
        }

        #endregion PostActivity

        #region DeleteActivity

        /// <summary>
        /// Deletes the activityinvalid model.
        /// </summary>
        /// <returns>Task.</returns>
        [Fact]
        public async Task DeleteActivityTestInvalidModel()
        {
            // arrange
            var id = 3L;
            this.target.ModelState.AddModelError("someerrorkey", "someerrormessage");

            // act
            var result = await this.target.DeleteActivity(id) as BadRequestObjectResult;

            // assert
            result.Should().NotBeNull();
            result.Value.ShouldBeEquivalentTo(new SerializableError(this.target.ModelState));
        }

        /// <summary>
        /// Deletes the activity test not found.
        /// </summary>
        /// <returns>Task.</returns>
        [Fact]
        public async Task DeleteActivityTestNotFound()
        {
            // arrange
            var id = 3L;
            this.repositoryMock.Setup(it => it.FindByIdAsync(id)).ReturnsAsync(default(Activity));

            // act
            var result = await this.target.DeleteActivity(id) as NotFoundResult;

            // assert
            result.Should().NotBeNull();
        }

        /// <summary>
        /// Deletes the activity test.
        /// </summary>
        /// <returns>Task.</returns>
        [Fact]
        public async Task DeleteActivityTest()
        {
            // arrange
            var id = 3L;
            var entity = this.entitiesForTest.CreateActivity();
            var model = this.entitiesForTest.CreateActivityViewModel();
            this.mapperMock.Setup(it => it.Map<ActivityViewModel>(entity)).Returns(model);
            this.repositoryMock.Setup(it => it.FindByIdAsync(id)).ReturnsAsync(entity);

            // act
            var result = await this.target.DeleteActivity(id) as OkObjectResult;

            // assert
            result.Should().NotBeNull();
            result.Value.ShouldBeEquivalentTo(model);
            this.repositoryMock.Verify(it => it.DeleteAsync(entity));
        }

        #endregion DeleteActivity
    }
}