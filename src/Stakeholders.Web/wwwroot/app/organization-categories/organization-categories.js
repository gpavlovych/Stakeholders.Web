'use strict';

angular
    .module('porlaDashboard.categories', ['ngRoute'])
    .config([
        '$routeProvider', function($routeProvider) {
            $routeProvider.when('/organization-categories',
            {
                templateUrl: 'organization-categories/organization-categories.html',
                controller: 'organizationCategoriesController',
                activetab: 'organization-categories'
            });
        }
    ])
    .factory('OrganizationCategory', ['$resource',
        function($resource) {
            return $resource(
                '/api/OrganizationCategories/:id',
                null,
                {
                    'update': { method: 'PUT' }
                });
        }
    ])
    .factory('Company', ['$resource',
        function($resource) {
            return $resource(
                '/api/Companies/:id',
                null,
                {
                    'update': { method: 'PUT' }
                });
        }
    ])
    .controller('organizationCategoriesController',
    [
        '$scope',
        'OrganizationCategory',
        'Company',
        'dialogService',
        function ($scope, OrganizationCategory, Company, dialogService) {
            $scope.search = "";
            $scope.switchView = false;

            function refresh() {
                OrganizationCategory.query({ start: 0, count: 10, search: $scope.search },
                    function (organizationCategories) {
                        $scope.organizationCategories = organizationCategories;
                    });
            }

            refresh();
            $scope.filter = function () {
                refresh();
            };

            $scope.editedOrganizationCategory = null;
            $scope.editOrganizationCategory = function (id) {
                Company.query(function (result) {
                    $scope.editedOrganizationCategoryCompanies = result;
                });
                OrganizationCategory.get({ id: id }, function (category) {
                    Company.get({ id: category.companyId }, function (company) {
                        category.company = company;
                    });
                    $scope.editedOrganizationCategory = category;
                });
            };

            $scope.closeEditor = function () {
                $scope.editedOrganizationCategory = null;
            };

            $scope.saveEditor = function (event) {
                dialogService.showConfirmationSaveDialog(event,
                    function () {
                        $scope.editedOrganizationCategory.companyId =
                            $scope.editedOrganizationCategory.company != null
                            ? $scope.editedOrganizationCategory.company.id
                            : null;
                        $scope.editedOrganizationCategory.$update({ id: $scope.editedOrganizationCategory.id },
                            function () {
                                dialogService.showMessageSavedDialog(event, null);
                                refresh();
                            });
                        $scope.editedOrganizationCategory = null;
                    },
                    null);
            };

            $scope.removeOrganizationCategory = function (id) {
                dialogService.showConfirmationDeleteDialog(null,
                    function () {
                        $scope.editedOrganizationCategory.$remove({ id: id },
                            function () {
                                refresh();
                            });
                    },
                    null);
            };
        }
    ]);