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
            //TODO
        }
    ]);