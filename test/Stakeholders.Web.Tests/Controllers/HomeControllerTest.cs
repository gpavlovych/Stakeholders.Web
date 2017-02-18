// ***********************************************************************
// Assembly         : Stakeholders.Web.Tests
// Author           : George
// Created          : 02-18-2017
//
// Last Modified By : George
// Last Modified On : 02-18-2017
// ***********************************************************************
// <copyright file="HomeControllerTest.cs" company="">
//     Copyright (c) . All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************
using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using Stakeholders.Web.Controllers;
using Xunit;

namespace Stakeholders.Web.Tests.Controllers
{
    /// <summary>
    /// Class HomeControllerTest.
    /// </summary>
    public class HomeControllerTest
    {
        /// <summary>
        /// The target
        /// </summary>
        private readonly HomeController target;

        /// <summary>
        /// Initializes a new instance of the <see cref="HomeControllerTest"/> class.
        /// </summary>
        public HomeControllerTest()
        {
            this.target = new HomeController();
        }

        /// <summary>
        /// Tests <see cref="HomeController.Index"/> action.
        /// </summary>
        [Fact]
        public void IndexTest()
        {
            // act
            var result = this.target.Index() as ViewResult;

            //assert
            result.Should().NotBeNull();
        }

        /// <summary>
        /// Tests <see cref="HomeController.Contact"/> action.
        /// </summary>
        [Fact]
        public void ContactTest()
        {
            // act
            var result = this.target.Contact() as ViewResult;

            //assert
            result.Should().NotBeNull();
            result.ViewData["Message"].Should().Be("Your contact page.");
        }

        /// <summary>
        /// Tests <see cref="HomeController.About"/> action.
        /// </summary>
        [Fact]
        public void AboutTest()
        {
            // act
            var result = this.target.About() as ViewResult;

            //assert
            result.Should().NotBeNull();
            result.ViewData["Message"].Should().Be("Your application description page.");
        }

        /// <summary>
        /// Tests <see cref="HomeController.Error"/> action.
        /// </summary>
        [Fact]
        public void ErrorTest()
        {
            // act
            var result = this.target.Error() as ViewResult;

            //assert
            result.Should().NotBeNull();
        }
    }
}
