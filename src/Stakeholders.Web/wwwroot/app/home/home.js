'use strict';

angular.module('porlaDashboard.home', ['ngRoute'])
    .config([
        '$routeProvider',
        function($routeProvider) {
            $routeProvider.when('/home',
            {
                templateUrl: 'home/home.html',
                controller: 'homeController',
                activetab: 'home'
            });
        }
    ])
    .controller('homeController',
    [
        '$scope',
        function ($scope) {
            $scope.labels = ["Task in progress", "Tasks Completed", "Tasks ready to start"];
            $scope.data = [50, 30, 20];
            $scope.goals = [
                {
                    title: 'Improve the relationship with the ministry of education',
                    valueProcess: 3,
                    percentProcess: 30,
                    valueCompleted: 5,
                    percentCompleted: 50,
                    valueReady: 2,
                    percentReady: 20
                },
                {
                    title: 'Improve relationship with the neighbors of the factory',
                    valueProcess: 3,
                    percentProcess: 30,
                    valueCompleted: 5,
                    percentCompleted: 50,
                    valueReady: 2,
                    percentReady: 20
                },
                {
                    title: 'Renegotiate budget with partners and suppliers',
                    valueProcess: 3,
                    percentProcess: 30,
                    valueCompleted: 5,
                    percentCompleted: 50,
                    valueReady: 2,
                    percentReady: 20
                },
                {
                    title: 'Research new markets for expansion',
                    valueProcess: 3,
                    percentProcess: 30,
                    valueCompleted: 5,
                    percentCompleted: 50,
                    valueReady: 2,
                    percentReady: 20
                },
                {
                    title: 'Improve relationship with the neighbors of the factory',
                    valueProcess: 3,
                    percentProcess: 30,
                    valueCompleted: 5,
                    percentCompleted: 50,
                    valueReady: 2,
                    percentReady: 20
                },
                {
                    title: 'Renegotiate budget with partners and suppliers',
                    valueProcess: 3,
                    percentProcess: 30,
                    valueCompleted: 5,
                    percentCompleted: 50,
                    valueReady: 2,
                    percentReady: 20
                }
            ];
        }
    ]);