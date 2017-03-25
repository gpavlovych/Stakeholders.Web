// JavaScript source code
porlaDashboard.factory('$fabricaDialogoService', function ($mdDialog) {
    return {
        mostrarDialogoParaExclusao: function (event, fnOk, fnCancel) {
            var confirm = $mdDialog.confirm()
                .title('Confirme Exclusion')
                .textContent('Are you sure you want to delete the item?')
                .ariaLabel('Confirme Exclusion')
                .targetEvent(event)
                .ok('Yes')
                .cancel('No');
            $mdDialog.show(confirm).then(fnOk, fnCancel);
        },
        mostrarDialogoConfirmacao: function (event, fnOk) {
            $mdDialog.show(
                $mdDialog.alert()
                    .parent(angular.element(document.querySelector('#popupContainer')))
                    .clickOutsideToClose(true)
                    .title('Confirme Saved')
                    .textContent('Content saved successfully!')
                    .ariaLabel('Confirme Saved')
                    .ok('Ok')
                    .targetEvent(event)
            ).then(fnOk);
        },
    };
});