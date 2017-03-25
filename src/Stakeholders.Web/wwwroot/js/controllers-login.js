porlaDashboard.controller('dirDashboard', function ($scope, $location, $timeout) {
      $scope.msg = 'ltr';
      $scope.changeRTL = function () {
            $scope.msg = 'rtl';
            $scope.urlPage = $location.absUrl();
      }
      $scope.changeLTR = function () {
            $scope.msg = 'ltr';
            $scope.urlPage = $location.absUrl();
      }
});

porlaDashboard.controller('appDashboard', function ($rootScope, $scope) {
      $rootScope.title = 'PORIA DASHBOARD';
      $rootScope.pathImage = 'images/';
      $rootScope.pathUser = 'user/';
});

porlaDashboard.controller('loginController',  function($rootScope, $scope, $location){
      $rootScope.activetab = $location.path();
});
