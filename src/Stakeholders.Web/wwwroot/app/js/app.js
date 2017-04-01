var porlaDashboard = angular.module('porlaDashboard', ['ngRoute', 'chart.js', 'ngMaterial', 'ngSanitize', 'ngScrollbars', 'ngAnimate', 'ngDropdowns', 'md.chips.select', 'pascalprecht.translate']);

porlaDashboard.run(function ($window, $http) {
    var currentUser = angular.fromJson($window.localStorage.currentUser);
    if (currentUser) {
        $http.defaults.headers.common.Authorization = 'Bearer ' + currentUser.token;
    }
});

porlaDashboard.config(function (ChartJsProvider) {
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
});

porlaDashboard.config(function($translateProvider) {
    $translateProvider.useStaticFilesLoader({
        prefix: 'i18n/',
        suffix: '.json'
    });

    $translateProvider.registerAvailableLanguageKeys(['en', 'he'], {
        'en-US': 'en',
        'en-UK': 'en',
        'he-IL': 'he'
    });

    $translateProvider.uniformLanguageTag('bcp47');
    $translateProvider.fallbackLanguage('en');

});

porlaDashboard.config(function (ScrollBarsProvider) {
      ScrollBarsProvider.defaults = {
            scrollButtons: {
                  scrollAmount: 'auto',
                  enable: false
            },
            axis: 'y',
            theme: 'light',
            autoHideScrollbar: true,
            // callbacks:{
            //       onScrollStart:function(){
            //             addClassCallback(this,"footer");
            //       },
            //       onTotalScrollBackOffset:50,
            //       onTotalScrollBack:function(){
            //             removeClassCallback(this,"footer");
            //       },
            // }
      };
      // function addClassCallback(el,id){
      //       $("footer").addClass("visibleFooter");
      // }
      // function removeClassCallback(el,id){
      //       $("footer").removeClass("visibleFooter");
      // }
});

porlaDashboard.decorator('$mdAria', function mdAriaDecorator($delegate) {
    $delegate.expect = angular.noop;
    $delegate.expectAsync = angular.noop;
    $delegate.expectWithText = angular.noop;
    return $delegate;
});

porlaDashboard.config(function($routeProvider, $locationProvider)
{
      $routeProvider
      .when('/', {
            templateUrl : 'partials/home.html',
            controller : 'homeController',
            activetab : 'home'
      })

      .when('/ceo-dashboard', {
            templateUrl : 'partials/ceo-dashboard.html',
            controller : 'ceoController'
      })

      .when('/managers-dashboard', {
            templateUrl : 'partials/managers-dashboard.html',
            controller : 'managersController'
      })

      .when('/tasks', {
            templateUrl : 'partials/tasks.html',
            controller : 'tasksController'
      })

      .when('/activities', {
            templateUrl : 'partials/activities.html',
            controller : 'activitiesController'
      })

      .when('/organizations', {
            templateUrl : 'partials/organizations.html',
            controller : 'organizationsController'
      })

      .when('/contacts', {
            templateUrl : 'partials/contacts.html',
            controller : 'contactsController'
      })

      .when('/companies', {
            templateUrl : 'partials/companies.html',
            controller : 'companiesController'
      })

      .when('/categories', {
            templateUrl : 'partials/categories.html',
            controller : 'categoriesController'
      })

      .when('/users', {
            templateUrl : 'partials/users.html',
            controller : 'usersController'
      })

      //.when('/login', {
      //      templateUrl : 'login.html',
      //      controller : 'loginController'
      //})

      .otherwise ({ redirectTo: '/' });

      $locationProvider.html5Mode(true);
});
