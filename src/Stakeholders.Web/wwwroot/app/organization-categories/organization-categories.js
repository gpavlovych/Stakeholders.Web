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

    .service('organizationCategoryService', [
        '$http',
        function ($http) {
            this.get = function (start, count, search) {
                var url = "/api/OrganizationCategories?start=" + start + "&count=" + count + "&search=" + search;
                return $http.get(url).then(handleSuccess, handleError('Error getting activities'));
            };

            this.getById = function (id) {
                var url = "/api/OrganizationCategories/" + id;
                return $http.get(url).then(handleSuccess, handleError('Error getting activities by id'));
            };

            this.count = function () {
                var url = "/api/OrganizationCategories/count";
                return $http.get(url).then(handleSuccess, handleError('Error getting activities count'));
            };

            this.create = function (activity) {
                var url = "/api/OrganizationCategories";
                return $http.post(url, activity).then(handleSuccess, handleError('Error creating activity'));
            };

            this.update = function (activity, id) {
                var url = "/api/OrganizationCategories/" + id;
                return $http.put(url, activity).then(handleSuccess, handleError('Error updating activity'));
            };

            this.remove = function (id) {
                var url = "/api/OrganizationCategories/" + id;
                return $http.delete(url).then(handleSuccess, handleError('Error deleting activity'));
            };

            function handleSuccess(res) {
                return { success: true, data: res.data };
            }

            function handleError(error) {
                return function () {
                    return { success: false, message: error };
                };
            }
        }
    ])

    .controller('categoriesController',
    [
        '$scope',
        'organizationCategoryService',
        function ($scope, organizationCategoryService) {
            organizationCategoryService.get(0, 10)
                     .then(function (result) {
                         if (result.success) {
                             $scope.organizationCategories = result.data;
                         }
                     });
        }
    ]);