angular
    .module('porlaDashboard.activityForm',[])
.component('activityForm',
    {
        transclude: true,
        //require: {
        //    activityFormController: '^activityForm',
        //    taskFormController: '^taskForm'
        //},
        controller: [
            '$rootScope', '$resource', 'dialogService', function($rootScope, $resource, dialogService) {

                var Activity = $resource(
                    '/api/Activities/:id',
                    null,
                    {
                        'update': { method: 'PUT' }
                    });

                var vm = this;
                this.activity = null;
                $rootScope.$on('newActivity',
                    function() {
                        vm.open();
                    });
                this.open = function() {
                    vm.activity = new Activity();
                }
                this.close = function() {
                    vm.activity = null;
                }
                this.save = function() {
                    vm.activity.$save(function() {
                        dialogService.showMessageSavedDialog(event, null);
                        $rootScope.$emit("refreshActivities", vm.activity);
                    });
                    vm.activity = null;
                }
            }
        ],
        templateUrl: 'activity-form/activity-form.html'
    })