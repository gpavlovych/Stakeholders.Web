﻿'use strict';

angular
    .module('porlaDashboard.organizations', ['ngRoute'])
    .config([
        '$routeProvider', function($routeProvider) {
            $routeProvider.when('/organizations',
            {
                templateUrl: 'organizations/organizations.html',
                controller: 'organizationsController',
                activetab: 'organizations'
            });
        }
    ])
    .factory('Organization',
    [
        '$resource',
        function($resource) {
            return $resource(
                '/api/Organizations/:id',
                null,
                {
                    'update': { method: 'PUT' }
                });
        }
    ])
    .factory('OrganizationType',
    [
        '$resource',
        function($resource) {
            return $resource(
                '/api/OrganizationTypes/:id',
                null,
                {
                    'update': { method: 'PUT' }
                });
        }
    ])
    .factory('OrganizationCategory',
    [
        '$resource',
        function($resource) {
            return $resource(
                '/api/OrganizationCategories/:id',
                null,
                {
                    'update': { method: 'PUT' }
                });
        }
    ])
    .factory('User',
    [
        '$resource',
        function($resource) {
            return $resource(
                '/api/ApplicationUsers/:id',
                null,
                {
                    'update': { method: 'PUT' }
                });
        }
    ])
    .controller('organizationsController',
    [
        '$scope',
        'Organization',
        'OrganizationType',
        'OrganizationCategory',
        'User',
        'dialogService',
        function ($scope, Organization, OrganizationType, OrganizationCategory, User, dialogService) {
            $scope.search = "";
            $scope.switchView = false;

            function refresh() {
                Organization.query({ start: 0, count: 10, search: $scope.search },
                    function (organizations) {
                        for (var index = 0; index < organizations.length; index++) {
                            var organization = organizations[index];
                            organization.type = OrganizationType.get({ id: organization.typeId });
                            organization.category = OrganizationCategory.get({ id: organization.categoryId });
                            organization.user = User.get({ id: organization.userId });
                        }
                        $scope.organizations = organizations;
                    });
            }

            refresh();
            $scope.filter = function () {
                refresh();
            };

            $scope.editOrganization = function (id) {
                $scope.editedOrganizationCategories = OrganizationCategory.query(function(result) {
                    $scope.editedOrganizationCategories = result;
                });
                $scope.editedOrganizationTypes = OrganizationType.query(function (result) {
                    $scope.editedOrganizationTypes = result;
                });
                $scope.editedOrganizationUsers = User.query(function (result) {
                    $scope.editedOrganizationUsers = result;
                });

                $scope.editedOrganization = Organization.get({ id: id },
                    function(result) {

                        $scope.editedOrganizationSelectedCategory = OrganizationCategory
                            .get({ id: result.categoryId },
                                function(result) {
                                    $scope.editedOrganizationSelectedCategory = result;
                                });
                        $scope.editedOrganizationSelectedType = OrganizationType
                            .get({ id: result.typeId },
                                function(result) {
                                    $scope.editedOrganizationSelectedType = result;
                                });
                        $scope.editedOrganizationSelectedUser = User.get({ id: result.userId },
                            function(result) {
                                $scope.editedOrganizationSelectedUser = result;
                            });
                    });
            };

            $scope.closeEditor = function () {
                $scope.editedOrganization = null;
            };

            $scope.saveEditor = function(event) {
                dialogService.showConfirmationSaveDialog(event,
                    function() {
                        $scope.editedOrganization.categoryId =
                            $scope.editedOrganizationSelectedCategory != null
                            ? $scope.editedOrganizationSelectedCategory.id
                            : null;
                        $scope.editedOrganization.typeId =
                            $scope.editedOrganizationSelectedType != null
                            ? $scope.editedOrganizationSelectedType.id
                            : null;
                        $scope.editedOrganization.userId =
                            $scope.editedOrganizationSelectedUser != null
                            ? $scope.editedOrganizationSelectedUser.id
                            : null;
                        $scope.editedOrganization.$update({ id: $scope.editedOrganization.id },
                            function() {
                                dialogService.showMessageSavedDialog(event, null);
                                refresh();
                            });
                        $scope.editedOrganization = null;
                    },
                    null);
            };

            $scope.removeOrganization = function (event, id) {
                dialogService.showConfirmationDeleteDialog(event,
                    function () {
                        $scope.editedOrganization.$remove({ id: id },
                            function () {
                                refresh();
                            });
                    },
                    null);
            };
        }
    ]);