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
using System;
using System.Linq;
using System.Threading.Tasks;
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
        /// Initializes a new instance of the <see cref="ActivitiesControllerTest" /> class.
        /// </summary>
        public ActivityTaskStatusesControllerTest()
        {
            this.entitiesForTest = new EntitiesForTest();
            this.repositoryMock = new Mock<IRepository<ActivityTaskStatus>>();

            this.target = new ActivityTaskStatusesController(
                this.repositoryMock.Object);
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
            var expectedResult = entities.Select(ActivityTaskStatusesControllerTest.ToInfoViewModel).ToArray();
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

            var expectedResult = ActivityTaskStatusesControllerTest.ToViewModel(entity);

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
            ActivityTaskStatusesControllerTest.ToViewModel(entity).ShouldBeEquivalentTo(viewModel);
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
            var id = 3L;

            var viewModel = this.entitiesForTest.CreateActivityTaskStatusViewModel();

            this.repositoryMock.Setup(it => it.InsertAsync(It.IsAny<ActivityTaskStatus>()))
                .Returns<ActivityTaskStatus>(
                    entity =>
                        Task.Run(
                            () =>
                            {
                                entity.Id = id;
                                ToViewModel(entity).ShouldBeEquivalentTo(viewModel);
                            }));

            // act
            var result = await this.target.PostActivityTaskStatus(viewModel) as CreatedAtActionResult;

            // assert
            result.Should().NotBeNull();
            result.ActionName.Should().Be("GetActivityTaskStatus");
            result.RouteValues["id"].Should().Be(id);
            this.repositoryMock.Verify(it => it.InsertAsync(It.IsAny<ActivityTaskStatus>()));
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
            var expectedResultViewModel = ActivityTaskStatusesControllerTest.ToViewModel(entity);
            ActivityTaskStatusesControllerTest.UpdateViewModel(expectedResultViewModel, entity);
            this.repositoryMock.Setup(it => it.FindByIdAsync(id)).ReturnsAsync(entity);

            // act
            var result = await this.target.DeleteActivityTaskStatus(id) as OkObjectResult;

            // assert
            result.Should().NotBeNull();
            result.Value.ShouldBeEquivalentTo(expectedResultViewModel);
            this.repositoryMock.Verify(it => it.DeleteAsync(entity));
        }

        #endregion DeleteActivityTaskStatus

        /// <summary>
        /// To the information view model.
        /// </summary>
        /// <param name="entity">The entity.</param>
        /// <returns>ActivityTaskStatusInfoViewModel.</returns>
        private static ActivityTaskStatusInfoViewModel ToInfoViewModel(ActivityTaskStatus entity)
        {
            var result = new ActivityTaskStatusInfoViewModel()
            {
                Id = entity.Id
            };
            ActivityTaskStatusesControllerTest.UpdateViewModel(result, entity);
            return result;
        }

        /// <summary>
        /// To the view model.
        /// </summary>
        /// <param name="entity">The entity.</param>
        /// <returns>ActivityTaskStatusViewModel.</returns>
        private static ActivityTaskStatusViewModel ToViewModel(ActivityTaskStatus entity)
        {
            var result = new ActivityTaskStatusViewModel();
            ActivityTaskStatusesControllerTest.UpdateViewModel(result, entity);
            return result;
        }

        /// <summary>
        /// Updates the view model.
        /// </summary>
        /// <param name="result">The result.</param>
        /// <param name="entity">The entity.</param>
        private static void UpdateViewModel(ActivityTaskStatusViewModel model, ActivityTaskStatus entity)
        {
            model.Name = entity.Name;
            model.NameEn = entity.NameEn;
        }
    }
}