// Controller responsável pela modal para cadastro de atividades 
porlaDashboard.controller('modalFormActivityController', ['$scope', '$activityService', '$editActivityFormService', '$dialogServiceFactory', function ($scope, $activityService, $editActivityFormService, $dialogServiceFactory) {
    $scope.activity = {};

    $scope.saveActivity = function (event) {
        $activityService.addActivity($scope.activity);
        $dialogServiceFactory.showConfirmationDialog(event, function () {
            $scope.activity = {};
            $editActivityFormService.toggleModal();
        });
    };
}]);