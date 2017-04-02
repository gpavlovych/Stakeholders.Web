// JavaScript source code

porlaDashboard.controller('activitiesResultController',
['$window', '$rootScope', '$scope', 'ActivityService', '$dialogServiceFactory',
    function ($window, $rootScope, $scope, ActivityService, $dialogServiceFactory) {

        function refresh() {
            ActivityService.get(0, 10)
                .then(function (result) {
                    if (result.success) {
                        $scope.activities = result.data;
                    } else {
                        $window.location.href = '/app/login.html';
                    }
                });
        }

        refresh();

        $scope.editActivity = function (id) {
            ActivityService.getById(id).then(function (result) {
                if (result.success) {
                    $scope.editedActivity = result.data;
                } else {
                    $window.location.href = '/app/login.html';
                }
            });
        };
        $scope.closeEditor = function() {
            $scope.editedActivity = null;
        };
        $scope.saveEditor = function(event) {
            $dialogServiceFactory.showConfirmationSaveDialog(event,
                function () {
                    ActivityService.update($scope.editedActivity, $scope.editedActivity.id)
                        .then(function(result) {
                            if (result.success) {
                                $dialogServiceFactory.showMessageSavedDialog(event, null);
                                refresh();
                            } else {
                                $window.location.href = '/app/login.html';
                            }
                        });
                    $scope.closeEditor();
                },
                null);
        };
        $scope.removeActivity = function (event, id) {
            $dialogServiceFactory.showConfirmationDeleteDialog(event,
                function () {
                    ActivityService.remove(id)
                        .then(function (result) {
                            if (result.success) {
                                refresh();
                            } else {
                                $window.location.href = '/app/login.html';
                            }
                        });
                },
                null);
        };
    }
]);