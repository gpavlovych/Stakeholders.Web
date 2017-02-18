// ***********************************************************************
// Assembly         : Stakeholders.Web
// Author           : George
// Created          : 02-16-2017
//
// Last Modified By : George
// Last Modified On : 02-16-2017
// ***********************************************************************
// <copyright file="HomeController.cs" company="">
//     Copyright (c) . All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************

using Microsoft.AspNetCore.Mvc;

namespace Stakeholders.Web.Controllers
{
    /// <summary>
    /// Class HomeController.
    /// </summary>
    /// <seealso cref="Microsoft.AspNetCore.Mvc.Controller" />
    public class HomeController : Controller
    {
        /// <summary>
        /// Indexes this instance.
        /// </summary>
        /// <returns>IActionResult.</returns>
        public IActionResult Index()
        {
            return this.View();
        }

        /// <summary>
        /// Abouts this instance.
        /// </summary>
        /// <returns>IActionResult.</returns>
        public IActionResult About()
        {
            this.ViewData["Message"] = "Your application description page.";

            return this.View();
        }

        /// <summary>
        /// Contacts this instance.
        /// </summary>
        /// <returns>IActionResult.</returns>
        public IActionResult Contact()
        {
            this.ViewData["Message"] = "Your contact page.";

            return this.View();
        }

        /// <summary>
        /// Errors this instance.
        /// </summary>
        /// <returns>IActionResult.</returns>
        public IActionResult Error()
        {
            return this.View();
        }
    }
}
