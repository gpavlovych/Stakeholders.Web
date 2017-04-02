'use strict';

angular
    .module('porlaDashboard.categories', ['ngRoute'])
    .config([
        '$routeProvider', function($routeProvider) {
            $routeProvider.when('/categories',
            {
                templateUrl: 'categories/categories.html',
                controller: 'categoriesController',
                activetab: 'categories'
            });
        }
    ])
    .controller('categoriesController',
    [
        '$scope',
        function ($scope) {
            activityService.get(0, 10)
                     .then(function (result) {
                         if (result.success) {
                             $scope.companies = result.data;
                         }
                     });
        }
    ]);