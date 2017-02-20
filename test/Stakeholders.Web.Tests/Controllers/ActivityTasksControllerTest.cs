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
using System.Linq;
using System.Threading.Tasks;
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
        /// The repository user mock
        /// </summary>
        private readonly Mock<IRepository<ApplicationUser>> repositoryUserMock;

        /// <summary>
        /// The repository contact mock
        /// </summary>
        private readonly Mock<IRepository<Contact>> repositoryContactMock;

        /// <summary>
        /// The repository goal mock
        /// </summary>
        private readonly Mock<IRepository<Goal>> repositoryGoalMock;

        /// <summary>
        /// The repository activity task status mock
        /// </summary>
        private readonly Mock<IRepository<ActivityTaskStatus>> repositoryActivityTaskStatusMock;

        /// <summary>
        /// Initializes a new instance of the <see cref="ActivitiesControllerTest" /> class.
        /// </summary>
        public ActivityTasksControllerTest()
        {
            this.entitiesForTest = new EntitiesForTest();
            this.repositoryMock = new Mock<IRepository<ActivityTask>>();
            this.repositoryUserMock = new Mock<IRepository<ApplicationUser>>();
            this.repositoryContactMock = new Mock<IRepository<Contact>>();
            this.repositoryGoalMock = new Mock<IRepository<Goal>>();
            this.repositoryActivityTaskStatusMock = new Mock<IRepository<ActivityTaskStatus>>();

            this.target = new ActivityTasksController(
                this.repositoryMock.Object,
                this.repositoryUserMock.Object,
                this.repositoryContactMock.Object,
                this.repositoryGoalMock.Object,
                this.repositoryActivityTaskStatusMock.Object);
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
            var expectedResult = entities.Select(ActivityTasksControllerTest.ToInfoViewModel).ToArray();
            var start = 2;
            var count = 3;
            this.repositoryMock.Setup(it => it.GetAll(start, count)).Returns(entities);

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

            var expectedResult = ActivityTasksControllerTest.ToViewModel(entity);

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
            var newContact = this.entitiesForTest.CreateContact();
            viewModel.ContactIds = new[] {newContact.Id};

            var newAssignToUser = this.entitiesForTest.CreateApplicationUser();
            viewModel.AssignToId = newAssignToUser.Id;

            var newCreatedByUser = this.entitiesForTest.CreateApplicationUser();
            viewModel.CreatedById = newCreatedByUser.Id;

            var newObserverUser = this.entitiesForTest.CreateApplicationUser();
            viewModel.ObserverUserIds = new[] {newObserverUser.Id};

            var newGoal = this.entitiesForTest.CreateGoal();
            viewModel.GoalId = newGoal.Id;

            var newStatus = this.entitiesForTest.CreateActivityTaskStatus();
            viewModel.StatusId = newStatus.Id;

            this.repositoryContactMock.Setup(it => it.FindById(newContact.Id)).Returns(newContact);
            this.repositoryUserMock.Setup(it => it.FindById(newAssignToUser.Id)).Returns(newAssignToUser);
            this.repositoryUserMock.Setup(it => it.FindById(newCreatedByUser.Id)).Returns(newCreatedByUser);
            this.repositoryUserMock.Setup(it => it.FindById(newObserverUser.Id)).Returns(newObserverUser);
            this.repositoryGoalMock.Setup(it => it.FindById(newGoal.Id)).Returns(newGoal);
            this.repositoryActivityTaskStatusMock.Setup(it => it.FindById(newStatus.Id)).Returns(newStatus);
            this.repositoryMock.Setup(it => it.FindByIdAsync(id)).ReturnsAsync(entity);

            viewModel.DateCreated = entity.DateCreated;

            // act
            var result = await this.target.PutActivityTask(id, viewModel) as NoContentResult;

            // assert
            result.Should().NotBeNull();
            ActivityTasksControllerTest.ToViewModel(entity).ShouldBeEquivalentTo(viewModel);
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
            var id = 3L;

            var viewModel = this.entitiesForTest.CreateActivityTaskViewModel();
            var newContact = this.entitiesForTest.CreateContact();
            viewModel.ContactIds = new[] {newContact.Id};

            var newAssignToUser = this.entitiesForTest.CreateApplicationUser();
            viewModel.AssignToId = newAssignToUser.Id;

            var newCreatedByUser = this.entitiesForTest.CreateApplicationUser();
            viewModel.CreatedById = newCreatedByUser.Id;

            var newObserverUser = this.entitiesForTest.CreateApplicationUser();
            viewModel.ObserverUserIds = new[] {newObserverUser.Id};

            var newGoal = this.entitiesForTest.CreateGoal();
            viewModel.GoalId = newGoal.Id;

            var newStatus = this.entitiesForTest.CreateActivityTaskStatus();
            viewModel.StatusId = newStatus.Id;

            this.repositoryContactMock.Setup(it => it.FindById(newContact.Id)).Returns(newContact);
            this.repositoryUserMock.Setup(it => it.FindById(newAssignToUser.Id)).Returns(newAssignToUser);
            this.repositoryUserMock.Setup(it => it.FindById(newCreatedByUser.Id)).Returns(newCreatedByUser);
            this.repositoryUserMock.Setup(it => it.FindById(newObserverUser.Id)).Returns(newObserverUser);
            this.repositoryGoalMock.Setup(it => it.FindById(newGoal.Id)).Returns(newGoal);
            this.repositoryActivityTaskStatusMock.Setup(it => it.FindById(newStatus.Id)).Returns(newStatus);

            this.repositoryMock.Setup(it => it.InsertAsync(It.IsAny<ActivityTask>()))
                .Returns<ActivityTask>(
                    entity =>
                        Task.Run(
                            () =>
                            {
                                entity.Id = id;
                                entity.DateCreated = viewModel.DateCreated ?? DateTime.UtcNow;
                                ToViewModel(entity).ShouldBeEquivalentTo(viewModel);
                            }));

            // act
            var result = await this.target.PostActivityTask(viewModel) as CreatedAtActionResult;

            // assert
            result.Should().NotBeNull();
            result.ActionName.Should().Be("GetActivityTask");
            result.RouteValues["id"].Should().Be(id);
            this.repositoryMock.Verify(it => it.InsertAsync(It.IsAny<ActivityTask>()));
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
            var expectedResultViewModel = ActivityTasksControllerTest.ToViewModel(entity);
            ActivityTasksControllerTest.UpdateViewModel(expectedResultViewModel, entity);
            this.repositoryMock.Setup(it => it.FindByIdAsync(id)).ReturnsAsync(entity);

            // act
            var result = await this.target.DeleteActivityTask(id) as OkObjectResult;

            // assert
            result.Should().NotBeNull();
            result.Value.ShouldBeEquivalentTo(expectedResultViewModel);
            this.repositoryMock.Verify(it => it.DeleteAsync(entity));
        }

        #endregion DeleteActivityTask

        /// <summary>
        /// To the information view model.
        /// </summary>
        /// <param name="entity">The entity.</param>
        /// <returns>ActivityTaskInfoViewModel.</returns>
        private static ActivityTaskInfoViewModel ToInfoViewModel(ActivityTask entity)
        {
            var result = new ActivityTaskInfoViewModel()
            {
                Id = entity.Id
            };
            ActivityTasksControllerTest.UpdateViewModel(result, entity);
            return result;
        }

        /// <summary>
        /// To the view model.
        /// </summary>
        /// <param name="entity">The entity.</param>
        /// <returns>ActivityTaskViewModel.</returns>
        private static ActivityTaskViewModel ToViewModel(ActivityTask entity)
        {
            var result = new ActivityTaskViewModel();
            ActivityTasksControllerTest.UpdateViewModel(result, entity);
            return result;
        }

        /// <summary>
        /// Updates the view model.
        /// </summary>
        /// <param name="result">The result.</param>
        /// <param name="entity">The entity.</param>
        private static void UpdateViewModel(ActivityTaskViewModel model, ActivityTask entity)
        {
            model.DateCreated = entity.DateCreated.ToNullable();
            model.AssignToId = entity.AssignTo?.Id;
            model.ContactIds = entity.Contacts?.Select(it => it.Contact.Id).ToArray();
            model.CreatedById = entity.CreatedBy?.Id;
            model.GoalId = entity.Goal?.Id;
            model.ObserverUserIds = entity.ObserverUsers.Select(it => it.User.Id).ToArray();
            model.Subject = entity.Subject;
            model.DateDeadline = entity.DateDeadline;
            model.DateEnd = entity.DateEnd;
            model.Description = entity.Description;
            model.IsImportant = entity.IsImportant;
            model.StatusId = entity.Status?.Id;
            model.SuccessFactor = entity.SuccessFactor;
        }
    }
}