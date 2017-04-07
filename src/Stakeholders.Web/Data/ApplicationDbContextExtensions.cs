// ***********************************************************************
// Assembly         : Stakeholders.Web
// Author           : George
// Created          : 02-19-2017
//
// Last Modified By : George
// Last Modified On : 04-07-2017
// ***********************************************************************
// <copyright file="ApplicationDbContextExtensions.cs" company="">
//     Copyright (c) . All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************
using System;
using System.Linq;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Stakeholders.Web.Models;
using Microsoft.AspNetCore.Identity;

namespace Stakeholders.Web.Data
{
    /// <summary>
    /// Class ApplicationDbContextExtensions.
    /// </summary>
    public static class ApplicationDbContextExtensions
    {
        /// <summary>
        /// Ensures the seed data.
        /// </summary>
        /// <param name="context">The context.</param>
        public static void EnsureSeedData(this ApplicationDbContext context)
        {
            var loremLong =
                "על זקוק בחירות הקנאים תנך. בה ארץ כיצד קסאם, מה זאת רוסית פולנית לויקיפדים, כדי על הרוח ולחבר יוצרים. דת החול דפים בגרסה בקר, שתי עמוד ביוני גם, אם הטבע שנתי ייִדיש ארץ. ליום הארץ תיקונים אנא ב, לטיפול לעריכת האנציקלופדיה ויש גם. מה ראשי שנתי סטטיסטיקה ארץ, שמות להפוך והגולשים אתה ב. רבה או הארץ כלשהו, ב ציור ספינות ארץ. הטבע הבהרה מאמרשיחהצפה אם אתה. אל כתב מדינות אגרונומיה, על כתב רשימות לעריכה, שמו קהילה פוליטיקה בויקיפדיה דת. גם חרטומים רב־לשוני היסטוריה אתה, רבה אל הנדסת פוליטיקה. מדע לכאן הבקשה קישורים אל, אנא בהבנה מועמדים ממונרכיה בה.";
            var LoremShort =
                "לוח או הקהילה סטטיסטיקה, רביעי סוציולוגיה שער אל, בקר לחבר המלצת אינטרנט אל. או ויקימדיה אתנולוגיה שמו. ב והוא בגרסה עוד, דת כלשהו לעריכת בקר, בכפוף הבאים על עוד. העזרה לאחרונה ב צעד, מדע אם שונה מוגש, גם היא קסאם הקנאים.";
            var LoremTiny = "זאת או שמות קבלו התוכן.החלה שאלות תנך אל.";

            if (context.AllMigrationsApplied())
            {
                #region Stub Roles

                var role1 = context.FindRoleByNameOrCreate("Admin", "אדמיניסראטור");
                var role2 = context.FindRoleByNameOrCreate("CEO", "מנכ\"ל");
                var role3 = context.FindRoleByNameOrCreate("Manager", "מנהל");

                #endregion

                #region Stub companies

                var company0 = context.FindCompanyByNameOrCreate("פוריה ", "", "", "", "", "", "", "", "");
                var company1 = context.FindCompanyByNameOrCreate(
                    "הר טוב",
                    "HP32323",
                    "hartov.png",
                    "החמניות 12",
                    "תל אביב",
                    "03-3434344",
                    "into@hartov.com",
                    "משפיעים 1" + loremLong,
                    "מושפעים 1" + loremLong);
                var company2 = context.FindCompanyByNameOrCreate(
                    "קוקה קולה",
                    "HP32323",
                    "somecompany.png",
                    "הכלניות 4",
                    "תל אביב",
                    "03-54545555",
                    "into@somecompany.com",
                    "משפיעים 1" + loremLong,
                    "מושפעים 1" + loremLong);

                #endregion

                #region Stub users

                var user1 = context.FindApplicationUserByNameOrCreate("user1", "אוד", "אדמיניסטרטור", role1, company0);
                var user2 = context.FindApplicationUserByNameOrCreate("user2", "עמוס", "מנכ\"ל", role2, company1);
                var user3 = context.FindApplicationUserByNameOrCreate("user3", "דני", "שיווק", role3, company1);
                var user4 = context.FindApplicationUserByNameOrCreate("user4", "אהד", "קשרי לקוחות", role3, company1);
                var user5 = context.FindApplicationUserByNameOrCreate("user5", "ענת", "מכירות", role3, company1);

                #endregion

                #region Stub organization categories

                var organizationCategory1 = context.FindOrganizationCategoryByNameOrCreate(
                        "רגולטורים, רשויות, חברות ממשלתיות",
                        "Governmental.png",
                        company1,
                        "משפיעים 1" + loremLong,
                        "מושפעים 1" + loremLong);
                var organizationCategory2 = context.FindOrganizationCategoryByNameOrCreate(
                    "לקוחות",
                    "Clients.png",
                    company1,
                    "משפיעים 1" + loremLong,
                    "מושפעים 1" + loremLong);
                var organizationCategory3 = context.FindOrganizationCategoryByNameOrCreate(
                    "עובדים",
                    "Employees.png",
                    company1,
                    "משפיעים 1" + loremLong,
                    "מושפעים 1" + loremLong);
                var organizationCategory4 = context.FindOrganizationCategoryByNameOrCreate(
                    "מתחרים",
                    "Competitors.png",
                    company1,
                    "משפיעים 1" + loremLong,
                    "מושפעים 1" + loremLong);
                var organizationCategory5 = context.FindOrganizationCategoryByNameOrCreate(
                    "ספקים ויועצים",
                    "Suppliers.png",
                    company1,
                    "משפיעים 1" + loremLong,
                    "מושפעים 1" + loremLong);
                var organizationCategory6 = context.FindOrganizationCategoryByNameOrCreate(
                    "ארגונים אזרחיים",
                    "NGOs.png",
                    company1,
                    "משפיעים 1" + loremLong,
                    "מושפעים 1" + loremLong);
                var organizationCategory7 = context.FindOrganizationCategoryByNameOrCreate(
                    "קהילה וסביבה",
                    "Community.png",
                    company1,
                    "משפיעים 1" + loremLong,
                    "מושפעים 1" + loremLong);
                var organizationCategory8 = context.FindOrganizationCategoryByNameOrCreate(
                    "מפיצים",
                    "Distributors.png",
                    company1,
                    "משפיעים 1" + loremLong,
                    "מושפעים 1" + loremLong);
                var organizationCategory9 = context.FindOrganizationCategoryByNameOrCreate(
                    "שותפים עסקיים",
                    "Partners.png",
                    company1,
                    "משפיעים 1" + loremLong,
                    "מושפעים 1" + loremLong);
                var organizationCategory10 = context.FindOrganizationCategoryByNameOrCreate(
                    "בעלי מניות",
                    "Share_holders.png",
                    company1,
                    "משפיעים 1" + loremLong,
                    "מושפעים 1" + loremLong);
                var organizationCategory11 = context.FindOrganizationCategoryByNameOrCreate(
                    "תקשורת",
                    "Media.png",
                    company1,
                    "משפיעים 1" + loremLong,
                    "מושפעים 1" + loremLong);
                var organizationCategory12 =
                    context.FindOrganizationCategoryByNameOrCreate(
                        "מכוני מחקר, גופי ידע ואקדמיה",
                        "Research.png",
                        company1,
                        "משפיעים 1" + loremLong,
                        "מושפעים 1" + loremLong);
                var organizationCategory13 = context.FindOrganizationCategoryByNameOrCreate(
                    "נבחרי ציבור",
                    "Elected.png",
                    company1,
                    "משפיעים 1" + loremLong,
                    "מושפעים 1" + loremLong);
                var organizationCategory14 = context.FindOrganizationCategoryByNameOrCreate(
                    "מובילי דעה",
                    "Opinion.png",
                    company1,
                    "משפיעים 1" + loremLong,
                    "מושפעים 1" + loremLong);

                #endregion

                #region Stub organization types

                var organizationType1 = context.FindOrganizationTypeByTypeOrCreate("עיר שכנה");
                var organizationType2 = context.FindOrganizationTypeByTypeOrCreate("ראשות מקומית");
                var organizationType3 = context.FindOrganizationTypeByTypeOrCreate("בית ספר");
                var organizationType4 = context.FindOrganizationTypeByTypeOrCreate("כללי");

                #endregion

                #region Stub organizations

                var organization1 = context.FindOrganizationByNameOrCreate(
                    "גוף 1",
                    organizationCategory1,
                    organizationType1,
                    user3,
                    company1,
                    "משפיעים 1" + loremLong,
                    "מושפעים 1" + loremLong);
                var organization2 = context.FindOrganizationByNameOrCreate(
                    "גוף 2",
                    organizationCategory1,
                    organizationType2,
                    user3,
                    company1,
                    "משפיעים 2" + loremLong,
                    "מושפעים 2" + loremLong);
                var organization3 = context.FindOrganizationByNameOrCreate(
                    "גוף 3",
                    organizationCategory2,
                    organizationType2,
                    user4,
                    company1,
                    "משפיעים 3" + loremLong,
                    "מושפעים 3" + loremLong);
                var organization4 = context.FindOrganizationByNameOrCreate(
                    "גוף 4",
                    organizationCategory2,
                    organizationType3,
                    user4,
                    company1,
                    "משפיעים 4" + loremLong,
                    "מושפעים 4" + loremLong);
                var organization5 = context.FindOrganizationByNameOrCreate(
                    "גוף 5",
                    organizationCategory3,
                    organizationType4,
                    user5,
                    company1,
                    "משפיעים 5" + loremLong,
                    "מושפעים 5" + loremLong);
                var organization6 = context.FindOrganizationByNameOrCreate(
                    "גוף 6",
                    organizationCategory4,
                    organizationType4,
                    user5,
                    company1,
                    "משפיעים 6" + loremLong,
                    "מושפעים 6" + loremLong);

                #endregion

                #region Stub contacts

                var contact1 = context.FindContactByNameFAndNameLOrCreate(
                    "Contact1",
                    "One",
                    "Title A",
                    organization1,
                    "03-23344234",
                    "one@contancs.com",
                    "one.png",
                    user3,
                    "No Comments",
                    company1);
                var contact2 = context.FindContactByNameFAndNameLOrCreate(
                    "Contact2",
                    "Two",
                    "Title B",
                    organization1,
                    "03-23344234",
                    "one@contancs.com",
                    "one.png",
                    user3,
                    "No Comments",
                    company1);
                var contact3 = context.FindContactByNameFAndNameLOrCreate(
                    "Contact3",
                    "Three",
                    "Title C",
                    organization1,
                    "03-23344234",
                    "one@contancs.com",
                    "one.png",
                    user3,
                    "No Comments",
                    company1);
                var contact4 = context.FindContactByNameFAndNameLOrCreate(
                    "Contact4",
                    "Four",
                    "Title D",
                    organization1,
                    "03-23344234",
                    "one@contancs.com",
                    "one.png",
                    user3,
                    "No Comments",
                    company1);
                var contact5 = context.FindContactByNameFAndNameLOrCreate(
                    "Contact5",
                    "Five",
                    "Title E",
                    organization1,
                    "03-23344234",
                    "one@contancs.com",
                    "one.png",
                    user3,
                    "No Comments",
                    company1);
                var contact6 = context.FindContactByNameFAndNameLOrCreate(
                    "Contact6",
                    "Six",
                    "Title F",
                    organization1,
                    "03-23344234",
                    "one@contancs.com",
                    "one.png",
                    user3,
                    "No Comments",
                    company1);
                var contact7 = context.FindContactByNameFAndNameLOrCreate(
                    "Contact7",
                    "Seven",
                    "Title G",
                    organization1,
                    "03-23344234",
                    "one@contancs.com",
                    "one.png",
                    user3,
                    "No Comments",
                    company1);
                var contact8 = context.FindContactByNameFAndNameLOrCreate(
                    "Contact8",
                    "Eight",
                    "Title H",
                    organization1,
                    "03-23344234",
                    "one@contancs.com",
                    "one.png",
                    user3,
                    "No Comments",
                    company1);
                var contact9 = context.FindContactByNameFAndNameLOrCreate(
                    "Contact9",
                    "Nine",
                    "Title I",
                    organization1,
                    "03-23344234",
                    "one@contancs.com",
                    "one.png",
                    user3,
                    "No Comments",
                    company1);
                var contact10 = context.FindContactByNameFAndNameLOrCreate(
                    "Contact10",
                    "Ten",
                    "Title G",
                    organization1,
                    "03-23344234",
                    "one@contancs.com",
                    "one.png",
                    user3,
                    "No Comments",
                    company1);
                var contact11 = context.FindContactByNameFAndNameLOrCreate(
                    "Contact11",
                    "Eleven",
                    "Title K",
                    organization1,
                    "03-23344234",
                    "one@contancs.com",
                    "one.png",
                    user3,
                    "No Comments",
                    company1);
                var contact12 = context.FindContactByNameFAndNameLOrCreate(
                    "Contact12",
                    "Twelve",
                    "Title L",
                    organization1,
                    "03-23344234",
                    "one@contancs.com",
                    "one.png",
                    user3,
                    "No Comments",
                    company1);

                //var contact1 = context.FindContactByNameFAndNameLOrCreate(
                //    "somenamef1",
                //    "somenamel1",
                //    user1,
                //    company1,
                //    organization1);
                //var contact2 = context.FindContactByNameFAndNameLOrCreate(
                //    "somenamef2",
                //    "somenamel2",
                //    user2,
                //    company1,
                //    organization2);

                #endregion

                #region Stub activity task statuses

                var activityTaskStatus1 = context.FindActivityTaskStatusByNameOrCreate(
                    "Awaiting",
                    "לא התחילה",
                    "לא התחילה");
                var activityTaskStatus2 = context.FindActivityTaskStatusByNameOrCreate("InProcess", "בתהליך", "בתהליך");
                var activityTaskStatus3 = context.FindActivityTaskStatusByNameOrCreate("OnHold", "בהמתנה", "בהמתנה");
                var activityTaskStatus4 = context.FindActivityTaskStatusByNameOrCreate("Done", "הסתיימה", "הסתיימה");

                #endregion

                #region Stub activity types

                var activityType1 = context.FindActivityTypeByNameOrCreate("שיחת טלפון");
                var activityType2 = context.FindActivityTypeByNameOrCreate("אימייל");
                var activityType3 = context.FindActivityTypeByNameOrCreate("פגישה");

                #endregion

                #region Stub goals

                var goal1 = context.FindGoalByTitleOrCreate(" מטרה 1 " + LoremTiny);
                var goal2 = context.FindGoalByTitleOrCreate(" מטרה 2 " + LoremTiny);
                var goal3 = context.FindGoalByTitleOrCreate(" מטרה 3 " + LoremTiny);
                var goal4 = context.FindGoalByTitleOrCreate(" מטרה 4 " + LoremTiny);
                var goal5 = context.FindGoalByTitleOrCreate(" מטרה 5 " + LoremTiny);
                var goal6 = context.FindGoalByTitleOrCreate(" מטרה 6 " + LoremTiny);

                #endregion

                #region Stub tasks

                var task1 = context.FindActivityTaskBySubjectOrCreate(
                    "משימה 1",
                    activityTaskStatus1,
                    user2,
                    user3,
                    false,
                    new DateTime(2015, 12, 31),
                    new DateTime(2016, 1, 1),
                    new DateTime(2016, 3, 31),
                    goal1,
                    company1,
                    "מדד הצלחה 1" + @LoremTiny,
                    "תיאור 1" + @LoremShort);
                var task2 = context.FindActivityTaskBySubjectOrCreate(
                    "משימה 2",
                    activityTaskStatus2,
                    user2,
                    user3,
                    false,
                    new DateTime(2015, 12, 31),
                    new DateTime(2016, 4, 1),
                    new DateTime(2016, 7, 31),
                    goal2,
                    company1,
                    "מדד הצלחה 2" + @LoremTiny,
                    "תיאור 2" + @LoremShort);
                var task3 = context.FindActivityTaskBySubjectOrCreate(
                    "משימה 3",
                    activityTaskStatus2,
                    user2,
                    user4,
                    false,
                    new DateTime(2015, 12, 31),
                    new DateTime(2016, 1, 1),
                    new DateTime(2016, 5, 30),
                    goal2,
                    company1,
                    "מדד הצלחה 3" + @LoremTiny,
                    "תיאור 3" + @LoremShort);
                var task4 = context.FindActivityTaskBySubjectOrCreate(
                    "משימה 4",
                    activityTaskStatus3,
                    user2,
                    user4,
                    false,
                    new DateTime(2015, 12, 31),
                    new DateTime(2016, 4, 1),
                    new DateTime(2016, 8, 31),
                    goal3,
                    company1,
                    "מדד הצלחה 4" + @LoremTiny,
                    "תיאור 4" + @LoremShort);
                var task5 = context.FindActivityTaskBySubjectOrCreate(
                    "משימה 5",
                    activityTaskStatus3,
                    user2,
                    user5,
                    false,
                    new DateTime(2015, 12, 31),
                    new DateTime(2016, 8, 1),
                    new DateTime(2016, 11, 30),
                    goal3,
                    company1,
                    "מדד הצלחה 5" + @LoremTiny,
                    "תיאור 5" + @LoremShort);
                var task6 = context.FindActivityTaskBySubjectOrCreate(
                    "משימה 6",
                    activityTaskStatus4,
                    user2,
                    user5,
                    false,
                    new DateTime(2015, 12, 31),
                    new DateTime(2016, 12, 1),
                    new DateTime(2016, 12, 31),
                    goal4,
                    company1,
                    "מדד הצלחה 6" + @LoremTiny,
                    "תיאור 6" + @LoremShort);

                #endregion

                #region Stub activities

                var activity1 = context.FindActivityBySubjectOrCreate(
                    contact1,
                    user3,
                    activityType1,
                    task1,
                    new DateTime(2016, 4, 10),
                    new DateTime(2016, 4, 15),
                    company1,
                    "נושא 1" + @LoremTiny,
                    "תיאור 1" + @LoremShort);
                var activity2 = context.FindActivityBySubjectOrCreate(
                    contact2,
                    user4,
                    activityType1,
                    task1,
                    new DateTime(2016, 6, 10),
                    new DateTime(2016, 6, 15),
                    company1,
                    "נושא 2" + @LoremTiny,
                    "תיאור 2" + @LoremShort);
                var activity3 = context.FindActivityBySubjectOrCreate(
                    contact3,
                    user4,
                    activityType1,
                    task1,
                    new DateTime(2016, 6, 10),
                    new DateTime(2016, 6, 15),
                    company1,
                    "נושא 3" + @LoremTiny,
                    "תיאור 3" + @LoremShort);
                var activity4 = context.FindActivityBySubjectOrCreate(
                    contact4,
                    user4,
                    activityType1,
                    task1,
                    new DateTime(2016, 8, 10),
                    new DateTime(2016, 8, 15),
                    company1,
                    "נושא 4" + @LoremTiny,
                    "תיאור 4" + @LoremShort);
                var activity5 = context.FindActivityBySubjectOrCreate(
                    contact5,
                    user5,
                    activityType1,
                    task1,
                    new DateTime(2016, 8, 10),
                    new DateTime(2016, 8, 15),
                    company1,
                    "נושא 5" + @LoremTiny,
                    "תיאור 5" + @LoremShort);
                var activity6 = context.FindActivityBySubjectOrCreate(
                    contact6,
                    user5,
                    activityType1,
                    task1,
                    new DateTime(2016, 11, 10),
                    new DateTime(2016, 11, 15),
                    company1,
                    "נושא 6" + @LoremTiny,
                    "תיאור 6" + @LoremShort);
                var activity7 = context.FindActivityBySubjectOrCreate(
                    contact7,
                    user5,
                    activityType1,
                    task1,
                    new DateTime(2016, 11, 10),
                    new DateTime(2016, 11, 15),
                    company1,
                    "נושא 7" + @LoremTiny,
                    "תיאור 7" + @LoremShort);
                var activity8 = context.FindActivityBySubjectOrCreate(
                    contact8,
                    user5,
                    activityType1,
                    task1,
                    new DateTime(2016, 11, 10),
                    new DateTime(2016, 11, 15),
                    company1,
                    "נושא 8" + @LoremTiny,
                    "תיאור 8" + @LoremShort);

                #endregion

                #region Stub task observer users

                var taskObserverUser1 = context.CreateTaskObserverUsers(task1, user2);
                var taskObserverUser2 = context.CreateTaskObserverUsers(task2, user2);
                var taskObserverUser3 = context.CreateTaskObserverUsers(task3, user2);
                var taskObserverUser4 = context.CreateTaskObserverUsers(task4, user2);
                var taskObserverUser5 = context.CreateTaskObserverUsers(task5, user2);
                var taskObserverUser6 = context.CreateTaskObserverUsers(task6, user2);
                var taskObserverUser7 = context.CreateTaskObserverUsers(task6, user3);

                #endregion

                #region Stub task contacts

                var taskContact1 = context.CreateTaskContacts(task1, contact1);
                var taskContact2 = context.CreateTaskContacts(task2, contact2);
                var taskContact3 = context.CreateTaskContacts(task3, contact3);
                var taskContact4 = context.CreateTaskContacts(task4, contact4);
                var taskContact5 = context.CreateTaskContacts(task5, contact5);
                var taskContact6 = context.CreateTaskContacts(task6, contact6);
                var taskContact7 = context.CreateTaskContacts(task6, contact7);

                #endregion

                #region Stub task organizations

                var taskOrganization1 = context.CreateTaskOrganizations(task1, organization1);
                var taskOrganization2 = context.CreateTaskOrganizations(task2, organization2);
                var taskOrganization3 = context.CreateTaskOrganizations(task3, organization3);
                var taskOrganization4 = context.CreateTaskOrganizations(task4, organization4);
                var taskOrganization5 = context.CreateTaskOrganizations(task5, organization5);
                var taskOrganization6 = context.CreateTaskOrganizations(task6, organization6);
                var taskOrganization7 = context.CreateTaskOrganizations(task6, organization5);

                #endregion

                #region Stub activity observer users

                var activityUser1 = context.CreateActivityObserverUsers(activity1, user4);
                var activityUser2 = context.CreateActivityObserverUsers(activity1, user5);
                var activityUser3 = context.CreateActivityObserverUsers(activity2, user2);
                var activityUser4 = context.CreateActivityObserverUsers(activity3, user2);

                #endregion

                #region Stub activity observer companies

                var activityCompany1 = context.CreateActivityObserverCompanies(activity1, company1);
                var activityCompany2 = context.CreateActivityObserverCompanies(activity2, company1);
                var activityCompany3 = context.CreateActivityObserverCompanies(activity3, company1);

                #endregion
            }
        }

        /// <summary>
        /// Alls the migrations applied.
        /// </summary>
        /// <param name="context">The context.</param>
        /// <returns><c>true</c> if XXXX, <c>false</c> otherwise.</returns>
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

        /// <summary>
        /// Creates the task organizations.
        /// </summary>
        /// <param name="context">The context.</param>
        /// <param name="task">The task.</param>
        /// <param name="organization">The organization.</param>
        /// <returns>ActivityTaskOrganization.</returns>
        private static ActivityTaskOrganization CreateTaskOrganizations(
            this ApplicationDbContext context,
            ActivityTask task,
            Organization organization)
        {
            var result =
                context.ActivityTaskOrganizations.FirstOrDefault(
                    it => (it.OrganizationId == organization.Id) && (it.TaskId == task.Id));
            if (result == null)
            {
                result = new ActivityTaskOrganization()
                {
                    Task = task,
                    Organization = organization
                };
                context.ActivityTaskOrganizations.Add(result);
                context.SaveChanges();
            }
            return result;
        }

        /// <summary>
        /// Creates the activity observer users.
        /// </summary>
        /// <param name="context">The context.</param>
        /// <param name="activity">The activity.</param>
        /// <param name="user">The user.</param>
        /// <returns>ActivityObserverUser.</returns>
        private static ActivityObserverUser CreateActivityObserverUsers(
          this ApplicationDbContext context,
          Activity activity,
          ApplicationUser user)
        {
            var result =
                context.ActivityObserverUsers.FirstOrDefault(it => (it.ActivityId == activity.Id) && (it.UserId == user.Id));
            if (result == null)
            {
                result = new ActivityObserverUser()
                {
                    Activity = activity,
                    User = user
                };
                context.ActivityObserverUsers.Add(result);
                context.SaveChanges();
            }
            return result;
        }

        /// <summary>
        /// Creates the activity observer companies.
        /// </summary>
        /// <param name="context">The context.</param>
        /// <param name="activity">The activity.</param>
        /// <param name="company">The company.</param>
        /// <returns>ActivityObserverCompany.</returns>
        private static ActivityObserverCompany CreateActivityObserverCompanies(
          this ApplicationDbContext context,
          Activity activity,
          Company company)
        {
            var result =
                context.ActivityObserverCompanies.FirstOrDefault(it => (it.ActivityId == activity.Id) && (it.CompanyId == company.Id));
            if (result == null)
            {
                result = new ActivityObserverCompany()
                {
                    Activity = activity,
                    Company = company
                };
                context.ActivityObserverCompanies.Add(result);
                context.SaveChanges();
            }
            return result;
        }

        /// <summary>
        /// Creates the task observer users.
        /// </summary>
        /// <param name="context">The context.</param>
        /// <param name="task">The task.</param>
        /// <param name="user">The user.</param>
        /// <returns>ActivityTaskObserverUser.</returns>
        private static ActivityTaskObserverUser CreateTaskObserverUsers(
            this ApplicationDbContext context,
            ActivityTask task,
            ApplicationUser user)
        {
            var result =
                context.ActivityTaskObserverUsers.FirstOrDefault(it => (it.TaskId == task.Id) && (it.UserId == user.Id));
            if (result == null)
            {
                result = new ActivityTaskObserverUser()
                {
                    Task = task,
                    User = user
                };
                context.ActivityTaskObserverUsers.Add(result);
                context.SaveChanges();
            }
            return result;
        }

        /// <summary>
        /// Creates the task contacts.
        /// </summary>
        /// <param name="context">The context.</param>
        /// <param name="task">The task.</param>
        /// <param name="contact">The contact.</param>
        /// <returns>ActivityTaskContact.</returns>
        private static ActivityTaskContact CreateTaskContacts(
           this ApplicationDbContext context,
           ActivityTask task,
           Contact contact)
        {
            var result =
                context.ActivityTaskContacts.FirstOrDefault(it => (it.TaskId == task.Id) && (it.ContactId == contact.Id));
            if (result == null)
            {
                result = new ActivityTaskContact()
                {
                    Task = task,
                    Contact = contact
                };
                context.ActivityTaskContacts.Add(result);
                context.SaveChanges();
            }
            return result;
        }

        /// <summary>
        /// Finds the activity by subject or create.
        /// </summary>
        /// <param name="context">The context.</param>
        /// <param name="contact">The contact.</param>
        /// <param name="user">The user.</param>
        /// <param name="activityType">Type of the activity.</param>
        /// <param name="task">The task.</param>
        /// <param name="dateActivity">The date activity.</param>
        /// <param name="dateCreated">The date created.</param>
        /// <param name="company">The company.</param>
        /// <param name="subject">The subject.</param>
        /// <param name="description">The description.</param>
        /// <returns>Activity.</returns>
        private static Activity FindActivityBySubjectOrCreate(
            this ApplicationDbContext context,
            Contact contact,
            ApplicationUser user,
            ActivityType activityType,
            ActivityTask task,
            DateTime dateActivity,
            DateTime dateCreated,
            Company company,
            string subject,
            string description)
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
                    Description = description,
                    DateActivity = dateActivity,
                    DateCreated = dateCreated,
                    Contact = contact,
                    User = user
                };
                context.Activities.Add(result);
                context.SaveChanges();
            }
            else
            {
                result.Company = company;
                result.Task = task;
                result.Type = activityType;
                result.Description = description;
                result.DateActivity = dateActivity;
                result.DateCreated = dateCreated;
                result.Contact = contact;
                result.User = user;
                context.SaveChanges();
            }
            return result;
        }

        /// <summary>
        /// Finds the activity task by subject or create.
        /// </summary>
        /// <param name="context">The context.</param>
        /// <param name="subject">The subject.</param>
        /// <param name="status">The status.</param>
        /// <param name="userCreatedBy">The user created by.</param>
        /// <param name="userAssignTo">The user assign to.</param>
        /// <param name="isImportant">if set to <c>true</c> [is important].</param>
        /// <param name="dateDeadline">The date deadline.</param>
        /// <param name="dateEnd">The date end.</param>
        /// <param name="dateCreated">The date created.</param>
        /// <param name="goal">The goal.</param>
        /// <param name="company">The company.</param>
        /// <param name="successFactor">The success factor.</param>
        /// <param name="description">The description.</param>
        /// <returns>ActivityTask.</returns>
        private static ActivityTask FindActivityTaskBySubjectOrCreate(
            this ApplicationDbContext context,
            string subject,
            ActivityTaskStatus status,
            ApplicationUser userCreatedBy,
            ApplicationUser userAssignTo,
            bool isImportant,
            DateTime dateDeadline, 
            DateTime dateEnd,
            DateTime dateCreated,
            Goal goal,
            Company company,
            string successFactor,
            string description)
        {
            var task1 = context.ActivityTasks.FirstOrDefault(it => it.Subject == subject);
            if (task1 == null)
            {
                task1 = new ActivityTask()
                {
                    AssignTo = userAssignTo,
                    CreatedBy = userCreatedBy,
                    Subject = subject,
                    DateCreated = dateCreated,
                    DateDeadline = dateDeadline,
                    DateEnd = dateEnd,
                    Description = description,
                    IsImportant = isImportant,
                    Status = status,
                    SuccessFactor = successFactor,
                    Company =  company,
                    Goal = goal
                };
                context.ActivityTasks.Add(task1);
                context.SaveChanges();
            }
            return task1;
        }

        /// <summary>
        /// Finds the goal by title or create.
        /// </summary>
        /// <param name="context">The context.</param>
        /// <param name="title">The title.</param>
        /// <returns>Goal.</returns>
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

        /// <summary>
        /// Finds the activity type by name or create.
        /// </summary>
        /// <param name="context">The context.</param>
        /// <param name="name">The name.</param>
        /// <returns>ActivityType.</returns>
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

        /// <summary>
        /// Finds the activity task status by name or create.
        /// </summary>
        /// <param name="context">The context.</param>
        /// <param name="name">The name.</param>
        /// <param name="nameEn">The name en.</param>
        /// <returns>ActivityTaskStatus.</returns>
        private static ActivityTaskStatus FindActivityTaskStatusByNameOrCreate(
            this ApplicationDbContext context,
            string alias,
            string name,
            string nameEn)
        {
            var result =
                context.ActivityTaskStatuses.FirstOrDefault(it => it.Alias == alias);
            if (result == null)
            {
                result = new ActivityTaskStatus()
                {
                    Alias = alias,
                    Name = name
                };
                context.ActivityTaskStatuses.Add(result);
                context.SaveChanges();
            }
            else
            {
                result.Name = name;
                context.SaveChanges();
            }
            return result;
        }

        /// <summary>
        /// Finds the contact by name f and name l or create.
        /// </summary>
        /// <param name="context">The context.</param>
        /// <param name="nameF">The name f.</param>
        /// <param name="nameL">The name l.</param>
        /// <param name="title">The title.</param>
        /// <param name="organization">The organization.</param>
        /// <param name="phone">The phone.</param>
        /// <param name="email">The email.</param>
        /// <param name="photoUrl">The photo URL.</param>
        /// <param name="user">The user.</param>
        /// <param name="comments">The comments.</param>
        /// <param name="company">The company.</param>
        /// <returns>Contact.</returns>
        private static Contact FindContactByNameFAndNameLOrCreate(
            this ApplicationDbContext context,
            string nameF, 
            string nameL, 
            string title, 
            Organization organization, 
            string phone, 
            string email, 
            string photoUrl, 
            ApplicationUser user, 
            string comments, 
            Company company)
        {
            var result = context.Contacts.FirstOrDefault(it => (it.NameF == nameF) && (it.NameL == nameL));
            if (result == null)
            {
                result = new Contact()
                {
                    User = user,
                    Comments = comments,
                    Company = company,
                    Email = email,
                    NameF = nameF,
                    NameL = nameL,
                    Organization = organization,
                    Phone = phone,
                    PhotoUrl = photoUrl,
                    Title = title
                };
                context.Contacts.Add(result);
                context.SaveChanges();
            }
            return result;
        }

        /// <summary>
        /// Finds the organization by name or create.
        /// </summary>
        /// <param name="context">The context.</param>
        /// <param name="name">The name.</param>
        /// <param name="category">The category.</param>
        /// <param name="type">The type.</param>
        /// <param name="user">The user.</param>
        /// <param name="company">The company.</param>
        /// <param name="influencing">The influencing.</param>
        /// <param name="influencedBy">The influenced by.</param>
        /// <returns>Organization.</returns>
        private static Organization FindOrganizationByNameOrCreate(
            this ApplicationDbContext context,
            string name,
            OrganizationCategory category,
            OrganizationType type,
            ApplicationUser user,
            Company company,
            string influencing,
            string influencedBy)
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
                    InfluencedBy = influencing,
                    Influencing = influencedBy,
                    User = user
                };
                context.Organizations.Add(result);
                context.SaveChanges();
            }
            else
            {
                result.Type = type;
                result.Company = company;
                result.Category = category;
                result.InfluencedBy = influencing;
                result.Influencing = influencedBy;
                result.User = user;
                context.SaveChanges();
            }

            return result;
        }

        /// <summary>
        /// Finds the organization type by type or create.
        /// </summary>
        /// <param name="context">The context.</param>
        /// <param name="type">The type.</param>
        /// <returns>OrganizationType.</returns>
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

        /// <summary>
        /// Finds the organization category by name or create.
        /// </summary>
        /// <param name="context">The context.</param>
        /// <param name="name">The name.</param>
        /// <param name="iconUrl">The icon URL.</param>
        /// <param name="company">The company.</param>
        /// <param name="influencing">The influencing.</param>
        /// <param name="influencedBy">The influenced by.</param>
        /// <returns>OrganizationCategory.</returns>
        private static OrganizationCategory FindOrganizationCategoryByNameOrCreate(
            this ApplicationDbContext context,
            string name,
            string iconUrl,
            Company company,
            string influencing,
            string influencedBy)
        {
            var result = context.OrganizationCategories.FirstOrDefault(
                it => it.Name == name);
            if (result == null)
            {
                result = new OrganizationCategory()
                {
                    Name = name,
                    Company = company,
                    IconUrl = iconUrl,
                    Influencing = influencing,
                    InfluencedBy = influencedBy
                };
                context.OrganizationCategories.Add(result);
                context.SaveChanges();
            }
            return result;
        }

        /// <summary>
        /// Finds the application user by name or create.
        /// </summary>
        /// <param name="context">The context.</param>
        /// <param name="name">The name.</param>
        /// <param name="title">The title.</param>
        /// <param name="role">The role.</param>
        /// <param name="company">The company.</param>
        /// <returns>ApplicationUser.</returns>
        private static ApplicationUser FindApplicationUserByNameOrCreate(
            this ApplicationDbContext context,
            string alias,
            string name,
            string title,
            Role role,
            Company company)
        {
            var passwordHash = new PasswordHasher<ApplicationUser>();

            var email = alias + "@example.com";
            var result = context.Users.FirstOrDefault(it => it.Name == name);
            if (result == null)
            {
                result = new ApplicationUser()
                {
                    Role = role,
                    Name = name,
                    Email = email,
                    NormalizedEmail = email,
                    NormalizedUserName = email,
                    UserName = email,
                    Title = title,
                    Company = company
                };
                result.PasswordHash = passwordHash.HashPassword(result, "Password@123");
                context.Users.Add(result);
                context.SaveChanges();
            }
            else
            {
                result.Email = email;
                result.NormalizedUserName = email;
                result.NormalizedEmail = email;
                result.UserName = email; 
                result.PasswordHash = passwordHash.HashPassword(result, "Password@123");
                context.SaveChanges();
            }
            return result;
        }

        /// <summary>
        /// Finds the company by name or create.
        /// </summary>
        /// <param name="context">The context.</param>
        /// <param name="name">The name.</param>
        /// <param name="companyCode">The company code.</param>
        /// <param name="logoUrl">The logo URL.</param>
        /// <param name="address">The address.</param>
        /// <param name="city">The city.</param>
        /// <param name="phone">The phone.</param>
        /// <param name="email">The email.</param>
        /// <param name="influencing">The influencing.</param>
        /// <param name="influencedBy">The influenced by.</param>
        /// <returns>Company.</returns>
        private static Company FindCompanyByNameOrCreate(
            this ApplicationDbContext context,
            string name,
            string companyCode,
            string logoUrl,
            string address,
            string city,
            string phone,
            string email,
            string influencing,
            string influencedBy)
        {
            var result = context.Companies.FirstOrDefault(it => it.Name == name);
            if (result == null)
            {
                result = new Company()
                {
                    Address = address,
                    City = city,
                    CompanyCode = companyCode,
                    Email = email,
                    InfluencedBy = influencedBy,
                    Influencing = influencing,
                    LogoUrl =
                       logoUrl,
                    Name = name,
                    Phone = phone
                };

                context.Companies.Add(result);
                context.SaveChanges();
            }
            return result;
        }

        /// <summary>
        /// Finds the role by name or create.
        /// </summary>
        /// <param name="context">The context.</param>
        /// <param name="name">The name.</param>
        /// <param name="nameEn">The name en.</param>
        /// <returns>Role.</returns>
        private static Role FindRoleByNameOrCreate(this ApplicationDbContext context, string alias, string name)
        {
            var result = context.Roles.FirstOrDefault(it => it.Alias == alias);
            if (result == null)
            {
                result = new Role()
                {
                    Alias = alias,
                    Name = name
                };
                context.Roles.Add(result);
                context.SaveChanges();
            }
            else
            {
                result.Name = name;
                context.SaveChanges();
            }
            return result;
        }
    }
}