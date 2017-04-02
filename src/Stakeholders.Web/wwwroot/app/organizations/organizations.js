'use strict';

angular
    .module('porlaDashboard.organizations', ['ngRoute'])
    .config([
        '$routeProvider', function ($routeProvider) {
            $routeProvider.when('/organizations',
            {
                templateUrl: 'organizations/organizations.html',
                controller: 'organizationsController',
                activetab: 'organizations'
            });
        }
    ])
    .controller('organizationsController',
    [
        '$scope',
        function ($scope) {
            //TODO
        }
    ]);