// ***********************************************************************
// Assembly         : 
// Author           : George
// Created          : 02-16-2017
//
// Last Modified By : George
// Last Modified On : 02-16-2017
// ***********************************************************************
// <copyright file="MessageServices.cs" company="">
//     Copyright (c) . All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************
using System.Threading.Tasks;

namespace Stakeholders.Web.Services
{
    // This class is used by the application to send Email and SMS
    // when you turn on two-factor authentication in ASP.NET Identity.
    // For more details see this link http://go.microsoft.com/fwlink/?LinkID=532713
    /// <summary>
    /// Class AuthMessageSender.
    /// </summary>
    /// <seealso cref="Stakeholders.Web.Services.IEmailSender" />
    /// <seealso cref="Stakeholders.Web.Services.ISmsSender" />
    public class AuthMessageSender : IEmailSender, ISmsSender
    {
        /// <summary>
        /// Sends the email asynchronous.
        /// </summary>
        /// <param name="email">The email.</param>
        /// <param name="subject">The subject.</param>
        /// <param name="message">The message.</param>
        /// <returns>Task.</returns>
        public Task SendEmailAsync(string email, string subject, string message)
        {
            // Plug in your email service here to send an email.
            return Task.FromResult(0);
        }

        /// <summary>
        /// Sends the SMS asynchronous.
        /// </summary>
        /// <param name="number">The number.</param>
        /// <param name="message">The message.</param>
        /// <returns>Task.</returns>
        public Task SendSmsAsync(string number, string message)
        {
            // Plug in your SMS service here to send a text message.
            return Task.FromResult(0);
        }
    }
}
