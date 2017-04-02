
/* Serviço criado para manipular o modal do formulário de tarefas */
/* *** Pensar posteriormente em incluir este serviço na controller responsável pelo formulário *** */
porlaDashboard.factory('$editTaskFormService', [function () {
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