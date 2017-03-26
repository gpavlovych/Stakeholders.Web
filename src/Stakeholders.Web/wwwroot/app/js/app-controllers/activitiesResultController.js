// JavaScript source code

porlaDashboard.controller('activitiesResultController', function ($rootScope, $scope, $atividadeService, $fabricaDialogoService) {
    $scope.activities = $atividadeService.obterAtividades();

    $scope.removerAtividade = function (event, index) {
        $fabricaDialogoService.mostrarDialogoParaExclusao(event,
            function () {
                $atividadeService.removerAtividade(index);
                $scope.activities = $atividadeService.obterAtividades();
            }, null);
    };
});