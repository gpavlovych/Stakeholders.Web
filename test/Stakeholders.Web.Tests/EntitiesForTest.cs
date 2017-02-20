﻿// ***********************************************************************
// Assembly         : Stakeholders.Web.Tests
// Author           : George
// Created          : 02-20-2017
//
// Last Modified By : George
// Last Modified On : 02-20-2017
// ***********************************************************************
// <copyright file="EntitiesForTest.cs" company="">
//     Copyright (c) . All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************
using System;
using System.Collections.Generic;
using System.Linq;
using Stakeholders.Web.Models;
using Stakeholders.Web.Models.ActivityTaskViewModels;
using Stakeholders.Web.Models.ActivityViewModels;
using Stakeholders.Web.Models.OrganizationTypeViewModels;

namespace Stakeholders.Web.Tests
{
    /// <summary>
    /// Class EntitiesForTest.
    /// </summary>
    public class EntitiesForTest
    {
        /// <summary>
        /// The random
        /// </summary>
        private readonly Random random;

        /// <summary>
        /// Initializes a new instance of the <see cref="EntitiesForTest" /> class.
        /// </summary>
        public EntitiesForTest()
        {
            this.random = new Random();
        }

        /// <summary>
        /// Creates the int.
        /// </summary>
        /// <returns>System.Int32.</returns>
        public long CreateInt()
        {
            return this.random.Next();
        }

        /// <summary>
        /// Creates the string.
        /// </summary>
        /// <returns>System.String.</returns>
        public string CreateString()
        {
            return $"some string {Guid.NewGuid():N}";
        }

        /// <summary>
        /// Creates the type of the organization.
        /// </summary>
        /// <returns>OrganizationType.</returns>
        public OrganizationType CreateOrganizationType()
        {
            return new OrganizationType()
            {
                Id = this.CreateInt(),
                Type = this.CreateString()
            };
        }

        /// <summary>
        /// Creates the organization category.
        /// </summary>
        /// <returns>OrganizationCategory.</returns>
        public OrganizationCategory CreateOrganizationCategory()
        {
            return new OrganizationCategory()
            {
                Id = this.CreateInt(),
                Company = this.CreateCompany(),
                IconUrl = this.CreateString(),
                Influencing = this.CreateString(),
                InfluencedBy = this.CreateString(),
                Name = this.CreateString()
            };
        }

        /// <summary>
        /// Creates the company.
        /// </summary>
        /// <returns>Company.</returns>
        public Company CreateCompany()
        {
            return new Company()
            {
                Id = this.CreateInt(),
                Name = this.CreateString(),
                Address = this.CreateString(),
                Influencing = this.CreateString(),
                InfluencedBy = this.CreateString(),
                City = this.CreateString(),
                Email = this.CreateString(),
                Phone = this.CreateString(),
                CompanyCode = this.CreateString(),
                LogoUrl = this.CreateString()
            };
        }

        /// <summary>
        /// Creates the application user.
        /// </summary>
        /// <returns>ApplicationUser.</returns>
        public ApplicationUser CreateApplicationUser()
        {
            return new ApplicationUser()
            {
                Id = this.CreateInt(),
                Name = this.CreateString(),
                Role = this.CreateRole(),
                Title = this.CreateString(),
                Company = this.CreateCompany()
            };
        }

        /// <summary>
        /// Creates the role.
        /// </summary>
        /// <returns>Role.</returns>
        public Role CreateRole()
        {
            return new Role()
            {
                Id = this.random.Next(),
                Name = this.CreateString(),
                NameEn = this.CreateString(),
            };
        }

        /// <summary>
        /// Creates the activity observer user company.
        /// </summary>
        /// <param name="activity">The activity.</param>
        /// <param name="user">The user.</param>
        /// <param name="company">The company.</param>
        /// <returns>ActivityObserverUserCompany.</returns>
        public ActivityObserverUserCompany CreateActivityObserverUserCompany(
            Activity activity = null,
            ApplicationUser user = null,
            Company company = null)
        {
            return new ActivityObserverUserCompany()
            {
                Activity = activity,
                Company = company,
                Id = this.CreateInt(),
                User = user
            };
        }

        /// <summary>
        /// Creates the collection.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="count">The count.</param>
        /// <param name="factory">The factory.</param>
        /// <returns>ICollection&lt;T&gt;.</returns>
        public ICollection<T> CreateCollection<T>(int count, Func<T> factory)
        {
            return Enumerable.Repeat(0, count).Select(it => factory()).ToArray();
        }

        /// <summary>
        /// Creates the organization type view model.
        /// </summary>
        /// <returns>OrganizationTypeViewModel.</returns>
        public OrganizationTypeViewModel CreateOrganizationTypeViewModel()
        {
            return new OrganizationTypeViewModel()
            {
                Name = this.CreateString()
            };
        }

        /// <summary>
        /// Creates the activity.
        /// </summary>
        /// <returns>Activity.</returns>
        public Activity CreateActivity()
        {
            return new Activity()
            {
                Id = this.CreateInt(),
                Company = this.CreateCompany(),
                Contact = this.CreateContact(),
                DateActivity = this.CreateDate(),
                DateCreated = this.CreateDate(),
                Subject = this.CreateString(),
                Description = this.CreateString(),
                Task = this.CreateActivityTask(),
                User = this.CreateApplicationUser(),
                Type = this.CreateActivityType(),
                ObserverUsersCompanies = this.CreateCollection(
                    3,
                    () => this.CreateBool()
                        ? this.CreateActivityObserverUserCompany(user: this.CreateApplicationUser())
                        : this.CreateActivityObserverUserCompany(company: this.CreateCompany()))
            };
        }

        /// <summary>
        /// Creates the date.
        /// </summary>
        /// <returns>DateTime.</returns>
        public DateTime CreateDate()
        {
            return DateTime.Now;
        }

        /// <summary>
        /// Creates the contact.
        /// </summary>
        /// <returns>Contact.</returns>
        public Contact CreateContact()
        {
            return new Contact()
            {
                Id = this.CreateInt(),
                Comments = this.CreateString(),
                Company = this.CreateCompany(),
                Email = this.CreateString(),
                User = this.CreateApplicationUser(),
                Title = this.CreateString(),
                Phone = this.CreateString(),
                NameL = this.CreateString(),
                NameF = this.CreateString(),
                Organization = this.CreateOrganization(),
                PhotoUrl = this.CreateString()
            };
        }

        /// <summary>
        /// Creates the organization.
        /// </summary>
        /// <returns>Organization.</returns>
        public Organization CreateOrganization()
        {
            return new Organization()
            {
                Company = this.CreateCompany(),
                Category = this.CreateOrganizationCategory(),
                Id = this.CreateInt(),
                Influencing = this.CreateString(),
                InfluencedBy = this.CreateString(),
                Name = this.CreateString(),
                User = this.CreateApplicationUser(),
                Type = this.CreateOrganizationType()
            };
        }

        /// <summary>
        /// Creates the activity task.
        /// </summary>
        /// <returns>ActivityTask.</returns>
        public ActivityTask CreateActivityTask()
        {
            return new ActivityTask()
            {
                Subject = this.CreateString(),
                AssignTo = this.CreateApplicationUser(),
                Id = this.CreateInt(),
                DateCreated = this.CreateDate(),
                Description = this.CreateString(),
                Goal = this.CreateGoal(),
                ObserverUsers =
                    this.CreateCollection(
                        2,
                        () => this.CreateActivityTaskObserverUser(user: this.CreateApplicationUser())),
                CreatedBy = this.CreateApplicationUser(),
                DateEnd = this.CreateDate(),
                DateDeadline = this.CreateDate(),
                IsImportant = this.CreateBool(),
                Status = this.CreateActivityTaskStatus(),
                SuccessFactor = this.CreateString(),
                Contacts = this.CreateCollection(3, () => this.CreateActivityTaskContact(contact: this.CreateContact()))
            };
        }

        /// <summary>
        /// Creates the activity task contact.
        /// </summary>
        /// <param name="task">The task.</param>
        /// <param name="contact">The contact.</param>
        /// <returns>ActivityTaskContact.</returns>
        public ActivityTaskContact CreateActivityTaskContact(ActivityTask task = null, Contact contact = null)
        {
            return new ActivityTaskContact()
            {
                Id = this.CreateInt(),
                Contact = contact,
                Task = task
            };
        }

        /// <summary>
        /// Creates the activity task status.
        /// </summary>
        /// <returns>ActivityTaskStatus.</returns>
        public ActivityTaskStatus CreateActivityTaskStatus()
        {
            return new ActivityTaskStatus()
            {
                Id = this.CreateInt(),
                Name = this.CreateString(),
                NameEn = this.CreateString()
            };
        }

        /// <summary>
        /// Creates the bool.
        /// </summary>
        /// <returns><c>true</c> if XXXX, <c>false</c> otherwise.</returns>
        public bool CreateBool()
        {
            return this.CreateInt()%2 == 0;
        }

        /// <summary>
        /// Creates the activity task observer user.
        /// </summary>
        /// <param name="task">The task.</param>
        /// <param name="user">The user.</param>
        /// <returns>ActivityTaskObserverUser.</returns>
        public ActivityTaskObserverUser CreateActivityTaskObserverUser(
            ActivityTask task = null,
            ApplicationUser user = null)
        {
            return new ActivityTaskObserverUser()
            {
                Id = this.CreateInt(),
                Task = task,
                User = user
            };
        }

        /// <summary>
        /// Creates the goal.
        /// </summary>
        /// <returns>Goal.</returns>
        public Goal CreateGoal()
        {
            return new Goal()
            {
                Id = this.CreateInt(),
                Title = this.CreateString()
            };
        }

        /// <summary>
        /// Creates the type of the activity.
        /// </summary>
        /// <returns>ActivityType.</returns>
        public ActivityType CreateActivityType()
        {
            return new ActivityType()
            {
                Id = this.CreateInt(),
                Name = this.CreateString()
            };
        }

        /// <summary>
        /// Creates the activity view model.
        /// </summary>
        /// <returns>ActivityViewModel.</returns>
        public ActivityViewModel CreateActivityViewModel()
        {
            return new ActivityViewModel()
            {
                CompanyId = this.CreateInt(),
                ContactId = this.CreateInt(),
                DateCreated = this.CreateDate(),
                DateActivity = this.CreateDate(),
                Description = this.CreateString(),
                Subject = this.CreateString(),
                TypeId = this.CreateInt(),
                ObserverCompanyIds = this.CreateCollection(3, () => this.CreateInt()).ToArray(),
                TaskId = this.CreateInt(),
                ObserverUserIds = this.CreateCollection(3, () => this.CreateInt()).ToArray(),
                UserId = this.CreateInt()
            };
        }

        public ActivityTaskViewModel CreateActivityTaskViewModel()
        {
            return new ActivityTaskViewModel()
            {
                AssignToId = this.CreateInt(),
                ContactIds = this.CreateCollection(3, () => this.CreateInt()).ToArray(),
                ObserverUserIds = this.CreateCollection(3, () => this.CreateInt()).ToArray(),
                Subject = this.CreateString(),
                DateCreated = this.CreateDate(),
                Description = this.CreateString(),
                DateEnd = this.CreateDate(),
                DateDeadline = this.CreateDate(),
                IsImportant = this.CreateBool(),
                SuccessFactor = this.CreateString(),
                CreatedById = this.CreateInt(),
                StatusId = this.CreateInt(),
                GoalId = this.CreateInt()
            };
        }
    }
}