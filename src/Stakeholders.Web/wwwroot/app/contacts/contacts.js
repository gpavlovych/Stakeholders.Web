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
    .factory('Organization', ['$resource',
        function ($resource) {
            return $resource(
                '/api/Organizations/:id',
                null,
                {
                    'update': { method: 'PUT' }
                });
        }
    ])
    .factory('User', ['$resource',
        function ($resource) {
            return $resource(
                '/api/ApplicationUsers/:id',
                null,
                {
                    'update': { method: 'PUT' }
                });
        }
    ])
    .factory('Company', ['$resource',
        function($resource) {
            return $resource(
                '/api/Companies/:id',
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
        'Organization',
        'User',
        'Company',
        'dialogService',
        function ($scope, Contact,  Organization, User, Company, dialogService) {
            $scope.search = "";
            $scope.switchView = false;

            function refresh() {
                Contact.query({ start: 0, count: 10, search: $scope.search },
                    function (contacts) {
                        for (var index = 0; index < contacts.length; index++) {
                            var contact = contacts[index];
                            contact.organization = Organization.get({ id: contact.organizationId });
                            contact.user = User.get({ id: contact.userId });
                            contact.company = Company.get({ id: contact.companyId });
                            contact.tasksCompleted = 20;
                        }
                        $scope.contacts = contacts;
                    });
            }

            refresh();
            $scope.filter = function () {
                refresh();
            };

            $scope.editContact = function (id) {
                Organization.query(function (result) {
                    $scope.editedContactOrganizations = result;
                });
                User.query(function (result) {
                    $scope.editedContactUsers = result;
                });
                Company.query(function (result) {
                    $scope.editedContactCompanies = result;
                });
                Contact.get({ id: id }, function (contact) {
                    Organization.get({ id: contact.organizationId },
                        function(organization) {
                            contact.organization = organization;
                        });
                    User.get({ id: contact.userId },
                        function(user) {
                            contact.user = user;
                        });
                    Company.get({ id: contact.companyId },
                        function(company) {
                            contact.company = company;
                        });
                    $scope.editedContact = contact;
                });
            };

            $scope.closeEditor = function () {
                $scope.editedContact = null;
            };

            $scope.saveEditor = function(event) {
                dialogService.showConfirmationSaveDialog(event,
                    function() {
                        $scope.editedContact.organizationId =
                            $scope.editedContact.organization != null
                            ? $scope.editedContact.organization.id
                            : null;
                        $scope.editedContact.userId =
                            $scope.editedContact.user != null
                            ? $scope.editedContact.user.id
                            : null;
                        $scope.editedContact.companyId =
                            $scope.editedContact.company != null
                            ? $scope.editedContact.company.id
                            : null;
                        $scope.editedContact.$update({ id: $scope.editedContact.id },
                            function() {
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