// ***********************************************************************
// Assembly         : Stakeholders.Web
// Author           : George
// Created          : 02-20-2017
//
// Last Modified By : George
// Last Modified On : 02-20-2017
// ***********************************************************************
// <copyright file="EntityToViewModel.cs" company="">
//     Copyright (c) . All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************
using System.Linq;
using AutoMapper;
using Stakeholders.Web.Models.ActivityTaskViewModels;
using Stakeholders.Web.Models.ActivityViewModels;
using Stakeholders.Web.Models.ApplicationUserViewModels;
using Stakeholders.Web.Models.CompanyViewModels;
using Stakeholders.Web.Models.ContactViewModels;

namespace Stakeholders.Web.Models
{
    /// <summary>
    /// Class EntityToViewModel.
    /// </summary>
    /// <seealso cref="AutoMapper.IMappingAction{Stakeholders.Web.Models.Contact, Stakeholders.Web.Models.ContactViewModels.ContactViewModel}" />
    /// <seealso cref="AutoMapper.IMappingAction{Stakeholders.Web.Models.ApplicationUser, Stakeholders.Web.Models.ApplicationUserViewModels.ApplicationUserViewModel}" />
    /// <seealso cref="AutoMapper.IMappingAction{Stakeholders.Web.Models.Activity, Stakeholders.Web.Models.ActivityViewModels.ActivityViewModel}" />
    /// <seealso cref="AutoMapper.IMappingAction{Stakeholders.Web.Models.ActivityTask, Stakeholders.Web.Models.ActivityTaskViewModels.ActivityTaskViewModel}" />
    /// <seealso cref="AutoMapper.IMappingAction{Stakeholders.Web.Models.Company, Stakeholders.Web.Models.CompanyViewModels.CompanyViewModel}" />
    public class EntityToViewModel
        : IMappingAction<Activity, ActivityViewModel>,
            IMappingAction<ActivityTask, ActivityTaskViewModel>,
            IMappingAction<Company, CompanyViewModel>,
            IMappingAction<ApplicationUser, ApplicationUserViewModel>,
            IMappingAction<Contact, ContactViewModel>
    {
        /// <summary>
        /// Implementors can modify both the source and destination objects
        /// </summary>
        /// <param name="source">Source object</param>
        /// <param name="destination">Destination object</param>
        public void Process(Activity source, ActivityViewModel destination)
        {
            destination.ObserverCompanyIds =
            (source.ObserverCompanies?.Where(it => it.Company != null).Select(it => it.Company.Id) ??
             Enumerable.Empty<long>()).ToArray();
            destination.ObserverUserIds =
            (source.ObserverUsers?.Where(it => it.User != null).Select(it => it.User.Id) ??
             Enumerable.Empty<long>()).ToArray();
        }

        /// <summary>
        /// Implementors can modify both the source and destination objects
        /// </summary>
        /// <param name="source">Source object</param>
        /// <param name="destination">Destination object</param>
        public void Process(ActivityTask source, ActivityTaskViewModel destination)
        {
            destination.ObserverUserIds =
            (source.ObserverUsers?.Where(it => it.User != null).Select(it => it.User.Id) ??
             Enumerable.Empty<long>()).ToArray();
        }

        /// <summary>
        /// Implementors can modify both the source and destination objects
        /// </summary>
        /// <param name="source">Source object</param>
        /// <param name="destination">Destination object</param>
        public void Process(Company source, CompanyViewModel destination)
        {
            destination.ObserverActivityIds =
            (source.ObserverActivities?.Where(it => it.Activity != null).Select(it => it.Activity.Id) ??
             Enumerable.Empty<long>()).ToArray();
        }

        /// <summary>
        /// Implementors can modify both the source and destination objects
        /// </summary>
        /// <param name="source">Source object</param>
        /// <param name="destination">Destination object</param>
        public void Process(ApplicationUser source, ApplicationUserViewModel destination)
        {
            destination.ObserverActivityIds =
            (source.ObserverActivities?.Where(it => it.Activity != null).Select(it => it.Activity.Id) ??
             Enumerable.Empty<long>()).ToArray();

            destination.ObserverTaskIds =
            (source.ObserverTasks?.Where(it => it.Task != null).Select(it => it.Task.Id) ??
             Enumerable.Empty<long>()).ToArray();
        }

        /// <summary>
        /// Implementors can modify both the source and destination objects
        /// </summary>
        /// <param name="source">Source object</param>
        /// <param name="destination">Destination object</param>
        public void Process(Contact source, ContactViewModel destination)
        {
            destination.TaskIds =
            (source.Tasks?.Where(it => it.Task != null).Select(it => it.Task.Id) ??
             Enumerable.Empty<long>()).ToArray();
        }
    }
}