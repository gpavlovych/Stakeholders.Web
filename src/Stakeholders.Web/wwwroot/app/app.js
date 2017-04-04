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
        'porlaDashboard.filter',
        'porlaDashboard.taskForm',
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
                colors: ["#1cc327", "#fb375c", "#0e84fc", "#46BFBD", "#FDB45C", "#949FB1", "#4D5360"]
            });
            // Configure all doughnut charts
            ChartJsProvider.setOptions('doughnut',
            {
                cutoutPercentage: 80,
                tooltips: { enabled: false }
            });
        }
    ])
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
            $locationProvider.html5Mode(true);
        }
    ])
    .config([
        '$routeProvider',
        function($routeProvider) {
            $routeProvider.when(
                '/',
                {
                    redirectTo: '/home'
                });
        }
    ])
    .service('dialogService',
    [
        '$mdDialog',
        function($mdDialog) {
            this.showConfirmationDeleteDialog = function(event, fnOk, fnCancel) {
                var confirm = $mdDialog.confirm()
                    .title('Confirm Delete')
                    .textContent('Are you sure you want to delete the item?')
                    .ariaLabel('Confirm Delete')
                    .targetEvent(event)
                    .ok('Yes')
                    .cancel('No');
                $mdDialog.show(confirm).then(fnOk, fnCancel);
            };

            this.showConfirmationSaveDialog = function(event, fnOk, fnCancel) {
                var confirm = $mdDialog.confirm()
                    .title('Confirm Save')
                    .textContent('Are you sure you want to save the item?')
                    .ariaLabel('Confirm Save')
                    .targetEvent(event)
                    .ok('Yes')
                    .cancel('No');
                $mdDialog.show(confirm).then(fnOk, fnCancel);
            };

            this.showMessageSavedDialog = function(event, fnOk) {
                $mdDialog.show(
                        $mdDialog.alert()
                        .parent(angular.element(document.querySelector('#popupContainer')))
                        .clickOutsideToClose(true)
                        .title('Confirm Save')
                        .textContent('Content saved successfully!')
                        .ariaLabel('Confirm Save')
                        .ok('Ok')
                        .targetEvent(event)
                    )
                    .then(fnOk);
            };
        }
    ])
    .run([
        '$rootScope',
        '$translate',
        '$route',
        '$location',
        '$localStorage',
        'jwtHelper',
        function ($rootScope, $translate, $route, $location, $localStorage, jwtHelper) {
            $rootScope.$route = $route;

            $rootScope.setLanguage = function (language, direction) {
                $localStorage.language = language;
                $localStorage.msg = direction;
                $rootScope.msg = direction;
                $translate.use(language);
                $rootScope.urlPage = $location.absUrl();
            }
            $rootScope.setLanguage($localStorage.language || "he", $localStorage.msg || "rtl");

            $rootScope.$on('$locationChangeStart',
                function() {
                    var publicPages = [
                        '/login',
                        //    '/register'
                    ];
                    var restrictedPage = publicPages.indexOf($location.path()) === -1;
                    if (restrictedPage && ((!$rootScope.user) || (!$localStorage.loggedIn) || (!$localStorage.loggedIn.token) || (jwtHelper.isTokenExpired($localStorage.loggedIn.token)))) {
                        $location.path('/login');
                    }
                });
        }
    ]);
