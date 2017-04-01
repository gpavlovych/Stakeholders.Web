// JavaScript source code
porlaDashboard.controller('modalFormTaskController', ['$scope', '$taskService', '$editTaskFormService', '$dialogServiceFactory', function ($scope, $taskService, $editTaskFormService, $dialogServiceFactory) {
    // Objeto a ser manipulado no formulário, modal.
    $scope.task = {};

    $scope.saveTask = function (event) {
        $taskService.addTask($scope.task);
        $dialogServiceFactory.showConfirmationDialog(event, function () {
            $scope.task = {};
            $editTaskFormService.toggleModal();
        });
    };
}]);