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
        '$translate',
        'Goal',
        function ($scope, $rootScope, $translate, Goal) {
            $translate(['TASKS_IN_PROCESS', 'TASKS_COMPLETED', 'TASKS_READY_TO_START'])
            .then(function (translation) {
                $scope.labels = [translation.TASKS_IN_PROCESS, translation.TASKS_COMPLETED, translation.TASKS_READY_TO_START];
            });
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