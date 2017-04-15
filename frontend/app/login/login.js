'use strict';

angular
    .module('porlaDashboard.login',
    [
        'ngRoute',
        'ngStorage'
    ])
    .config([
        '$routeProvider',
        function($routeProvider) {
            $routeProvider.when('/login',
            {
                templateUrl: 'login/login.html',
                controller: 'loginController',
                activetab: 'login'
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
    .service('loginService',
    [
        '$http',
        '$localStorage',
        '$rootScope',
        'User',
        function ($http, $localStorage, $rootScope, User) {
            this.login = function(username, password, callback) {
                $http.post('/token', { email: username, password: password })
                    .then(function(response) {
                        var data = response.data;
                        if (data) {
                            var token = data.access_token;
                            // login successful if there's a token in the response
                            if (token) {
                                $http.defaults.headers.common.Authorization = 'Bearer ' + token;
                                //usersService.GetByUsername(username)
                                //    .then(function(users) {
                                //        var user = users[0];
                                // store username and token in local storage to keep user logged in between page refreshes
                                User.get({ id: 'current' }, function (user) {
                                    $localStorage.loggedIn = {
                                        user: user,
                                        token: token
                                    };
                                    $rootScope.user = user;
                                    // execute callback with true to indicate successful login
                                    callback(true);
                                    //})
                                    //.catch(function() {
                                    //    callback(false);
                                    //});
                                    // add jwt token to auth header for all requests made by the $http service

                                    // execute callback with true to indicate successful login
                                });
                            } else {

                                // execute callback with false to indicate failed login
                                callback(false);
                            }
                        } else {
                            callback(false);
                        }
                    })
                    .catch(function() {
                        callback(false);
                    });
            };

            this.logout = function() {
                delete $localStorage.loggedIn;
                delete $rootScope.user;
                $http.defaults.headers.common.Authorization = '';
            };
        }
    ])
    .controller('loginController',
    [
        '$scope',
        '$location',
        '$translate',
        'loginService',
        function ($scope, $location, $translate, loginService) {
            loginService.logout();
            $scope.login = function() {
                loginService.login(
                    $scope.user.name,
                    $scope.user.password,
                    function(success) {
                        if (success) {
                            $location.path('/');
                        } else {
                            $translate(['USERNAME_OR_PASSWORD_INCORRECT'])
                             .then(function (translation) {
                                 alert(translation.USERNAME_OR_PASSWORD_INCORRECT);
                             });

                        }
                    });

            };
        }
    ]);