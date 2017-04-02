angular
    .module('porlaDashboard.dialControl',
    [])
.component('dialControl',
    {
        transclude: true,
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
                alert('TODO open filter!');
            };
        }],
        templateUrl: 'dial-control/dial-control.html'
    })