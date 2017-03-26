
/* Serviço criado para manipular o modal do formulário de tarefas */
/* *** Pensar posteriormente em incluir este serviço na controller responsável pelo formulário *** */
porlaDashboard.factory('$manipuladorFormTarefaService', function () {
    var boolModalAberto = true;
    return {
        obterStatusModal: function () {
            return boolModalAberto;
        },
        toggleModal: function () {
            boolModalAberto = boolModalAberto === false ? true : false;
        }
    };
});