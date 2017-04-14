'use strict';

angular
    .module('porlaDashboard',
    [
        'ngRoute',
        'ngStorage',
        'chart.js',
        'ngMaterial',
        'ngSanitize',
        'ngScrollbars',
        'ngAnimate',
        'ngDropdowns',
        'ngResource',
        'hateoas',
        'angular-jwt',
        'md.chips.select',
        'pascalprecht.translate',
        'porlaDashboard.activityForm',
        'porlaDashboard.error',
        'porlaDashboard.filter',
        'porlaDashboard.taskForm',
        'porlaDashboard.timeFilter',
        'porlaDashboard.dialControl',
        'porlaDashboard.activities',
        'porlaDashboard.categories',
        'porlaDashboard.ceoDashboard',
        'porlaDashboard.companies',
        'porlaDashboard.contacts',
        'porlaDashboard.managersDashboard',
        'porlaDashboard.home',
        'porlaDashboard.login',
        'porlaDashboard.organizations',
        'porlaDashboard.tasks',
        'porlaDashboard.users'
    ])
    .config([
        'ChartJsProvider',
        function(ChartJsProvider) {
            // Configure all charts
            ChartJsProvider.setOptions({
                colours: [
                    {
                        fillColor: '#1cc327',
                        strokeColor: '#1cc327',
                        highlightFill: '#1cc327',
                        highlightStroke: '#1cc327'
                    }, "#fb375c", "#0e84fc", "#46BFBD", "#FDB45C", "#949FB1", "#4D5360"
                ]
            });
            // Configure all doughnut charts
            ChartJsProvider.setOptions('doughnut',
            {
                cutoutPercentage: 80,
                tooltips: { enabled: false }
            });
        }
    ])
    .factory('testInterceptor',
    ['$q', '$location', function ($q, $location) {
            var service = {
                responseError: responseError
            };

            return service;

            function responseError(rejection) {
                if (rejection.status === 404) {
                    $location.path('/error');
                    return $q(function() { return null; });
                }
                if (rejection.status === 401) {
                    $location.path('/login');
                    return $q(function() { return null; });
                }
                return $q.reject(rejection);
            }
        }
    ])
    .config(['$httpProvider', function ($httpProvider) {
        $httpProvider.interceptors.push('testInterceptor');
    }])
    .config(['HateoasInterceptorProvider', function (HateoasInterceptorProvider) {
        HateoasInterceptorProvider.transformAllResponses();
    }])
    .config([
        '$translateProvider',
        function($translateProvider) {
            $translateProvider.useStaticFilesLoader({
                prefix: '/i18n/',
                suffix: '.json'
            });

            $translateProvider.registerAvailableLanguageKeys(['en', 'he'],
            {
                'en-US': 'en',
                'en-UK': 'en',
                'he-IL': 'he'
            });

            $translateProvider.uniformLanguageTag('bcp47');
            $translateProvider.fallbackLanguage('en');

        }
    ])
    .config([
        'ScrollBarsProvider',
        function(ScrollBarsProvider) {
            ScrollBarsProvider.defaults = {
                scrollButtons: {
                    scrollAmount: 'auto',
                    enable: false
                },
                axis: 'y',
                theme: 'light',
                autoHideScrollbar: true,
            };
        }
    ])
    .decorator('$mdAria',
    [
        '$delegate',
        function($delegate) {
            $delegate.expect = angular.noop;
            $delegate.expectAsync = angular.noop;
            $delegate.expectWithText = angular.noop;
            return $delegate;
        }
    ])
    .config([
        '$locationProvider',
        function($locationProvider) {
            $locationProvider.html5Mode(false);
        }
    ])
    .config([
        '$routeProvider',
        function($routeProvider) {
            $routeProvider.otherwise(
                {
                    redirectTo: '/home'
                });
        }
    ])
    .service('dialogService',
    [
        '$mdDialog',
        '$translate',
        function ($mdDialog, $translate) {
            
            this.showConfirmationDeleteDialog = function(event, fnOk, fnCancel) {
                $translate(['CONFIRM_DELETE', 'SURE_WANT_DELETE', 'YES', 'NO'])
                    .then(function(translation) {
                        var confirm = $mdDialog.confirm()
                            .title(translation.CONFIRM_DELETE)
                            .textContent(translation.SURE_WANT_DELETE)
                            .ariaLabel(translation.CONFIRM_DELETE)
                            .targetEvent(event)
                            .ok(translation.YES)
                            .cancel(translation.NO);
                        $mdDialog.show(confirm).then(fnOk, fnCancel);
                    });
            };

            this.showConfirmationSaveDialog = function(event, fnOk, fnCancel) {
                $translate(['CONFIRM_SAVE', 'SURE_WANT_SAVE', 'YES', 'NO'])
                    .then(function(translation) {
                        var confirm = $mdDialog.confirm()
                            .title(translation.CONFIRM_SAVE)
                            .textContent(translation.SURE_WANT_SAVE)
                            .ariaLabel(translation.CONFIRM_SAVE)
                            .targetEvent(event)
                            .ok(translation.YES)
                            .cancel(translation.NO);
                        $mdDialog.show(confirm).then(fnOk, fnCancel);
                    });
            };

            this.showMessageSavedDialog = function (event, fnOk) {
                $translate(['CONFIRM_SAVE','SAVED_SUCCESSFULLY', 'OK'])
                    .then(function(translation) {
                        $mdDialog.show(
                                $mdDialog.alert()
                                .parent(angular.element(document.querySelector('#popupContainer')))
                                .clickOutsideToClose(true)
                                .title(translation.CONFIRM_SAVE)
                                .textContent(translation.SAVED_SUCCESSFULLY)
                                .ariaLabel(translation.CONFIRM_SAVE)
                                .ok(translation.OK)
                                .targetEvent(event)
                            )
                            .then(fnOk);
                    });
            };
        }
    ])
    .run([
        '$rootScope',
        '$translate',
        '$route',
        '$location',
        '$localStorage',
        '$http',
        'jwtHelper',
        function ($rootScope, $translate, $route, $location, $localStorage, $http, jwtHelper) {
            $rootScope.$route = $route;

            $rootScope.setLanguage = function (language, direction) {
                $localStorage.language = language;
                $localStorage.msg = direction;
                $rootScope.msg = direction;
                $translate.use(language);
                $rootScope.urlPage = $location.absUrl();
                $rootScope.$emit('setLanguage', { language: language, direction: direction });
            }
            $rootScope.setLanguage($localStorage.language || "he", $localStorage.msg || "rtl");

            $rootScope.$on('$locationChangeStart',
                function() {
                    var publicPages = [
                        '/login',
                        //    '/register'
                    ];
                    var restrictedPage = publicPages.indexOf($location.path()) === -1;
                    if (restrictedPage) {
                        if ((!$localStorage.loggedIn) ||
                            (!$localStorage.loggedIn.user) ||
                            (!$localStorage.loggedIn.token) ||
                            (jwtHelper.isTokenExpired($localStorage.loggedIn.token))) {
                            $location.path('/login');
                        }
                        else {
                            $rootScope.user = $localStorage.loggedIn.user;
                            $http.defaults.headers.common.Authorization = 'Bearer ' + $localStorage.loggedIn.token;
                        }
                    }

                });
        }
    ]);
