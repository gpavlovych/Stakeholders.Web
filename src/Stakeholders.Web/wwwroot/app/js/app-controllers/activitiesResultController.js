// JavaScript source code

porlaDashboard.controller('activitiesResultController', ['$window', '$rootScope', '$scope', 'ActivityService', '$dialogServiceFactory', function ($window, $rootScope, $scope, ActivityService, $dialogServiceFactory) {
    ActivityService.get(0,10)
        .then(function (result) {
            if (result.success) {
                $scope.activities = result.data;
            } else {
                $window.location.href = '/app/login.html';
            }
        });

    $scope.removeActivity = function (event, index) {
        $dialogServiceFactory.showConfirmationDeleteDialog(event,
            function () {
                ActivityService.removeActivity(index)
                    .then(function() {
                        ActivityService.get(0, 10)
                            .then(function (activities) {
                                $scope.activities = activities;
                            });
                    });
            }, null);
    };
}]);