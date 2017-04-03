// ***********************************************************************
// Assembly         : Stakeholders.Web.Tests
// Author           : George
// Created          : 02-20-2017
//
// Last Modified By : George
// Last Modified On : 02-20-2017
// ***********************************************************************
// <copyright file="CompaniesControllerTest.cs" company="">
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
using Stakeholders.Web.Models.CompanyViewModels;
using Xunit;

namespace Stakeholders.Web.Tests.Controllers
{
    /// <summary>
    /// Class CompaniesControllerTest.
    /// </summary>
    public class CompaniesControllerTest
    {
        /// <summary>
        /// The entities for test
        /// </summary>
        private readonly EntitiesForTest entitiesForTest;

        /// <summary>
        /// The repository mock
        /// </summary>
        private readonly Mock<IRepository<Company>> repositoryMock;

        /// <summary>
        /// The target
        /// </summary>
        private readonly CompaniesController target;

        /// <summary>
        /// The mapper mock
        /// </summary>
        private readonly Mock<IMapper> mapperMock;

        /// <summary>
        /// Initializes a new instance of the <see cref="ActivitiesControllerTest" /> class.
        /// </summary>
        public CompaniesControllerTest()
        {
            this.entitiesForTest = new EntitiesForTest();
            this.repositoryMock = new Mock<IRepository<Company>>();
            this.mapperMock = new Mock<IMapper>();

            this.target = new CompaniesController(
                this.repositoryMock.Object,
                this.mapperMock.Object);
        }

        #region GetCompanies

        /// <summary>
        /// Gets the companies with no search criteria.
        /// </summary>
        [Fact]
        public void GetCompaniesTestSearchEmpty()
        {
            // arrange
            var entities = this.entitiesForTest.CreateCollection(4, this.entitiesForTest.CreateCompany);
            var models = new List<CompanyViewModel>();
            foreach (var entity in entities)
            {
                var model = this.entitiesForTest.CreateCompanyViewModel();
                this.mapperMock.Setup(it => it.Map<CompanyViewModel>(entity)).Returns(model);
                models.Add(model);
            }

            var expectedResult = models.ToArray();
            var start = 2;
            var count = 3;
            this.repositoryMock.Setup(it => it.GetAll(start, count, null))
                .Returns(entities);

            // act 
            var result = this.target.GetCompanies(start, count, "");

            // assert
            result.ShouldBeEquivalentTo(expectedResult);
        }

        /// <summary>
        /// Gets the companies.
        /// </summary>
        [Fact]
        public void GetCompaniesTest()
        {
            // arrange
            var entities = this.entitiesForTest.CreateCollection(4, this.entitiesForTest.CreateCompany);
            var entities2 = this.entitiesForTest.CreateCollection(2, this.entitiesForTest.CreateCompany);
            var models = new List<CompanyViewModel>();
            foreach (var entity in entities)
            {
                var model = this.entitiesForTest.CreateCompanyViewModel();
                this.mapperMock.Setup(it => it.Map<CompanyViewModel>(entity)).Returns(model);
                models.Add(model);
            }

            var expectedResult = models.ToArray();
            var start = 2;
            var count = 3;
            this.repositoryMock.Setup(it => it.GetAll(start, count, It.Is<Func<Company, bool>>(func => func != null)))
                .Returns(entities);

            // act 
            var result = this.target.GetCompanies(start, count, "somesearch");

            // assert
            result.ShouldBeEquivalentTo(expectedResult);
        }

        #endregion GetCompanies

        #region GetCompaniesCount

        /// <summary>
        /// Gets the companies count test.
        /// </summary>
        [Fact]
        public void GetCompaniesCountTest()
        {
            // arrange
            var expectedResult = 42L;
            this.repositoryMock.Setup(it => it.Count()).Returns(expectedResult);

            // act 
            var result = this.target.GetCompaniesCount();

            // assert
            result.ShouldBeEquivalentTo(expectedResult);
        }

        #endregion GetCompaniesCount

        #region GetCompany

        /// <summary>
        /// Gets the company test invalid model.
        /// </summary>
        /// <returns>Task.</returns>
        [Fact]
        public async Task GetCompanyTestInvalidModel()
        {
            // arrange
            var id = 3L;
            this.target.ModelState.AddModelError("someerrorkey", "someerrormessage");

            // act
            var result = await this.target.GetCompany(id) as BadRequestObjectResult;

            // assert
            result.Should().NotBeNull();
            result.Value.ShouldBeEquivalentTo(new SerializableError(this.target.ModelState));
        }

        /// <summary>
        /// Gets the company test not found.
        /// </summary>
        /// <returns>Task.</returns>
        [Fact]
        public async Task GetCompanyTestNotFound()
        {
            // arrange
            var id = 3L;
            this.repositoryMock.Setup(it => it.FindByIdAsync(id)).ReturnsAsync(default(Company));

            // act
            var result = await this.target.GetCompany(id) as NotFoundResult;

            // assert
            result.Should().NotBeNull();
        }

        /// <summary>
        /// Gets the company test.
        /// </summary>
        /// <returns>Task.</returns>
        [Fact]
        public async Task GetCompanyTest()
        {
            // arrange
            var id = 3L;
            var entity = this.entitiesForTest.CreateCompany();
            this.repositoryMock.Setup(it => it.FindByIdAsync(id)).ReturnsAsync(entity);


            var expectedResult = this.entitiesForTest.CreateCompanyViewModel();
            this.mapperMock.Setup(it => it.Map<CompanyViewModel>(entity)).Returns(expectedResult);

            // act
            var result = await this.target.GetCompany(id) as OkObjectResult;

            // assert
            result.Should().NotBeNull();
            result.Value.ShouldBeEquivalentTo(expectedResult);
        }

        #endregion GetCompany

        #region PutCompany

        /// <summary>
        /// Puts the company test invalid model.
        /// </summary>
        /// <returns>Task.</returns>
        [Fact]
        public async Task PutCompanyTestInvalidModel()
        {
            // arrange
            var id = 3L;
            this.target.ModelState.AddModelError("someerrorkey", "someerrormessage");
            var viewModel = this.entitiesForTest.CreateCompanyViewModel();

            // act
            var result = await this.target.PutCompany(id, viewModel) as BadRequestObjectResult;

            // assert
            result.Should().NotBeNull();
            result.Value.ShouldBeEquivalentTo(new SerializableError(this.target.ModelState));
        }

        /// <summary>
        /// Puts the company test not found.
        /// </summary>
        /// <returns>Task.</returns>
        [Fact]
        public async Task PutCompanyTestNotFound()
        {
            // arrange
            var id = 3L;
            var viewModel = this.entitiesForTest.CreateCompanyViewModel();
            this.repositoryMock.Setup(it => it.FindByIdAsync(id)).ReturnsAsync(default(Company));

            // act
            var result = await this.target.PutCompany(id, viewModel) as NotFoundResult;

            // assert
            result.Should().NotBeNull();
        }

        /// <summary>
        /// Puts the company test.
        /// </summary>
        /// <returns>Task.</returns>
        [Fact]
        public async Task PutCompanyTest()
        {
            // arrange
            var id = 3L;
            var entity = this.entitiesForTest.CreateCompany();
            var viewModel = this.entitiesForTest.CreateCompanyViewModel();
            this.repositoryMock.Setup(it => it.FindByIdAsync(id)).ReturnsAsync(entity);

            // act
            var result = await this.target.PutCompany(id, viewModel) as NoContentResult;

            // assert
            result.Should().NotBeNull();
            this.mapperMock.Verify(it => it.Map(viewModel, entity));
            this.repositoryMock.Verify(it => it.UpdateAsync(entity));
        }

        #endregion PutCompany

        #region PostCompany

        /// <summary>
        /// Posts the company test invalid model.
        /// </summary>
        /// <returns>Task.</returns>
        [Fact]
        public async Task PostCompanyTestInvalidModel()
        {
            // arrange
            this.target.ModelState.AddModelError("someerrorkey", "someerrormessage");
            var viewModel = this.entitiesForTest.CreateCompanyViewModel();

            // act
            var result = await this.target.PostCompany(viewModel) as BadRequestObjectResult;

            // assert
            result.Should().NotBeNull();
            result.Value.ShouldBeEquivalentTo(new SerializableError(this.target.ModelState));
        }

        /// <summary>
        /// Posts the company test.
        /// </summary>
        /// <returns>Task.</returns>
        [Fact]
        public async Task PostCompanyTest()
        {
            // arrange
            var model = this.entitiesForTest.CreateCompanyViewModel();
            var entity = this.entitiesForTest.CreateCompany();
            this.mapperMock.Setup(it => it.Map<Company>(model)).Returns(entity);

            // act
            var result = await this.target.PostCompany(model) as CreatedAtActionResult;

            // assert
            result.Should().NotBeNull();
            result.ActionName.Should().Be("GetCompany");
            result.RouteValues["id"].Should().Be(entity.Id);
            this.repositoryMock.Verify(it => it.InsertAsync(entity));
        }

        #endregion PostCompany

        #region DeleteCompany

        /// <summary>
        /// Deletes the organization type invalid model.
        /// </summary>
        /// <returns>Task.</returns>
        [Fact]
        public async Task DeleteCompanyTestInvalidModel()
        {
            // arrange
            var id = 3L;
            this.target.ModelState.AddModelError("someerrorkey", "someerrormessage");

            // act
            var result = await this.target.DeleteCompany(id) as BadRequestObjectResult;

            // assert
            result.Should().NotBeNull();
            result.Value.ShouldBeEquivalentTo(new SerializableError(this.target.ModelState));
        }

        /// <summary>
        /// Deletes the organization type test not found.
        /// </summary>
        /// <returns>Task.</returns>
        [Fact]
        public async Task DeleteCompanyTestNotFound()
        {
            // arrange
            var id = 3L;
            this.repositoryMock.Setup(it => it.FindByIdAsync(id)).ReturnsAsync(default(Company));

            // act
            var result = await this.target.DeleteCompany(id) as NotFoundResult;

            // assert
            result.Should().NotBeNull();
        }

        /// <summary>
        /// Deletes the organization type test.
        /// </summary>
        /// <returns>Task.</returns>
        [Fact]
        public async Task DeleteCompanyTest()
        {
            // arrange
            var id = 3L;
            var entity = this.entitiesForTest.CreateCompany();
            var model = this.entitiesForTest.CreateCompanyViewModel();
            this.mapperMock.Setup(it => it.Map<CompanyViewModel>(entity)).Returns(model);
            this.repositoryMock.Setup(it => it.FindByIdAsync(id)).ReturnsAsync(entity);

            // act
            var result = await this.target.DeleteCompany(id) as OkObjectResult;

            // assert
            result.Should().NotBeNull();
            result.Value.ShouldBeEquivalentTo(model);
            this.repositoryMock.Verify(it => it.DeleteAsync(entity));
        }

        #endregion DeleteCompany
    }
}