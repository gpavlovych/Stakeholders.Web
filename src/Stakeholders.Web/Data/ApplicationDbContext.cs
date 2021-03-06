﻿// ***********************************************************************
// Assembly         : Stakeholders.Web
// Author           : George
// Created          : 02-16-2017
//
// Last Modified By : George
// Last Modified On : 02-18-2017
// ***********************************************************************
// <copyright file="ApplicationDbContext.cs" company="">
//     Copyright (c) . All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************

using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using Stakeholders.Web.Models;

namespace Stakeholders.Web.Data
{
    /// <summary>
    /// The application database context
    /// </summary>
    /// <seealso cref="Microsoft.AspNetCore.Identity.EntityFrameworkCore.IdentityDbContext{Stakeholders.Web.Models.ApplicationUser, Stakeholders.Web.Models.Role, System.Int64}" />
    /// <seealso cref="Microsoft.AspNetCore.Identity.EntityFrameworkCore.IdentityDbContext{ApplicationUser, Role, string}" />
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser, Role, long>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ApplicationDbContext" /> class.
        /// </summary>
        /// <param name="options">The options.</param>
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        /// <summary>
        /// Gets or sets the activities.
        /// </summary>
        /// <value>The activities.</value>
        public DbSet<Activity> Activities { get; set; }

        /// <summary>
        /// Gets or sets the activity tasks.
        /// </summary>
        /// <value>The activity tasks.</value>
        public DbSet<ActivityTask> ActivityTasks { get; set; }

        /// <summary>
        /// Gets or sets the activity task statuses.
        /// </summary>
        /// <value>The activity task statuses.</value>
        public DbSet<ActivityTaskStatus> ActivityTaskStatuses { get; set; }

        /// <summary>
        /// Gets or sets the activity types.
        /// </summary>
        /// <value>The activity types.</value>
        public DbSet<ActivityType> ActivityTypes { get; set; }

        /// <summary>
        /// Gets or sets the companies.
        /// </summary>
        /// <value>The companies.</value>
        public DbSet<Company> Companies { get; set; }

        /// <summary>
        /// Gets or sets the contacts.
        /// </summary>
        /// <value>The contacts.</value>
        public DbSet<Contact> Contacts { get; set; }

        /// <summary>
        /// Gets or sets the goals.
        /// </summary>
        /// <value>The goals.</value>
        public DbSet<Goal> Goals { get; set; }

        /// <summary>
        /// Gets or sets the organizations.
        /// </summary>
        /// <value>The organizations.</value>
        public DbSet<Organization> Organizations { get; set; }

        /// <summary>
        /// Gets or sets the organization types.
        /// </summary>
        /// <value>The organization types.</value>
        public DbSet<OrganizationType> OrganizationTypes { get; set; }

        /// <summary>
        /// Gets or sets the organization categories.
        /// </summary>
        /// <value>The organization categories.</value>
        public DbSet<OrganizationCategory> OrganizationCategories { get; set; }

        /// <summary>
        /// Gets or sets the activity task contacts.
        /// </summary>
        /// <value>The activity task contacts.</value>
        public DbSet<ActivityTaskContact> ActivityTaskContacts { get; set; }

        /// <summary>
        /// Gets or sets the activity task organizations.
        /// </summary>
        /// <value>The activity task organizations.</value>
        public DbSet<ActivityTaskOrganization> ActivityTaskOrganizations { get; set; }

        /// <summary>
        /// Gets or sets the activity task observer users.
        /// </summary>
        /// <value>The activity task observer users.</value>
        public DbSet<ActivityTaskObserverUser> ActivityTaskObserverUsers { get; set; }

        /// <summary>
        /// Gets or sets the activity observer users.
        /// </summary>
        /// <value>The activity observer users.</value>
        public DbSet<ActivityObserverUser> ActivityObserverUsers { get; set; }

        /// <summary>
        /// Configures the schema needed for the identity framework.
        /// </summary>
        /// <param name="builder">The builder being used to construct the model for this context.</param>
        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            // Customize the ASP.NET Identity model and override the defaults if needed.
            // For example, you can rename the ASP.NET Identity table names and more.
            // Add your customizations after calling base.OnModelCreating(builder);
            var identityRoleEntityBuilder = builder.Entity<Role>();
            identityRoleEntityBuilder.ToTable("Role");

            var applicationUserEntityBuilder = builder.Entity<ApplicationUser>();
            applicationUserEntityBuilder.HasKey(it => it.Id);
            applicationUserEntityBuilder.HasMany(it => it.Activities).WithOne(it => it.User).OnDelete(DeleteBehavior.SetNull);
            applicationUserEntityBuilder.HasMany(it => it.AssignedTasks).WithOne(it => it.AssignTo).OnDelete(DeleteBehavior.SetNull);
            applicationUserEntityBuilder.HasMany(it => it.CreatedTasks).WithOne(it => it.CreatedBy).OnDelete(DeleteBehavior.Restrict);
            applicationUserEntityBuilder.HasMany(it => it.ObserverActivities).WithOne(it => it.User).OnDelete(DeleteBehavior.Cascade);
            applicationUserEntityBuilder.HasMany(it => it.ObserverTasks).WithOne(it => it.User).OnDelete(DeleteBehavior.Cascade);

            var activityEntityBuilder = builder.Entity<Activity>();
            activityEntityBuilder.HasKey(it => it.Id);
            activityEntityBuilder.HasMany(it => it.ObserverUsers).WithOne(it => it.Activity).OnDelete(DeleteBehavior.Cascade);
            //activityEntityBuilder.HasMany(it => it.ObserverCompanies).WithOne(it => it.Activity).OnDelete(DeleteBehavior.Cascade);
            activityEntityBuilder.HasOne(it => it.User).WithMany(it=>it.Activities);
            //activityEntityBuilder.HasOne(it => it.Company);
            activityEntityBuilder.HasOne(it => it.Contact).WithMany(it => it.Activities);
            activityEntityBuilder.HasOne(it => it.Task).WithMany(it => it.Activities);
            activityEntityBuilder.HasOne(it => it.Type);

            var activityTaskEntityBuilder = builder.Entity<ActivityTask>();
            activityTaskEntityBuilder.HasKey(it => it.Id);
            activityTaskEntityBuilder.HasOne(it => it.AssignTo).WithMany(it=>it.AssignedTasks);
            activityTaskEntityBuilder.HasOne(it => it.CreatedBy).WithMany(it=>it.CreatedTasks);
            activityTaskEntityBuilder.HasOne(it => it.Goal).WithMany(it => it.Tasks);
            activityTaskEntityBuilder.HasOne(it => it.Status);
            activityTaskEntityBuilder.HasMany(it => it.Activities).WithOne(it => it.Task).OnDelete(DeleteBehavior.SetNull);
            activityTaskEntityBuilder.HasMany(it => it.Contacts).WithOne(it => it.Task).OnDelete(DeleteBehavior.Cascade);
            activityTaskEntityBuilder.HasMany(it => it.ObserverUsers).WithOne(it => it.Task).OnDelete(DeleteBehavior.Cascade);
            activityTaskEntityBuilder.HasMany(it => it.Organizations).WithOne(it => it.Task).OnDelete(DeleteBehavior.Cascade);

            var activityTypeEntityBuilder = builder.Entity<ActivityType>();
            activityTypeEntityBuilder.HasKey(it => it.Id);

            var companyEntityBuilder = builder.Entity<Company>();
            companyEntityBuilder.HasKey(it => it.Id);

            var contactEntityBuilder = builder.Entity<Contact>();
            contactEntityBuilder.HasKey(it => it.Id);
            contactEntityBuilder.HasOne(it => it.Organization).WithMany(it => it.Contacts);
            contactEntityBuilder.HasMany(it => it.Tasks).WithOne(it => it.Contact).OnDelete(DeleteBehavior.Cascade);
            contactEntityBuilder.HasMany(it => it.Activities).WithOne(it => it.Contact).OnDelete(DeleteBehavior.SetNull);

            var goalEntityBuilder = builder.Entity<Goal>();
            goalEntityBuilder.HasKey(it => it.Id);
            goalEntityBuilder.HasMany(it => it.Tasks).WithOne(it => it.Goal).OnDelete(DeleteBehavior.SetNull);

            var organizationEntityBuilder = builder.Entity<Organization>();
            organizationEntityBuilder.HasKey(it => it.Id);
            organizationEntityBuilder.HasOne(it => it.Category).WithMany(it => it.Organizations);
            organizationEntityBuilder.HasMany(it => it.Contacts).WithOne(it => it.Organization).OnDelete(DeleteBehavior.SetNull);
            organizationEntityBuilder.HasOne(it => it.Company);
            organizationEntityBuilder.HasOne(it => it.Type);
            organizationEntityBuilder.HasOne(it => it.User);
            organizationEntityBuilder.HasMany(it => it.Tasks).WithOne(it => it.Organization).OnDelete(DeleteBehavior.Cascade);

            var organizationTypeEntityBuilder = builder.Entity<OrganizationType>();
            organizationTypeEntityBuilder.HasKey(it => it.Id);

            var organizationCategoryEntityBuilder = builder.Entity<OrganizationCategory>();
            organizationCategoryEntityBuilder.HasKey(it => it.Id);
            organizationCategoryEntityBuilder.HasOne(it => it.Company);
            organizationCategoryEntityBuilder.HasMany(it => it.Organizations).WithOne(it => it.Category).OnDelete(DeleteBehavior.SetNull);

            var activityTaskStatusEntityBuilder = builder.Entity<ActivityTaskStatus>();
            activityTaskStatusEntityBuilder.HasKey(it => it.Id);

            var observerActivityUserEntityBuilder = builder.Entity<ActivityObserverUser>();
            observerActivityUserEntityBuilder.HasKey(it => new {it.ActivityId, it.UserId});
            observerActivityUserEntityBuilder
                .HasOne(bc => bc.Activity)
                .WithMany(b => b.ObserverUsers)
                .HasForeignKey(bc => bc.ActivityId);
            observerActivityUserEntityBuilder
                .HasOne(bc => bc.User)
                .WithMany(b => b.ObserverActivities)
                .HasForeignKey(bc => bc.UserId);

            var activityTaskObserverUserEntityBuilder = builder.Entity<ActivityTaskObserverUser>();
            activityTaskObserverUserEntityBuilder.HasKey(it => new {it.UserId, it.TaskId});
            activityTaskObserverUserEntityBuilder
               .HasOne(bc => bc.Task)
               .WithMany(b => b.ObserverUsers)
               .HasForeignKey(bc => bc.TaskId);
            activityTaskObserverUserEntityBuilder
                .HasOne(bc => bc.User)
                .WithMany(b => b.ObserverTasks)
                .HasForeignKey(bc => bc.UserId);

            var activityTaskContactEntityBuilder = builder.Entity<ActivityTaskContact>();
            activityTaskContactEntityBuilder.HasKey(it => new { it.ContactId, it.TaskId });
            activityTaskContactEntityBuilder
               .HasOne(bc => bc.Task)
               .WithMany(b => b.Contacts)
               .HasForeignKey(bc => bc.TaskId);
            activityTaskContactEntityBuilder
                .HasOne(bc => bc.Contact)
                .WithMany(b => b.Tasks)
                .HasForeignKey(bc => bc.ContactId);

            var activityTaskOrganizationEntityBuilder = builder.Entity<ActivityTaskOrganization>();
            activityTaskOrganizationEntityBuilder.HasKey(it => new { it.OrganizationId, it.TaskId });
            activityTaskOrganizationEntityBuilder
               .HasOne(bc => bc.Task)
               .WithMany(b => b.Organizations)
               .HasForeignKey(bc => bc.TaskId);
            activityTaskOrganizationEntityBuilder
                .HasOne(bc => bc.Organization)
                .WithMany(b => b.Tasks)
                .HasForeignKey(bc => bc.OrganizationId);
        }
    }
}