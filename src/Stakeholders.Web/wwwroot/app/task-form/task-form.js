angular
    .module('porlaDashboard.taskForm', [])
.component('taskForm',
    {
        transclude: true,
        //require: {
        //    activityFormController: '^activityForm',
        //    taskFormController: '^taskForm'
        //},
        controller: ['$rootScope', function ($rootScope) {
            var vm = this;
            this.task = null;
            $rootScope.$on('newTask', function (event, data) {
                vm.open();
            });
            this.open = function () {

                this.task = { subject: "subj", description: "descr" };
            }
            this.close = function () {
                //todo
                this.task = null;
            }
            this.save = function () {
                //todo
                this.task = null;
            }
        }],
        templateUrl: 'task-form/task-form.html'
    })