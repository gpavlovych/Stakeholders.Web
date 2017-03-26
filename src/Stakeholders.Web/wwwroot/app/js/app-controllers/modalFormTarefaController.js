// JavaScript source code
porlaDashboard.controller('modalFormTarefaController', function ($scope, $tarefaService, $manipuladorFormTarefaService, $fabricaDialogoService) {
    // Objeto a ser manipulado no formulário, modal.
    $scope.tarefa = {};

    $scope.salvarTarefa = function (event) {
        $tarefaService.adicionarTarefa($scope.tarefa);
        $fabricaDialogoService.mostrarDialogoConfirmacao(event, function () {
            $scope.tarefa = {};
            $manipuladorFormTarefaService.toggleModal();
        });
    };
});