
/* Servi�o criado para manipular o modal do formul�rio de atividades */
/* *** Pensar posteriormente em incluir este servi�o na controller respons�vel pelo formul�rio *** */
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