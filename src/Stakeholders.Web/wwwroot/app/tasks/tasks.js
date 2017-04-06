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
                ActivityTaskStatus.query(function (result) {
                    $scope.editedTaskStatuses = result;
                });
                Goal.query(function (result) {
                    $scope.editedTaskGoals = result;
                });
                Organization.query(function (result) {
                    $scope.editedTaskOrganizations = result;
                });
                Contact.query(function (result) {
                    $scope.editedTaskContacts = result;
                });
                User.query(function (result) {
                    $scope.editedTaskUsers = result;
                });
                ActivityTask.get({ id: id },
                   function (task) {
                       ActivityTaskStatus.get({ id: task.statusId },
                               function (status) {
                                   task.status = status;
                               });
                       Goal.get({ id: task.goalId },
                               function (goal) {
                                   task.goal = goal;
                               });
                       User.get({ id: task.userId },
                           function (user) {
                               task.user = user;
                           });
                       task.dateEndDate = Date.parse(task.dateEnd);
                       task.dateDeadlineDate = Date.parse(task.dateDeadline);
                       $scope.editedTaskSelectedOrganizations = [];
                       $scope.editedTaskSelectedContacts = [];
                       if (task.contactIds != null) {
                           for (var index = 0; index < task.contactIds.length; index++) {
                               var contactId = task.contactIds[index];
                               Contact.get({ id: contactId },
                                   function(contact) {
                                       $scope.editedTaskSelectedContacts.push(contact);
                                   });
                           }
                       }
                       $scope.editedTaskSelectedObserverUsers = [];
                       if (task.observerUserId != null) {
                           for (var index = 0; index < task.observerUserIds.length; index++) {
                               var userId = task.observerUserIds[index];
                               User.get({ id: userId },
                                   function(user) {
                                       $scope.editedTaskSelectedObserverUsers.push(user);
                                   });
                           }
                       }
                       $scope.editedTask = task;
                   });
            };

            $scope.closeEditor = function () {
                $scope.editedTask = null;
            };

            $scope.saveEditor = function (event) {
                dialogService.showConfirmationSaveDialog(event,
                    function () {
                        $scope.editedTask.dateEnd = $scope.editedTask.dateEndDate;
                        $scope.editedTask.dateDeadline = $scope.editedTask.dateDeadlineDate;
                        $scope.editedTask.statusId =
                                                  $scope.editedTask.status != null
                                                 ? $scope.editedTask.status.id
                                                 : null;
                        $scope.editedTask.goalId =
                           $scope.editedTask.goal != null
                            ? $scope.editedTask.goal.id
                            : null;
                        $scope.editedTask.userId =
                            $scope.editedTask.user != null
                            ? $scope.editedTask.user.id
                            : null;
                        if ($scope.editedTaskSelectedObserverUsers != null) {
                            $scope.editedTask.observerUserIds = [];
                            for (var index = 0; index < $scope.editedTaskSelectedObserverUsers.length; index++) {
                                var user = $scope.editedTaskSelectedObserverUsers[index];
                                $scope.editedTask.observerUserIds.push(user.id);
                            }
                        } else {
                            $scope.editedTask.observerUserIds = null;
                        }
                        if ($scope.editedTaskSelectedContacts != null) {
                            $scope.editedTask.contactIds = [];
                            for (var index = 0; index < $scope.editedTaskSelectedContacts.length; index++) {
                                var contact = $scope.editedTaskSelectedContacts[index];
                                $scope.editedTask.contactIds.push(contact.id);
                            }
                        } else {
                            $scope.editedTask.contactIds = null;
                        }
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