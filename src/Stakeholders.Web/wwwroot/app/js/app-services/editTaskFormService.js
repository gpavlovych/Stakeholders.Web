
/* Servi�o criado para manipular o modal do formul�rio de tarefas */
/* *** Pensar posteriormente em incluir este servi�o na controller respons�vel pelo formul�rio *** */
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