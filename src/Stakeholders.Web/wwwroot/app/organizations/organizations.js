'use strict';

angular
    .module('porlaDashboard.organizations', ['ngRoute'])
    .config([
        '$routeProvider', function ($routeProvider) {
            $routeProvider.when('/organizations',
            {
                templateUrl: 'organizations/organizations.html',
                controller: 'organizationsController',
                activetab: 'organizations'
            });
        }
    ])
  .factory('Organization', ['$resource',
    function ($resource) {
        return $resource(
            '/api/Organizations/:id',
            null,
            {
                'update': { method: 'PUT' }
            });
    }])
  .controller('organizationsController',
    [[
        '$scope',
        'Organization',
        'dialogService',
        function ($scope, Organization, dialogService) {
            $scope.search = "";
            $scope.switchView = false;

            function refresh() {
                Organization.query({ start: 0, count: 10, search: $scope.search },
                    function (organizations) {
                        $scope.organizations = organizations;
                    });
            }

            refresh();
            $scope.filter = function () {
                refresh();
            };

            $scope.editOrganization = function (id) {
                $scope.editedOrganization = Organization.get({ id: id });
            };

            $scope.closeEditor = function () {
                $scope.editedOrganization = null;
            };

            $scope.saveEditor = function (event) {
                dialogService.showConfirmationSaveDialog(event,
                    function () {
                        $scope.editedOrganization.$update({ id: $scope.editedOrganization.id },
                            function () {
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
    ]
    ]);