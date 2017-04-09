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
            $scope.categoryChanged = function (categoryId) {
                $scope.categoryId = categoryId;
                refresh();
            };
            $scope.organizationChanged = function (organizationId) {
                $scope.organizationId = organizationId;
                refresh();
            };
            $scope.contactChanged = function (contactId) {
                $scope.contactId = contactId;
                refresh();
            };
            $scope.refresh = function () {
                refresh();
            };
            $scope.search = "";
            $scope.switchView = false;

            $scope.editedTaskSelectedOrganizations = [];
            $scope.editedTaskSelectedContacts = [];
            $scope.editedTaskOrganizations = [];
            $scope.editedTaskContacts = [];

            function refresh() {
                Organization.query(function (result) {
                    $scope.editedTaskOrganizations = result;
                });
                Contact.query(function (result) {
                    $scope.editedTaskContacts = result;
                });

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
                $scope.editedTaskSelectedOrganizations = [];
                $scope.editedTaskSelectedContacts = [];

                ActivityTask.get({ id: id },
                   function (task) {
                       $scope.dateEndDate = task.dateEnd != null ? new Date(task.dateEnd) : null;
                       $scope.dateDeadlineDate = task.dateDeadline != null ? new Date(task.dateDeadline) : null;
                       var editedTaskSelectedOrganizations = [];
                       if (task.organizationIds != null) {
                           for (var index = 0; index < task.organizationIds.length; index++) {
                               var organizationId = task.organizationIds[index];
                               Organization.get({ id: organizationId},
                                   function (organization) {
                                       editedTaskSelectedOrganizations.push(organization);
                                       $scope.editedTaskSelectedOrganizations = editedTaskSelectedOrganizations;
                                   });
                           }
                       }
                       var editedTaskSelectedContacts = [];
                       if (task.contactIds != null) {
                           for (var index = 0; index < task.contactIds.length; index++) {
                               var contactId = task.contactIds[index];
                               Contact.get({ id: contactId },
                                   function(contact) {
                                       editedTaskSelectedContacts.push(contact);
                                       $scope.editedTaskSelectedContacts = editedTaskSelectedContacts;
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
                        $scope.editedTask.dateEnd = $scope.dateEndDate != null
                            ? $scope.dateEndDate.toISOString()
                            : null;
                        $scope.editedTask.dateDeadline = $scope.dateDeadlineDate != null
                            ? $scope.dateDeadlineDate.toISOString()
                            : null;
                       
                        if ($scope.editedTaskSelectedOrganizations != null) {
                            $scope.editedTask.organizationIds = [];
                            for (var index = 0; index < $scope.editedTaskSelectedOrganizations.length; index++) {
                                var organization = $scope.editedTaskSelectedOrganizations[index];
                                $scope.editedTask.organizationIds.push(organization.id);
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