
/* Servi�o criado para manipular o modal do formul�rio de tarefas */
/* *** Pensar posteriormente em incluir este servi�o na controller respons�vel pelo formul�rio *** */
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