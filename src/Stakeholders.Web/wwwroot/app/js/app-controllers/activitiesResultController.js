// JavaScript source code

porlaDashboard.controller('activitiesResultController',
['$window', '$rootScope', '$scope', 'ActivityService', '$dialogServiceFactory',
    function($window, $rootScope, $scope, ActivityService, $dialogServiceFactory) {
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