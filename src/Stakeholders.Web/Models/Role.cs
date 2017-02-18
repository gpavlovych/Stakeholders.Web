﻿// ***********************************************************************
// Assembly         : Stakeholders.Web
// Author           : George
// Created          : 02-16-2017
//
// Last Modified By : George
// Last Modified On : 02-18-2017
// ***********************************************************************
// <copyright file="Role.cs" company="">
//     Copyright (c) . All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

namespace Stakeholders.Web.Models
{
    /// <summary>
    /// Class Role.
    /// </summary>
    /// <seealso cref="Microsoft.AspNetCore.Identity.EntityFrameworkCore.IdentityRole{System.String}" />
    /// <seealso cref="string" />
    public class Role: IdentityRole<string>
    {
        /// <summary>
        /// Gets or sets the name en.
        /// </summary>
        /// <value>The name en.</value>
        public string NameEn { get; set; }
    }
}