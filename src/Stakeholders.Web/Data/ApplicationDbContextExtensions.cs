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
            if (context.AllMigrationsApplied())
            {
                #region Stub Roles
                //INSERT INTO[dbo].[Roles]([id] ,[name],[name_en]) VALUES(1,'Admin','אדמיניסראטור')
                //INSERT INTO[dbo].[Roles]([id] ,[name],[name_en]) VALUES(2,'CEO','מנכ"ל')
                //INSERT INTO[dbo].[Roles]([id] ,[name],[name_en]) VALUES(3,'Manager','מנהל')
                var role1 = context.FindRoleByNameOrCreate("Admin", "אדמיניסראטור");
                var role2 = context.FindRoleByNameOrCreate("CEO", "מנכ\"ל");
                var role3 = context.FindRoleByNameOrCreate("Admin", "אדמיניסראטור");

                #endregion

                #region Stub companies

                var company0 = context.FindCompanyByNameOrCreate("Poria", "", "", "", "", "", "", "", "");
                var company1 = context.FindCompanyByNameOrCreate("Har Tov", "HP32323", "hartov.png", "somewhere 12", "Tel Aviv", "03-3434344", "into@hartov.com", "Lorem ipsum influencing dolor sit amet, consectetur adipiscing elit, sed do eiusmod tempor incididunt ut labore et dolore magna aliqua. Ut enim ad minim veniam, quis nostrud exercitation ullamco laboris nisi ut aliquip ex ea commodo consequat. Duis aute irure dolor in reprehenderit in voluptate velit esse cillum dolore eu fugiat nulla pariatur. Excepteur sint occaecat cupidatat non proident, sunt in culpa qui officia deserunt mollit anim id est laborum", "Lorem ipsum influencedBy dolor sit amet, consectetur adipiscing elit, sed do eiusmod tempor incididunt ut labore et dolore magna aliqua. Ut enim ad minim veniam, quis nostrud exercitation ullamco laboris nisi ut aliquip ex ea commodo consequat. Duis aute irure dolor in reprehenderit in voluptate velit esse cillum dolore eu fugiat nulla pariatur. Excepteur sint occaecat cupidatat non proident, sunt in culpa qui officia deserunt mollit anim id est laborum");
                var company2= context.FindCompanyByNameOrCreate("Some company", "HP32323", "somecompany.png", "somewhere 4", "Tel Aviv", "03-54545555", "into@somecompany.com", "Lorem ipsum influencing dolor sit amet, consectetur adipiscing elit, sed do eiusmod tempor incididunt ut labore et dolore magna aliqua. Ut enim ad minim veniam, quis nostrud exercitation ullamco laboris nisi ut aliquip ex ea commodo consequat. Duis aute irure dolor in reprehenderit in voluptate velit esse cillum dolore eu fugiat nulla pariatur. Excepteur sint occaecat cupidatat non proident, sunt in culpa qui officia deserunt mollit anim id est laborum", "Lorem ipsum influencedBy dolor sit amet, consectetur adipiscing elit, sed do eiusmod tempor incididunt ut labore et dolore magna aliqua. Ut enim ad minim veniam, quis nostrud exercitation ullamco laboris nisi ut aliquip ex ea commodo consequat. Duis aute irure dolor in reprehenderit in voluptate velit esse cillum dolore eu fugiat nulla pariatur. Excepteur sint occaecat cupidatat non proident, sunt in culpa qui officia deserunt mollit anim id est laborum");

                #endregion

                #region Stub users

                var user1 = context.FindApplicationUserByNameOrCreate("Ud", "Puria admin", role1, company0);
                var user2 = context.FindApplicationUserByNameOrCreate("Amos", "CEO", role2, company1);
                var user3 = context.FindApplicationUserByNameOrCreate("Danny", "Marketing", role3, company1);
                var user4= context.FindApplicationUserByNameOrCreate("Ohad", "Puria admin", role3, company1);
                var user5 = context.FindApplicationUserByNameOrCreate("Anat", "Sales", role3, company1);

                #endregion

                #region Stub organization categories

                var organizationCategory1 = context.FindOrganizationCategoryByNameOrCreate("Governmental","Governmental.png", company1,"Lorem ipsum influencing dolor sit amet, consectetur adipiscing elit, sed do eiusmod tempor incididunt ut labore et dolore magna aliqua. Ut enim ad minim veniam, quis nostrud exercitation ullamco laboris nisi ut aliquip ex ea commodo consequat. Duis aute irure dolor in reprehenderit in voluptate velit esse cillum dolore eu fugiat nulla pariatur. Excepteur sint occaecat cupidatat non proident, sunt in culpa qui officia deserunt mollit anim id est laborum","Lorem ipsum influencedBy dolor sit amet, consectetur adipiscing elit, sed do eiusmod tempor incididunt ut labore et dolore magna aliqua. Ut enim ad minim veniam, quis nostrud exercitation ullamco laboris nisi ut aliquip ex ea commodo consequat. Duis aute irure dolor in reprehenderit in voluptate velit esse cillum dolore eu fugiat nulla pariatur. Excepteur sint occaecat cupidatat non proident, sunt in culpa qui officia deserunt mollit anim id est laborum");
                var organizationCategory2 = context.FindOrganizationCategoryByNameOrCreate("Clients","Clients.png", company1,"Lorem ipsum influencing dolor sit amet, consectetur adipiscing elit, sed do eiusmod tempor incididunt ut labore et dolore magna aliqua. Ut enim ad minim veniam, quis nostrud exercitation ullamco laboris nisi ut aliquip ex ea commodo consequat. Duis aute irure dolor in reprehenderit in voluptate velit esse cillum dolore eu fugiat nulla pariatur. Excepteur sint occaecat cupidatat non proident, sunt in culpa qui officia deserunt mollit anim id est laborum","Lorem ipsum influencedBy dolor sit amet, consectetur adipiscing elit, sed do eiusmod tempor incididunt ut labore et dolore magna aliqua. Ut enim ad minim veniam, quis nostrud exercitation ullamco laboris nisi ut aliquip ex ea commodo consequat. Duis aute irure dolor in reprehenderit in voluptate velit esse cillum dolore eu fugiat nulla pariatur. Excepteur sint occaecat cupidatat non proident, sunt in culpa qui officia deserunt mollit anim id est laborum");
                var organizationCategory3 = context.FindOrganizationCategoryByNameOrCreate("Employees","Employees.png", company1,"Lorem ipsum influencing dolor sit amet, consectetur adipiscing elit, sed do eiusmod tempor incididunt ut labore et dolore magna aliqua. Ut enim ad minim veniam, quis nostrud exercitation ullamco laboris nisi ut aliquip ex ea commodo consequat. Duis aute irure dolor in reprehenderit in voluptate velit esse cillum dolore eu fugiat nulla pariatur. Excepteur sint occaecat cupidatat non proident, sunt in culpa qui officia deserunt mollit anim id est laborum","Lorem ipsum influencedBy dolor sit amet, consectetur adipiscing elit, sed do eiusmod tempor incididunt ut labore et dolore magna aliqua. Ut enim ad minim veniam, quis nostrud exercitation ullamco laboris nisi ut aliquip ex ea commodo consequat. Duis aute irure dolor in reprehenderit in voluptate velit esse cillum dolore eu fugiat nulla pariatur. Excepteur sint occaecat cupidatat non proident, sunt in culpa qui officia deserunt mollit anim id est laborum");
                var organizationCategory4 = context.FindOrganizationCategoryByNameOrCreate("Competitors","Competitors.png", company1,"Lorem ipsum influencing dolor sit amet, consectetur adipiscing elit, sed do eiusmod tempor incididunt ut labore et dolore magna aliqua. Ut enim ad minim veniam, quis nostrud exercitation ullamco laboris nisi ut aliquip ex ea commodo consequat. Duis aute irure dolor in reprehenderit in voluptate velit esse cillum dolore eu fugiat nulla pariatur. Excepteur sint occaecat cupidatat non proident, sunt in culpa qui officia deserunt mollit anim id est laborum","Lorem ipsum influencedBy dolor sit amet, consectetur adipiscing elit, sed do eiusmod tempor incididunt ut labore et dolore magna aliqua. Ut enim ad minim veniam, quis nostrud exercitation ullamco laboris nisi ut aliquip ex ea commodo consequat. Duis aute irure dolor in reprehenderit in voluptate velit esse cillum dolore eu fugiat nulla pariatur. Excepteur sint occaecat cupidatat non proident, sunt in culpa qui officia deserunt mollit anim id est laborum");
                var organizationCategory5 = context.FindOrganizationCategoryByNameOrCreate("Suppliers","Suppliers.png", company1,"Lorem ipsum influencing dolor sit amet, consectetur adipiscing elit, sed do eiusmod tempor incididunt ut labore et dolore magna aliqua. Ut enim ad minim veniam, quis nostrud exercitation ullamco laboris nisi ut aliquip ex ea commodo consequat. Duis aute irure dolor in reprehenderit in voluptate velit esse cillum dolore eu fugiat nulla pariatur. Excepteur sint occaecat cupidatat non proident, sunt in culpa qui officia deserunt mollit anim id est laborum","Lorem ipsum influencedBy dolor sit amet, consectetur adipiscing elit, sed do eiusmod tempor incididunt ut labore et dolore magna aliqua. Ut enim ad minim veniam, quis nostrud exercitation ullamco laboris nisi ut aliquip ex ea commodo consequat. Duis aute irure dolor in reprehenderit in voluptate velit esse cillum dolore eu fugiat nulla pariatur. Excepteur sint occaecat cupidatat non proident, sunt in culpa qui officia deserunt mollit anim id est laborum");
                var organizationCategory6 = context.FindOrganizationCategoryByNameOrCreate("NGOs","NGOs.png", company1,"Lorem ipsum influencing dolor sit amet, consectetur adipiscing elit, sed do eiusmod tempor incididunt ut labore et dolore magna aliqua. Ut enim ad minim veniam, quis nostrud exercitation ullamco laboris nisi ut aliquip ex ea commodo consequat. Duis aute irure dolor in reprehenderit in voluptate velit esse cillum dolore eu fugiat nulla pariatur. Excepteur sint occaecat cupidatat non proident, sunt in culpa qui officia deserunt mollit anim id est laborum","Lorem ipsum influencedBy dolor sit amet, consectetur adipiscing elit, sed do eiusmod tempor incididunt ut labore et dolore magna aliqua. Ut enim ad minim veniam, quis nostrud exercitation ullamco laboris nisi ut aliquip ex ea commodo consequat. Duis aute irure dolor in reprehenderit in voluptate velit esse cillum dolore eu fugiat nulla pariatur. Excepteur sint occaecat cupidatat non proident, sunt in culpa qui officia deserunt mollit anim id est laborum");
                var organizationCategory7 = context.FindOrganizationCategoryByNameOrCreate("Community","Community.png", company1,"Lorem ipsum influencing dolor sit amet, consectetur adipiscing elit, sed do eiusmod tempor incididunt ut labore et dolore magna aliqua. Ut enim ad minim veniam, quis nostrud exercitation ullamco laboris nisi ut aliquip ex ea commodo consequat. Duis aute irure dolor in reprehenderit in voluptate velit esse cillum dolore eu fugiat nulla pariatur. Excepteur sint occaecat cupidatat non proident, sunt in culpa qui officia deserunt mollit anim id est laborum","Lorem ipsum influencedBy dolor sit amet, consectetur adipiscing elit, sed do eiusmod tempor incididunt ut labore et dolore magna aliqua. Ut enim ad minim veniam, quis nostrud exercitation ullamco laboris nisi ut aliquip ex ea commodo consequat. Duis aute irure dolor in reprehenderit in voluptate velit esse cillum dolore eu fugiat nulla pariatur. Excepteur sint occaecat cupidatat non proident, sunt in culpa qui officia deserunt mollit anim id est laborum");
                var organizationCategory8 = context.FindOrganizationCategoryByNameOrCreate("Distributors","Distributors.png", company1,"Lorem ipsum influencing dolor sit amet, consectetur adipiscing elit, sed do eiusmod tempor incididunt ut labore et dolore magna aliqua. Ut enim ad minim veniam, quis nostrud exercitation ullamco laboris nisi ut aliquip ex ea commodo consequat. Duis aute irure dolor in reprehenderit in voluptate velit esse cillum dolore eu fugiat nulla pariatur. Excepteur sint occaecat cupidatat non proident, sunt in culpa qui officia deserunt mollit anim id est laborum","Lorem ipsum influencedBy dolor sit amet, consectetur adipiscing elit, sed do eiusmod tempor incididunt ut labore et dolore magna aliqua. Ut enim ad minim veniam, quis nostrud exercitation ullamco laboris nisi ut aliquip ex ea commodo consequat. Duis aute irure dolor in reprehenderit in voluptate velit esse cillum dolore eu fugiat nulla pariatur. Excepteur sint occaecat cupidatat non proident, sunt in culpa qui officia deserunt mollit anim id est laborum");
                var organizationCategory9 = context.FindOrganizationCategoryByNameOrCreate("Business Partners","Partners.png", company1,"Lorem ipsum influencing dolor sit amet, consectetur adipiscing elit, sed do eiusmod tempor incididunt ut labore et dolore magna aliqua. Ut enim ad minim veniam, quis nostrud exercitation ullamco laboris nisi ut aliquip ex ea commodo consequat. Duis aute irure dolor in reprehenderit in voluptate velit esse cillum dolore eu fugiat nulla pariatur. Excepteur sint occaecat cupidatat non proident, sunt in culpa qui officia deserunt mollit anim id est laborum","Lorem ipsum influencedBy dolor sit amet, consectetur adipiscing elit, sed do eiusmod tempor incididunt ut labore et dolore magna aliqua. Ut enim ad minim veniam, quis nostrud exercitation ullamco laboris nisi ut aliquip ex ea commodo consequat. Duis aute irure dolor in reprehenderit in voluptate velit esse cillum dolore eu fugiat nulla pariatur. Excepteur sint occaecat cupidatat non proident, sunt in culpa qui officia deserunt mollit anim id est laborum");
                var organizationCategory10 = context.FindOrganizationCategoryByNameOrCreate("Share holders","Share_holders.png", company1,"Lorem ipsum influencing dolor sit amet, consectetur adipiscing elit, sed do eiusmod tempor incididunt ut labore et dolore magna aliqua. Ut enim ad minim veniam, quis nostrud exercitation ullamco laboris nisi ut aliquip ex ea commodo consequat. Duis aute irure dolor in reprehenderit in voluptate velit esse cillum dolore eu fugiat nulla pariatur. Excepteur sint occaecat cupidatat non proident, sunt in culpa qui officia deserunt mollit anim id est laborum","Lorem ipsum influencedBy dolor sit amet, consectetur adipiscing elit, sed do eiusmod tempor incididunt ut labore et dolore magna aliqua. Ut enim ad minim veniam, quis nostrud exercitation ullamco laboris nisi ut aliquip ex ea commodo consequat. Duis aute irure dolor in reprehenderit in voluptate velit esse cillum dolore eu fugiat nulla pariatur. Excepteur sint occaecat cupidatat non proident, sunt in culpa qui officia deserunt mollit anim id est laborum");
                var organizationCategory11 = context.FindOrganizationCategoryByNameOrCreate("Media","Media.png", company1,"Lorem ipsum influencing dolor sit amet, consectetur adipiscing elit, sed do eiusmod tempor incididunt ut labore et dolore magna aliqua. Ut enim ad minim veniam, quis nostrud exercitation ullamco laboris nisi ut aliquip ex ea commodo consequat. Duis aute irure dolor in reprehenderit in voluptate velit esse cillum dolore eu fugiat nulla pariatur. Excepteur sint occaecat cupidatat non proident, sunt in culpa qui officia deserunt mollit anim id est laborum","Lorem ipsum influencedBy dolor sit amet, consectetur adipiscing elit, sed do eiusmod tempor incididunt ut labore et dolore magna aliqua. Ut enim ad minim veniam, quis nostrud exercitation ullamco laboris nisi ut aliquip ex ea commodo consequat. Duis aute irure dolor in reprehenderit in voluptate velit esse cillum dolore eu fugiat nulla pariatur. Excepteur sint occaecat cupidatat non proident, sunt in culpa qui officia deserunt mollit anim id est laborum");
                var organizationCategory12 = context.FindOrganizationCategoryByNameOrCreate("Research institutes","Research.png", company1,"Lorem ipsum influencing dolor sit amet, consectetur adipiscing elit, sed do eiusmod tempor incididunt ut labore et dolore magna aliqua. Ut enim ad minim veniam, quis nostrud exercitation ullamco laboris nisi ut aliquip ex ea commodo consequat. Duis aute irure dolor in reprehenderit in voluptate velit esse cillum dolore eu fugiat nulla pariatur. Excepteur sint occaecat cupidatat non proident, sunt in culpa qui officia deserunt mollit anim id est laborum","Lorem ipsum influencedBy dolor sit amet, consectetur adipiscing elit, sed do eiusmod tempor incididunt ut labore et dolore magna aliqua. Ut enim ad minim veniam, quis nostrud exercitation ullamco laboris nisi ut aliquip ex ea commodo consequat. Duis aute irure dolor in reprehenderit in voluptate velit esse cillum dolore eu fugiat nulla pariatur. Excepteur sint occaecat cupidatat non proident, sunt in culpa qui officia deserunt mollit anim id est laborum");
                var organizationCategory13 = context.FindOrganizationCategoryByNameOrCreate("Elected Officials/politicians","Elected.png", company1,"Lorem ipsum influencing dolor sit amet, consectetur adipiscing elit, sed do eiusmod tempor incididunt ut labore et dolore magna aliqua. Ut enim ad minim veniam, quis nostrud exercitation ullamco laboris nisi ut aliquip ex ea commodo consequat. Duis aute irure dolor in reprehenderit in voluptate velit esse cillum dolore eu fugiat nulla pariatur. Excepteur sint occaecat cupidatat non proident, sunt in culpa qui officia deserunt mollit anim id est laborum","Lorem ipsum influencedBy dolor sit amet, consectetur adipiscing elit, sed do eiusmod tempor incididunt ut labore et dolore magna aliqua. Ut enim ad minim veniam, quis nostrud exercitation ullamco laboris nisi ut aliquip ex ea commodo consequat. Duis aute irure dolor in reprehenderit in voluptate velit esse cillum dolore eu fugiat nulla pariatur. Excepteur sint occaecat cupidatat non proident, sunt in culpa qui officia deserunt mollit anim id est laborum");
                var organizationCategory14 = context.FindOrganizationCategoryByNameOrCreate("Opinion leaders","Opinion.png", company1,"Lorem ipsum influencing dolor sit amet, consectetur adipiscing elit, sed do eiusmod tempor incididunt ut labore et dolore magna aliqua. Ut enim ad minim veniam, quis nostrud exercitation ullamco laboris nisi ut aliquip ex ea commodo consequat. Duis aute irure dolor in reprehenderit in voluptate velit esse cillum dolore eu fugiat nulla pariatur. Excepteur sint occaecat cupidatat non proident, sunt in culpa qui officia deserunt mollit anim id est laborum","Lorem ipsum influencedBy dolor sit amet, consectetur adipiscing elit, sed do eiusmod tempor incididunt ut labore et dolore magna aliqua. Ut enim ad minim veniam, quis nostrud exercitation ullamco laboris nisi ut aliquip ex ea commodo consequat. Duis aute irure dolor in reprehenderit in voluptate velit esse cillum dolore eu fugiat nulla pariatur. Excepteur sint occaecat cupidatat non proident, sunt in culpa qui officia deserunt mollit anim id est laborum");

                #endregion

                #region Stub organization types

                var organizationType1 = context.FindOrganizationTypeByTypeOrCreate("Nearby City");
                var organizationType2 = context.FindOrganizationTypeByTypeOrCreate("City Council");
                var organizationType3 = context.FindOrganizationTypeByTypeOrCreate("School");
                var organizationType4 = context.FindOrganizationTypeByTypeOrCreate("General");

                #endregion

                #region Stub organizations

                var organization1 = context.FindOrganizationByNameOrCreate("Org1", organizationCategory1, organizationType1, user3, company1, "Lorem ipsum influencing dolor sit amet, consectetur adipiscing elit, sed do eiusmod tempor incididunt ut labore et dolore magna aliqua. Ut enim ad minim veniam, quis nostrud exercitation ullamco laboris nisi ut aliquip ex ea commodo consequat. Duis aute irure dolor in reprehenderit in voluptate velit esse cillum dolore eu fugiat nulla pariatur. Excepteur sint occaecat cupidatat non proident, sunt in culpa qui officia deserunt mollit anim id est laborum","Lorem ipsum influencedBy dolor sit amet, consectetur adipiscing elit, sed do eiusmod tempor incididunt ut labore et dolore magna aliqua. Ut enim ad minim veniam, quis nostrud exercitation ullamco laboris nisi ut aliquip ex ea commodo consequat. Duis aute irure dolor in reprehenderit in voluptate velit esse cillum dolore eu fugiat nulla pariatur. Excepteur sint occaecat cupidatat non proident, sunt in culpa qui officia deserunt mollit anim id est laborum");
                var organization2 = context.FindOrganizationByNameOrCreate("Org2", organizationCategory1, organizationType2, user3, company1, "Lorem ipsum influencing dolor sit amet, consectetur adipiscing elit, sed do eiusmod tempor incididunt ut labore et dolore magna aliqua. Ut enim ad minim veniam, quis nostrud exercitation ullamco laboris nisi ut aliquip ex ea commodo consequat. Duis aute irure dolor in reprehenderit in voluptate velit esse cillum dolore eu fugiat nulla pariatur. Excepteur sint occaecat cupidatat non proident, sunt in culpa qui officia deserunt mollit anim id est laborum","Lorem ipsum influencedBy dolor sit amet, consectetur adipiscing elit, sed do eiusmod tempor incididunt ut labore et dolore magna aliqua. Ut enim ad minim veniam, quis nostrud exercitation ullamco laboris nisi ut aliquip ex ea commodo consequat. Duis aute irure dolor in reprehenderit in voluptate velit esse cillum dolore eu fugiat nulla pariatur. Excepteur sint occaecat cupidatat non proident, sunt in culpa qui officia deserunt mollit anim id est laborum");
                var organization3 = context.FindOrganizationByNameOrCreate("Org3", organizationCategory2, organizationType2, user4, company1, "Lorem ipsum influencing dolor sit amet, consectetur adipiscing elit, sed do eiusmod tempor incididunt ut labore et dolore magna aliqua. Ut enim ad minim veniam, quis nostrud exercitation ullamco laboris nisi ut aliquip ex ea commodo consequat. Duis aute irure dolor in reprehenderit in voluptate velit esse cillum dolore eu fugiat nulla pariatur. Excepteur sint occaecat cupidatat non proident, sunt in culpa qui officia deserunt mollit anim id est laborum","Lorem ipsum influencedBy dolor sit amet, consectetur adipiscing elit, sed do eiusmod tempor incididunt ut labore et dolore magna aliqua. Ut enim ad minim veniam, quis nostrud exercitation ullamco laboris nisi ut aliquip ex ea commodo consequat. Duis aute irure dolor in reprehenderit in voluptate velit esse cillum dolore eu fugiat nulla pariatur. Excepteur sint occaecat cupidatat non proident, sunt in culpa qui officia deserunt mollit anim id est laborum");
                var organization4 = context.FindOrganizationByNameOrCreate("Org4", organizationCategory2, organizationType3, user4, company1, "Lorem ipsum influencing dolor sit amet, consectetur adipiscing elit, sed do eiusmod tempor incididunt ut labore et dolore magna aliqua. Ut enim ad minim veniam, quis nostrud exercitation ullamco laboris nisi ut aliquip ex ea commodo consequat. Duis aute irure dolor in reprehenderit in voluptate velit esse cillum dolore eu fugiat nulla pariatur. Excepteur sint occaecat cupidatat non proident, sunt in culpa qui officia deserunt mollit anim id est laborum","Lorem ipsum influencedBy dolor sit amet, consectetur adipiscing elit, sed do eiusmod tempor incididunt ut labore et dolore magna aliqua. Ut enim ad minim veniam, quis nostrud exercitation ullamco laboris nisi ut aliquip ex ea commodo consequat. Duis aute irure dolor in reprehenderit in voluptate velit esse cillum dolore eu fugiat nulla pariatur. Excepteur sint occaecat cupidatat non proident, sunt in culpa qui officia deserunt mollit anim id est laborum");
                var organization5 = context.FindOrganizationByNameOrCreate("Org5", organizationCategory3, organizationType4, user5, company1, "Lorem ipsum influencing dolor sit amet, consectetur adipiscing elit, sed do eiusmod tempor incididunt ut labore et dolore magna aliqua. Ut enim ad minim veniam, quis nostrud exercitation ullamco laboris nisi ut aliquip ex ea commodo consequat. Duis aute irure dolor in reprehenderit in voluptate velit esse cillum dolore eu fugiat nulla pariatur. Excepteur sint occaecat cupidatat non proident, sunt in culpa qui officia deserunt mollit anim id est laborum","Lorem ipsum influencedBy dolor sit amet, consectetur adipiscing elit, sed do eiusmod tempor incididunt ut labore et dolore magna aliqua. Ut enim ad minim veniam, quis nostrud exercitation ullamco laboris nisi ut aliquip ex ea commodo consequat. Duis aute irure dolor in reprehenderit in voluptate velit esse cillum dolore eu fugiat nulla pariatur. Excepteur sint occaecat cupidatat non proident, sunt in culpa qui officia deserunt mollit anim id est laborum");
                var organization6 = context.FindOrganizationByNameOrCreate("Org6", organizationCategory4, organizationType4, user5, company1, "Lorem ipsum influencing dolor sit amet, consectetur adipiscing elit, sed do eiusmod tempor incididunt ut labore et dolore magna aliqua. Ut enim ad minim veniam, quis nostrud exercitation ullamco laboris nisi ut aliquip ex ea commodo consequat. Duis aute irure dolor in reprehenderit in voluptate velit esse cillum dolore eu fugiat nulla pariatur. Excepteur sint occaecat cupidatat non proident, sunt in culpa qui officia deserunt mollit anim id est laborum","Lorem ipsum influencedBy dolor sit amet, consectetur adipiscing elit, sed do eiusmod tempor incididunt ut labore et dolore magna aliqua. Ut enim ad minim veniam, quis nostrud exercitation ullamco laboris nisi ut aliquip ex ea commodo consequat. Duis aute irure dolor in reprehenderit in voluptate velit esse cillum dolore eu fugiat nulla pariatur. Excepteur sint occaecat cupidatat non proident, sunt in culpa qui officia deserunt mollit anim id est laborum");

                #endregion

                #region Stub contacts
                var contact1 = context.FindContactByNameFAndNameLOrCreate("Contact1","One", "Title A",organization1,"03-23344234","one@contancs.com","one.png",user3,"No Comments", company1);
                var contact2 = context.FindContactByNameFAndNameLOrCreate("Contact2","Two", "Title B", organization1, "03-23344234","one@contancs.com","one.png", user3, "No Comments", company1);
                var contact3 = context.FindContactByNameFAndNameLOrCreate("Contact3","Three", "Title C", organization1, "03-23344234","one@contancs.com","one.png", user3, "No Comments", company1);
                var contact4 = context.FindContactByNameFAndNameLOrCreate("Contact4","Four", "Title D", organization1, "03-23344234","one@contancs.com","one.png", user3, "No Comments", company1);
                var contact5 = context.FindContactByNameFAndNameLOrCreate("Contact5","Five", "Title E", organization1, "03-23344234","one@contancs.com","one.png", user3, "No Comments", company1);
                var contact6 = context.FindContactByNameFAndNameLOrCreate("Contact6","Six", "Title F", organization1, "03-23344234","one@contancs.com","one.png", user3, "No Comments", company1);
                var contact7 = context.FindContactByNameFAndNameLOrCreate("Contact7","Seven", "Title G", organization1, "03-23344234","one@contancs.com","one.png", user3, "No Comments", company1);
                var contact8 = context.FindContactByNameFAndNameLOrCreate("Contact8","Eight", "Title H", organization1, "03-23344234","one@contancs.com","one.png", user3, "No Comments", company1);
                var contact9 = context.FindContactByNameFAndNameLOrCreate("Contact9","Nine", "Title I", organization1, "03-23344234","one@contancs.com","one.png", user3, "No Comments", company1);
                var contact10 = context.FindContactByNameFAndNameLOrCreate("Contact10","Ten", "Title G", organization1, "03-23344234","one@contancs.com","one.png", user3, "No Comments", company1);
                var contact11 = context.FindContactByNameFAndNameLOrCreate("Contact11","Eleven", "Title K", organization1, "03-23344234","one@contancs.com","one.png", user3, "No Comments", company1);
                var contact12 = context.FindContactByNameFAndNameLOrCreate("Contact12","Twelve", "Title L", organization1, "03-23344234","one@contancs.com","one.png", user3, "No Comments", company1);
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

                var activityTaskStatus1 = context.FindActivityTaskStatusByNameOrCreate("Awaiting","Awaiting");
                var activityTaskStatus2 = context.FindActivityTaskStatusByNameOrCreate("In Process", "In Process");
                var activityTaskStatus3 = context.FindActivityTaskStatusByNameOrCreate("On Hold", "On Hold");
                var activityTaskStatus4 = context.FindActivityTaskStatusByNameOrCreate("Done", "Done");

                #endregion

                #region Stub activity types

                var activityType1 = context.FindActivityTypeByNameOrCreate("Phone call");
                var activityType2 = context.FindActivityTypeByNameOrCreate("Email");
                var activityType3 = context.FindActivityTypeByNameOrCreate("Meeting");

                #endregion

                #region Stub goals

                var goal1 = context.FindGoalByTitleOrCreate("Lorem ipsum Goal1 dolor sit amet, consectetur adipiscing elit, sed do eiusmod tempor");
                var goal2 = context.FindGoalByTitleOrCreate("Lorem ipsum Goal2 dolor sit amet, consectetur adipiscing elit, sed do eiusmod tempor");
                var goal3 = context.FindGoalByTitleOrCreate("Lorem ipsum Goal3 dolor sit amet, consectetur adipiscing elit, sed do eiusmod tempor");
                var goal4 = context.FindGoalByTitleOrCreate("Lorem ipsum Goal4 dolor sit amet, consectetur adipiscing elit, sed do eiusmod tempor");
                var goal5 = context.FindGoalByTitleOrCreate("Lorem ipsum Goal5 dolor sit amet, consectetur adipiscing elit, sed do eiusmod tempor");
                var goal6 = context.FindGoalByTitleOrCreate("Lorem ipsum Goal6 dolor sit amet, consectetur adipiscing elit, sed do eiusmod tempor");

                #endregion

                #region Stub tasks

                var task1 = context.FindActivityTaskBySubjectOrCreate("Task 1", activityTaskStatus1, user2, user3, false, new DateTime(2015,12,31), new DateTime(2016, 1, 1), new DateTime(2016, 3, 31), goal1, company1, "Lorem ipsum successFactor1 dolor sit amet, consectetur adipiscing elit, sed do eiusmod tempor", "Lorem ipsum description1 dolor sit amet, consectetur adipiscing elit, sed do eiusmod tempor");
                var task2 = context.FindActivityTaskBySubjectOrCreate("Task 2", activityTaskStatus2, user2, user3, false, new DateTime(2015, 12, 31), new DateTime(2016, 4, 1), new DateTime(2016, 7, 31), goal2, company1, "Lorem ipsum successFactor2 dolor sit amet, consectetur adipiscing elit, sed do eiusmod tempor", "Lorem ipsum description2 dolor sit amet, consectetur adipiscing elit, sed do eiusmod tempor");
                var task3 = context.FindActivityTaskBySubjectOrCreate("Task 3", activityTaskStatus2, user2, user4, false, new DateTime(2015, 12, 31), new DateTime(2016, 1, 1), new DateTime(2016, 5, 30), goal2, company1, "Lorem ipsum successFactor3 dolor sit amet, consectetur adipiscing elit, sed do eiusmod tempor", "Lorem ipsum description3 dolor sit amet, consectetur adipiscing elit, sed do eiusmod tempor");
                var task4 = context.FindActivityTaskBySubjectOrCreate("Task 4", activityTaskStatus3, user2, user4, false, new DateTime(2015, 12, 31), new DateTime(2016, 4, 1), new DateTime(2016, 8, 31), goal3, company1, "Lorem ipsum successFactor4 dolor sit amet, consectetur adipiscing elit, sed do eiusmod tempor", "Lorem ipsum description4 dolor sit amet, consectetur adipiscing elit, sed do eiusmod tempor");
                var task5 = context.FindActivityTaskBySubjectOrCreate("Task 5", activityTaskStatus3, user2, user5, false, new DateTime(2015, 12, 31), new DateTime(2016, 8, 1), new DateTime(2016, 11, 30), goal3, company1, "Lorem ipsum successFactor5 dolor sit amet, consectetur adipiscing elit, sed do eiusmod tempor", "Lorem ipsum description5 dolor sit amet, consectetur adipiscing elit, sed do eiusmod tempor");
                var task6 = context.FindActivityTaskBySubjectOrCreate("Task 6", activityTaskStatus4, user2, user5, false, new DateTime(2015, 12, 31), new DateTime(2016, 12, 1), new DateTime(2016, 12, 31), goal4, company1, "Lorem ipsum successFactor6 dolor sit amet, consectetur adipiscing elit, sed do eiusmod tempor", "Lorem ipsum description6 dolor sit amet, consectetur adipiscing elit, sed do eiusmod tempor");

                #endregion

                #region Stub activities

                var activity1 = context.FindActivityBySubjectOrCreate(contact1, user3, activityType1, task1, new DateTime(2016, 4, 10), new DateTime(2016, 4, 15), company1, "Lorem ipsum subject1 dolor sit amet, consectetur ", "Lorem ipsum description1 dolor sit amet, consectetur adipiscing elit, sed do eiusmod tempor");
                var activity2 = context.FindActivityBySubjectOrCreate(contact2, user4, activityType1, task1, new DateTime(2016, 6, 10), new DateTime(2016, 6, 15), company1, "Lorem ipsum subject2 dolor sit amet, consectetur ", "Lorem ipsum description2 dolor sit amet, consectetur adipiscing elit, sed do eiusmod tempor");
                var activity3 = context.FindActivityBySubjectOrCreate(contact3, user4, activityType1, task1, new DateTime(2016, 6, 10), new DateTime(2016, 6, 15), company1, "Lorem ipsum subject3 dolor sit amet, consectetur ", "Lorem ipsum description3 dolor sit amet, consectetur adipiscing elit, sed do eiusmod tempor");
                var activity4 = context.FindActivityBySubjectOrCreate(contact4, user4, activityType1, task1, new DateTime(2016, 8, 10), new DateTime(2016, 8, 15), company1, "Lorem ipsum subject4 dolor sit amet, consectetur ", "Lorem ipsum description4 dolor sit amet, consectetur adipiscing elit, sed do eiusmod tempor");
                var activity5 = context.FindActivityBySubjectOrCreate(contact5, user5, activityType1, task1, new DateTime(2016, 8, 10), new DateTime(2016, 8, 15), company1, "Lorem ipsum subject5 dolor sit amet, consectetur ", "Lorem ipsum description5 dolor sit amet, consectetur adipiscing elit, sed do eiusmod tempor");
                var activity6 = context.FindActivityBySubjectOrCreate(contact6, user5, activityType1, task1, new DateTime(2016, 11, 10), new DateTime(2016, 11, 15), company1, "Lorem ipsum subject6 dolor sit amet, consectetur ", "Lorem ipsum description6 dolor sit amet, consectetur adipiscing elit, sed do eiusmod tempor");
                var activity7 = context.FindActivityBySubjectOrCreate(contact7, user5, activityType1, task1, new DateTime(2016, 11, 10), new DateTime(2016, 11, 15), company1, "Lorem ipsum subject7 dolor sit amet, consectetur ", "Lorem ipsum description7 dolor sit amet, consectetur adipiscing elit, sed do eiusmod tempor");
                var activity8 = context.FindActivityBySubjectOrCreate(contact8, user5, activityType1, task1, new DateTime(2016, 11, 10), new DateTime(2016, 11, 15), company1, "Lorem ipsum subject8 dolor sit amet, consectetur ", "Lorem ipsum description8 dolor sit amet, consectetur adipiscing elit, sed do eiusmod tempor");

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
            string name,
            string nameEn)
        {
            var result =
                context.ActivityTaskStatuses.FirstOrDefault(it => it.Name == name);
            if (result == null)
            {
                result = new ActivityTaskStatus()
                {
                    Name = name,
                    NameEn = nameEn
                };
                context.ActivityTaskStatuses.Add(result);
                context.SaveChanges();
            }
            else
            {
                result.NameEn = nameEn;
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
            string name,
            string title,
            Role role,
            Company company)
        {
            var passwordHash = new PasswordHasher<ApplicationUser>();

            var email = name + "@example.com";
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
        private static Role FindRoleByNameOrCreate(this ApplicationDbContext context, string name, string nameEn)
        {
            var result = context.Roles.FirstOrDefault(it => it.Name == name);
            if (result == null)
            {
                result = new Role()
                {
                    Name = name,
                    NameEn = nameEn
                };
                context.Roles.Add(result);
                context.SaveChanges();
            }
            else
            {
                result.NameEn = nameEn;
                context.SaveChanges();
            }
            return result;
        }
    }
}