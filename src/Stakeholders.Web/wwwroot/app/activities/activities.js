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
        'Company',
        'User',
        'dialogService',
        function ($scope, $rootScope, Activity, Company, User, dialogService) {
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
            $rootScope.$on("refreshActivities", function() {
                refresh();
            });
            $scope.editActivity = function(id) {
              
                User.query(function(result) {
                    $scope.editedActivityUsers = result;
                });
                Company.query(function(result) {
                    $scope.editedActivityCompanies = result;
                });
                
                Activity.get({ id: id },
                    function(activity) {
                        $scope.dateActivityDate = activity.dateActivity != null
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

            $scope.saveEditor = function () {
                dialogService.showConfirmationSaveDialog(null,
                    function () {
                        $scope.editedActivity
                            .dateActivity = $scope.dateActivityDate != null
                            ? $scope.dateActivityDate.toISOString()
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
        }
    ]);