'use strict';

angular
    .module('porlaDashboard.users', ['ngRoute'])
    .config([
        '$routeProvider', function ($routeProvider) {
            $routeProvider.when('/users',
            {
                templateUrl: 'users/users.html',
                controller: 'usersController',
                activetab: 'users'
            });
        }
    ])
      .factory('User', ['$resource',
    function ($resource) {
        return $resource(
            '/api/ApplicationUsers/:id',
            null,
            {
                'update': { method: 'PUT' }
            });
    }])
  .controller('usersController',
    [
        '$scope',
        'User',
        'dialogService',
        function ($scope, User, dialogService) {
            $scope.search = "";
            $scope.switchView = false;
            $scope.searchChanged = function(ctrl) {
                $scope.search = ctrl.search;
                refresh();
            };

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