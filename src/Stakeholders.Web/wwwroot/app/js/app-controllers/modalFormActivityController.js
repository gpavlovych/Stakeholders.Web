// Controller responsável pela modal para cadastro de atividades 
porlaDashboard.controller('modalFormActivityController', ['$scope', 'ActivityService', '$editActivityFormService', '$dialogServiceFactory', function ($scope, $activityService, $editActivityFormService, $dialogServiceFactory) {
    $scope.activity = {};

    $scope.saveActivity = function (event) {
        ActivityService.create($scope.activity);
        $dialogServiceFactory.showConfirmationDialog(event, function () {
            $scope.activity = {};
            $editActivityFormService.toggleModal();
        });
    };
}]);