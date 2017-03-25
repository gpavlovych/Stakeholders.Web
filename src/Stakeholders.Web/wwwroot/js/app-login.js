var porlaDashboard = angular.module('porlaDashboard',['ngRoute','ngMaterial','ngSanitize']);
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

      .when('/login', {
            templateUrl : 'login.html',
            controller : 'loginController'
      })

      .otherwise ({ redirectTo: '/' });

      // use the HTML5 History API
      $locationProvider.html5Mode(true);
});
