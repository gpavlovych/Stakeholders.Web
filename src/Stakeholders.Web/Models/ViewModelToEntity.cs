// ***********************************************************************
// Assembly         : Stakeholders.Web
// Author           : George
// Created          : 02-20-2017
//
// Last Modified By : George
// Last Modified On : 02-20-2017
// ***********************************************************************
// <copyright file="ViewModelToEntity.cs" company="">
//     Copyright (c) . All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************
using System.Linq;
using AutoMapper;
using Stakeholders.Web.Data;
using Stakeholders.Web.Models.ActivityTaskViewModels;
using Stakeholders.Web.Models.ActivityViewModels;
using Stakeholders.Web.Models.ApplicationUserViewModels;
using Stakeholders.Web.Models.CompanyViewModels;
using Stakeholders.Web.Models.ContactViewModels;
using Stakeholders.Web.Models.OrganizationCategoryViewModels;
using Stakeholders.Web.Models.OrganizationViewModels;

namespace Stakeholders.Web.Models
{
    /// <summary>
    /// Class ViewModelToEntity.
    /// </summary>
    /// <seealso cref="AutoMapper.IMappingAction{Stakeholders.Web.Models.ApplicationUserViewModels.ApplicationUserViewModel, Stakeholders.Web.Models.ApplicationUser}" />
    /// <seealso cref="AutoMapper.IMappingAction{Stakeholders.Web.Models.ContactViewModels.ContactViewModel, Stakeholders.Web.Models.Contact}" />
    /// <seealso cref="AutoMapper.IMappingAction{Stakeholders.Web.Models.RoleViewModels.RoleViewModel, Stakeholders.Web.Models.Role}" />
    /// <seealso cref="AutoMapper.IMappingAction{Stakeholders.Web.Models.OrganizationViewModels.OrganizationViewModel, Stakeholders.Web.Models.Organization}" />
    /// <seealso cref="AutoMapper.IMappingAction{Stakeholders.Web.Models.OrganizationCategoryViewModels.OrganizationCategoryViewModel, Stakeholders.Web.Models.OrganizationCategory}" />
    /// <seealso cref="AutoMapper.IMappingAction{Stakeholders.Web.Models.CompanyViewModels.CompanyViewModel, Stakeholders.Web.Models.Company}" />
    /// <seealso cref="AutoMapper.IMappingAction{Stakeholders.Web.Models.ActivityViewModels.ActivityViewModel, Stakeholders.Web.Models.Activity}" />
    /// <seealso cref="AutoMapper.IMappingAction{Stakeholders.Web.Models.ActivityTaskViewModels.ActivityTaskViewModel, Stakeholders.Web.Models.ActivityTask}" />
    public class ViewModelToEntity
        : IMappingAction<CompanyViewModel, Company>,
            IMappingAction<ActivityViewModel, Activity>,
            IMappingAction<ActivityTaskViewModel, ActivityTask>,
            IMappingAction<ApplicationUserViewModel, ApplicationUser>,
            IMappingAction<ContactViewModel, Contact>,
            IMappingAction<OrganizationViewModel, Organization>,
            IMappingAction<OrganizationCategoryViewModel, OrganizationCategory>
    {
        /// <summary>
        /// The repository activities
        /// </summary>
        private readonly IRepository<Activity> repositoryActivities;

        /// <summary>
        /// The repository users
        /// </summary>
        private readonly IRepository<ApplicationUser> repositoryUsers;

        /// <summary>
        /// The repository contacts
        /// </summary>
        private readonly IRepository<Contact> repositoryContacts;

        /// <summary>
        /// The repository goals
        /// </summary>
        private readonly IRepository<Goal> repositoryGoals;

        /// <summary>
        /// The repository statuses
        /// </summary>
        private readonly IRepository<ActivityTaskStatus> repositoryStatuses;

        /// <summary>
        /// The repository tasks
        /// </summary>
        private readonly IRepository<ActivityTask> repositoryTasks;

        /// <summary>
        /// The repository companies
        /// </summary>
        private readonly IRepository<Company> repositoryCompanies;

        /// <summary>
        /// The repository activity types
        /// </summary>
        private readonly IRepository<ActivityType> repositoryActivityTypes;

        /// <summary>
        /// The repository organization types
        /// </summary>
        private readonly IRepository<OrganizationType> repositoryOrganizationTypes;

        /// <summary>
        /// The repository organization categories
        /// </summary>
        private readonly IRepository<OrganizationCategory> repositoryOrganizationCategories;

        /// <summary>
        /// The repository organizations
        /// </summary>
        private readonly IRepository<Organization> repositoryOrganizations;

        /// <summary>
        /// The repository roles
        /// </summary>
        private readonly IRepository<Role> repositoryRoles;

        /// <summary>
        /// The context
        /// </summary>
        private readonly ApplicationDbContext context;

        /// <summary>
        /// Initializes a new instance of the <see cref="ViewModelToEntity" /> class.
        /// </summary>
        /// <param name="repositoryActivities">The repository activities.</param>
        /// <param name="repositoryUsers">The repository users.</param>
        /// <param name="repositoryContacts">The repository contacts.</param>
        /// <param name="repositoryGoals">The repository goals.</param>
        /// <param name="repositoryStatuses">The repository statuses.</param>
        /// <param name="repositoryTasks">The repository tasks.</param>
        /// <param name="repositoryCompanies">The repository companies.</param>
        /// <param name="repositoryActivityTypes">The repository activity types.</param>
        /// <param name="repositoryOrganizationTypes">The repository organization types.</param>
        /// <param name="repositoryOrganizationCategories">The repository organization categories.</param>
        /// <param name="repositoryOrganizations">The repository organizations.</param>
        /// <param name="repositoryRoles">The repository roles.</param>
        public ViewModelToEntity(
            IRepository<Activity> repositoryActivities,
            IRepository<ApplicationUser> repositoryUsers,
            IRepository<Contact> repositoryContacts,
            IRepository<Goal> repositoryGoals,
            IRepository<ActivityTaskStatus> repositoryStatuses,
            IRepository<ActivityTask> repositoryTasks,
            IRepository<Company> repositoryCompanies,
            IRepository<ActivityType> repositoryActivityTypes,
            IRepository<OrganizationType> repositoryOrganizationTypes,
            IRepository<OrganizationCategory> repositoryOrganizationCategories,
            IRepository<Organization> repositoryOrganizations,
            IRepository<Role> repositoryRoles,
            ApplicationDbContext context)
        {
            this.repositoryActivities = repositoryActivities;
            this.repositoryUsers = repositoryUsers;
            this.repositoryContacts = repositoryContacts;
            this.repositoryGoals = repositoryGoals;
            this.repositoryStatuses = repositoryStatuses;
            this.repositoryTasks = repositoryTasks;
            this.repositoryCompanies = repositoryCompanies;
            this.repositoryActivityTypes = repositoryActivityTypes;
            this.repositoryOrganizationTypes = repositoryOrganizationTypes;
            this.repositoryOrganizationCategories = repositoryOrganizationCategories;
            this.repositoryOrganizations = repositoryOrganizations;
            this.repositoryRoles = repositoryRoles;
            this.context = context;
        }

        /// <summary>
        /// Implementors can modify both the source and destination objects
        /// </summary>
        /// <param name="source">Source object</param>
        /// <param name="destination">Destination object</param>
        public void Process(CompanyViewModel source, Company destination)
        {
            destination.ObserverActivities = source.ObserverActivityIds?
                .Select(
                    activityId =>
                        this.context.ActivityObserverCompanies.FirstOrDefault(
                            it =>
                                (it.CompanyId == destination.Id) &&
                                (it.ActivityId == activityId)) ??
                        new ActivityObserverCompany
                        {
                            CompanyId = destination.Id,
                            Company = destination,
                            ActivityId = activityId,
                            Activity = this.repositoryActivities.FindById(activityId)
                        })
                .ToList();
        }

        /// <summary>
        /// Implementors can modify both the source and destination objects
        /// </summary>
        /// <param name="source">The activity view model.</param>
        /// <param name="destination">The entity.</param>
        public void Process(ActivityViewModel source, Activity destination)
        {
            var contactId = source.ContactId;
            destination.Contact = contactId != null ? this.repositoryContacts.FindById(contactId.Value) : null;

            var taskId = source.TaskId;
            destination.Task = taskId != null ? this.repositoryTasks.FindById(taskId.Value) : null;

            var activityObserverCompanies = source.ObserverCompanyIds?
                                                .Select(
                                                    observerCompanyId =>
                                                        this.context.ActivityObserverCompanies.FirstOrDefault(
                                                            it =>
                                                                (it.CompanyId == observerCompanyId) &&
                                                                (it.ActivityId == destination.Id)) ??
                                                        new ActivityObserverCompany()
                                                        {
                                                            ActivityId = destination.Id,
                                                            Activity = destination,
                                                            CompanyId = observerCompanyId,
                                                            Company =
                                                                this.repositoryCompanies.FindById(observerCompanyId)
                                                        }) ?? Enumerable.Empty<ActivityObserverCompany>();

            var activityObserverUsers = source.ObserverUserIds?
                                            .Select(
                                                observerUserId => this.context.ActivityObserverUsers.FirstOrDefault(
                                                                      it =>
                                                                          (it.UserId == observerUserId) &&
                                                                          (it.ActivityId == destination.Id)) ??
                                                                  new ActivityObserverUser()
                                                                  {
                                                                      ActivityId = destination.Id,
                                                                      Activity = destination,
                                                                      UserId = observerUserId,
                                                                      User =
                                                                          this.repositoryUsers.FindById(observerUserId)
                                                                  }) ?? Enumerable.Empty<ActivityObserverUser>();

            destination.ObserverCompanies = activityObserverCompanies.ToList();
            destination.ObserverUsers = activityObserverUsers.ToList();

            var typeId = source.TypeId;
            destination.Type = typeId != null ? this.repositoryActivityTypes.FindById(typeId.Value) : null;

            var userId = source.UserId;
            destination.User = userId != null ? this.repositoryUsers.FindById(userId.Value) : null;

            var companyId = source.CompanyId;
            destination.Company = companyId != null ? this.repositoryCompanies.FindById(companyId.Value) : null;
        }

        /// <summary>
        /// Implementors can modify both the source and destination objects
        /// </summary>
        /// <param name="source">The model.</param>
        /// <param name="destination">The entity.</param>
        public void Process(ActivityTaskViewModel source, ActivityTask destination)
        {
            var assignToId = source.AssignToId;
            destination.AssignTo = assignToId != null ? this.repositoryUsers.FindById(assignToId.Value) : null;

            destination.Contacts =
                source.ContactIds?.Select(
                    contactId =>
                        this.context.ActivityTaskContacts.FirstOrDefault(
                            it =>
                                (it.ContactId == contactId) &&
                                (it.TaskId == destination.Id)) ??
                        new ActivityTaskContact()
                        {
                            TaskId = destination.Id,
                            Task = destination,
                            ContactId = contactId,
                            Contact = this.repositoryContacts.FindById(contactId)
                        }).ToList();

            var createdById = source.CreatedById; //TODO: to be auto-filled
            destination.CreatedBy = createdById != null ? this.repositoryUsers.FindById(createdById.Value) : null;

            var goalId = source.GoalId;
            destination.Goal = goalId != null ? this.repositoryGoals.FindById(goalId.Value) : null;

            destination.ObserverUsers =
                source.ObserverUserIds?.Select(
                    userId =>
                        this.context.ActivityTaskObserverUsers.FirstOrDefault(
                            it =>
                                (it.UserId == userId) &&
                                (it.TaskId == destination.Id)) ??
                        new ActivityTaskObserverUser()
                        {
                            TaskId = destination.Id,
                            Task = destination,
                            UserId = userId,
                            User = this.repositoryUsers.FindById(userId)
                        }).ToList();

            var statusId = source.StatusId;
            destination.Status = statusId != null ? this.repositoryStatuses.FindById(statusId.Value) : null;

            destination.Organizations = source.OrganizationIds?.Select(
                    organizationId =>
                        this.context.ActivityTaskOrganizations.FirstOrDefault(
                            it =>
                                (it.OrganizationId == organizationId) &&
                                (it.TaskId == destination.Id)) ??
                        new ActivityTaskOrganization()
                        {
                            TaskId = destination.Id,
                            Task = destination,
                            OrganizationId = organizationId,
                            Organization = this.repositoryOrganizations.FindById(organizationId)
                        }).ToList();
        }

        /// <summary>
        /// Implementors can modify both the source and destination objects
        /// </summary>
        /// <param name="source">Source object</param>
        /// <param name="destination">Destination object</param>
        /// <exception cref="System.NotImplementedException"></exception>
        public void Process(ApplicationUserViewModel source, ApplicationUser destination)
        {
            destination.ObserverActivities =
                source.ObserverActivityIds?.Select(
                    activityId =>
                        this.context.ActivityObserverUsers.FirstOrDefault(
                            it =>
                                (it.UserId == destination.Id) &&
                                (it.ActivityId == activityId)) ??
                        new ActivityObserverUser()
                        {
                            UserId = destination.Id,
                            User = destination,
                            ActivityId = activityId,
                            Activity = this.repositoryActivities.FindById(activityId)
                        }).ToList();

            destination.ObserverTasks =
                source.ObserverTaskIds?.Select(
                    taskId =>
                        this.context.ActivityTaskObserverUsers.FirstOrDefault(
                            it =>
                                (it.UserId == destination.Id) &&
                                (it.TaskId == taskId)) ??
                        new ActivityTaskObserverUser()
                        {
                            UserId = destination.Id,
                            User = destination,
                            TaskId = taskId,
                            Task = this.repositoryTasks.FindById(taskId)
                        }).ToList();

            var companyId = source.CompanyId;
            destination.Company = companyId != null ? this.repositoryCompanies.FindById(companyId.Value) : null;

            var roleId = source.RoleId;
            destination.Role = roleId != null ? this.repositoryRoles.FindById(roleId.Value) : null;
        }

        /// <summary>
        /// Implementors can modify both the source and destination objects
        /// </summary>
        /// <param name="source">Source object</param>
        /// <param name="destination">Destination object</param>
        /// <exception cref="System.NotImplementedException"></exception>
        public void Process(ContactViewModel source, Contact destination)
        {
            var companyId = source.CompanyId;
            destination.Company = companyId != null ? this.repositoryCompanies.FindById(companyId.Value) : null;

            var organizationId = source.OrganizationId;
            destination.Organization = organizationId != null
                ? this.repositoryOrganizations.FindById(organizationId.Value)
                : null;

            var userId = source.UserId;
            destination.User = userId != null ? this.repositoryUsers.FindById(userId.Value) : null;

            destination.Tasks =
                source.TaskIds?.Select(
                    taskId =>
                        this.context.ActivityTaskContacts.FirstOrDefault(
                            it =>
                                (it.ContactId == destination.Id) &&
                                (it.TaskId == taskId)) ??
                        new ActivityTaskContact()
                        {
                            ContactId = destination.Id,
                            Contact = destination,
                            TaskId = taskId,
                            Task = this.repositoryTasks.FindById(taskId)
                        }).ToList();
        }

        /// <summary>
        /// Implementors can modify both the source and destination objects
        /// </summary>
        /// <param name="source">Source object</param>
        /// <param name="destination">Destination object</param>
        /// <exception cref="System.NotImplementedException"></exception>
        public void Process(OrganizationViewModel source, Organization destination)
        {
            var companyId = source.CompanyId;
            destination.Company = companyId != null ? this.repositoryCompanies.FindById(companyId.Value) : null;

            var typeId = source.TypeId;
            destination.Type = typeId != null ? this.repositoryOrganizationTypes.FindById(typeId.Value) : null;

            var categoryId = source.CategoryId;
            destination.Category = categoryId != null
                ? this.repositoryOrganizationCategories.FindById(categoryId.Value)
                : null;

            var userId = source.UserId;
            destination.User = userId != null ? this.repositoryUsers.FindById(userId.Value) : null;
        }

        /// <summary>
        /// Implementors can modify both the source and destination objects
        /// </summary>
        /// <param name="source">Source object</param>
        /// <param name="destination">Destination object</param>
        /// <exception cref="System.NotImplementedException"></exception>
        public void Process(OrganizationCategoryViewModel source, OrganizationCategory destination)
        {
            var companyId = source.CompanyId;
            destination.Company = companyId != null ? this.repositoryCompanies.FindById(companyId.Value) : null;
        }
    }
}