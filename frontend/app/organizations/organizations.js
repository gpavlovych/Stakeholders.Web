'use strict';

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
        '$rootScope',
        'Organization',
        'dialogService',
        function ($scope, $rootScope, Organization, dialogService) {
            $scope.search = "";
            $scope.switchView = false;
            $scope.categoryChanged = function(value) {
                $scope.categoryId = value;
                refresh();
            };
            $scope.searchChanged = function(ctrl) {
                $scope.search = ctrl.search;
                refresh();
            }
            $rootScope.$on("refresh", function () {
                refresh();
            });
            function refresh() {
                Organization.query({ start: 0, count: 10, search: $scope.search, organizationCategoryId: $scope.categoryId },
                    function (organizations) {
                        $scope.organizations = organizations;
                    });
            }

            refresh();
            $scope.filter = function () {
                refresh();
            };
            $scope.editOrganizationCategoryChanged = function(value) {
                if ($scope.editedOrganization) {
                    $scope.editedOrganization.categoryId = value;
                }
            };
            $scope.editOrganizationTypeChanged = function(value) {
                if ($scope.editedOrganization) {
                    $scope.editedOrganization.typeId = value;
                }
            };
            $scope.editOrganizationUserChanged = function(value) {
                if ($scope.editedOrganization) {
                    $scope.editedOrganization.userId = value;
                }
            };
            $scope.editOrganization = function (id) {
                 Organization.get({ id: id },
                    function(organization) {
                        $scope.editedOrganization = organization;
                    });
            };

            $scope.closeEditor = function () {
                $scope.editedOrganization = null;
            };

            $scope.saveEditor = function() {
                dialogService.showConfirmationSaveDialog(null,
                    function() {
                        $scope.editedOrganization.$update({ id: $scope.editedOrganization.id },
                            function() {
                                dialogService.showMessageSavedDialog(null, null);
                                refresh();
                            });
                        $scope.editedOrganization = null;
                    },
                    null);
            };

            $scope.removeOrganization = function (id) {
                dialogService.showConfirmationDeleteDialog(null,
                    function () {
                        Organization.delete({ id: id },
                            function () {
                                refresh();
                            });
                    },
                    null);
            };
        }
    ]);