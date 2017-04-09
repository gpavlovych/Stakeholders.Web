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
    .factory('Contact', ['$resource',
        function($resource) {
            return $resource(
                '/api/Contacts/:id',
                null,
                {
                    'update': { method: 'PUT' }
                });
        }
    ])
    .controller('contactsController',
    [
        '$scope',
        'Contact',
        'dialogService',
        function ($scope, Contact, dialogService) {
            $scope.search = "";
            $scope.switchView = false;
            $scope.categoryChanged = function(value) {
                $scope.categoryId = value;
                refresh();
            };
            $scope.organizationChanged = function(value) {
                $scope.organizationId = value;
                refresh();
            };
            $scope.searchChanged = function (ctrl) {
                $scope.search = ctrl.search;
                refresh();
            }
            function refresh() {
                Contact.query({
                        start: 0,
                        count: 10,
                        search: $scope.search,
                        organizationId: $scope.organizationId,
                        organizationCategoryId: $scope.organizationCategoryId
                    },
                    function (contacts) {
                        $scope.contacts = contacts;
                    });
            }

            refresh();
            $scope.filter = function () {
                refresh();
            };
            $scope.editContactOrganizationChanged = function(value) {
                if ($scope.editedContact) {
                    $scope.editedContact.organizationId = value;
                }
            };
            $scope.editContactCompanyChanged = function (value) {
                if ($scope.editedContact) {
                    $scope.editedContact.companyId = value;
                }
            };
            $scope.editContact = function (id) {
                Contact.get({ id: id }, function (contact) {
                    $scope.editedContact = contact;
                });
            };

            $scope.closeEditor = function () {
                $scope.editedContact = null;
            };

            $scope.saveEditor = function() {
                dialogService.showConfirmationSaveDialog(null,
                    function() {
                        $scope.editedContact.$update({ id: $scope.editedContact.id },
                            function() {
                                dialogService.showMessageSavedDialog(null, null);
                                refresh();
                            });
                        $scope.editedContact = null;
                    },
                    null);
            };

            $scope.removeContact = function (id) {
                dialogService.showConfirmationDeleteDialog(null,
                    function () {
                        Contact.delete({ id: id },
                            function () {
                                refresh();
                            });
                    },
                    null);
            };
        }
    ]);