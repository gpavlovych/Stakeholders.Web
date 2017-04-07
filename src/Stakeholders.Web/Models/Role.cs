// ***********************************************************************
// Assembly         : Stakeholders.Web
// Author           : George
// Created          : 02-16-2017
//
// Last Modified By : George
// Last Modified On : 02-21-2017
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
    /// <seealso cref="Microsoft.AspNetCore.Identity.EntityFrameworkCore.IdentityRole{System.Int64}" />
    /// <seealso cref="Stakeholders.Web.Models.IBaseEntity" />
    /// <seealso cref="Microsoft.AspNetCore.Identity.EntityFrameworkCore.IdentityRole{int}" />
    /// <seealso cref="string" />
    public class Role: IdentityRole<long>, IBaseEntity
    {
        /// <summary>
        /// Gets or sets the alias.
        /// </summary>
        /// <value>The alias.</value>
        public string Alias { get; set; }
    }
}