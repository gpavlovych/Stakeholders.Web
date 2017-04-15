angular
    .module('porlaDashboard.filter',
    [])
.component('filter',
    {
        transclude: true,
        controller: ['$rootScope', function ($rootScope) {
            var vm = this;
            vm.isOpen = false;
            $rootScope.$on('openFilter', function(data) {
                vm.isOpen = !vm.isOpen;
            });
        }],
        templateUrl: 'filter/filter.html'
    })