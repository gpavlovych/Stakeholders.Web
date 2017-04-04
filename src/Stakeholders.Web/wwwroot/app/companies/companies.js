'use strict';

angular
    .module('porlaDashboard.companies', [
        'ngRoute'
    ])
    .config([
        '$routeProvider', function ($routeProvider) {
            $routeProvider.when('/companies',
            {
                templateUrl: 'companies/companies.html',
                controller: 'companiesController',
                activetab: 'companies'
            });
        }
    ])
    .factory('Company', ['$resource',
    function ($resource) {
        return $resource(
            '/api/Companies/:id',
            null,
            {
                'update': { method: 'PUT' }
            });
    }])
    .controller('companiesController',
    [
        '$scope',
        'Company',
        'dialogService',
        function($scope, Company, dialogService) {
            $scope.search = "";

            function refresh() {
                Company.query({ start: 0, count: 10, search: $scope.search },
                    function(companies) {
                        $scope.companies = companies;
                    });
            }

            refresh();
            $scope.filter = function() {
                refresh();
            };

            $scope.editCompany = function(id) {
                $scope.editedCompany = Company.get({ id: id });
            };

            $scope.closeEditor = function() {
                $scope.editedCompany = null;
            };

            $scope.saveEditor = function(event) {
                dialogService.showConfirmationSaveDialog(event,
                    function() {
                        $scope.editedCompany.$update({ id: $scope.editedCompany.id },
                            function() {
                                dialogService.showMessageSavedDialog(event, null);
                                refresh();
                            });
                        $scope.editedCompany = null;
                    },
                    null);
            };

            $scope.removeCompany = function(event, id) {
                dialogService.showConfirmationDeleteDialog(event,
                    function() {
                        $scope.editedCompany.$remove({ id: id },
                            function() {
                                refresh();
                            });
                    },
                    null);
            };
        }
    ]);