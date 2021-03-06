﻿'use strict';

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
        'User',
        'dialogService',
        function ($scope, $rootScope, Activity, User, dialogService) {
            function refresh() {
                Activity.query({ period: $scope.period, organizationId: $scope.organizationId, contactId: $scope.contactId, organizationCategoryId: $scope.categoryId }, function (activities) {
                        $scope.activities = activities;
                    });
            }

            $scope.periodChanged = function(period) {
                $scope.period = period;
                refresh();
            };
            $scope.categoryChanged = function(categoryId) {
                $scope.categoryId = categoryId;
                refresh();
            };
            $scope.organizationChanged = function(organizationId) {
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

            refresh();
            $rootScope.$on("refresh", function() {
                refresh();
            });
            $scope.editActivityTypeChanged = function(value) {
                if ($scope.editedActivity) {
                    $scope.editedActivity.typeId = value;
                }
            };
            $scope.editActivityTaskChanged = function(value) {
                if ($scope.editedActivity) {
                    $scope.editedActivity.taskId = value;
                }
            };
            $scope.editActivityUserChanged = function(value) {
                if ($scope.editedActivity) {
                    $scope.editedActivity.userId = value;
                }
            };
            $scope.editActivityContactChanged = function(value) {
                if ($scope.editedActivity) {
                    $scope.editedActivity.contactId = value;
                }
            };
            $scope.editActivity = function (id) {
              
                User.query(function(result) {
                    $scope.editedActivityUsers = result;
                });

                Activity.get({ id: id },
                    function(activity) {
                        $scope.dateActivityDate = activity.dateActivity != null
                            ? new Date(activity.dateActivity)
                            : null;
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

            $scope.saveEditor = function () {
                dialogService.showConfirmationSaveDialog(null,
                    function () {
                        $scope.editedActivity
                            .dateActivity = $scope.dateActivityDate != null
                            ? $scope.dateActivityDate.toISOString()
                            : null;
                        
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
                                dialogService.showMessageSavedDialog(null, null);
                                refresh();
                            });
                        $scope.closeEditor();
                    },
                    null);
            };

            $scope.removeActivity = function (id) {
                dialogService.showConfirmationDeleteDialog(null,
                    function () {
                        Activity.delete({ id: id },function () {
                                refresh();
                            });
                    },
                    null);
            };

            $scope.selectedObserverUsers = [];
        }
    ]);