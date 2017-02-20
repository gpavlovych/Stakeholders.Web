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
using Stakeholders.Web.Models.CompanyViewModels;

namespace Stakeholders.Web.Models
{
    /// <summary>
    /// Class EntityToViewModel.
    /// </summary>
    /// <seealso cref="AutoMapper.IMappingAction{Stakeholders.Web.Models.Activity, Stakeholders.Web.Models.ActivityViewModels.ActivityViewModel}" />
    /// <seealso cref="AutoMapper.IMappingAction{Stakeholders.Web.Models.ActivityTask, Stakeholders.Web.Models.ActivityTaskViewModels.ActivityTaskViewModel}" />
    /// <seealso cref="AutoMapper.IMappingAction{Stakeholders.Web.Models.Company, Stakeholders.Web.Models.CompanyViewModels.CompanyViewModel}" />
    public class EntityToViewModel
        : IMappingAction<Activity, ActivityViewModel>,
            IMappingAction<ActivityTask, ActivityTaskViewModel>,
            IMappingAction<Company, CompanyViewModel>
    {

        ///// <summary>
        ///// Gets the observer user ids.
        ///// </summary>
        ///// <param name="entity">The entity.</param>
        ///// <returns>System.Int64[].</returns>
        //public static long[] GetObserverUserIds(ActivityTask entity)
        //{
        //    return (entity.ObserverUsers?.Where(it => it.User != null)
        //                .Select(it => it.User.Id) ??
        //            Enumerable.Empty<long>()).ToArray();
        //}

        ///// <summary>
        ///// Gets the contact ids.
        ///// </summary>
        ///// <param name="entity">The entity.</param>
        ///// <returns>System.Int64[].</returns>
        //public static long[] GetContactIds(ActivityTask entity)
        //{
        //    return (entity.Contacts?.Where(it => it.Contact != null)
        //                .Select(it => it.Contact.Id) ??
        //            Enumerable.Empty<long>()).ToArray();
        //}

        ///// <summary>
        ///// Gets the observer user ids.
        ///// </summary>
        ///// <param name="entity">The entity.</param>
        ///// <returns>System.Int64[].</returns>
        //public static long[] GetObserverUserIds(Activity entity)
        //{
        //    return (entity.ObserverUsersCompanies?.Where(it => it.User != null)
        //                .Select(it => it.User.Id) ??
        //            Enumerable.Empty<long>()).ToArray();
        //}

        ///// <summary>
        ///// Gets the observer company ids.
        ///// </summary>
        ///// <param name="entity">The entity.</param>
        ///// <returns>System.Int64[].</returns>
        //public static long[] GetObserverCompanyIds(Activity entity)
        //{
        //    return (entity.ObserverUsersCompanies?.Where(it => it.Company != null)
        //                .Select(it => it.Company.Id) ??
        //            Enumerable.Empty<long>()).ToArray();
        //}

        ///// <summary>
        ///// Gets the observer activity ids.
        ///// </summary>
        ///// <param name="entity">The entity.</param>
        ///// <returns>System.Int64[].</returns>
        //public static long[] GetObserverActivityIds(Company entity)
        //{
        //    return (entity.ObserverActivities?.Where(it => it.Activity != null)
        //                .Select(it => it.Activity.Id) ??
        //            Enumerable.Empty<long>()).ToArray();
        //}

        /// <summary>
        /// Implementors can modify both the source and destination objects
        /// </summary>
        /// <param name="source">Source object</param>
        /// <param name="destination">Destination object</param>
        public void Process(Activity source, ActivityViewModel destination)
        {
            //destination.TaskId = source.Task?.Id;
            //destination.CompanyId = source.Company?.Id;
            //destination.ContactId = source.Contact?.Id;
            //destination.DateActivity = source.DateActivity;
            //destination.DateCreated = source.DateCreated.ToNullable();
            //destination.Description = source.Description;
            destination.ObserverCompanyIds =
                source.ObserverUsersCompanies?.Where(it => it.Company != null).Select(it => it.Company.Id).ToArray();
            destination.ObserverUserIds =
                source.ObserverUsersCompanies?.Where(it => it.User != null).Select(it => it.User.Id).ToArray();
            //destination.Subject = source.Subject;
            //destination.TypeId = source.Type?.Id;
            //destination.UserId = source.User?.Id;
        }

        /// <summary>
        /// Implementors can modify both the source and destination objects
        /// </summary>
        /// <param name="source">Source object</param>
        /// <param name="destination">Destination object</param>
        public void Process(ActivityTask source, ActivityTaskViewModel destination)
        {
            //destination.AssignToId = source.AssignTo?.Id;
            //destination.ContactIds = source.Contacts?.Select(it => it.Contact.Id).ToArray();
            //destination.CreatedById = source.CreatedBy?.Id;
            //destination.GoalId = source.Goal?.Id;
            destination.ObserverUserIds = source.ObserverUsers.Select(it => it.User.Id).ToArray();
            //destination.StatusId = source.Status?.Id;
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
    }
}