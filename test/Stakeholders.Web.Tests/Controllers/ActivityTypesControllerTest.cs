// ***********************************************************************
// Assembly         : Stakeholders.Web.Tests
// Author           : George
// Created          : 02-20-2017
//
// Last Modified By : George
// Last Modified On : 02-20-2017
// ***********************************************************************
// <copyright file="ActivityTypesControllerTest.cs" company="">
//     Copyright (c) . All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************
using System.Linq;
using System.Threading.Tasks;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using Moq;
using Stakeholders.Web.Controllers;
using Stakeholders.Web.Data;
using Stakeholders.Web.Models;
using Stakeholders.Web.Models.ActivityTypeViewModels;
using Xunit;

namespace Stakeholders.Web.Tests.Controllers
{
    /// <summary>
    /// Class ActivityTypesControllerTest.
    /// </summary>
    public class ActivityTypesControllerTest
    {
        /// <summary>
        /// The entities for test
        /// </summary>
        private readonly EntitiesForTest entitiesForTest;

        /// <summary>
        /// The repository mock
        /// </summary>
        private readonly Mock<IRepository<ActivityType>> repositoryMock;

        /// <summary>
        /// The target
        /// </summary>
        private readonly ActivityTypesController target;

        /// <summary>
        /// Initializes a new instance of the <see cref="ActivitiesControllerTest" /> class.
        /// </summary>
        public ActivityTypesControllerTest()
        {
            this.entitiesForTest = new EntitiesForTest();
            this.repositoryMock = new Mock<IRepository<ActivityType>>();

            this.target = new ActivityTypesController(
                this.repositoryMock.Object);
        }

        #region GetActivityTypes

        /// <summary>
        /// Gets the activity types.
        /// </summary>
        [Fact]
        public void GetActivityTypesTest()
        {
            // arrange
            var entities = this.entitiesForTest.CreateCollection(4, this.entitiesForTest.CreateActivityType);
            var expectedResult = entities.Select(ActivityTypesControllerTest.ToInfoViewModel).ToArray();
            var start = 2;
            var count = 3;
            this.repositoryMock.Setup(it => it.GetAll(start, count)).Returns(entities);

            // act 
            var result = this.target.GetActivityTypes(start, count);

            // assert
            result.ShouldBeEquivalentTo(expectedResult);
        }

        #endregion GetActivityTypes

        #region GetActivityTypesCount

        /// <summary>
        /// Gets the activity types count test.
        /// </summary>
        [Fact]
        public void GetActivityTypesCountTest()
        {
            // arrange
            var expectedResult = 42L;
            this.repositoryMock.Setup(it => it.Count()).Returns(expectedResult);

            // act 
            var result = this.target.GetActivityTypesCount();

            // assert
            result.ShouldBeEquivalentTo(expectedResult);
        }

        #endregion GetActivityTypesCount

        #region GetActivityType

        /// <summary>
        /// Gets the activity type test invalid model.
        /// </summary>
        /// <returns>Task.</returns>
        [Fact]
        public async Task GetActivityTypeTestInvalidModel()
        {
            // arrange
            var id = 3L;
            this.target.ModelState.AddModelError("someerrorkey", "someerrormessage");

            // act
            var result = await this.target.GetActivityType(id) as BadRequestObjectResult;

            // assert
            result.Should().NotBeNull();
            result.Value.ShouldBeEquivalentTo(new SerializableError(this.target.ModelState));
        }

        /// <summary>
        /// Gets the activity type test not found.
        /// </summary>
        /// <returns>Task.</returns>
        [Fact]
        public async Task GetActivityTypeTestNotFound()
        {
            // arrange
            var id = 3L;
            this.repositoryMock.Setup(it => it.FindByIdAsync(id)).ReturnsAsync(default(ActivityType));

            // act
            var result = await this.target.GetActivityType(id) as NotFoundResult;

            // assert
            result.Should().NotBeNull();
        }

        /// <summary>
        /// Gets the activity type test.
        /// </summary>
        /// <returns>Task.</returns>
        [Fact]
        public async Task GetActivityTypeTest()
        {
            // arrange
            var id = 3L;
            var entity = this.entitiesForTest.CreateActivityType();
            this.repositoryMock.Setup(it => it.FindByIdAsync(id)).ReturnsAsync(entity);

            var expectedResult = ActivityTypesControllerTest.ToViewModel(entity);

            // act
            var result = await this.target.GetActivityType(id) as OkObjectResult;

            // assert
            result.Should().NotBeNull();
            result.Value.ShouldBeEquivalentTo(expectedResult);
        }

        #endregion GetActivityType

        #region PutActivityType

        /// <summary>
        /// Puts the activity type test invalid model.
        /// </summary>
        /// <returns>Task.</returns>
        [Fact]
        public async Task PutActivityTypeTestInvalidModel()
        {
            // arrange
            var id = 3L;
            this.target.ModelState.AddModelError("someerrorkey", "someerrormessage");
            var viewModel = this.entitiesForTest.CreateActivityTypeViewModel();

            // act
            var result = await this.target.PutActivityType(id, viewModel) as BadRequestObjectResult;

            // assert
            result.Should().NotBeNull();
            result.Value.ShouldBeEquivalentTo(new SerializableError(this.target.ModelState));
        }

        /// <summary>
        /// Puts the activity type test not found.
        /// </summary>
        /// <returns>Task.</returns>
        [Fact]
        public async Task PutActivityTypeTestNotFound()
        {
            // arrange
            var id = 3L;
            var viewModel = this.entitiesForTest.CreateActivityTypeViewModel();
            this.repositoryMock.Setup(it => it.FindByIdAsync(id)).ReturnsAsync(default(ActivityType));

            // act
            var result = await this.target.PutActivityType(id, viewModel) as NotFoundResult;

            // assert
            result.Should().NotBeNull();
        }

        /// <summary>
        /// Puts the activity type test.
        /// </summary>
        /// <returns>Task.</returns>
        [Fact]
        public async Task PutActivityTypeTest()
        {
            // arrange
            var id = 3L;
            var entity = this.entitiesForTest.CreateActivityType();
            var viewModel = this.entitiesForTest.CreateActivityTypeViewModel();
            this.repositoryMock.Setup(it => it.FindByIdAsync(id)).ReturnsAsync(entity);

            // act
            var result = await this.target.PutActivityType(id, viewModel) as NoContentResult;

            // assert
            result.Should().NotBeNull();
            ActivityTypesControllerTest.ToViewModel(entity).ShouldBeEquivalentTo(viewModel);
            this.repositoryMock.Verify(it => it.UpdateAsync(entity));
        }

        #endregion PutActivityType

        #region PostActivityType

        /// <summary>
        /// Posts the activity type test invalid model.
        /// </summary>
        /// <returns>Task.</returns>
        [Fact]
        public async Task PostActivityTypeTestInvalidModel()
        {
            // arrange
            this.target.ModelState.AddModelError("someerrorkey", "someerrormessage");
            var viewModel = this.entitiesForTest.CreateActivityTypeViewModel();

            // act
            var result = await this.target.PostActivityType(viewModel) as BadRequestObjectResult;

            // assert
            result.Should().NotBeNull();
            result.Value.ShouldBeEquivalentTo(new SerializableError(this.target.ModelState));
        }

        /// <summary>
        /// Posts the activity type test.
        /// </summary>
        /// <returns>Task.</returns>
        [Fact]
        public async Task PostActivityTypeTest()
        {
            // arrange
            var id = 3L;

            var viewModel = this.entitiesForTest.CreateActivityTypeViewModel();

            this.repositoryMock.Setup(it => it.InsertAsync(It.IsAny<ActivityType>()))
                .Returns<ActivityType>(
                    entity =>
                        Task.Run(
                            () =>
                            {
                                entity.Id = id;
                                ActivityTypesControllerTest.ToViewModel(entity).ShouldBeEquivalentTo(viewModel);
                            }));

            // act
            var result = await this.target.PostActivityType(viewModel) as CreatedAtActionResult;

            // assert
            result.Should().NotBeNull();
            result.ActionName.Should().Be("GetActivityType");
            result.RouteValues["id"].Should().Be(id);
            this.repositoryMock.Verify(it => it.InsertAsync(It.IsAny<ActivityType>()));
        }

        #endregion PostActivityType

        #region DeleteActivityType

        /// <summary>
        /// Deletes the organization type invalid model.
        /// </summary>
        /// <returns>Task.</returns>
        [Fact]
        public async Task DeleteActivityTypeTestInvalidModel()
        {
            // arrange
            var id = 3L;
            this.target.ModelState.AddModelError("someerrorkey", "someerrormessage");

            // act
            var result = await this.target.DeleteActivityType(id) as BadRequestObjectResult;

            // assert
            result.Should().NotBeNull();
            result.Value.ShouldBeEquivalentTo(new SerializableError(this.target.ModelState));
        }

        /// <summary>
        /// Deletes the organization type test not found.
        /// </summary>
        /// <returns>Task.</returns>
        [Fact]
        public async Task DeleteActivityTypeTestNotFound()
        {
            // arrange
            var id = 3L;
            this.repositoryMock.Setup(it => it.FindByIdAsync(id)).ReturnsAsync(default(ActivityType));

            // act
            var result = await this.target.DeleteActivityType(id) as NotFoundResult;

            // assert
            result.Should().NotBeNull();
        }

        /// <summary>
        /// Deletes the organization type test.
        /// </summary>
        /// <returns>Task.</returns>
        [Fact]
        public async Task DeleteActivityTypeTest()
        {
            // arrange
            var id = 3L;
            var entity = this.entitiesForTest.CreateActivityType();
            var expectedResultViewModel = ActivityTypesControllerTest.ToViewModel(entity);
            ActivityTypesControllerTest.UpdateViewModel(expectedResultViewModel, entity);
            this.repositoryMock.Setup(it => it.FindByIdAsync(id)).ReturnsAsync(entity);

            // act
            var result = await this.target.DeleteActivityType(id) as OkObjectResult;

            // assert
            result.Should().NotBeNull();
            result.Value.ShouldBeEquivalentTo(expectedResultViewModel);
            this.repositoryMock.Verify(it => it.DeleteAsync(entity));
        }

        #endregion DeleteActivityType

        /// <summary>
        /// To the information view model.
        /// </summary>
        /// <param name="entity">The entity.</param>
        /// <returns>ActivityTypeInfoViewModel.</returns>
        private static ActivityTypeInfoViewModel ToInfoViewModel(ActivityType entity)
        {
            var result = new ActivityTypeInfoViewModel()
            {
                Id = entity.Id
            };
            ActivityTypesControllerTest.UpdateViewModel(result, entity);
            return result;
        }

        /// <summary>
        /// To the view model.
        /// </summary>
        /// <param name="entity">The entity.</param>
        /// <returns>ActivityTypeViewModel.</returns>
        private static ActivityTypeViewModel ToViewModel(ActivityType entity)
        {
            var result = new ActivityTypeViewModel();
            ActivityTypesControllerTest.UpdateViewModel(result, entity);
            return result;
        }

        /// <summary>
        /// Updates the view model.
        /// </summary>
        /// <param name="result">The result.</param>
        /// <param name="entity">The entity.</param>
        private static void UpdateViewModel(ActivityTypeViewModel model, ActivityType entity)
        {
            model.Name = entity.Name;
        }
    }
}