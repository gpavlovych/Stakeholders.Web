angular
    .module('porlaDashboard.dialControl',
        [])
    .component('dialControl',
    {
        transclude: true,
        bindings: {
            hideFilterButton: "<"
        },
        controller: ['$rootScope', function ($rootScope) {
            this.newActivity = function () {
                $rootScope.$emit('newActivity', {
                    someData: 'myData'
                }); 
            };

            this.newTask = function () {
                $rootScope.$emit('newTask', {
                    someData: 'myData'
                });
            };

            this.openFilter = function() {
                $rootScope.$emit('openFilter', {
                    someData: 'myData'
                });
            };
        }],
        templateUrl: 'dial-control/dial-control.html'
    })