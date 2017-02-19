using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Stakeholders.Web.Data;
using Stakeholders.Web.Models;

namespace Stakeholders.Web
{
    public static class ApplicationDbContextExtensions
    {
        public static void EnsureSeedData(this ApplicationDbContext context)
        {
            if (context.AllMigrationsApplied())
            {
                #region Stub Roles

                var role1 = context.FindRoleByNameOrCreate("somerole");

                #endregion

                #region Stub companies

                var company1 = context.FindCompanyByNameOrCreate("Microsoft");

                #endregion

                #region Stub users

                var user1 = context.FindApplicationUserByNameOrCreate("user1", role1, company1);
                var user2 = context.FindApplicationUserByNameOrCreate("user2", role1, company1);

                #endregion

                #region Stub organizations

                var organizationCategory1 = context.FindOrganizationCategoryByNameOrCreate(
                    "some org category",
                    company1);
                var organizationCategory2 = context.FindOrganizationCategoryByNameOrCreate(
                    "some org category2",
                    company1);

                #endregion

                #region Stub organization types

                var organizationType1 = context.FindOrganizationTypeByTypeOrCreate("some type");
                var organizationType2 = context.FindOrganizationTypeByTypeOrCreate("some type 2");

                #endregion

                #region Stub organizations

                var organization1 = context.FindOrganizationByNameOrCreate(
                    "some org",
                    organizationType1,
                    company1,
                    organizationCategory1,
                    user1);
                var organization2 = context.FindOrganizationByNameOrCreate(
                    "some org 2",
                    organizationType2,
                    company1,
                    organizationCategory2,
                    user2);

                #endregion

                #region Stub contacts

                var contact1 = context.FindContactByNameFAndNameLOrCreate(
                    "somenamef1",
                    "somenamel1",
                    user1,
                    company1,
                    organization1);
                var contact2 = context.FindContactByNameFAndNameLOrCreate(
                    "somenamef2",
                    "somenamel2",
                    user2,
                    company1,
                    organization2);

                #endregion

                #region Stub activity task statuses

                var activityTaskStatusNew = context.FindActivityTaskStatusByNameOrCreate("New");
                var activityTaskStatusDone = context.FindActivityTaskStatusByNameOrCreate("Done");

                #endregion

                #region Stub activity types

                var activityType1 = context.FindActivityTypeByNameOrCreate("My Activity Type1");
                var activityType2 = context.FindActivityTypeByNameOrCreate("My Activity Type2");

                #endregion

                #region Stub goals

                var goal1 = context.FindGoalByTitleOrCreate("My Activity Type1");
                var goal2 = context.FindGoalByTitleOrCreate("My Activity Type1");

                #endregion

                #region Stub tasks

                var task1 = context.FindActivityTaskBySubjectOrCreate(
                    "some subject 1",
                    user1,
                    user2,
                    activityTaskStatusNew,
                    goal1,
                    Enumerable.Repeat(contact1, 1));
                var task2 = context.FindActivityTaskBySubjectOrCreate(
                    "some subject 2",
                    user1,
                    user2,
                    activityTaskStatusDone,
                    goal2,
                    Enumerable.Repeat(contact1, 1));

                #endregion

                #region Stub activities

                context.FindActivityBySubjectOrCreate(
                    "some subject 1",
                    company1,
                    task1,
                    activityType1,
                    contact1,
                    user1,
                    Enumerable.Repeat(user1, 1),
                    Enumerable.Repeat(company1, 1));
                context.FindActivityBySubjectOrCreate(
                    "some subject 2",
                    company1,
                    task2,
                    activityType2,
                    contact2,
                    user2,
                    Enumerable.Repeat(user2, 1),
                    Enumerable.Repeat(company1, 1));

                #endregion
            }
        }

        private static bool AllMigrationsApplied(this ApplicationDbContext context)
        {
            var applied = context.GetService<IHistoryRepository>()
                .GetAppliedMigrations()
                .Select(m => m.MigrationId);

            var total = context.GetService<IMigrationsAssembly>()
                .Migrations
                .Select(m => m.Key);

            return !total.Except(applied).Any();
        }

        private static Activity FindActivityBySubjectOrCreate(
            this ApplicationDbContext context,
            string subject,
            Company company,
            ActivityTask task,
            ActivityType activityType,
            Contact contact,
            ApplicationUser user,
            IEnumerable<ApplicationUser> activityObserverUsers,
            IEnumerable<Company> activityObserverCompanies)
        {
            var result = context.Activities.FirstOrDefault(it => it.Subject == subject);
            if (result == null)
            {
                result = new Activity()
                {
                    Subject = subject,
                    Company = company,
                    Task = task,
                    Type = activityType,
                    Description = "some description1",
                    DateActivity = DateTime.UtcNow,
                    Contact = contact,
                    User = user,
                    ObserverUsersCompanies = activityObserverUsers.Select(
                        it => new ActivityObserverUserCompany()
                        {
                            User = it
                        }).Concat(
                        activityObserverCompanies.Select(
                            it => new ActivityObserverUserCompany()
                            {
                                Company = it
                            })
                    ).ToList()
                };
                context.Activities.Add(result);
                context.SaveChanges();
            }
            return result;
        }

        private static ActivityTask FindActivityTaskBySubjectOrCreate(
            this ApplicationDbContext context,
            string subject,
            ApplicationUser userAssignTo,
            ApplicationUser userCreatedBy,
            ActivityTaskStatus status,
            Goal goal,
            IEnumerable<Contact> contacts)
        {
            var task1 = context.ActivityTasks.FirstOrDefault(it => it.Subject == subject);
            if (task1 == null)
            {
                task1 = new ActivityTask()
                {
                    AssignTo = userAssignTo,
                    CreatedBy = userCreatedBy,
                    Subject = subject,
                    DateDeadline = DateTime.UtcNow.AddDays(4),
                    DateEnd = DateTime.UtcNow.AddDays(3),
                    Description = "some description",
                    IsImportant = true,
                    Status = status,
                    SuccessFactor = "some success factor",
                    Goal = goal,
                    Contacts = contacts.Select(it=>new ActivityTaskContact()
                    {
                        Contact = it
                    }).ToList()
                };
                context.ActivityTasks.Add(task1);
                context.SaveChanges();
            }
            return task1;
        }

        private static Goal FindGoalByTitleOrCreate(
          this ApplicationDbContext context,
          string title)
        {
            var result =
                context.Goals.FirstOrDefault(it => it.Title == title);
            if (result == null)
            {
                result = new Goal()
                {
                    Title = title
                };
                context.Goals.Add(result);
                context.SaveChanges();
            }
            return result;
        }

        private static ActivityType FindActivityTypeByNameOrCreate(
            this ApplicationDbContext context,
            string name)
        {
            var result =
                context.ActivityTypes.FirstOrDefault(it => it.Name == name);
            if (result == null)
            {
                result = new ActivityType()
                {
                    Name = name
                };
                context.ActivityTypes.Add(result);
                context.SaveChanges();
            }
            return result;
        }

        private static ActivityTaskStatus FindActivityTaskStatusByNameOrCreate(
            this ApplicationDbContext context,
            string name)
        {
            var result =
                context.ActivityTaskStatuses.FirstOrDefault(it => it.Name == name);
            if (result == null)
            {
                result = new ActivityTaskStatus()
                {
                    Name = name,
                    NameEn = name
                };
                context.ActivityTaskStatuses.Add(result);
                context.SaveChanges();
            }
            return result;
        }

        private static Contact FindContactByNameFAndNameLOrCreate(
            this ApplicationDbContext context,
            string nameF,
            string nameL,
            ApplicationUser user,
            Company company,
            Organization organization)
        {
            var result = context.Contacts.FirstOrDefault(it => (it.NameF == nameF) && (it.NameL == nameL));
            if (result == null)
            {
                result = new Contact()
                {
                    User = user,
                    Comments = "some comments",
                    Company = company,
                    Email = "someemail@example.com",
                    NameF = nameF,
                    NameL = nameL,
                    Organization = organization,
                    Phone = "9-11",
                    PhotoUrl =
                        "https://encrypted-tbn3.gstatic.com/images?q=tbn:ANd9GcRwyRtEsYBw-PRRscvqnEgD0DvQEbRb3HpHm3pOnWNFvhk9hwS3rgzkbAo",
                    Title = "some title"
                };
                context.Contacts.Add(result);
                context.SaveChanges();
            }
            return result;
        }

        private static Organization FindOrganizationByNameOrCreate(
            this ApplicationDbContext context,
            string name,
            OrganizationType type,
            Company company,
            OrganizationCategory category,
            ApplicationUser user)
        {
            var result = context.Organizations.FirstOrDefault(it => it.Name == name);
            if (result == null)
            {
                result = new Organization()
                {
                    Name = name,
                    Type = type,
                    Company = company,
                    Category = category,
                    InfluencedBy = "sm1",
                    Influencing = "sm2",
                    User = user
                };
                context.Organizations.Add(result);
                context.SaveChanges();
            }
            return result;
        }

        private static OrganizationType FindOrganizationTypeByTypeOrCreate(
            this ApplicationDbContext context,
            string type)
        {
            var result = context.OrganizationTypes.FirstOrDefault(it => it.Type == type);
            if (result == null)
            {
                result = new OrganizationType()
                {
                    Type = type
                };
                context.OrganizationTypes.Add(result);
                context.SaveChanges();
            }
            return result;
        }

        private static OrganizationCategory FindOrganizationCategoryByNameOrCreate(
            this ApplicationDbContext context,
            string name,
            Company company)
        {
            var result = context.OrganizationCategories.FirstOrDefault(
                it => it.Name == name);
            if (result == null)
            {
                result = new OrganizationCategory()
                {
                    Name = name,
                    Company = company,
                    IconUrl = company.LogoUrl,
                    Influencing = "sminf",
                    InfluencedBy = "sb"
                };
                context.OrganizationCategories.Add(result);
                context.SaveChanges();
            }
            return result;
        }

        private static ApplicationUser FindApplicationUserByNameOrCreate(
            this ApplicationDbContext context,
            string name,
            Role role,
            Company company)
        {
            var result = context.Users.FirstOrDefault(it => it.Name == name);
            if (result == null)
            {
                result = new ApplicationUser()
                {
                    Role = role,
                    Name = name,
                    Title = "Mr.",
                    Company = company
                };
                context.Users.Add(result);
                context.SaveChanges();
            }
            return result;
        }

        private static Company FindCompanyByNameOrCreate(this ApplicationDbContext context, string name)
        {
            var result = context.Companies.FirstOrDefault(it => it.Name == name);
            if (result == null)
            {
                result = new Company()
                {
                    Address = "some address",
                    City = "Seattle, WA",
                    CompanyCode = "MS",
                    Email = "myself@example.com",
                    InfluencedBy = "some1",
                    Influencing = "some2",
                    LogoUrl =
                        "https://assets.onestore.ms/cdnfiles/external/uhf/long/9a49a7e9d8e881327e81b9eb43dabc01de70a9bb/images/microsoft-gray.png",
                    Name = name,
                    Phone = "9-11"
                };

                context.Companies.Add(result);
                context.SaveChanges();
            }
            return result;
        }

        private static Role FindRoleByNameOrCreate(this ApplicationDbContext context, string name)
        {
            var result = context.Roles.FirstOrDefault(it => it.Name == name);
            if (result == null)
            {
                result = new Role()
                {
                    Name = name,
                    NameEn = name
                };
                context.Roles.Add(result);
                context.SaveChanges();
            }
            return result;
        }
    }
}