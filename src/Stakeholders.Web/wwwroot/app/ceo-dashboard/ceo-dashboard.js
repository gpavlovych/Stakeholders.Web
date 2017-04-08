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
    .factory('Activity',
    [
        '$resource',
        function ($resource) {
            return $resource(
                '/api/Activities/:id',
                null,
                {
                    'update': { method: 'PUT' }
                });
        }
    ])
    .factory('User',
    [
        '$resource',
        function ($resource) {
            return $resource(
                '/api/ApplicationUsers/:id',
                null,
                {
                    'update': { method: 'PUT' }
                });
        }
    ])
    .controller('ceoDashboardController',
    [
        '$scope',
        'Activity',
        'User',
        function ($scope, Activity, User) {
            Activity.query({period: period}, function (result) {
                $scope.activities = result;
            });
            User.query({ period: period }, function (result) {
                $scope.users = result;
            });
        }
    ]);