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
    $rootScope.pathImage = '/app/images/';
    $rootScope.pathUser = '/app/user/';
});

porlaDashboard.controller('loginController', function ($rootScope, $scope, $location, $window, $authenticationService) {
    $window.localStorage.setItem("poria_users",
          angular.toJson( [
           ]));
    $scope.login = function() {
        $authenticationService.login($scope.user.name,
            $scope.user.password,
            function(success) {
                if (success) {
                    $window.localStorage.setItem("poria_users",
                         angular.toJson([
                                {
                                    name: 'Sara',
                                    last: 'Forester',
                                    occupation: 'Accountant',
                                    contact: '866-878-7382',
                                    image: '/app/user/sara.png',
                                    status: 'on'
                                }
                            ]));
                    $window.location.assign("/app/index.html");
                }
            });
       
    };
    $rootScope.activetab = $location.path();

});
