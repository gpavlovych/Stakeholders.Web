// Controller responsável pela modal para cadastro de atividades 
porlaDashboard.controller('modalFormActivityController', ['$scope', 'ActivityService', '$editActivityFormService', '$dialogServiceFactory', function ($scope, $activityService, $editActivityFormService, $dialogServiceFactory) {
    $scope.createdActivity = {};

    $scope.saveActivity = function (event) {
        $dialogServiceFactory.showConfirmationSaveDialog(event,
                 function () {
                     ActivityService.create($scope.createdActivity)
                         .then(function (result) {
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
}]);