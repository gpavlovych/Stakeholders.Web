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
    [[
        '$scope',
        'User',
        'dialogService',
        function ($scope, User, dialogService) {
            $scope.search = "";
            $scope.switchView = false;

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
                $scope.editedUser = User.get({ id: id });
            };

            $scope.closeEditor = function () {
                $scope.editedUser = null;
            };

            $scope.saveEditor = function (event) {
                dialogService.showConfirmationSaveDialog(event,
                    function () {
                        $scope.editedUser.$update({ id: $scope.editedOrganization.id },
                            function () {
                                dialogService.showMessageSavedDialog(event, null);
                                refresh();
                            });
                        $scope.editedUser = null;
                    },
                    null);
            };

            $scope.removeUser = function (event, id) {
                dialogService.showConfirmationDeleteDialog(event,
                    function () {
                        $scope.editedUser.$remove({ id: id },
                            function () {
                                refresh();
                            });
                    },
                    null);
            };
        }
    ]
    ]);