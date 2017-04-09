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
    .controller('organizationCategoriesController',
    [
        '$scope',
        'OrganizationCategory',
        'dialogService',
        function ($scope, OrganizationCategory, dialogService) {
            $scope.search = "";
            $scope.switchView = false;
            $scope.searchChanged = function (ctrl) {
                $scope.search = ctrl.search;
                refresh();
            };

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

            $scope.editOrganizationCategoryOrganizationChanged = function(value) {
                if ($scope.editedOrganizationCategory) {
                    $scope.editedOrganizationCategory.organizationId = value;
                }
            };
            $scope.editOrganizationCategory = function (id) {
                OrganizationCategory.get({ id: id }, function (category) {
                    $scope.editedOrganizationCategory = category;
                });
            };

            $scope.closeEditor = function () {
                $scope.editedOrganizationCategory = null;
            };

            $scope.saveEditor = function (event) {
                dialogService.showConfirmationSaveDialog(event,
                    function () {
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
                        OrganizationCategory.delete({ id: id },
                            function () {
                                refresh();
                            });
                    },
                    null);
            };
        }
    ]);