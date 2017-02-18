using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Stakeholders.Web.Models;

namespace Stakeholders.Web.Data
{
    /// <summary>
    /// The application database context
    /// </summary>
    /// <seealso cref="Microsoft.AspNetCore.Identity.EntityFrameworkCore.IdentityDbContext{Stakeholders.Web.Models.ApplicationUser, Stakeholders.Web.Data.Role, System.String}" />
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser, Role, string>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ApplicationDbContext"/> class.
        /// </summary>
        /// <param name="options">The options.</param>
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        /// <summary>
        /// Gets or sets the activities.
        /// </summary>
        /// <value>
        /// The activities.
        /// </value>
        public DbSet<Activity> Activities { get; set; }

        /// <summary>
        /// Gets or sets the activity tasks.
        /// </summary>
        /// <value>
        /// The activity tasks.
        /// </value>
        public DbSet<ActivityTask> ActivityTasks { get; set; }

        /// <summary>
        /// Gets or sets the activity types.
        /// </summary>
        /// <value>
        /// The activity types.
        /// </value>
        public DbSet<ActivityType> ActivityTypes { get; set; }

        /// <summary>
        /// Gets or sets the companies.
        /// </summary>
        /// <value>
        /// The companies.
        /// </value>
        public DbSet<Company> Companies{ get; set; }

        /// <summary>
        /// Gets or sets the contacts.
        /// </summary>
        /// <value>
        /// The contacts.
        /// </value>
        public DbSet<Contact> Contacts { get; set; }

        /// <summary>
        /// Gets or sets the goals.
        /// </summary>
        /// <value>
        /// The goals.
        /// </value>
        public DbSet<Goal> Goals { get; set; }

        /// <summary>
        /// Gets or sets the organizations.
        /// </summary>
        /// <value>
        /// The organizations.
        /// </value>
        public DbSet<Organization> Organizations { get; set; }

        /// <summary>
        /// Gets or sets the organization types.
        /// </summary>
        /// <value>
        /// The organization types.
        /// </value>
        public DbSet<OrganizationType> OrganizationTypes { get; set; }

        /// <summary>
        /// Gets or sets the organization categories.
        /// </summary>
        /// <value>
        /// The organization categories.
        /// </value>
        public DbSet<OrganizationCategory> OrganizationCategories { get; set; }

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
            applicationUserEntityBuilder.HasMany(it => it.ObserverActivities);

            var activityEntityBuilder = builder.Entity<Activity>();
            activityEntityBuilder.HasKey(it => it.Id);
            activityEntityBuilder.HasMany(it => it.ObserverUsers);
            activityEntityBuilder.HasMany(it => it.ObserverCompanies);
            activityEntityBuilder.HasOne(it => it.User);
            activityEntityBuilder.HasOne(it => it.Company);
            activityEntityBuilder.HasOne(it => it.Contact);
            activityEntityBuilder.HasOne(it => it.Task);
            activityEntityBuilder.HasOne(it => it.Type);

            var activityTaskEntityBuilder = builder.Entity<ActivityTask>();
            activityTaskEntityBuilder.HasKey(it => it.Id);
            activityTaskEntityBuilder.HasOne(it => it.AssignTo);
            activityTaskEntityBuilder.HasOne(it => it.CreatedBy);
            activityTaskEntityBuilder.HasOne(it => it.Goal);
            activityTaskEntityBuilder.HasOne(it => it.Status);

            var activityTypeEntityBuilder = builder.Entity<ActivityType>();
            activityTypeEntityBuilder.HasKey(it => it.Id);

            var companyEntityBuilder = builder.Entity<Company>();
            companyEntityBuilder.HasKey(it => it.Id);
            companyEntityBuilder.HasMany(it => it.ObserverActivities);

            var contactEntityBuilder = builder.Entity<Contact>();
            contactEntityBuilder.HasKey(it => it.Id);

            var goalEntityBuilder = builder.Entity<Goal>();
            goalEntityBuilder.HasKey(it => it.Id);

            var organizationEntityBuilder = builder.Entity<Organization>();
            organizationEntityBuilder.HasKey(it => it.Id);
            organizationEntityBuilder.HasOne(it => it.Category);
            organizationEntityBuilder.HasOne(it => it.Company);
            organizationEntityBuilder.HasOne(it => it.Type);
            organizationEntityBuilder.HasOne(it => it.User);

            var organizationTypeEntityBuilder = builder.Entity<OrganizationType>();
            organizationTypeEntityBuilder.HasKey(it => it.Id);

            var organizationCategoryEntityBuilder = builder.Entity<OrganizationCategory>();
            organizationCategoryEntityBuilder.HasKey(it => it.Id);
            organizationCategoryEntityBuilder.HasOne(it => it.Company);
        }
    }
}
