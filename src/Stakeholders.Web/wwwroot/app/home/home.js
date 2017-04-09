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
        '$rootScope',
        'Goal',
        function ($scope, $rootScope, Goal) {
            $scope.labels = ["Task in progress", "Tasks Completed", "Tasks ready to start"];

            function refresh() {
                Goal.query(function(goals) {
                    $scope.goals = goals;
                });
            }

            $rootScope.$on("refresh", function () {
                refresh();
            });
            refresh();
        }
    ]);