
/* Serviço criado para manipular o modal do formulário de atividades */
/* *** Pensar posteriormente em incluir este serviço na controller responsável pelo formulário *** */
porlaDashboard.factory('$manipuladorFormAtividadeService', function () {
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