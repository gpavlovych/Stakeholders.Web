// ***********************************************************************
// Assembly         : Stakeholders.Web.Tests
// Author           : George
// Created          : 02-20-2017
//
// Last Modified By : George
// Last Modified On : 02-20-2017
// ***********************************************************************
// <copyright file="ActivityTaskStatusesControllerTest.cs" company="">
//     Copyright (c) . All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************

using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using Moq;
using Stakeholders.Web.Controllers;
using Stakeholders.Web.Data;
using Stakeholders.Web.Models;
using Stakeholders.Web.Models.ActivityTaskStatusViewModels;
using Xunit;

namespace Stakeholders.Web.Tests.Controllers
{
    /// <summary>
    /// Class ActivityTaskStatusesControllerTest.
    /// </summary>
    public class ActivityTaskStatusesControllerTest
    {
        /// <summary>
        /// The entities for test
        /// </summary>
        private readonly EntitiesForTest entitiesForTest;

        /// <summary>
        /// The repository mock
        /// </summary>
        private readonly Mock<IRepository<ActivityTaskStatus>> repositoryMock;

        /// <summary>
        /// The target
        /// </summary>
        private readonly ActivityTaskStatusesController target;

        /// <summary>
        /// The mapper mock
        /// </summary>
        private readonly Mock<IMapper> mapperMock;

        /// <summary>
        /// Initializes a new instance of the <see cref="ActivitiesControllerTest" /> class.
        /// </summary>
        public ActivityTaskStatusesControllerTest()
        {
            this.entitiesForTest = new EntitiesForTest();
            this.repositoryMock = new Mock<IRepository<ActivityTaskStatus>>();
            this.mapperMock = new Mock<IMapper>();

            this.target = new ActivityTaskStatusesController(
                this.repositoryMock.Object,
                this.mapperMock.Object);
        }

        #region GetActivityTaskStatuses

        /// <summary>
        /// Gets the activity tasks.
        /// </summary>
        [Fact]
        public void GetActivityTaskStatusesTest()
        {
            // arrange
            var entities = this.entitiesForTest.CreateCollection(4, this.entitiesForTest.CreateActivityTaskStatus);
            var models = new List<ActivityTaskStatusViewModel>();
            foreach (var entity in entities)
            {
                var model = this.entitiesForTest.CreateActivityTaskStatusViewModel();
                this.mapperMock.Setup(it => it.Map<ActivityTaskStatusViewModel>(entity)).Returns(model);
                models.Add(model);
            }

            var expectedResult = models.ToArray();
            var start = 2;
            var count = 3;
            this.repositoryMock.Setup(it => it.GetAll(start, count)).Returns(entities);

            // act 
            var result = this.target.GetActivityTaskStatuses(start, count);

            // assert
            result.ShouldBeEquivalentTo(expectedResult);
        }

        #endregion GetActivityTaskStatuses

        #region GetActivityTaskStatusesCount

        /// <summary>
        /// Gets the activity tasks count test.
        /// </summary>
        [Fact]
        public void GetActivityTaskStatusesCountTest()
        {
            // arrange
            var expectedResult = 42L;
            this.repositoryMock.Setup(it => it.Count()).Returns(expectedResult);

            // act 
            var result = this.target.GetActivityTaskStatusesCount();

            // assert
            result.ShouldBeEquivalentTo(expectedResult);
        }

        #endregion GetActivityTaskStatusesCount

        #region GetActivityTaskStatus

        /// <summary>
        /// Gets the activity task test invalid model.
        /// </summary>
        /// <returns>Task.</returns>
        [Fact]
        public async Task GetActivityTaskStatusTestInvalidModel()
        {
            // arrange
            var id = 3L;
            this.target.ModelState.AddModelError("someerrorkey", "someerrormessage");

            // act
            var result = await this.target.GetActivityTaskStatus(id) as BadRequestObjectResult;

            // assert
            result.Should().NotBeNull();
            result.Value.ShouldBeEquivalentTo(new SerializableError(this.target.ModelState));
        }

        /// <summary>
        /// Gets the activity task test not found.
        /// </summary>
        /// <returns>Task.</returns>
        [Fact]
        public async Task GetActivityTaskStatusTestNotFound()
        {
            // arrange
            var id = 3L;
            this.repositoryMock.Setup(it => it.FindByIdAsync(id)).ReturnsAsync(default(ActivityTaskStatus));

            // act
            var result = await this.target.GetActivityTaskStatus(id) as NotFoundResult;

            // assert
            result.Should().NotBeNull();
        }

        /// <summary>
        /// Gets the activity task test.
        /// </summary>
        /// <returns>Task.</returns>
        [Fact]
        public async Task GetActivityTaskStatusTest()
        {
            // arrange
            var id = 3L;
            var entity = this.entitiesForTest.CreateActivityTaskStatus();
            this.repositoryMock.Setup(it => it.FindByIdAsync(id)).ReturnsAsync(entity);


            var expectedResult = this.entitiesForTest.CreateActivityTaskStatusViewModel();
            this.mapperMock.Setup(it => it.Map<ActivityTaskStatusViewModel>(entity)).Returns(expectedResult);

            // act
            var result = await this.target.GetActivityTaskStatus(id) as OkObjectResult;

            // assert
            result.Should().NotBeNull();
            result.Value.ShouldBeEquivalentTo(expectedResult);
        }

        #endregion GetActivityTaskStatus

        #region PutActivityTaskStatus

        /// <summary>
        /// Puts the activity task test invalid model.
        /// </summary>
        /// <returns>Task.</returns>
        [Fact]
        public async Task PutActivityTaskStatusTestInvalidModel()
        {
            // arrange
            var id = 3L;
            this.target.ModelState.AddModelError("someerrorkey", "someerrormessage");
            var viewModel = this.entitiesForTest.CreateActivityTaskStatusViewModel();

            // act
            var result = await this.target.PutActivityTaskStatus(id, viewModel) as BadRequestObjectResult;

            // assert
            result.Should().NotBeNull();
            result.Value.ShouldBeEquivalentTo(new SerializableError(this.target.ModelState));
        }

        /// <summary>
        /// Puts the activity task test not found.
        /// </summary>
        /// <returns>Task.</returns>
        [Fact]
        public async Task PutActivityTaskStatusTestNotFound()
        {
            // arrange
            var id = 3L;
            var viewModel = this.entitiesForTest.CreateActivityTaskStatusViewModel();
            this.repositoryMock.Setup(it => it.FindByIdAsync(id)).ReturnsAsync(default(ActivityTaskStatus));

            // act
            var result = await this.target.PutActivityTaskStatus(id, viewModel) as NotFoundResult;

            // assert
            result.Should().NotBeNull();
        }

        /// <summary>
        /// Puts the activity task test.
        /// </summary>
        /// <returns>Task.</returns>
        [Fact]
        public async Task PutActivityTaskStatusTest()
        {
            // arrange
            var id = 3L;
            var entity = this.entitiesForTest.CreateActivityTaskStatus();
            var viewModel = this.entitiesForTest.CreateActivityTaskStatusViewModel();
            this.repositoryMock.Setup(it => it.FindByIdAsync(id)).ReturnsAsync(entity);

            // act
            var result = await this.target.PutActivityTaskStatus(id, viewModel) as NoContentResult;

            // assert
            result.Should().NotBeNull();
            this.mapperMock.Verify(it => it.Map(viewModel, entity));
            this.repositoryMock.Verify(it => it.UpdateAsync(entity));
        }

        #endregion PutActivityTaskStatus

        #region PostActivityTaskStatus

        /// <summary>
        /// Posts the activity task test invalid model.
        /// </summary>
        /// <returns>Task.</returns>
        [Fact]
        public async Task PostActivityTaskStatusTestInvalidModel()
        {
            // arrange
            this.target.ModelState.AddModelError("someerrorkey", "someerrormessage");
            var viewModel = this.entitiesForTest.CreateActivityTaskStatusViewModel();

            // act
            var result = await this.target.PostActivityTaskStatus(viewModel) as BadRequestObjectResult;

            // assert
            result.Should().NotBeNull();
            result.Value.ShouldBeEquivalentTo(new SerializableError(this.target.ModelState));
        }

        /// <summary>
        /// Posts the activity task test.
        /// </summary>
        /// <returns>Task.</returns>
        [Fact]
        public async Task PostActivityTaskStatusTest()
        {
            // arrange
            var model = this.entitiesForTest.CreateActivityTaskStatusViewModel();
            var entity = this.entitiesForTest.CreateActivityTaskStatus();
            this.mapperMock.Setup(it => it.Map<ActivityTaskStatus>(model)).Returns(entity);

            // act
            var result = await this.target.PostActivityTaskStatus(model) as CreatedAtActionResult;

            // assert
            result.Should().NotBeNull();
            result.ActionName.Should().Be("GetActivityTaskStatus");
            result.RouteValues["id"].Should().Be(entity.Id);
            this.repositoryMock.Verify(it => it.InsertAsync(entity));
        }

        #endregion PostActivityTaskStatus

        #region DeleteActivityTaskStatus

        /// <summary>
        /// Deletes the organization type invalid model.
        /// </summary>
        /// <returns>Task.</returns>
        [Fact]
        public async Task DeleteActivityTaskStatusTestInvalidModel()
        {
            // arrange
            var id = 3L;
            this.target.ModelState.AddModelError("someerrorkey", "someerrormessage");

            // act
            var result = await this.target.DeleteActivityTaskStatus(id) as BadRequestObjectResult;

            // assert
            result.Should().NotBeNull();
            result.Value.ShouldBeEquivalentTo(new SerializableError(this.target.ModelState));
        }

        /// <summary>
        /// Deletes the organization type test not found.
        /// </summary>
        /// <returns>Task.</returns>
        [Fact]
        public async Task DeleteActivityTaskStatusTestNotFound()
        {
            // arrange
            var id = 3L;
            this.repositoryMock.Setup(it => it.FindByIdAsync(id)).ReturnsAsync(default(ActivityTaskStatus));

            // act
            var result = await this.target.DeleteActivityTaskStatus(id) as NotFoundResult;

            // assert
            result.Should().NotBeNull();
        }

        /// <summary>
        /// Deletes the organization type test.
        /// </summary>
        /// <returns>Task.</returns>
        [Fact]
        public async Task DeleteActivityTaskStatusTest()
        {
            // arrange
            var id = 3L;
            var entity = this.entitiesForTest.CreateActivityTaskStatus();
            var model = this.entitiesForTest.CreateActivityTaskStatusViewModel();
            this.mapperMock.Setup(it => it.Map<ActivityTaskStatusViewModel>(entity)).Returns(model);
            this.repositoryMock.Setup(it => it.FindByIdAsync(id)).ReturnsAsync(entity);

            // act
            var result = await this.target.DeleteActivityTaskStatus(id) as OkObjectResult;

            // assert
            result.Should().NotBeNull();
            result.Value.ShouldBeEquivalentTo(model);
            this.repositoryMock.Verify(it => it.DeleteAsync(entity));
        }

        #endregion DeleteActivityTaskStatus
    }
}