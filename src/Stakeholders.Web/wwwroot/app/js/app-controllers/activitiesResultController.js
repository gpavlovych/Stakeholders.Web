// JavaScript source code

porlaDashboard.controller('activitiesResultController', function ($rootScope, $scope, $activityService, $dialogServiceFactory) {
    $scope.activities = $activityService.getActivities();

    $scope.removeActivity = function (event, index) {
        $dialogServiceFactory.showConfirmationDeleteDialog(event,
            function () {
                $activityService.removeActivity(index);
                $scope.activities = $activityService.getActivities();
            }, null);
    };
});