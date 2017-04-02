'use strict';

angular
    .module('porlaDashboard.ceoDashboard', ['ngRoute'])
    .config([
        '$routeProvider', function ($routeProvider) {
            $routeProvider.when('/ceo-dashboard',
            {
                templateUrl: 'ceo-dashboard/ceo-dashboard.html',
                controller: 'ceoDashboardController',
                activetab: 'ceo-dashboard'
            });
        }
    ])
    .controller('ceoDashboardController',
    [
        '$scope',
        function ($scope) {
            //TODO
        }
    ]);