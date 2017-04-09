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
    .factory('ActivityTask',
    [
        '$resource',
        function ($resource) {
            return $resource(
                '/api/ActivityTasks/:id',
                null,
                {
                    'update': { method: 'PUT' }
                });
        }
    ])
    .factory('OrganizationCategory',
    [
        '$resource',
        function ($resource) {
            return $resource(
                '/api/OrganizationCategories/:id',
                null,
                {
                    'update': { method: 'PUT' }
                });
        }
    ])
    .controller('managersDashboardController',
    [
        '$scope',
        'ActivityTask',
        'OrganizationCategory',
        function ($scope, ActivityTask, OrganizationCategory) {
            $scope.periodChanged = function (period) {
                $scope.period = period;
                refresh();
            };
            refresh();
            function refresh() {
                ActivityTask.query({period: 3},//TODO: hardcoded 1 month, change
                    function (tasksDeadline) {
                        $scope.tasksDeadline = tasksDeadline;
                    });
                OrganizationCategory.query({ period: $scope.period, includeStats: 1 },
                   function (categories) {
                       $scope.categories = categories;
                   });
            }
        }
    ]);