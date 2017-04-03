// ***********************************************************************
// Assembly         : Stakeholders.Web.Tests
// Author           : George
// Created          : 02-20-2017
//
// Last Modified By : George
// Last Modified On : 02-20-2017
// ***********************************************************************
// <copyright file="ActivityTasksControllerTest.cs" company="">
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
using Stakeholders.Web.Models.ActivityTaskViewModels;
using Xunit;

namespace Stakeholders.Web.Tests.Controllers
{
    /// <summary>
    /// Class ActivityTasksControllerTest.
    /// </summary>
    public class ActivityTasksControllerTest
    {
        /// <summary>
        /// The entities for test
        /// </summary>
        private readonly EntitiesForTest entitiesForTest;

        /// <summary>
        /// The repository mock
        /// </summary>
        private readonly Mock<IRepository<ActivityTask>> repositoryMock;

        /// <summary>
        /// The target
        /// </summary>
        private readonly ActivityTasksController target;

        /// <summary>
        /// The mapper mock
        /// </summary>
        private readonly Mock<IMapper> mapperMock;

        /// <summary>
        /// Initializes a new instance of the <see cref="ActivitiesControllerTest" /> class.
        /// </summary>
        public ActivityTasksControllerTest()
        {
            this.entitiesForTest = new EntitiesForTest();
            this.repositoryMock = new Mock<IRepository<ActivityTask>>();
            this.mapperMock = new Mock<IMapper>();

            this.target = new ActivityTasksController(
                this.repositoryMock.Object,
                this.mapperMock.Object);
        }

        #region GetActivityTasks

        /// <summary>
        /// Gets the activity tasks.
        /// </summary>
        [Fact]
        public void GetActivityTasksTest()
        {
            // arrange
            var entities = this.entitiesForTest.CreateCollection(4, this.entitiesForTest.CreateActivityTask);
            var models = new List<ActivityTaskViewModel>();
            foreach (var entity in entities)
            {
                var model = this.entitiesForTest.CreateActivityTaskViewModel();
                this.mapperMock.Setup(it => it.Map<ActivityTaskViewModel>(entity)).Returns(model);
                models.Add(model);
            }

            var expectedResult = models.ToArray();
            var start = 2;
            var count = 3;
            this.repositoryMock.Setup(it => it.GetAll(start, count, It.IsAny<Func<ActivityTask, bool>>())).Returns(entities);

            // act 
            var result = this.target.GetActivityTasks(start, count);

            // assert
            result.ShouldBeEquivalentTo(expectedResult);
        }

        #endregion GetActivityTasks

        #region GetActivityTasksCount

        /// <summary>
        /// Gets the activity tasks count test.
        /// </summary>
        [Fact]
        public void GetActivityTasksCountTest()
        {
            // arrange
            var expectedResult = 42L;
            this.repositoryMock.Setup(it => it.Count()).Returns(expectedResult);

            // act 
            var result = this.target.GetActivityTasksCount();

            // assert
            result.ShouldBeEquivalentTo(expectedResult);
        }

        #endregion GetActivityTasksCount

        #region GetActivityTask

        /// <summary>
        /// Gets the activity task test invalid model.
        /// </summary>
        /// <returns>Task.</returns>
        [Fact]
        public async Task GetActivityTaskTestInvalidModel()
        {
            // arrange
            var id = 3L;
            this.target.ModelState.AddModelError("someerrorkey", "someerrormessage");

            // act
            var result = await this.target.GetActivityTask(id) as BadRequestObjectResult;

            // assert
            result.Should().NotBeNull();
            result.Value.ShouldBeEquivalentTo(new SerializableError(this.target.ModelState));
        }

        /// <summary>
        /// Gets the activity task test not found.
        /// </summary>
        /// <returns>Task.</returns>
        [Fact]
        public async Task GetActivityTaskTestNotFound()
        {
            // arrange
            var id = 3L;
            this.repositoryMock.Setup(it => it.FindByIdAsync(id)).ReturnsAsync(default(ActivityTask));

            // act
            var result = await this.target.GetActivityTask(id) as NotFoundResult;

            // assert
            result.Should().NotBeNull();
        }

        /// <summary>
        /// Gets the activity task test.
        /// </summary>
        /// <returns>Task.</returns>
        [Fact]
        public async Task GetActivityTaskTest()
        {
            // arrange
            var id = 3L;
            var entity = this.entitiesForTest.CreateActivityTask();
            this.repositoryMock.Setup(it => it.FindByIdAsync(id)).ReturnsAsync(entity);


            var expectedResult = this.entitiesForTest.CreateActivityTaskViewModel();
            this.mapperMock.Setup(it => it.Map<ActivityTaskViewModel>(entity)).Returns(expectedResult);

            // act
            var result = await this.target.GetActivityTask(id) as OkObjectResult;

            // assert
            result.Should().NotBeNull();
            result.Value.ShouldBeEquivalentTo(expectedResult);
        }

        #endregion GetActivityTask

        #region PutActivityTask

        /// <summary>
        /// Puts the activity task test invalid model.
        /// </summary>
        /// <returns>Task.</returns>
        [Fact]
        public async Task PutActivityTaskTestInvalidModel()
        {
            // arrange
            var id = 3L;
            this.target.ModelState.AddModelError("someerrorkey", "someerrormessage");
            var viewModel = this.entitiesForTest.CreateActivityTaskViewModel();

            // act
            var result = await this.target.PutActivityTask(id, viewModel) as BadRequestObjectResult;

            // assert
            result.Should().NotBeNull();
            result.Value.ShouldBeEquivalentTo(new SerializableError(this.target.ModelState));
        }

        /// <summary>
        /// Puts the activity task test not found.
        /// </summary>
        /// <returns>Task.</returns>
        [Fact]
        public async Task PutActivityTaskTestNotFound()
        {
            // arrange
            var id = 3L;
            var viewModel = this.entitiesForTest.CreateActivityTaskViewModel();
            this.repositoryMock.Setup(it => it.FindByIdAsync(id)).ReturnsAsync(default(ActivityTask));

            // act
            var result = await this.target.PutActivityTask(id, viewModel) as NotFoundResult;

            // assert
            result.Should().NotBeNull();
        }

        /// <summary>
        /// Puts the activity task test.
        /// </summary>
        /// <returns>Task.</returns>
        [Fact]
        public async Task PutActivityTaskTest()
        {
            // arrange
            var id = 3L;
            var entity = this.entitiesForTest.CreateActivityTask();
            var viewModel = this.entitiesForTest.CreateActivityTaskViewModel();
            this.repositoryMock.Setup(it => it.FindByIdAsync(id)).ReturnsAsync(entity);

            // act
            var result = await this.target.PutActivityTask(id, viewModel) as NoContentResult;

            // assert
            result.Should().NotBeNull();
            this.mapperMock.Verify(it => it.Map(viewModel, entity));
            this.repositoryMock.Verify(it => it.UpdateAsync(entity));
        }

        #endregion PutActivityTask

        #region PostActivityTask

        /// <summary>
        /// Posts the activity task test invalid model.
        /// </summary>
        /// <returns>Task.</returns>
        [Fact]
        public async Task PostActivityTaskTestInvalidModel()
        {
            // arrange
            this.target.ModelState.AddModelError("someerrorkey", "someerrormessage");
            var viewModel = this.entitiesForTest.CreateActivityTaskViewModel();

            // act
            var result = await this.target.PostActivityTask(viewModel) as BadRequestObjectResult;

            // assert
            result.Should().NotBeNull();
            result.Value.ShouldBeEquivalentTo(new SerializableError(this.target.ModelState));
        }

        /// <summary>
        /// Posts the activity task test.
        /// </summary>
        /// <returns>Task.</returns>
        [Fact]
        public async Task PostActivityTaskTest()
        {
            // arrange
            var model = this.entitiesForTest.CreateActivityTaskViewModel();
            var entity = this.entitiesForTest.CreateActivityTask();
            this.mapperMock.Setup(it => it.Map<ActivityTask>(model)).Returns(entity);

            // act
            var result = await this.target.PostActivityTask(model) as CreatedAtActionResult;

            // assert
            result.Should().NotBeNull();
            result.ActionName.Should().Be("GetActivityTask");
            result.RouteValues["id"].Should().Be(entity.Id);
            this.repositoryMock.Verify(it => it.InsertAsync(entity));
        }

        #endregion PostActivityTask

        #region DeleteActivityTask

        /// <summary>
        /// Deletes the organization type invalid model.
        /// </summary>
        /// <returns>Task.</returns>
        [Fact]
        public async Task DeleteActivityTaskTestInvalidModel()
        {
            // arrange
            var id = 3L;
            this.target.ModelState.AddModelError("someerrorkey", "someerrormessage");

            // act
            var result = await this.target.DeleteActivityTask(id) as BadRequestObjectResult;

            // assert
            result.Should().NotBeNull();
            result.Value.ShouldBeEquivalentTo(new SerializableError(this.target.ModelState));
        }

        /// <summary>
        /// Deletes the organization type test not found.
        /// </summary>
        /// <returns>Task.</returns>
        [Fact]
        public async Task DeleteActivityTaskTestNotFound()
        {
            // arrange
            var id = 3L;
            this.repositoryMock.Setup(it => it.FindByIdAsync(id)).ReturnsAsync(default(ActivityTask));

            // act
            var result = await this.target.DeleteActivityTask(id) as NotFoundResult;

            // assert
            result.Should().NotBeNull();
        }

        /// <summary>
        /// Deletes the organization type test.
        /// </summary>
        /// <returns>Task.</returns>
        [Fact]
        public async Task DeleteActivityTaskTest()
        {
            // arrange
            var id = 3L;
            var entity = this.entitiesForTest.CreateActivityTask();
            var model = this.entitiesForTest.CreateActivityTaskViewModel();
            this.mapperMock.Setup(it => it.Map<ActivityTaskViewModel>(entity)).Returns(model);
            this.repositoryMock.Setup(it => it.FindByIdAsync(id)).ReturnsAsync(entity);

            // act
            var result = await this.target.DeleteActivityTask(id) as OkObjectResult;

            // assert
            result.Should().NotBeNull();
            result.Value.ShouldBeEquivalentTo(model);
            this.repositoryMock.Verify(it => it.DeleteAsync(entity));
        }

        #endregion DeleteActivityTask
    }
}