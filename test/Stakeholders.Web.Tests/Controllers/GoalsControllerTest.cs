// ***********************************************************************
// Assembly         : Stakeholders.Web.Tests
// Author           : George
// Created          : 02-19-2017
//
// Last Modified By : George
// Last Modified On : 02-20-2017
// ***********************************************************************
// <copyright file="GoalsControllerTest.cs" company="">
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
using Stakeholders.Web.Models.GoalViewModels;
using Xunit;

namespace Stakeholders.Web.Tests.Controllers
{
    /// <summary>
    /// Class GoalsControllerTest.
    /// </summary>
    public class GoalsControllerTest
    {
        /// <summary>
        /// The entities for test
        /// </summary>
        private readonly EntitiesForTest entitiesForTest;

        /// <summary>
        /// The repository mock
        /// </summary>
        private readonly Mock<IRepository<Goal>> repositoryMock;

        /// <summary>
        /// The target
        /// </summary>
        private readonly GoalsController target;

        /// <summary>
        /// The mapper mock
        /// </summary>
        private readonly Mock<IMapper> mapperMock;

        /// <summary>
        /// Initializes a new instance of the <see cref="GoalsControllerTest" /> class.
        /// </summary>
        public GoalsControllerTest()
        {
            this.entitiesForTest = new EntitiesForTest();
            this.repositoryMock = new Mock<IRepository<Goal>>();
            this.mapperMock = new Mock<IMapper>();

            this.target = new GoalsController(
                this.repositoryMock.Object,
                this.mapperMock.Object);
        }

        #region GetGoals

        /// <summary>
        /// Gets the organization types test.
        /// </summary>
        [Fact]
        public void GetOrganizationTypesTest()
        {
            // arrange
            var entities = this.entitiesForTest.CreateCollection(4, this.entitiesForTest.CreateGoal);
            var models = new List<GoalViewModel>();
            foreach (var entity in entities)
            {
                var model = this.entitiesForTest.CreateGoalViewModel();
                this.mapperMock.Setup(it => it.Map<GoalViewModel>(entity)).Returns(model);
                models.Add(model);
            }

            var expectedResult = models.ToArray();
            var start = 2;
            var count = 3;
            this.repositoryMock.Setup(it => it.GetAll(start, count, It.IsAny<Func<Goal, bool>>())).Returns(entities);

            // act 
            var result = this.target.GetGoals(start, count);

            // assert
            result.ShouldBeEquivalentTo(expectedResult);
        }

        #endregion GetGoals

        #region GetGoalsCount

        /// <summary>
        /// Gets the organization types count test.
        /// </summary>
        [Fact]
        public void GetGoalsCountTest()
        {
            // arrange
            var expectedResult = 42L;
            this.repositoryMock.Setup(it => it.Count()).Returns(expectedResult);

            // act 
            var result = this.target.GetGoalsCount();

            // assert
            result.ShouldBeEquivalentTo(expectedResult);
        }

        #endregion GetGoalsCount

        #region GetGoal

        /// <summary>
        /// Gets the Goal test invalid model.
        /// </summary>
        /// <returns>Task.</returns>
        [Fact]
        public async Task GetGoalTestInvalidModel()
        {
            // arrange
            var id = 3L;
            this.target.ModelState.AddModelError("someerrorkey", "someerrormessage");

            // act
            var result = await this.target.GetGoal(id) as BadRequestObjectResult;

            // assert
            result.Should().NotBeNull();
            result.Value.ShouldBeEquivalentTo(new SerializableError(this.target.ModelState));
        }

        /// <summary>
        /// Gets the Goal test not found.
        /// </summary>
        /// <returns>Task.</returns>
        [Fact]
        public async Task GetGoalTestNotFound()
        {
            // arrange
            var id = 3L;
            this.repositoryMock.Setup(it => it.FindByIdAsync(id)).ReturnsAsync(default(Goal));

            // act
            var result = await this.target.GetGoal(id) as NotFoundResult;

            // assert
            result.Should().NotBeNull();
        }

        /// <summary>
        /// Gets the Goal test.
        /// </summary>
        /// <returns>Task.</returns>
        [Fact]
        public async Task GetGoalTest()
        {
            // arrange
            var id = 3L;
            var entity = this.entitiesForTest.CreateGoal();
            this.repositoryMock.Setup(it => it.FindByIdAsync(id)).ReturnsAsync(entity);


            var expectedResult = this.entitiesForTest.CreateGoalViewModel();
            this.mapperMock.Setup(it => it.Map<GoalViewModel>(entity)).Returns(expectedResult);

            // act
            var result = await this.target.GetGoal(id) as OkObjectResult;

            // assert
            result.Should().NotBeNull();
            result.Value.ShouldBeEquivalentTo(expectedResult);
        }

        #endregion GetGoal

        #region PutGoal

        /// <summary>
        /// Puts the Goal test invalid model.
        /// </summary>
        /// <returns>Task.</returns>
        [Fact]
        public async Task PutGoalTestInvalidModel()
        {
            // arrange
            var id = 3L;
            this.target.ModelState.AddModelError("someerrorkey", "someerrormessage");
            var viewModel = this.entitiesForTest.CreateGoalViewModel();

            // act
            var result = await this.target.PutGoal(id, viewModel) as BadRequestObjectResult;

            // assert
            result.Should().NotBeNull();
            result.Value.ShouldBeEquivalentTo(new SerializableError(this.target.ModelState));
        }

        /// <summary>
        /// Puts the Goal test not found.
        /// </summary>
        /// <returns>Task.</returns>
        [Fact]
        public async Task PutGoalTestNotFound()
        {
            // arrange
            var id = 3L;
            var viewModel = this.entitiesForTest.CreateGoalViewModel();
            this.repositoryMock.Setup(it => it.FindByIdAsync(id)).ReturnsAsync(default(Goal));

            // act
            var result = await this.target.PutGoal(id, viewModel) as NotFoundResult;

            // assert
            result.Should().NotBeNull();
        }

        /// <summary>
        /// Puts the Goal test.
        /// </summary>
        /// <returns>Task.</returns>
        [Fact]
        public async Task PutGoalTest()
        {
            // arrange
            var id = 3L;
            var entity = this.entitiesForTest.CreateGoal();
            var viewModel = this.entitiesForTest.CreateGoalViewModel();
            this.repositoryMock.Setup(it => it.FindByIdAsync(id)).ReturnsAsync(entity);

            // act
            var result = await this.target.PutGoal(id, viewModel) as NoContentResult;

            // assert
            result.Should().NotBeNull();
            this.mapperMock.Verify(it => it.Map(viewModel, entity));
            this.repositoryMock.Verify(it => it.UpdateAsync(entity));
        }

        #endregion PutGoal

        #region PostGoal

        /// <summary>
        /// Posts the Goaltype test invalid model.
        /// </summary>
        /// <returns>Task.</returns>
        [Fact]
        public async Task PostGoalTestInvalidModel()
        {
            // arrange
            this.target.ModelState.AddModelError("someerrorkey", "someerrormessage");
            var viewModel = this.entitiesForTest.CreateGoalViewModel();

            // act
            var result = await this.target.PostGoal(viewModel) as BadRequestObjectResult;

            // assert
            result.Should().NotBeNull();
            result.Value.ShouldBeEquivalentTo(new SerializableError(this.target.ModelState));
        }

        /// <summary>
        /// Posts the Goaltype test.
        /// </summary>
        /// <returns>Task.</returns>
        [Fact]
        public async Task PostGoalTest()
        {
            // arrange
            var model = this.entitiesForTest.CreateGoalViewModel();
            var entity = this.entitiesForTest.CreateGoal();
            this.mapperMock.Setup(it => it.Map<Goal>(model)).Returns(entity);

            // act
            var result = await this.target.PostGoal(model) as CreatedAtActionResult;

            // assert
            result.Should().NotBeNull();
            result.ActionName.Should().Be("GetGoal");
            result.RouteValues["id"].Should().Be(entity.Id);
            this.repositoryMock.Verify(it => it.InsertAsync(entity));
        }

        #endregion PostGoal

        #region DeleteGoal

        /// <summary>
        /// Deletes the Goalinvalid model.
        /// </summary>
        /// <returns>Task.</returns>
        [Fact]
        public async Task DeleteGoalTestInvalidModel()
        {
            // arrange
            var id = 3L;
            this.target.ModelState.AddModelError("someerrorkey", "someerrormessage");

            // act
            var result = await this.target.DeleteGoal(id) as BadRequestObjectResult;

            // assert
            result.Should().NotBeNull();
            result.Value.ShouldBeEquivalentTo(new SerializableError(this.target.ModelState));
        }

        /// <summary>
        /// Deletes the Goal test not found.
        /// </summary>
        /// <returns>Task.</returns>
        [Fact]
        public async Task DeleteGoalTestNotFound()
        {
            // arrange
            var id = 3L;
            this.repositoryMock.Setup(it => it.FindByIdAsync(id)).ReturnsAsync(default(Goal));

            // act
            var result = await this.target.DeleteGoal(id) as NotFoundResult;

            // assert
            result.Should().NotBeNull();
        }

        /// <summary>
        /// Deletes the Goal test.
        /// </summary>
        /// <returns>Task.</returns>
        [Fact]
        public async Task DeleteGoalTest()
        {
            // arrange
            var id = 3L;
            var entity = this.entitiesForTest.CreateGoal();
            var model = this.entitiesForTest.CreateGoalViewModel();
            this.mapperMock.Setup(it => it.Map<GoalViewModel>(entity)).Returns(model);
            this.repositoryMock.Setup(it => it.FindByIdAsync(id)).ReturnsAsync(entity);

            // act
            var result = await this.target.DeleteGoal(id) as OkObjectResult;

            // assert
            result.Should().NotBeNull();
            result.Value.ShouldBeEquivalentTo(model);
            this.repositoryMock.Verify(it => it.DeleteAsync(entity));
        }

        #endregion DeleteGoal
    }
}