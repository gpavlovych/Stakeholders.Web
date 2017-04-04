﻿'use strict';

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
    ]).factory('Contact', ['$resource',
    function ($resource) {
        return $resource(
            '/api/Contacts/:id',
            null,
            {
                'update': { method: 'PUT' }
            });
    }])
    .controller('contactsController',
    [
        '$scope',
        'Contact',
        'dialogService',
        function ($scope, Contact, dialogService) {
            $scope.search = "";
            $scope.switchView = false;

            function refresh() {
                Contact.query({ start: 0, count: 10, search: $scope.search },
                    function (contacts) {
                        $scope.contacts = contacts;
                    });
            }

            refresh();
            $scope.filter = function () {
                refresh();
            };

            $scope.editContact = function (id) {
                $scope.editedContact = Contact.get({ id: id });
            };

            $scope.closeEditor = function () {
                $scope.editedContact = null;
            };

            $scope.saveEditor = function (event) {
                dialogService.showConfirmationSaveDialog(event,
                    function () {
                        $scope.editedContact.$update({ id: $scope.editedContact.id },
                            function () {
                                dialogService.showMessageSavedDialog(event, null);
                                refresh();
                            });
                        $scope.editedContact = null;
                    },
                    null);
            };

            $scope.removeContact = function (event, id) {
                dialogService.showConfirmationDeleteDialog(event,
                    function () {
                        $scope.editedContact.$remove({ id: id },
                            function () {
                                refresh();
                            });
                    },
                    null);
            };
        }
    ]);