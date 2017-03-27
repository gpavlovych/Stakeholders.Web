﻿// ***********************************************************************
// Assembly         : 
// Author           : George
// Created          : 02-21-2017
//
// Last Modified By : George
// Last Modified On : 02-21-2017
// ***********************************************************************
// <copyright file="ApplicationUserManager.cs" company="">
//     Copyright (c) . All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************
using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Stakeholders.Web.Models;

namespace Stakeholders.Web
{
    /// <summary>
    /// Class ApplicationUserManager.
    /// </summary>
    /// <seealso cref="Stakeholders.Web.IApplicationUserManager" />
    public class ApplicationUserManager : IApplicationUserManager
    {
        /// <summary>
        /// The user manager
        /// </summary>
        private readonly UserManager<ApplicationUser> userManager;

        /// <summary>
        /// Initializes a new instance of the <see cref="ApplicationUserManager"/> class.
        /// </summary>
        /// <param name="userManager">The user manager.</param>
        public ApplicationUserManager(UserManager<ApplicationUser> userManager)
        {
            this.userManager = userManager;
        }

        /// <summary>
        /// create as an asynchronous operation.
        /// </summary>
        /// <param name="user">The user.</param>
        /// <param name="password">The password.</param>
        /// <returns>Task.</returns>
        /// <exception cref="InvalidOperationException"></exception>
        public async Task CreateAsync(ApplicationUser user, string password)
        {
            var result = await this.userManager.CreateAsync(user, password);
            if (!result.Succeeded)
            {
                throw new InvalidOperationException(result.Errors.ToString());
            }
        }
    }
}