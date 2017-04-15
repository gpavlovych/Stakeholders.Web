'use strict';

angular
    .module('porlaDashboard.error', ['ngRoute'])
    .config([
        '$routeProvider', function ($routeProvider) {
            $routeProvider.when('/error',
            {
                templateUrl: 'error/error.html',
                controller: 'errorController'
            });
        }
    ])
    .controller('errorController',
    ['$scope',
        function($scope) {
        }
    ]);