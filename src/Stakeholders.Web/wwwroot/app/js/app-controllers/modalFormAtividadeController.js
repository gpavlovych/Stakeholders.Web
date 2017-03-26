// Controller responsável pela modal para cadastro de atividades 
porlaDashboard.controller('modalFormAtividadeController', function ($scope, $atividadeService, $manipuladorFormAtividadeService, $fabricaDialogoService) {
    $scope.activity = {};

    $scope.salvarAtividade = function (event) {
        $atividadeService.adicionarAtividade($scope.activity);
        $fabricaDialogoService.mostrarDialogoConfirmacao(event, function () {
            $scope.activity = {};
            $manipuladorFormAtividadeService.toggleModal();
        });
    };
});