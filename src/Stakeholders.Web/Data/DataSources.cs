// ***********************************************************************
// Assembly         : Stakeholders.Web
// Author           : George
// Created          : 02-19-2017
//
// Last Modified By : George
// Last Modified On : 02-20-2017
// ***********************************************************************
// <copyright file="IDataSource.cs" company="">
//     Copyright (c) . All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************
using System.Linq;
using Stakeholders.Web.Models;
using Microsoft.EntityFrameworkCore;

namespace Stakeholders.Web.Data
{
    /// <summary>
    /// Interface IDataSource
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public interface IDataSource<T> where T : class, IBaseEntity
    {
        /// <summary>
        /// Gets the data queryable.
        /// </summary>
        /// <returns>IQueryable&lt;T&gt;.</returns>
        IQueryable<T> GetDataQueryable();
    }

    /// <summary>
    /// Class RoleDataSource.
    /// </summary>
    /// <seealso cref="Stakeholders.Web.Data.IDataSource{Stakeholders.Web.Models.Role}" />
    public class RoleDataSource : IDataSource<Role>
    {
        /// <summary>
        /// The context
        /// </summary>
        private readonly ApplicationDbContext context;

        /// <summary>
        /// Initializes a new instance of the <see cref="RoleDataSource"/> class.
        /// </summary>
        /// <param name="context">The context.</param>
        public RoleDataSource(ApplicationDbContext context)
        {
            this.context = context;
        }

        /// <summary>
        /// Gets the data queryable.
        /// </summary>
        /// <returns>IQueryable&lt;Role&gt;.</returns>
        public IQueryable<Role> GetDataQueryable()
        {
            return this.context.Roles;
        }
    }

    /// <summary>
    /// Class OrganizationCategoryDataSource.
    /// </summary>
    /// <seealso cref="Stakeholders.Web.Data.IDataSource{Stakeholders.Web.Models.OrganizationCategory}" />
    public class OrganizationCategoryDataSource : IDataSource<OrganizationCategory>
    {
        /// <summary>
        /// The context
        /// </summary>
        private readonly ApplicationDbContext context;

        /// <summary>
        /// Initializes a new instance of the <see cref="OrganizationCategoryDataSource"/> class.
        /// </summary>
        /// <param name="context">The context.</param>
        public OrganizationCategoryDataSource(ApplicationDbContext context)
        {
            this.context = context;
        }

        /// <summary>
        /// Gets the data queryable.
        /// </summary>
        /// <returns>IQueryable&lt;OrganizationCategory&gt;.</returns>
        public IQueryable<OrganizationCategory> GetDataQueryable()
        {
            return this.context.OrganizationCategories
                .Include(it => it.Company);
        }
    }

    /// <summary>
    /// Class OrganizationDataSource.
    /// </summary>
    /// <seealso cref="Stakeholders.Web.Data.IDataSource{Stakeholders.Web.Models.Organization}" />
    public class OrganizationDataSource : IDataSource<Organization>
    {
        /// <summary>
        /// The context
        /// </summary>
        private readonly ApplicationDbContext context;

        /// <summary>
        /// Initializes a new instance of the <see cref="OrganizationDataSource"/> class.
        /// </summary>
        /// <param name="context">The context.</param>
        public OrganizationDataSource(ApplicationDbContext context)
        {
            this.context = context;
        }

        /// <summary>
        /// Gets the data queryable.
        /// </summary>
        /// <returns>IQueryable&lt;Organization&gt;.</returns>
        public IQueryable<Organization> GetDataQueryable()
        {
            return this.context.Organizations
                .Include(it => it.Company)
                .Include(it => it.Category)
                .Include(it => it.Type)
                .Include(it => it.User);
        }
    }

    /// <summary>
    /// Class GoalDataSource.
    /// </summary>
    /// <seealso cref="Stakeholders.Web.Data.IDataSource{Stakeholders.Web.Models.Goal}" />
    public class GoalDataSource : IDataSource<Goal>
    {
        /// <summary>
        /// The context
        /// </summary>
        private readonly ApplicationDbContext context;

        /// <summary>
        /// Initializes a new instance of the <see cref="GoalDataSource"/> class.
        /// </summary>
        /// <param name="context">The context.</param>
        public GoalDataSource(ApplicationDbContext context)
        {
            this.context = context;
        }

        /// <summary>
        /// Gets the data queryable.
        /// </summary>
        /// <returns>IQueryable&lt;Goal&gt;.</returns>
        public IQueryable<Goal> GetDataQueryable()
        {
            return this.context.Goals;
        }
    }

    /// <summary>
    /// Class ActivityTaskStatusDataSource.
    /// </summary>
    /// <seealso cref="Stakeholders.Web.Data.IDataSource{Stakeholders.Web.Models.ActivityTaskStatus}" />
    public class ActivityTaskStatusDataSource : IDataSource<ActivityTaskStatus>
    {
        /// <summary>
        /// The context
        /// </summary>
        private readonly ApplicationDbContext context;

        /// <summary>
        /// Initializes a new instance of the <see cref="ActivityTaskStatusDataSource"/> class.
        /// </summary>
        /// <param name="context">The context.</param>
        public ActivityTaskStatusDataSource(ApplicationDbContext context)
        {
            this.context = context;
        }

        /// <summary>
        /// Gets the data queryable.
        /// </summary>
        /// <returns>IQueryable&lt;ActivityTaskStatus&gt;.</returns>
        public IQueryable<ActivityTaskStatus> GetDataQueryable()
        {
            return this.context.ActivityTaskStatuses;
        }
    }

    /// <summary>
    /// Class ContactDataSource.
    /// </summary>
    /// <seealso cref="Stakeholders.Web.Data.IDataSource{Stakeholders.Web.Models.Contact}" />
    public class ContactDataSource : IDataSource<Contact>
    {
        /// <summary>
        /// The context
        /// </summary>
        private readonly ApplicationDbContext context;

        /// <summary>
        /// Initializes a new instance of the <see cref="ContactDataSource"/> class.
        /// </summary>
        /// <param name="context">The context.</param>
        public ContactDataSource(ApplicationDbContext context)
        {
            this.context = context;
        }

        /// <summary>
        /// Gets the data queryable.
        /// </summary>
        /// <returns>IQueryable&lt;Contact&gt;.</returns>
        public IQueryable<Contact> GetDataQueryable()
        {
            return this.context.Contacts
                .Include(it => it.Company)
                .Include(it => it.Organization)
                .Include(it => it.Tasks)
                    .ThenInclude(it => it.Task)
                .Include(it => it.User);
        }
    }

    /// <summary>
    /// Class OrganizationTypeDataSource.
    /// </summary>
    /// <seealso cref="Stakeholders.Web.Data.IDataSource{Stakeholders.Web.Models.OrganizationType}" />
    public class OrganizationTypeDataSource : IDataSource<OrganizationType>
    {
        /// <summary>
        /// The context
        /// </summary>
        private readonly ApplicationDbContext context;

        /// <summary>
        /// Initializes a new instance of the <see cref="OrganizationTypeDataSource"/> class.
        /// </summary>
        /// <param name="context">The context.</param>
        public OrganizationTypeDataSource(ApplicationDbContext context)
        {
            this.context = context;
        }

        /// <summary>
        /// Gets the data queryable.
        /// </summary>
        /// <returns>IQueryable&lt;OrganizationType&gt;.</returns>
        public IQueryable<OrganizationType> GetDataQueryable()
        {
            return this.context.OrganizationTypes;
        }
    }

    /// <summary>
    /// Class ActivityDataSource.
    /// </summary>
    /// <seealso cref="Stakeholders.Web.Data.IDataSource{Stakeholders.Web.Models.Activity}" />
    public class ActivityDataSource : IDataSource<Activity>
    {
        /// <summary>
        /// The context
        /// </summary>
        private readonly ApplicationDbContext context;

        /// <summary>
        /// Initializes a new instance of the <see cref="ActivityDataSource"/> class.
        /// </summary>
        /// <param name="context">The context.</param>
        public ActivityDataSource(ApplicationDbContext context)
        {
            this.context = context;
        }

        /// <summary>
        /// Gets the data queryable.
        /// </summary>
        /// <returns>IQueryable&lt;Activity&gt;.</returns>
        public IQueryable<Activity> GetDataQueryable()
        {
            return this.context.Activities
                .Include(it => it.Company)
                .Include(it => it.Contact)
                .Include(it => it.ObserverUsers)
                    .ThenInclude(it => it.User)
                .Include(it => it.ObserverCompanies)
                    .ThenInclude(it => it.Company)
                .Include(it => it.Type)
                .Include(it => it.User)
                .Include(it => it.Task);
        }
    }

    /// <summary>
    /// Class ActivityTaskDataSource.
    /// </summary>
    /// <seealso cref="Stakeholders.Web.Data.IDataSource{Stakeholders.Web.Models.ActivityTask}" />
    public class ActivityTaskDataSource : IDataSource<ActivityTask>
    {
        /// <summary>
        /// The context
        /// </summary>
        private readonly ApplicationDbContext context;

        /// <summary>
        /// Initializes a new instance of the <see cref="ActivityTaskDataSource"/> class.
        /// </summary>
        /// <param name="context">The context.</param>
        public ActivityTaskDataSource(ApplicationDbContext context)
        {
            this.context = context;
        }

        /// <summary>
        /// Gets the data queryable.
        /// </summary>
        /// <returns>IQueryable&lt;ActivityTask&gt;.</returns>
        public IQueryable<ActivityTask> GetDataQueryable()
        {
            return this.context.ActivityTasks
                .Include(it => it.AssignTo)
                .Include(it => it.CreatedBy)
                .Include(it => it.ObserverUsers)
                    .ThenInclude(it => it.User)
                .Include(it => it.Contacts)
                    .ThenInclude(it => it.Contact)
                .Include(it => it.Goal)
                .Include(it => it.Status);
        }
    }

    /// <summary>
    /// Class CompanyDataSource.
    /// </summary>
    /// <seealso cref="Stakeholders.Web.Data.IDataSource{Stakeholders.Web.Models.Company}" />
    public class CompanyDataSource : IDataSource<Company>
    {
        /// <summary>
        /// The context
        /// </summary>
        private readonly ApplicationDbContext context;

        /// <summary>
        /// Initializes a new instance of the <see cref="CompanyDataSource"/> class.
        /// </summary>
        /// <param name="context">The context.</param>
        public CompanyDataSource(ApplicationDbContext context)
        {
            this.context = context;
        }

        /// <summary>
        /// Gets the data queryable.
        /// </summary>
        /// <returns>IQueryable&lt;Company&gt;.</returns>
        public IQueryable<Company> GetDataQueryable()
        {
            return this.context.Companies
                .Include(it => it.ObserverActivities)
                    .ThenInclude(it => it.Activity);
        }
    }

    /// <summary>
    /// Class ActivityTypeDataSource.
    /// </summary>
    /// <seealso cref="Stakeholders.Web.Data.IDataSource{Stakeholders.Web.Models.ActivityType}" />
    public class ActivityTypeDataSource : IDataSource<ActivityType>
    {
        /// <summary>
        /// The context
        /// </summary>
        private readonly ApplicationDbContext context;

        /// <summary>
        /// Initializes a new instance of the <see cref="ActivityTypeDataSource"/> class.
        /// </summary>
        /// <param name="context">The context.</param>
        public ActivityTypeDataSource(ApplicationDbContext context)
        {
            this.context = context;
        }

        /// <summary>
        /// Gets the data queryable.
        /// </summary>
        /// <returns>IQueryable&lt;ActivityType&gt;.</returns>
        public IQueryable<ActivityType> GetDataQueryable()
        {
            return this.context.ActivityTypes;
        }
    }

    /// <summary>
    /// Class ApplicationUserDataSource.
    /// </summary>
    /// <seealso cref="Stakeholders.Web.Data.IDataSource{Stakeholders.Web.Models.ApplicationUser}" />
    public class ApplicationUserDataSource : IDataSource<ApplicationUser>
    {
        /// <summary>
        /// The context
        /// </summary>
        private readonly ApplicationDbContext context;

        /// <summary>
        /// Initializes a new instance of the <see cref="ApplicationUserDataSource"/> class.
        /// </summary>
        /// <param name="context">The context.</param>
        public ApplicationUserDataSource(ApplicationDbContext context)
        {
            this.context = context;
        }

        /// <summary>
        /// Gets the data queryable.
        /// </summary>
        /// <returns>IQueryable&lt;ApplicationUser&gt;.</returns>
        public IQueryable<ApplicationUser> GetDataQueryable()
        {
            return this.context.Users
                .Include(it => it.Company)
                .Include(it => it.ObserverActivities)
                .ThenInclude(it => it.Activity)
                .Include(it => it.ObserverTasks)
                .ThenInclude(it => it.Task)
                .Include(it=>it.Role);
        }
    }
}
