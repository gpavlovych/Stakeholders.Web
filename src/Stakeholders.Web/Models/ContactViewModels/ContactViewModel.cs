// ***********************************************************************
// Assembly         : Stakeholders.Web
// Author           : George
// Created          : 02-20-2017
//
// Last Modified By : George
// Last Modified On : 02-20-2017
// ***********************************************************************
// <copyright file="ContactViewModel.cs" company="">
//     Copyright (c) . All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************
using Stakeholders.Web.Models.Base;

namespace Stakeholders.Web.Models.ContactViewModels
{
    /// <summary>
    /// Class ContactViewModel.
    /// </summary>
    /// <seealso cref="Stakeholders.Web.Models.Base.ViewModelBase" />
    public class ContactViewModel : ViewModelBase
    {
        /// <summary>
        /// Gets or sets the name l.
        /// </summary>
        /// <value>The name l.</value>
        public string NameL { get; set; }

        /// <summary>
        /// Gets or sets the name f.
        /// </summary>
        /// <value>The name f.</value>
        public string NameF { get; set; }

        /// <summary>
        /// Gets or sets the title.
        /// </summary>
        /// <value>The title.</value>
        public string Title { get; set; }

        /// <summary>
        /// Gets or sets the organization identifier.
        /// </summary>
        /// <value>The organization identifier.</value>
        public long? OrganizationId { get; set; }

        public string OrganizationName { get; set; }
        
        /// <summary>
        /// Gets or sets the phone.
        /// </summary>
        /// <value>The phone.</value>
        public string Phone { get; set; }

        /// <summary>
        /// Gets or sets the email.
        /// </summary>
        /// <value>The email.</value>
        public string Email { get; set; }

        /// <summary>
        /// Gets or sets the photo URL.
        /// </summary>
        /// <value>The photo URL.</value>
        public string PhotoUrl { get; set; }

        /// <summary>
        /// Gets or sets the user identifier.
        /// </summary>
        /// <value>The user identifier.</value>
        public long? UserId { get; set; }

        public string UserName { get; set; }

        /// <summary>
        /// Gets or sets the comments.
        /// </summary>
        /// <value>The comments.</value>
        public string Comments { get; set; }

        /// <summary>
        /// Gets or sets the company identifier.
        /// </summary>
        /// <value>The company identifier.</value>
        public long? CompanyId { get; set; }

        public string CompanyName { get; set; }
        
        /// <summary>
        /// Gets or sets the task ids.
        /// </summary>
        /// <value>The task ids.</value>
        public long[] TaskIds { get; set; }

        public double? TasksCompleted { get; set; }

        public int Activities { get; set; }

        public string DisplayName { get; set; }
    }
}