using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Stakeholders.Web.Data;

namespace Stakeholders.Web.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    partial class ApplicationDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
            modelBuilder
                .HasAnnotation("ProductVersion", "1.0.1")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.EntityFrameworkCore.IdentityRoleClaim<long>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("ClaimType");

                    b.Property<string>("ClaimValue");

                    b.Property<long>("RoleId");

                    b.HasKey("Id");

                    b.HasIndex("RoleId");

                    b.ToTable("AspNetRoleClaims");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.EntityFrameworkCore.IdentityUserClaim<long>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("ClaimType");

                    b.Property<string>("ClaimValue");

                    b.Property<long>("UserId");

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserClaims");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.EntityFrameworkCore.IdentityUserLogin<long>", b =>
                {
                    b.Property<string>("LoginProvider");

                    b.Property<string>("ProviderKey");

                    b.Property<string>("ProviderDisplayName");

                    b.Property<long>("UserId");

                    b.HasKey("LoginProvider", "ProviderKey");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserLogins");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.EntityFrameworkCore.IdentityUserRole<long>", b =>
                {
                    b.Property<long>("UserId");

                    b.Property<long>("RoleId");

                    b.HasKey("UserId", "RoleId");

                    b.HasIndex("RoleId");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserRoles");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.EntityFrameworkCore.IdentityUserToken<long>", b =>
                {
                    b.Property<long>("UserId");

                    b.Property<string>("LoginProvider");

                    b.Property<string>("Name");

                    b.Property<string>("Value");

                    b.HasKey("UserId", "LoginProvider", "Name");

                    b.ToTable("AspNetUserTokens");
                });

            modelBuilder.Entity("Stakeholders.Web.Models.Activity", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<long?>("ContactId");

                    b.Property<DateTime?>("DateActivity");

                    b.Property<DateTime>("DateCreated");

                    b.Property<string>("Description");

                    b.Property<string>("Subject");

                    b.Property<long?>("TaskId");

                    b.Property<long?>("TypeId");

                    b.Property<long?>("UserId");

                    b.HasKey("Id");

                    b.HasIndex("ContactId");

                    b.HasIndex("TaskId");

                    b.HasIndex("TypeId");

                    b.HasIndex("UserId");

                    b.ToTable("Activities");
                });

            modelBuilder.Entity("Stakeholders.Web.Models.ActivityObserverUser", b =>
                {
                    b.Property<long>("ActivityId");

                    b.Property<long>("UserId");

                    b.HasKey("ActivityId", "UserId");

                    b.HasIndex("ActivityId");

                    b.HasIndex("UserId");

                    b.ToTable("ActivityObserverUsers");
                });

            modelBuilder.Entity("Stakeholders.Web.Models.ActivityTask", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<long?>("AssignToId");

                    b.Property<long?>("CompanyId");

                    b.Property<long?>("CreatedById");

                    b.Property<DateTime>("DateCreated");

                    b.Property<DateTime>("DateDeadline");

                    b.Property<DateTime>("DateEnd");

                    b.Property<string>("Description");

                    b.Property<long?>("GoalId");

                    b.Property<bool>("IsImportant");

                    b.Property<long?>("StatusId");

                    b.Property<string>("Subject");

                    b.Property<string>("SuccessFactor");

                    b.HasKey("Id");

                    b.HasIndex("AssignToId");

                    b.HasIndex("CompanyId");

                    b.HasIndex("CreatedById");

                    b.HasIndex("GoalId");

                    b.HasIndex("StatusId");

                    b.ToTable("ActivityTasks");
                });

            modelBuilder.Entity("Stakeholders.Web.Models.ActivityTaskContact", b =>
                {
                    b.Property<long>("ContactId");

                    b.Property<long>("TaskId");

                    b.HasKey("ContactId", "TaskId");

                    b.HasIndex("ContactId");

                    b.HasIndex("TaskId");

                    b.ToTable("ActivityTaskContacts");
                });

            modelBuilder.Entity("Stakeholders.Web.Models.ActivityTaskObserverUser", b =>
                {
                    b.Property<long>("UserId");

                    b.Property<long>("TaskId");

                    b.HasKey("UserId", "TaskId");

                    b.HasIndex("TaskId");

                    b.HasIndex("UserId");

                    b.ToTable("ActivityTaskObserverUsers");
                });

            modelBuilder.Entity("Stakeholders.Web.Models.ActivityTaskOrganization", b =>
                {
                    b.Property<long>("OrganizationId");

                    b.Property<long>("TaskId");

                    b.HasKey("OrganizationId", "TaskId");

                    b.HasIndex("OrganizationId");

                    b.HasIndex("TaskId");

                    b.ToTable("ActivityTaskOrganizations");
                });

            modelBuilder.Entity("Stakeholders.Web.Models.ActivityTaskStatus", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Alias");

                    b.Property<string>("Name");

                    b.HasKey("Id");

                    b.ToTable("ActivityTaskStatuses");
                });

            modelBuilder.Entity("Stakeholders.Web.Models.ActivityType", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Name");

                    b.HasKey("Id");

                    b.ToTable("ActivityTypes");
                });

            modelBuilder.Entity("Stakeholders.Web.Models.ApplicationUser", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("AccessFailedCount");

                    b.Property<long?>("CompanyId");

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken();

                    b.Property<string>("Email")
                        .HasAnnotation("MaxLength", 256);

                    b.Property<bool>("EmailConfirmed");

                    b.Property<bool>("LockoutEnabled");

                    b.Property<DateTimeOffset?>("LockoutEnd");

                    b.Property<string>("Name");

                    b.Property<string>("NormalizedEmail")
                        .HasAnnotation("MaxLength", 256);

                    b.Property<string>("NormalizedUserName")
                        .HasAnnotation("MaxLength", 256);

                    b.Property<string>("PasswordHash");

                    b.Property<string>("PhoneNumber");

                    b.Property<bool>("PhoneNumberConfirmed");

                    b.Property<string>("PhotoUrl");

                    b.Property<long?>("RoleId");

                    b.Property<string>("SecurityStamp");

                    b.Property<string>("Title");

                    b.Property<bool>("TwoFactorEnabled");

                    b.Property<string>("UserName")
                        .HasAnnotation("MaxLength", 256);

                    b.HasKey("Id");

                    b.HasIndex("CompanyId");

                    b.HasIndex("NormalizedEmail")
                        .HasName("EmailIndex");

                    b.HasIndex("NormalizedUserName")
                        .IsUnique()
                        .HasName("UserNameIndex");

                    b.HasIndex("RoleId");

                    b.ToTable("AspNetUsers");
                });

            modelBuilder.Entity("Stakeholders.Web.Models.Company", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Address");

                    b.Property<string>("City");

                    b.Property<string>("CompanyCode");

                    b.Property<string>("Email");

                    b.Property<string>("InfluencedBy");

                    b.Property<string>("Influencing");

                    b.Property<string>("LogoUrl");

                    b.Property<string>("Name");

                    b.Property<string>("Phone");

                    b.HasKey("Id");

                    b.ToTable("Companies");
                });

            modelBuilder.Entity("Stakeholders.Web.Models.Contact", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Comments");

                    b.Property<long?>("CompanyId");

                    b.Property<string>("Email");

                    b.Property<string>("NameF");

                    b.Property<string>("NameL");

                    b.Property<long?>("OrganizationId");

                    b.Property<string>("Phone");

                    b.Property<string>("PhotoUrl");

                    b.Property<string>("Title");

                    b.Property<long?>("UserId");

                    b.HasKey("Id");

                    b.HasIndex("CompanyId");

                    b.HasIndex("OrganizationId");

                    b.HasIndex("UserId");

                    b.ToTable("Contacts");
                });

            modelBuilder.Entity("Stakeholders.Web.Models.Goal", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Title");

                    b.HasKey("Id");

                    b.ToTable("Goals");
                });

            modelBuilder.Entity("Stakeholders.Web.Models.Organization", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<long?>("CategoryId");

                    b.Property<long?>("CompanyId");

                    b.Property<string>("InfluencedBy");

                    b.Property<string>("Influencing");

                    b.Property<string>("Name");

                    b.Property<long?>("TypeId");

                    b.Property<long?>("UserId");

                    b.HasKey("Id");

                    b.HasIndex("CategoryId");

                    b.HasIndex("CompanyId");

                    b.HasIndex("TypeId");

                    b.HasIndex("UserId");

                    b.ToTable("Organizations");
                });

            modelBuilder.Entity("Stakeholders.Web.Models.OrganizationCategory", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<long?>("CompanyId");

                    b.Property<string>("IconUrl");

                    b.Property<string>("InfluencedBy");

                    b.Property<string>("Influencing");

                    b.Property<string>("Name");

                    b.HasKey("Id");

                    b.HasIndex("CompanyId");

                    b.ToTable("OrganizationCategories");
                });

            modelBuilder.Entity("Stakeholders.Web.Models.OrganizationType", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Type");

                    b.HasKey("Id");

                    b.ToTable("OrganizationTypes");
                });

            modelBuilder.Entity("Stakeholders.Web.Models.Role", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Alias");

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken();

                    b.Property<string>("Name")
                        .HasAnnotation("MaxLength", 256);

                    b.Property<string>("NormalizedName")
                        .HasAnnotation("MaxLength", 256);

                    b.HasKey("Id");

                    b.HasIndex("NormalizedName")
                        .HasName("RoleNameIndex");

                    b.ToTable("Role");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.EntityFrameworkCore.IdentityRoleClaim<long>", b =>
                {
                    b.HasOne("Stakeholders.Web.Models.Role")
                        .WithMany("Claims")
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.EntityFrameworkCore.IdentityUserClaim<long>", b =>
                {
                    b.HasOne("Stakeholders.Web.Models.ApplicationUser")
                        .WithMany("Claims")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.EntityFrameworkCore.IdentityUserLogin<long>", b =>
                {
                    b.HasOne("Stakeholders.Web.Models.ApplicationUser")
                        .WithMany("Logins")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.EntityFrameworkCore.IdentityUserRole<long>", b =>
                {
                    b.HasOne("Stakeholders.Web.Models.Role")
                        .WithMany("Users")
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("Stakeholders.Web.Models.ApplicationUser")
                        .WithMany("Roles")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Stakeholders.Web.Models.Activity", b =>
                {
                    b.HasOne("Stakeholders.Web.Models.Contact", "Contact")
                        .WithMany("Activities")
                        .HasForeignKey("ContactId")
                        .OnDelete(DeleteBehavior.SetNull);

                    b.HasOne("Stakeholders.Web.Models.ActivityTask", "Task")
                        .WithMany("Activities")
                        .HasForeignKey("TaskId")
                        .OnDelete(DeleteBehavior.SetNull);

                    b.HasOne("Stakeholders.Web.Models.ActivityType", "Type")
                        .WithMany()
                        .HasForeignKey("TypeId");

                    b.HasOne("Stakeholders.Web.Models.ApplicationUser", "User")
                        .WithMany("Activities")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.SetNull);
                });

            modelBuilder.Entity("Stakeholders.Web.Models.ActivityObserverUser", b =>
                {
                    b.HasOne("Stakeholders.Web.Models.Activity", "Activity")
                        .WithMany("ObserverUsers")
                        .HasForeignKey("ActivityId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("Stakeholders.Web.Models.ApplicationUser", "User")
                        .WithMany("ObserverActivities")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Stakeholders.Web.Models.ActivityTask", b =>
                {
                    b.HasOne("Stakeholders.Web.Models.ApplicationUser", "AssignTo")
                        .WithMany("AssignedTasks")
                        .HasForeignKey("AssignToId")
                        .OnDelete(DeleteBehavior.SetNull);

                    b.HasOne("Stakeholders.Web.Models.Company", "Company")
                        .WithMany()
                        .HasForeignKey("CompanyId");

                    b.HasOne("Stakeholders.Web.Models.ApplicationUser", "CreatedBy")
                        .WithMany("CreatedTasks")
                        .HasForeignKey("CreatedById");

                    b.HasOne("Stakeholders.Web.Models.Goal", "Goal")
                        .WithMany("Tasks")
                        .HasForeignKey("GoalId")
                        .OnDelete(DeleteBehavior.SetNull);

                    b.HasOne("Stakeholders.Web.Models.ActivityTaskStatus", "Status")
                        .WithMany()
                        .HasForeignKey("StatusId");
                });

            modelBuilder.Entity("Stakeholders.Web.Models.ActivityTaskContact", b =>
                {
                    b.HasOne("Stakeholders.Web.Models.Contact", "Contact")
                        .WithMany("Tasks")
                        .HasForeignKey("ContactId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("Stakeholders.Web.Models.ActivityTask", "Task")
                        .WithMany("Contacts")
                        .HasForeignKey("TaskId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Stakeholders.Web.Models.ActivityTaskObserverUser", b =>
                {
                    b.HasOne("Stakeholders.Web.Models.ActivityTask", "Task")
                        .WithMany("ObserverUsers")
                        .HasForeignKey("TaskId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("Stakeholders.Web.Models.ApplicationUser", "User")
                        .WithMany("ObserverTasks")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Stakeholders.Web.Models.ActivityTaskOrganization", b =>
                {
                    b.HasOne("Stakeholders.Web.Models.Organization", "Organization")
                        .WithMany("Tasks")
                        .HasForeignKey("OrganizationId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("Stakeholders.Web.Models.ActivityTask", "Task")
                        .WithMany("Organizations")
                        .HasForeignKey("TaskId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Stakeholders.Web.Models.ApplicationUser", b =>
                {
                    b.HasOne("Stakeholders.Web.Models.Company", "Company")
                        .WithMany()
                        .HasForeignKey("CompanyId");

                    b.HasOne("Stakeholders.Web.Models.Role", "Role")
                        .WithMany()
                        .HasForeignKey("RoleId");
                });

            modelBuilder.Entity("Stakeholders.Web.Models.Contact", b =>
                {
                    b.HasOne("Stakeholders.Web.Models.Company", "Company")
                        .WithMany()
                        .HasForeignKey("CompanyId");

                    b.HasOne("Stakeholders.Web.Models.Organization", "Organization")
                        .WithMany("Contacts")
                        .HasForeignKey("OrganizationId")
                        .OnDelete(DeleteBehavior.SetNull);

                    b.HasOne("Stakeholders.Web.Models.ApplicationUser", "User")
                        .WithMany()
                        .HasForeignKey("UserId");
                });

            modelBuilder.Entity("Stakeholders.Web.Models.Organization", b =>
                {
                    b.HasOne("Stakeholders.Web.Models.OrganizationCategory", "Category")
                        .WithMany("Organizations")
                        .HasForeignKey("CategoryId")
                        .OnDelete(DeleteBehavior.SetNull);

                    b.HasOne("Stakeholders.Web.Models.Company", "Company")
                        .WithMany()
                        .HasForeignKey("CompanyId");

                    b.HasOne("Stakeholders.Web.Models.OrganizationType", "Type")
                        .WithMany()
                        .HasForeignKey("TypeId");

                    b.HasOne("Stakeholders.Web.Models.ApplicationUser", "User")
                        .WithMany()
                        .HasForeignKey("UserId");
                });

            modelBuilder.Entity("Stakeholders.Web.Models.OrganizationCategory", b =>
                {
                    b.HasOne("Stakeholders.Web.Models.Company", "Company")
                        .WithMany()
                        .HasForeignKey("CompanyId");
                });
        }
    }
}
