'use strict';

angular
    .module('porlaDashboard.tasks', ['ngRoute'])
    .config([
        '$routeProvider', function ($routeProvider) {
            $routeProvider.when('/tasks',
            {
                templateUrl: 'tasks/tasks.html',
                controller: 'tasksController',
                activetab: 'tasks'
            });
        }
    ])
  .factory('ActivityTask', ['$resource',
    function ($resource) {
        return $resource(
            '/api/ActivityTask/:id',
            null,
            {
                'update': { method: 'PUT' }
            });
    }])
  .controller('tasksController',
    [[
        '$scope',
        'ActivityTask',
        'dialogService',
        function ($scope, ActivityTask, dialogService) {
            $scope.search = "";
            $scope.switchView = false;

            function refresh() {
                ActivityTask.query({ start: 0, count: 10, search: $scope.search },
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