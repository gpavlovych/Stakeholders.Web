'use strict';

angular
    .module('porlaDashboard.activities',
    [
        'ngRoute',
        'hateoas'
    ])
    .config([
        '$routeProvider',
        function($routeProvider) {
            $routeProvider.when('/activities',
            {
                templateUrl: 'activities/activities.html',
                controller: 'activitiesController',
                activetab: 'activities'
            });
        }
    ])
    .factory('Activity',
    [
        '$resource',
        function($resource) {
            return $resource(
                '/api/Activities/:id',
                null,
                {
                    'update': { method: 'PUT' }
                });
        }
    ])
    .factory('ActivityType',
    [
        '$resource',
        function($resource) {
            return $resource(
                '/api/ActivityTypes/:id',
                null,
                {
                    'update': { method: 'PUT' }
                });
        }
    ])
    .factory('Company',
    [
        '$resource',
        function ($resource) {
            return $resource(
                '/api/Companies/:id',
                null,
                {
                    'update': { method: 'PUT' }
                });
        }
    ])
    .factory('Contact',
    [
        '$resource',
        function ($resource) {
            return $resource(
                '/api/Contacts/:id',
                null,
                {
                    'update': { method: 'PUT' }
                });
        }
    ])
    .factory('ActivityTask',
    [
        '$resource',
        function ($resource) {
            return $resource(
                '/api/ActivityTasks/:id',
                null,
                {
                    'update': { method: 'PUT' }
                });
        }
    ])
    .factory('Goal',
    [
        '$resource',
        function($resource) {
            return $resource(
                '/api/Goals/:id',
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
    .controller('activitiesController',
    [
        '$scope',
        '$rootScope',
        'Activity',
        'ActivityType',
        'Company',
        'Contact',
        'ActivityTask',
        'Goal',
        'User',
        'dialogService',
        function ($scope, $rootScope, Activity, ActivityType, Company, Contact, ActivityTask, Goal, User, dialogService) {
            function refresh() {
                Activity.query()
                    .$promise
                    .then(function (activities) {
                        for (var index = 0; index < activities.length; index++) {
                            var activity = activities[index];
                            activity.user = User.get({ id: activity.userId });

                            activity.relatedToGoal = Goal.get({ id: ActivityTask.get({ id: activity.taskId }) });
                        }
                        $scope.activities = activities;
                    });
            }

            refresh();
            $rootScope.$on("refreshActivities", function() {
                refresh();
            });
            $scope.editActivity = function(id) {
                ActivityType.query(function(result) {
                    $scope.editedActivityTypes = result;
                });
                ActivityTask.query(function(result) {
                    $scope.editedActivityTasks = result;
                });
                User.query(function(result) {
                    $scope.editedActivityUsers = result;
                });
                Company.query(function(result) {
                    $scope.editedActivityCompanies = result;
                });
                Contact.query(function(result) {
                    $scope.editedActivityContacts = result;
                });

                Activity.get({ id: id },
                    function(activity) {
                        ActivityType.get({ id: activity.typeId },
                            function(type) {
                                activity.type = type;
                            });
                        ActivityTask.get({ id: activity.taskId },
                            function(task) {
                                activity.task = task;
                            });
                        User.get({ id: activity.userId },
                            function(user) {
                                activity.user = user;
                            });
                        Company.get({ id: activity.companyId },
                            function(company) {
                                activity.company = company;
                            });
                        Contact.get({ id: activity.contactId },
                            function(contact) {
                                activity.contact = contact;
                            });
                        activity.dateActivityDate = activity.dateActivity != null
                            ? new Date(activity.dateActivity)
                            : null;
                        $scope.selectedObserverCompanies = [];
                        if (activity.observerCompanyIds != null) {
                            for (var index = 0; index < activity.observerCompanyIds.length; index++) {
                                var observerCompanyId = activity.observerCompanyIds[index];
                                Company.get({ id: observerCompanyId },
                                    function(company) {
                                        $scope.selectedObserverCompanies.push(company);
                                    });
                            }
                        }
                        $scope.selectedObserverUsers = [];
                        if (activity.observerUserIds != null) {
                            for (var index = 0; index < activity.observerUserIds.length; index++) {
                                var observerUserId = activity.observerUserIds[index];
                                User.get({ id: observerUserId },
                                    function(user) {
                                        $scope.selectedObserverUsers.push(user);
                                    });
                            }
                        }
                        $scope.editedActivity = activity;
                    });
            };

            $scope.closeEditor = function () {
                $scope.editedActivity = null;
            };

            $scope.saveEditor = function (event) {
                dialogService.showConfirmationSaveDialog(event,
                    function () {
                        $scope.editedActivity
                            .dateActivity = $scope.editedActivity.dateActivityDate != null
                            ? $scope.editedActivity.dateActivityDate.toISOString()
                            : null;
                        $scope.editedActivity.typeId =
                                                  $scope.editedActivity.type != null
                                                 ? $scope.editedActivity.type.id
                                                 : null;
                        $scope.editedActivity.taskId =
                                                  $scope.editedActivity.task != null
                                                 ? $scope.editedActivity.task.id
                                                 : null;
                        $scope.editedActivity.userId =
                            $scope.editedActivity.user != null
                            ? $scope.editedActivity.user.id
                            : null;
                        $scope.editedActivity.companyId =
                            $scope.editedActivity.company != null
                            ? $scope.editedActivity.company.id
                            : null;
                        $scope.editedActivity.contactId =
                            $scope.editedActivity.contact != null
                            ? $scope.editedActivity.contact.id
                            : null;
                        if ($scope.selectedObserverCompanies != null) {
                            $scope.editedActivity.observerCompanyIds = [];
                            for (var index = 0; index < $scope.selectedObserverCompanies.length; index++) {
                                var company = $scope.selectedObserverCompanies[index];
                                $scope.editedActivity.observerCompanyIds.push(company.id);
                            }
                        } else {
                            $scope.editedActivity.observerCompanyIds = null;
                        }
                        if ($scope.selectedObserverUsers != null) {
                            $scope.editedActivity.observerUserIds = [];
                            for (var index = 0; index < $scope.selectedObserverUsers.length; index++) {
                                var user = $scope.selectedObserverUsers[index];
                                $scope.editedActivity.observerUserIds.push(user.id);
                            }
                        } else {
                            $scope.editedActivity.observerUserIds = null;
                        }
                        Activity.update({ id: $scope.editedActivity.id }, $scope.editedActivity)
                            .$promise
                            .then(function () {
                                dialogService.showMessageSavedDialog(event, null);
                                refresh();
                            });
                        $scope.closeEditor();
                    },
                    null);
            };

            $scope.removeActivity = function (event, id) {
                dialogService.showConfirmationDeleteDialog(event,
                    function () {
                        activityService.$delete({ id: id })
                            .$promise
                            .then(function () {
                                refresh();
                            });
                    },
                    null);
            };
        }
    ]);