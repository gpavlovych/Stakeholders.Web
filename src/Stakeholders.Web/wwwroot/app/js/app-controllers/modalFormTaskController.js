// JavaScript source code
porlaDashboard.controller('modalFormTaskController', function ($scope, $taskService, $editTaskFormService, $dialogServiceFactory) {
    // Objeto a ser manipulado no formul�rio, modal.
    $scope.task = {};

    $scope.saveTask = function (event) {
        $taskService.addTask($scope.task);
        $dialogServiceFactory.showConfirmationDialog(event, function () {
            $scope.task = {};
            $editTaskFormService.toggleModal();
        });
    };
});