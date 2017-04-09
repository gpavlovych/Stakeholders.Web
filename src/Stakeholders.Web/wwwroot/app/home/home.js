'use strict';

angular.module('porlaDashboard.home', ['ngRoute'])
    .config([
        '$routeProvider',
        function($routeProvider) {
            $routeProvider.when('/home',
            {
                templateUrl: 'home/home.html',
                controller: 'homeController',
                activetab: 'home'
            });
        }
    ])
    .factory('Goal', ['$resource',
        function($resource) {
            return $resource(
                '/api/Goals/:id',
                null,
                {
                    'update': { method: 'PUT' }
                });
        }
    ])
    .controller('homeController',
    [
        '$scope',
        'Goal',
        function ($scope, Goal) {
            $scope.labels = ["Task in progress", "Tasks Completed", "Tasks ready to start"];
            Goal.query(function (goals) {
                $scope.goals = goals;
            });
        }
    ]);