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
using System.Linq;
using System.Threading.Tasks;
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
        /// The repository task mock
        /// </summary>
        private readonly Mock<IRepository<ActivityTask>> repositoryTaskMock;

        /// <summary>
        /// The repository company mock
        /// </summary>
        private readonly Mock<IRepository<Company>> repositoryCompanyMock;

        /// <summary>
        /// The repository user mock
        /// </summary>
        private readonly Mock<IRepository<ApplicationUser>> repositoryUserMock;

        /// <summary>
        /// The repository activity type mock
        /// </summary>
        private readonly Mock<IRepository<ActivityType>> repositoryActivityTypeMock;

        /// <summary>
        /// The repository activity type mock
        /// </summary>
        private readonly Mock<IRepository<Contact>> repositoryContactMock;

        /// <summary>
        /// The target
        /// </summary>
        private readonly ActivitiesController target;

        /// <summary>
        /// Initializes a new instance of the <see cref="ActivitiesControllerTest" /> class.
        /// </summary>
        public ActivitiesControllerTest()
        {
            this.entitiesForTest = new EntitiesForTest();
            this.repositoryMock = new Mock<IRepository<Activity>>();
            this.repositoryTaskMock = new Mock<IRepository<ActivityTask>>();
            this.repositoryCompanyMock = new Mock<IRepository<Company>>();
            this.repositoryUserMock = new Mock<IRepository<ApplicationUser>>();
            this.repositoryActivityTypeMock = new Mock<IRepository<ActivityType>>();
            this.repositoryContactMock = new Mock<IRepository<Contact>>();

            this.target = new ActivitiesController(
                this.repositoryMock.Object,
                this.repositoryTaskMock.Object,
                this.repositoryCompanyMock.Object,
                this.repositoryUserMock.Object,
                this.repositoryActivityTypeMock.Object,
                this.repositoryContactMock.Object);
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
            var expectedResult = entities.Select(
                activity =>
                {
                    var r= ActivitiesControllerTest.ToViewModel(activity);
                    r.Id = activity.Id;
                    return r;
                }).ToArray();
            var start = 2;
            var count = 3;
            this.repositoryMock.Setup(it => it.GetAll(start, count)).Returns(entities);

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
        /// Gets the organization type test invalid model.
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
        /// Gets the organization type test not found.
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
        /// Gets the organization type test.
        /// </summary>
        /// <returns>Task.</returns>
        [Fact]
        public async Task GetActivityTest()
        {
            // arrange
            var id = 3L;
            var entity = this.entitiesForTest.CreateActivity();
            this.repositoryMock.Setup(it => it.FindByIdAsync(id)).ReturnsAsync(entity);

            var expectedResult = ActivitiesControllerTest.ToViewModel(entity);

            // act
            var result = await this.target.GetActivity(id) as OkObjectResult;

            // assert
            result.Should().NotBeNull();
            result.Value.ShouldBeEquivalentTo(expectedResult);
        }

        #endregion GetActivity

        #region PutOrganizationType

        /// <summary>
        /// Puts the organization type test invalid model.
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
        /// Puts the organization type test not found.
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
        /// Puts the organization type test.
        /// </summary>
        /// <returns>Task.</returns>
        [Fact]
        public async Task PutActivityTest()
        {
            // arrange
            var id = 3L;
            var entity = this.entitiesForTest.CreateActivity();
            var viewModel = this.entitiesForTest.CreateActivityViewModel();
            var newContact = this.entitiesForTest.CreateContact();
            viewModel.ContactId = newContact.Id;
            var newCompany = this.entitiesForTest.CreateCompany();
            viewModel.CompanyId = newCompany.Id;
            var newUser = this.entitiesForTest.CreateApplicationUser();
            viewModel.UserId = newUser.Id;
            var newType = this.entitiesForTest.CreateActivityType();
            viewModel.TypeId = newType.Id;
            var newTask = this.entitiesForTest.CreateActivityTask();
            viewModel.TaskId = newTask.Id;
            var newObserverUser = this.entitiesForTest.CreateApplicationUser();
            viewModel.ObserverUserIds = new[] {newObserverUser.Id};
            var newObserverCompany = this.entitiesForTest.CreateCompany();
            viewModel.ObserverCompanyIds = new[] {newObserverCompany.Id};
            viewModel.DateCreated = entity.DateCreated;

            this.repositoryContactMock.Setup(it => it.FindById(newContact.Id)).Returns(newContact);
            this.repositoryCompanyMock.Setup(it => it.FindById(newCompany.Id)).Returns(newCompany);
            this.repositoryCompanyMock.Setup(it => it.FindById(newObserverCompany.Id)).Returns(newObserverCompany);
            this.repositoryUserMock.Setup(it => it.FindById(newUser.Id)).Returns(newUser);
            this.repositoryUserMock.Setup(it => it.FindById(newObserverUser.Id)).Returns(newObserverUser);
            this.repositoryActivityTypeMock.Setup(it => it.FindById(newType.Id)).Returns(newType);
            this.repositoryTaskMock.Setup(it => it.FindById(newTask.Id)).Returns(newTask);
            this.repositoryMock.Setup(it => it.FindByIdAsync(id)).ReturnsAsync(entity);

            // act
            var result = await this.target.PutActivity(id, viewModel) as NoContentResult;

            // assert
            result.Should().NotBeNull();
            ActivitiesControllerTest.ToViewModel(entity).ShouldBeEquivalentTo(viewModel, options=>options.Excluding(it=>it.Id));
            this.repositoryMock.Verify(it => it.UpdateAsync(entity));
        }

        #endregion PutActivity

        #region PostActivity

        /// <summary>
        /// Posts the organization type type test invalid model.
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
        /// Posts the organization type type test.
        /// </summary>
        /// <returns>Task.</returns>
        [Fact]
        public async Task PostActivityTest()
        {
            // arrange
            var id = 3L;
            var viewModel = this.entitiesForTest.CreateActivityViewModel();
            var newContact = this.entitiesForTest.CreateContact();
            viewModel.ContactId = newContact.Id;
            var newCompany = this.entitiesForTest.CreateCompany();
            viewModel.CompanyId = newCompany.Id;
            var newUser = this.entitiesForTest.CreateApplicationUser();
            viewModel.UserId = newUser.Id;
            var newType = this.entitiesForTest.CreateActivityType();
            viewModel.TypeId = newType.Id;
            var newTask = this.entitiesForTest.CreateActivityTask();
            viewModel.TaskId = newTask.Id;
            var newObserverUser = this.entitiesForTest.CreateApplicationUser();
            viewModel.ObserverUserIds = new[] { newObserverUser.Id };
            var newObserverCompany = this.entitiesForTest.CreateCompany();
            viewModel.ObserverCompanyIds = new[] { newObserverCompany.Id };

            this.repositoryContactMock.Setup(it => it.FindById(newContact.Id)).Returns(newContact);
            this.repositoryCompanyMock.Setup(it => it.FindById(newCompany.Id)).Returns(newCompany);
            this.repositoryCompanyMock.Setup(it => it.FindById(newObserverCompany.Id)).Returns(newObserverCompany);
            this.repositoryUserMock.Setup(it => it.FindById(newUser.Id)).Returns(newUser);
            this.repositoryUserMock.Setup(it => it.FindById(newObserverUser.Id)).Returns(newObserverUser);
            this.repositoryActivityTypeMock.Setup(it => it.FindById(newType.Id)).Returns(newType);
            this.repositoryTaskMock.Setup(it => it.FindById(newTask.Id)).Returns(newTask);

            this.repositoryMock.Setup(it => it.InsertAsync(It.IsAny<Activity>()))
                .Returns<Activity>(
                    entity =>
                        Task.Run(
                            () =>
                            {
                                entity.Id = id;
                                ToViewModel(entity).ShouldBeEquivalentTo(viewModel,options=>options.Excluding(it=>it.Id).Excluding(it=>it.DateCreated));
                            }));

            // act
            var result = await this.target.PostActivity(viewModel) as CreatedAtActionResult;

            // assert
            result.Should().NotBeNull();
            result.ActionName.Should().Be("GetActivity");
            result.RouteValues["id"].Should().Be(id);
            this.repositoryMock.Verify(it => it.InsertAsync(It.IsAny<Activity>()));
        }

        #endregion PostActivity

        #region DeleteActivity

        /// <summary>
        /// Deletes the organization type invalid model.
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
        /// Deletes the organization type test not found.
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
        /// Deletes the organization type test.
        /// </summary>
        /// <returns>Task.</returns>
        [Fact]
        public async Task DeleteActivityTest()
        {
            // arrange
            var id = 3L;
            var entity = this.entitiesForTest.CreateActivity();
            var expectedResultViewModel = ActivitiesControllerTest.ToViewModel(entity);
            ActivitiesControllerTest.UpdateViewModel(expectedResultViewModel, entity);
            this.repositoryMock.Setup(it => it.FindByIdAsync(id)).ReturnsAsync(entity);

            // act
            var result = await this.target.DeleteActivity(id) as OkObjectResult;

            // assert
            result.Should().NotBeNull();
            result.Value.ShouldBeEquivalentTo(expectedResultViewModel);
            this.repositoryMock.Verify(it => it.DeleteAsync(entity));
        }

        #endregion DeleteActivity

        /// <summary>
        /// To the view model.
        /// </summary>
        /// <param name="entity">The entity.</param>
        /// <returns>ActivityViewModel.</returns>
        private static ActivityViewModel ToViewModel(Activity entity)
        {
            var result = new ActivityViewModel();
            ActivitiesControllerTest.UpdateViewModel(result, entity);
            return result;
        }

        /// <summary>
        /// Updates the view model.
        /// </summary>
        /// <param name="result">The result.</param>
        /// <param name="entity">The entity.</param>
        private static void UpdateViewModel(ActivityViewModel result, Activity entity)
        {
            result.TaskId = entity.Task?.Id;
            result.CompanyId = entity.Company?.Id;
            result.ContactId = entity.Contact?.Id;
            result.DateActivity = entity.DateActivity;
            result.DateCreated = entity.DateCreated.ToNullable();
            result.Description = entity.Description;
            result.ObserverCompanyIds =
                entity.ObserverUsersCompanies?.Where(it => it.Company != null).Select(it => it.Company.Id).ToArray();
            result.ObserverUserIds =
                entity.ObserverUsersCompanies?.Where(it => it.User != null).Select(it => it.User.Id).ToArray();
            result.Subject = entity.Subject;
            result.TypeId = entity.Type?.Id;
            result.UserId = entity.User?.Id;
        }
    }
}