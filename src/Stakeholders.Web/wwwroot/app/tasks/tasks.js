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
    .controller('tasksController', [
            '$scope',
            '$rootScope',
            'ActivityTask',
            'Contact', 
            'Organization',
            'dialogService',
        function (
            $scope, 
            $rootScope, 
            ActivityTask, 
            Contact, 
            Organization,
            dialogService) {

            $scope.periodChanged = function (period) {
                $scope.period = period;
                refresh();
            };

            $scope.refresh = function () {
                refresh();
            };
            $scope.search = "";
            $scope.switchView = false;

            function refresh() {
                ActivityTask.query({ start: 0, count: 10, search: $scope.search, period: $scope.period, organizationId: $scope.organizationId, contactId: $scope.contactId, organizationCategoryId: $scope.categoryId },
                    function (tasks) {
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
                Organization.query(function (result) {
                    $scope.editedTaskOrganizations = result;
                });
                Contact.query(function (result) {
                    $scope.editedTaskContacts = result;
                });
               
                ActivityTask.get({ id: id },
                   function (task) {
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
                        $scope.editedTask.dateEnd = $scope.editedTask.dateEndDate.toISOString();
                        $scope.editedTask.dateDeadline = $scope.editedTask.dateDeadlineDate.toISOString();
                       
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
                                dialogService.showMessageSavedDialog(null, null);
                                refresh();
                            });
                        $scope.editedTask = null;
                    },
                    null);
            };

            $scope.removeTask = function (id) {
                dialogService.showConfirmationDeleteDialog(null,
                    function () {
                        ActivityTask.delete({ id: id },
                            function () {
                                refresh();
                            });
                    },
                    null);
            };
        }
    ]);