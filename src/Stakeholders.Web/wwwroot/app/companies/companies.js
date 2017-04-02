'use strict';

angular
    .module('porlaDashboard.companies', ['ngRoute'])
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
    .controller('companiesController',
    [
        '$scope',
        function ($scope) {
            //TODO
        }
    ]);