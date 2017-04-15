'use strict';

angular
    .module('porlaDashboard.users', ['ngRoute'])
    .config([
        '$routeProvider', function($routeProvider) {
            $routeProvider.when('/users',
            {
                templateUrl: 'users/users.html',
                controller: 'usersController',
                activetab: 'users'
            });
        }
    ])
    .factory('User',
    [
        '$resource',
        function($resource) {
            return $resource(
                '/api/ApplicationUsers/:id',
                null,
                {
                    'update': { method: 'PUT' }
                });
        }
    ])
    .controller('usersController',
    [
        '$scope',
        '$rootScope',
        'User',
        'dialogService',
        function ($scope, $rootScope, User, dialogService) {
            $scope.search = "";
            $scope.switchView = false;
            $scope.searchChanged = function(ctrl) {
                $scope.search = ctrl.search;
                refresh();
            };

            $rootScope.$on("refresh", function () {
                refresh();
            });

            function refresh() {
                User.query({ start: 0, count: 10, search: $scope.search },
                    function (users) {
                        $scope.users = users;
                    });
            }

            refresh();
            $scope.filter = function () {
                refresh();
            };

            $scope.editUserRoleChanged = function(value) {
                if ($scope.editedUser) {
                    $scope.editedUser.roleId = value;
                }
            };
            $scope.editUserCompanyChanged = function(value) {
                if ($scope.editedUser) {
                    $scope.editedUser.companyId = value;
                }
            };
            $scope.editUser = function (id) {
                User.get({ id: id }, function(result) {
                    $scope.editedUser = result;
                });
            };

            $scope.closeEditor = function () {
                $scope.editedUser = null;
            };

            $scope.saveEditor = function () {
                dialogService.showConfirmationSaveDialog(null,
                    function () {
                        $scope.editedUser.$update({ id: $scope.editedUser.id },
                            function () {
                                dialogService.showMessageSavedDialog(null, null);
                                refresh();
                            });
                        $scope.editedUser = null;
                    },
                    null);
            };

            $scope.removeUser = function (id) {
                dialogService.showConfirmationDeleteDialog(null,
                    function () {
                        User.delete({ id: id },
                            function () {
                                refresh();
                            });
                    },
                    null);
            };
        }
    ]);