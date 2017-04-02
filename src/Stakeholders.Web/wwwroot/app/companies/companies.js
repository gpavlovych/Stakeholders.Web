'use strict';

angular
    .module('porlaDashboard.companies', [
        'ngRoute'
    ])
    .config([
        '$routeProvider', function ($routeProvider) {
            $routeProvider.when('/companies',
            {
                templateUrl: 'companies/companies.html',
                controller: 'companiesController',
                activetab: 'companies'
            });
        }
    ])
    .service('companyService',
    [
        '$http',
        function($http) {
            this.get = function (start, count) {
                var url = "/api/Companies?start=" + start + "&count=" + count;
                return $http.get(url).then(handleSuccess, handleError('Error getting companies'));
            };

            this.getById = function (id) {
                var url = "/api/Companies/" + id;
                return $http.get(url).then(handleSuccess, handleError('Error getting company by id'));
            };

            this.count = function () {
                var url = "/api/Companies/count";
                return $http.get(url).then(handleSuccess, handleError('Error getting companies count'));
            };

            this.create = function (activity) {
                var url = "/api/Companies";
                return $http.post(url, activity).then(handleSuccess, handleError('Error creating company'));
            };

            this.update = function (activity, id) {
                var url = "/api/Companies/" + id;
                return $http.put(url, activity).then(handleSuccess, handleError('Error updating company'));
            };

            this.remove = function (id) {
                var url = "/api/Companies/" + id;
                return $http.delete(url).then(handleSuccess, handleError('Error deleting company'));
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
    .controller('companiesController',
    [
        '$scope',
        'companyService',
        'dialogService',
        function ($scope, companyService, dialogService) {
            function refresh() {
                companyService.get(0, 10)
                    .then(function (result) {
                        if (result.success) {
                            $scope.companies = result.data;
                        }
                    });
            }

            refresh();

            $scope.editCompany = function (id) {
                companyService.getById(id)
                    .then(function (result) {
                        if (result.success) {
                            $scope.editedCompany = result.data;
                        }
                    });
            };

            $scope.closeEditor = function () {
                $scope.editedCompany = null;
            };

            $scope.saveEditor = function (event) {
                dialogService.showConfirmationSaveDialog(event,
                    function () {
                        companyService.update($scope.editedCompany, $scope.editedCompany.id)
                            .then(function (result) {
                                if (result.success) {
                                    dialogService.showMessageSavedDialog(event, null);
                                    refresh();
                                }
                            });
                        $scope.editedCompany = null;
                    },
                    null);
            };

            $scope.removeCompany = function (event, id) {
                dialogService.showConfirmationDeleteDialog(event,
                    function () {
                        companyService.remove(id)
                            .then(function (result) {
                                if (result.success) {
                                    refresh();
                                }
                            });
                    },
                    null);
            };
        }
    ]);