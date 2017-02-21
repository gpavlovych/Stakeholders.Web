// ***********************************************************************
// Assembly         : 
// Author           : George
// Created          : 02-16-2017
//
// Last Modified By : George
// Last Modified On : 02-21-2017
// ***********************************************************************
// <copyright file="IEmailSender.cs" company="">
//     Copyright (c) . All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************
using System.Threading.Tasks;

namespace Stakeholders.Web.Services
{
    /// <summary>
    /// Interface IEmailSender
    /// </summary>
    public interface IEmailSender
    {
        /// <summary>
        /// Sends the email asynchronous.
        /// </summary>
        /// <param name="email">The email.</param>
        /// <param name="subject">The subject.</param>
        /// <param name="message">The message.</param>
        /// <returns>Task.</returns>
        Task SendEmailAsync(string email, string subject, string message);
    }
}
