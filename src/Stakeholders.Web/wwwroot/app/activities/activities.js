'use strict';

angular
    .module('porlaDashboard.activities',
    [
        'ngRoute',
        'hateoas'
    ])

    .config([
        '$routeProvider',
        function ($routeProvider) {
            $routeProvider.when('/activities',
            {
                templateUrl: 'activities/activities.html',
                controller: 'activitiesController',
                activetab: 'activities'
            });
        }
    ])
    .factory('activityService', ['$resource',
        function ($resource) {
            return $resource(
                '/api/Activities/:id',
                null,
                {
                    'update': { method: 'PUT' }
                });
        }
    ])
    .controller('activitiesController', [
        '$scope',
        '$rootScope',
        'activityService',
        'dialogService',
        function ($scope, $rootScope, activityService, dialogService) {
            function refresh() {
                activityService.query()
                    .$promise
                    .then(function(activities) {
                        $scope.activities = activities;
                    });
            }

            refresh();
            $rootScope.$on("refreshActivities", function() {
                refresh();
            });
            $scope.editActivity = function (id) {
                activityService.get({ id: id })
                    .$promise
                    .then(function(activity) {
                        $scope.editedActivity = activity;
                    });
            };

            $scope.closeEditor = function () {
                $scope.editedActivity = null;
            };

            $scope.saveEditor = function (event) {
                dialogService.showConfirmationSaveDialog(event,
                    function () {
                        activityService.update({ id: $scope.editedActivity.id }, $scope.editedActivity)
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