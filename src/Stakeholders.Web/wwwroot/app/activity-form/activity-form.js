angular
    .module('porlaDashboard.activityForm',[])
.component('activityForm',
    {
        transclude: true,
        //require: {
        //    activityFormController: '^activityForm',
        //    taskFormController: '^taskForm'
        //},
        controller: ['$rootScope', function ($rootScope) {
            var vm = this;
            this.activity = null;
            $rootScope.$on('newActivity', function (event, data) {
                vm.open();
            });
            this.open = function () {
               
                this.activity = { subject: "subj", description: "descr" };
            }
            this.close = function () {
                //todo
                this.activity = null;
            }
            this.save = function () {
                //todo
                this.activity = null;
            }
        }],
        templateUrl: 'activity-form/activity-form.html'
    })