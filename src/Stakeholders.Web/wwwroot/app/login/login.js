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
    .service('loginService',
    [
        '$http',
        '$localStorage',
        '$rootScope',
        function($http, $localStorage, $rootScope) {
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

                                var user = {
                                    name: username,
                                    status: "",
                                    image: "/images/user/sara.png"
                                };
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
                delete $localStorage.currentUser;
                delete $rootScope.user;
                $http.defaults.headers.common.Authorization = '';
            };
        }
    ])
    .controller('loginController',
    [
        '$scope',
        '$location',
        'loginService',
        function ($scope, $location, loginService) {
            loginService.logout();
            $scope.login = function() {
                loginService.login(
                    $scope.user.name,
                    $scope.user.password,
                    function(success) {
                        if (success) {
                            $location.path('/');
                        } else {
                            alert('username or password is incorrect');
                        }
                    });

            };
        }
    ]);