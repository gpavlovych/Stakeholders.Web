'use strict';

angular
    .module('porlaDashboard.activities', ['ngRoute'])

    .config(['$routeProvider', function($routeProvider) {
            $routeProvider.when('/activities',
            {
                templateUrl: 'activities/activities.html',
                controller: 'activitiesController',
                activetab: 'activities'
            });
        }
    ])

    .service('activityService', ['$http', function($http) {
            this.get = function(start, count) {
                var url = "/api/Activities?start=" + start + "&count=" + count;
                return $http.get(url).then(handleSuccess, handleError('Error getting activities'));
            };

            this.getById = function(id) {
                var url = "/api/Activities/" + id;
                return $http.get(url).then(handleSuccess, handleError('Error getting activities by id'));
            };

            this.count = function() {
                var url = "/api/Activities/count";
                return $http.get(url).then(handleSuccess, handleError('Error getting activities count'));
            };

            this.create = function(activity) {
                var url = "/api/Activities";
                return $http.post(url, activity).then(handleSuccess, handleError('Error creating activity'));
            };

            this.update = function(activity, id) {
                var url = "/api/Activities/" + id;
                return $http.put(url, activity).then(handleSuccess, handleError('Error updating activity'));
            };

            this.remove = function (id) {
                var url = "/api/Activities/" + id;
                return $http.delete(url).then(handleSuccess, handleError('Error deleting activity'));
            };

            function handleSuccess(res) {
                return { success: true, data: res.data };
            }

            function handleError(error) {
                return function () {
                    return { success: false, message: error };
                };
            }
        }
    ])

    .controller('activitiesController', ['$scope', 'activityService', 'dialogService', function ($scope, activityService, dialogService) {
            function refresh() {
                activityService.get(0, 10)
                    .then(function(result) {
                        if (result.success) {
                            $scope.activities = result.data;
                        }
                    });
            }

            refresh();

            $scope.editActivity = function(id) {
                activityService.getById(id)
                    .then(function(result) {
                        if (result.success) {
                            $scope.editedActivity = result.data;
                        }
                    });
            };

            $scope.closeEditor = function() {
                $scope.editedActivity = null;
            };

            $scope.saveEditor = function (event) {
                dialogService.showConfirmationSaveDialog(event,
                    function() {
                        activityService.update($scope.editedActivity, $scope.editedActivity.id)
                            .then(function(result) {
                                if (result.success) {
                                    dialogService.showMessageSavedDialog(event, null);
                                    refresh();
                                }
                            });
                        $scope.closeEditor();
                    },
                    null);
            };

            $scope.removeActivity = function(event, id) {
                dialogService.showConfirmationDeleteDialog(event,
                    function() {
                        activityService.remove(id)
                            .then(function(result) {
                                if (result.success) {
                                    refresh();
                                }
                            });
                    },
                    null);
            };
        }
    ]);