'use strict';

angular
    .module('porlaDashboard.contacts', ['ngRoute'])
    .config([
        '$routeProvider', function ($routeProvider) {
            $routeProvider.when('/contacts',
            {
                templateUrl: 'contacts/contacts.html',
                controller: 'contactsController',
                activetab: 'contacts'
            });
        }
    ])
    .controller('contactsController',
    [
        '$scope',
        function ($scope) {
            //TODO
        }
    ]);