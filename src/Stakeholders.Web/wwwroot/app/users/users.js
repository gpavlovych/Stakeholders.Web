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
    .service('usersService',
    [
        function() {
            this.GetByUsername = function(username) {
                return new Promise(/* executor */ function(resolve, reject) {
                    resolve([
                    {
                        name: username,
                        status: "",
                        image:""
                    }]);
                });
            };
        }
    ])
    .controller('usersController',
    [
        '$scope',
        function ($scope) {
            //TODO
        }
    ]);