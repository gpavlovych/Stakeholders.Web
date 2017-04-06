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
                '/api/ActivityTasks/:id',
                null,
                {
                    'update': { method: 'PUT' }
                });
        }])
    .factory('ActivityTaskStatus', ['$resource',
        function ($resource) {
            return $resource('/api/ActivityTaskStatuses/:id',
                        null,
                        {
                            'update': { method: 'PUT' }
                        });
        }])
    .factory('User', ['$resource',
        function ($resource) {
            return $resource('/api/ApplicationUsers/:id',
                        null,
                        {
                            'update': { method: 'PUT' }
                        });
        }])
    .factory('Contact', ['$resource',
        function ($resource) {
            return $resource('/api/Contacts/:id',
                        null,
                        {
                            'update': { method: 'PUT' }
                        });
        }])
    .factory('Organization', ['$resource',
        function ($resource) {
            return  $resource('/api/Organizations/:id',
                        null,
                        {
                            'update': { method: 'PUT' }
                        });
        }])
    .factory('Goal', ['$resource',
        function ($resource) {
            return $resource('/api/Goals/:id',
                        null,
                        {
                            'update': { method: 'PUT' }
                        });
        }])
    .controller('tasksController', [
            '$scope',
            '$rootScope',
            'ActivityTask',
            'ActivityTaskStatus', 
            'User', 
            'Contact', 
            'Organization',
            'Goal', 
            'dialogService',
        function (
            $scope, 
            $rootScope, 
            ActivityTask, 
            ActivityTaskStatus, 
            User, 
            Contact, 
            Organization, 
            Goal, 
            dialogService) {
            
            $scope.search = "";
            $scope.switchView = false;

            function refresh() {
                ActivityTask.query({ start: 0, count: 10, search: $scope.search },
                    function (tasks) {
                        for (var index = 0; index < tasks.length; index++) {
                            var task = tasks[index];
                            task.goal = Goal.get({ id: task.goalId });
                            task.status = ActivityTaskStatus.get({ id: task.statusId });
                        }
                        $scope.tasks = tasks;
                    });
            }

            refresh();
            $rootScope.$on("refreshActivityTasks", function () {
                refresh();
            });
            $scope.filter = function () {
                refresh();
            };

            $scope.editTask = function (id) {
                $scope.editedTask = ActivityTask.get({ id: id });
            };

            $scope.closeEditor = function () {
                $scope.editedTask = null;
            };

            $scope.saveEditor = function (event) {
                dialogService.showConfirmationSaveDialog(event,
                    function () {
                        $scope.editedTask.$update({ id: $scope.editedTask.id },
                            function () {
                                dialogService.showMessageSavedDialog(event, null);
                                refresh();
                            });
                        $scope.editedTask = null;
                    },
                    null);
            };

            $scope.removeTask = function (id) {
                dialogService.showConfirmationDeleteDialog(null,
                    function () {
                        $scope.editedTask.$remove({ id: id },
                            function () {
                                refresh();
                            });
                    },
                    null);
            };
        }
    ]);