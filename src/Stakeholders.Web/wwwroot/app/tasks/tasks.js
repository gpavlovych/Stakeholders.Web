'use strict';

angular
    .module('porlaDashboard.tasks', ['ngRoute'])
    .config([
        '$routeProvider', function ($routeProvider) {
            $routeProvider.when('/tasks',
            {
                templateUrl: 'tasks/tasks.html',
                controller: 'tasksController',
                activetab: 'tasks'
            });
        }
    ])
    .controller('tasksController',
    [
        '$scope',
        function ($scope) {
            //TODO
        }
    ]);