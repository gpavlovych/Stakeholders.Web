// ***********************************************************************
// Assembly         : Stakeholders.Web
// Author           : George
// Created          : 02-21-2017
//
// Last Modified By : George
// Last Modified On : 04-09-2017
// ***********************************************************************
// <copyright file="IApplicationUserManager.cs" company="">
//     Copyright (c) . All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************
using System.Threading.Tasks;
using Stakeholders.Web.Models;

namespace Stakeholders.Web
{
    /// <summary>
    /// Interface IApplicationUserManager
    /// </summary>
    public interface IApplicationUserManager
    {
        /// <summary>
        /// Creates the asynchronous.
        /// </summary>
        /// <param name="user">The user.</param>
        /// <param name="password">The password.</param>
        /// <returns>Task.</returns>
        Task CreateAsync(ApplicationUser user, string password);

        /// <summary>
        /// Finds the by name asynchronous.
        /// </summary>
        /// <param name="userName">Name of the user.</param>
        /// <returns>Task&lt;ApplicationUser&gt;.</returns>
        Task<ApplicationUser> FindByNameAsync(string userName);
    }
}