
/* Serviço criado para manipular o modal do formulário de atividades */
/* *** Pensar posteriormente em incluir este serviço na controller responsável pelo formulário *** */
porlaDashboard.factory('$editActivityFormService', [function () {
    var isOpen = true;
    return {
        getStatusModal: function () {
            return isOpen;
        },
        toggleModal: function () {
            isOpen = isOpen === false ? true : false;
        }
    };
}]);