// ***********************************************************************
// Assembly         : Stakeholders.Web.Tests
// Author           : George
// Created          : 02-19-2017
//
// Last Modified By : George
// Last Modified On : 02-19-2017
// ***********************************************************************
// <copyright file="OrganizationTypesControllerTest.cs" company="">
//     Copyright (c) . All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using Moq;
using Stakeholders.Web.Controllers;
using Stakeholders.Web.Data;
using Stakeholders.Web.Models;
using Stakeholders.Web.Models.OrganizationTypeViewModels;
using Xunit;

namespace Stakeholders.Web.Tests.Controllers
{
    /// <summary>
    /// Class OrganizationTypesControllerTest.
    /// </summary>
    public class OrganizationTypesControllerTest
    {
        /// <summary>
        /// The repository mock
        /// </summary>
        private readonly Mock<IRepository<OrganizationType>> repositoryMock;
        /// <summary>
        /// The target
        /// </summary>
        private readonly OrganizationTypesController target;

        /// <summary>
        /// Initializes a new instance of the <see cref="OrganizationTypesControllerTest"/> class.
        /// </summary>
        public OrganizationTypesControllerTest()
        {
            this.repositoryMock = new Mock<IRepository<OrganizationType>>();
            this.target = new OrganizationTypesController(this.repositoryMock.Object);
        }

        #region GetOrganizationTypes

        /// <summary>
        /// Gets the organization types test.
        /// </summary>
        [Fact]
        public void GetOrganizationTypesTest()
        {
            // arrange
            var entities = new List<OrganizationType>()
            {
                new OrganizationType()
                {
                    Id = 2,
                    Type = "organizationType1"
                },
                new OrganizationType()
                {
                    Id = 3,
                    Type = "organizationType2"
                }
            };
            var expectedResult = new GetOrganizationTypesViewModel()
            {
                OrganizationTypes = entities.Select(
                    it => new OrganizationTypeInfoViewModel()
                    {
                        Id = it.Id,
                        Name = it.Type
                    }).ToArray()
            };
            var start = 2;
            var count = 3;
            this.repositoryMock.Setup(it => it.GetAll(start, count)).Returns(entities);

            // act 
            var result = this.target.GetOrganizationTypes(start, count);

            // assert
            result.ShouldBeEquivalentTo(expectedResult);
        }

        #endregion GetOrganizationTypes

        #region GetOrganizationType

        /// <summary>
        /// Gets the organization type test invalid model.
        /// </summary>
        /// <returns>Task.</returns>
        [Fact]
        public async Task GetOrganizationTypeTestInvalidModel()
        {
            // arrange
            var id = 3;
            this.target.ModelState.AddModelError("someerrorkey", "someerrormessage");

            // act
            var result = await this.target.GetOrganizationType(id) as BadRequestObjectResult;

            // assert
            result.Should().NotBeNull();
            result.Value.ShouldBeEquivalentTo(new SerializableError(this.target.ModelState));
        }

        /// <summary>
        /// Gets the organization type test not found.
        /// </summary>
        /// <returns>Task.</returns>
        [Fact]
        public async Task GetOrganizationTypeTestNotFound()
        {
            // arrange
            var id = 3;
            this.repositoryMock.Setup(it => it.FindByIdAsync(id)).ReturnsAsync(default(OrganizationType));

            // act
            var result = await this.target.GetOrganizationType(id) as NotFoundResult;

            // assert
            result.Should().NotBeNull();
        }

        /// <summary>
        /// Gets the organization type test.
        /// </summary>
        /// <returns>Task.</returns>
        [Fact]
        public async Task GetOrganizationTypeTest()
        {
            // arrange
            var id = 3;
            var entity = new OrganizationType()
            {
                Id = id,
                Type = "some type"
            };
            this.repositoryMock.Setup(it => it.FindByIdAsync(id)).ReturnsAsync(entity);

            var expectedResult = new GetOrganizationTypeViewModel()
            {
                Name = entity.Type
            };

            // act
            var result = await this.target.GetOrganizationType(id) as OkObjectResult;

            // assert
            result.Should().NotBeNull();
            result.Value.ShouldBeEquivalentTo(expectedResult);
        }

        #endregion GetOrganizationType

        #region PutOrganizationType

        /// <summary>
        /// Puts the organization type test invalid model.
        /// </summary>
        /// <returns>Task.</returns>
        [Fact]
        public async Task PutOrganizationTypeTestInvalidModel()
        {
            // arrange
            var id = 3;
            this.target.ModelState.AddModelError("someerrorkey", "someerrormessage");
            var viewModel = new PutOrganizationTypeViewModel()
            {
                Name = "someName"
            };

            // act
            var result = await this.target.PutOrganizationType(id, viewModel) as BadRequestObjectResult;

            // assert
            result.Should().NotBeNull();
            result.Value.ShouldBeEquivalentTo(new SerializableError(this.target.ModelState));
        }

        /// <summary>
        /// Puts the organization type test not found.
        /// </summary>
        /// <returns>Task.</returns>
        [Fact]
        public async Task PutOrganizationTypeTestNotFound()
        {
            // arrange
            var id = 3;
            var viewModel = new PutOrganizationTypeViewModel()
            {
                Name = "someName"
            };
            this.repositoryMock.Setup(it => it.FindByIdAsync(id)).ReturnsAsync(default(OrganizationType));

            // act
            var result = await this.target.PutOrganizationType(id, viewModel) as NotFoundResult;

            // assert
            result.Should().NotBeNull();
        }

        /// <summary>
        /// Puts the organization type test.
        /// </summary>
        /// <returns>Task.</returns>
        [Fact]
        public async Task PutOrganizationTypeTest()
        {
            // arrange
            var id = 3;
            var entity = new OrganizationType()
            {
                Id = id,
                Type = "some type"
            };
            var viewModel = new PutOrganizationTypeViewModel()
            {
                Name = "someName"
            };
            this.repositoryMock.Setup(it => it.FindByIdAsync(id)).ReturnsAsync(entity);

            // act
            var result = await this.target.PutOrganizationType(id, viewModel) as NoContentResult;

            // assert
            result.Should().NotBeNull();
            entity.Type.Should().Be(viewModel.Name);
            this.repositoryMock.Verify(it => it.UpdateAsync(entity));
        }

        #endregion PutOrganizationType

        #region PostOrganizationType

        /// <summary>
        /// Posts the organization type type test invalid model.
        /// </summary>
        /// <returns>Task.</returns>
        [Fact]
        public async Task PostOrganizationTypeTypeTestInvalidModel()
        {
            // arrange
            var id = 3;
            this.target.ModelState.AddModelError("someerrorkey", "someerrormessage");
            var viewModel = new PutOrganizationTypeViewModel()
            {
                Name = "someName"
            };

            // act
            var result = await this.target.PutOrganizationType(id, viewModel) as BadRequestObjectResult;

            // assert
            result.Should().NotBeNull();
            result.Value.ShouldBeEquivalentTo(new SerializableError(this.target.ModelState));
        }

        /// <summary>
        /// Posts the organization type type test not found.
        /// </summary>
        /// <returns>Task.</returns>
        [Fact]
        public async Task PostOrganizationTypeTypeTestNotFound()
        {
            // arrange
            var id = 3;
            var viewModel = new PutOrganizationTypeViewModel()
            {
                Name = "someName"
            };
            this.repositoryMock.Setup(it => it.FindByIdAsync(id)).ReturnsAsync(default(OrganizationType));

            // act
            var result = await this.target.PutOrganizationType(id, viewModel) as NotFoundResult;

            // assert
            result.Should().NotBeNull();
        }

        /// <summary>
        /// Posts the organization type type test.
        /// </summary>
        /// <returns>Task.</returns>
        [Fact]
        public async Task PostOrganizationTypeTypeTest()
        {
            // arrange
            var id = 3;
            var viewModel = new PostOrganizationTypeViewModel()
            {
                Name = "someName"
            };
            this.repositoryMock.Setup(it => it.InsertAsync(It.IsAny<OrganizationType>()))
                .Returns<OrganizationType>(
                    entity =>
                        Task.Run(
                            () =>
                            {
                                entity.Id = id;
                            }));

            // act
            var result = await this.target.PostOrganizationType(viewModel) as CreatedAtActionResult;

            // assert
            result.Should().NotBeNull();
            result.ActionName.Should().Be("GetOrganizationType");
            result.RouteValues["id"].Should().Be(id);
            this.repositoryMock.Verify(
                it =>
                    it.InsertAsync(
                        It.Is<OrganizationType>(orgType => orgType.Type == viewModel.Name)));
        }

        #endregion PostOrganizationType

        #region DeleteOrganizationType

        /// <summary>
        /// Deletes the organization type invalid model.
        /// </summary>
        /// <returns>Task.</returns>
        [Fact]
        public async Task DeleteOrganizationTypeInvalidModel()
        {
            // arrange
            var id = 3;
            this.target.ModelState.AddModelError("someerrorkey", "someerrormessage");

            // act
            var result = await this.target.DeleteOrganizationType(id) as BadRequestObjectResult;

            // assert
            result.Should().NotBeNull();
            result.Value.ShouldBeEquivalentTo(new SerializableError(this.target.ModelState));
        }

        /// <summary>
        /// Deletes the organization type test not found.
        /// </summary>
        /// <returns>Task.</returns>
        [Fact]
        public async Task DeleteOrganizationTypeTestNotFound()
        {
            // arrange
            var id = 3;
            this.repositoryMock.Setup(it => it.FindByIdAsync(id)).ReturnsAsync(default(OrganizationType));

            // act
            var result = await this.target.DeleteOrganizationType(id) as NotFoundResult;

            // assert
            result.Should().NotBeNull();
        }

        /// <summary>
        /// Deletes the organization type test.
        /// </summary>
        /// <returns>Task.</returns>
        [Fact]
        public async Task DeleteOrganizationTypeTest()
        {
            // arrange
            var id = 3;
            var entity = new OrganizationType()
            {
                Id = id,
                Type = "some type"
            };
            var expectedResultViewModel = new DeleteOrganizationTypeViemModel()
            {
                Name = entity.Type
            };
            this.repositoryMock.Setup(it => it.FindByIdAsync(id)).ReturnsAsync(entity);

            // act
            var result = await this.target.DeleteOrganizationType(id) as OkObjectResult;

            // assert
            result.Should().NotBeNull();
            result.Value.ShouldBeEquivalentTo(expectedResultViewModel);
            this.repositoryMock.Verify(it => it.DeleteAsync(entity));
        }

        #endregion DeleteOrganizationType
    }
}
