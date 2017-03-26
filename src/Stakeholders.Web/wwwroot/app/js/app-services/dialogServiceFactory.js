// JavaScript source code
porlaDashboard.factory('$dialogServiceFactory', function ($mdDialog) {
    return {
        showConfirmationDeleteDialog: function (event, fnOk, fnCancel) {
            var confirm = $mdDialog.confirm()
                .title('Confirm Delete')
                .textContent('Are you sure you want to delete the item?')
                .ariaLabel('Confirm Delete')
                .targetEvent(event)
                .ok('Yes')
                .cancel('No');
            $mdDialog.show(confirm).then(fnOk, fnCancel);
        },
        showConfirmationDialog: function (event, fnOk) {
            $mdDialog.show(
                $mdDialog.alert()
                    .parent(angular.element(document.querySelector('#popupContainer')))
                    .clickOutsideToClose(true)
                    .title('Confirm Save')
                    .textContent('Content saved successfully!')
                    .ariaLabel('Confirm Save')
                    .ok('Ok')
                    .targetEvent(event)
            ).then(fnOk);
        },
    };
});