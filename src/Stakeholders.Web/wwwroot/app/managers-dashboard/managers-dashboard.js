'use strict';

angular
    .module('porlaDashboard.managersDashboard', ['ngRoute'])
    .config([
        '$routeProvider', function ($routeProvider) {
            $routeProvider.when('/managers-dashboard',
            {
                templateUrl: 'managers-dashboard/managers-dashboard.html',
                controller: 'managersDashboardController',
                activetab: 'managers-dashboard'
            });
        }
    ])
    .controller('managersDashboardController',
    [
        '$scope',
        function ($scope) {
            $scope.periodChanged = function (period) {
                $scope.period = period;
                refresh();
            };
        }
    ]);