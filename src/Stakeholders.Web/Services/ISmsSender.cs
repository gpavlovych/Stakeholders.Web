// ***********************************************************************
// Assembly         : 
// Author           : George
// Created          : 02-16-2017
//
// Last Modified By : George
// Last Modified On : 02-21-2017
// ***********************************************************************
// <copyright file="ISmsSender.cs" company="">
//     Copyright (c) . All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************
using System.Threading.Tasks;

namespace Stakeholders.Web.Services
{
    /// <summary>
    /// Interface ISmsSender
    /// </summary>
    public interface ISmsSender
    {
        /// <summary>
        /// Sends the SMS asynchronous.
        /// </summary>
        /// <param name="number">The number.</param>
        /// <param name="message">The message.</param>
        /// <returns>Task.</returns>
        Task SendSmsAsync(string number, string message);
    }
}
