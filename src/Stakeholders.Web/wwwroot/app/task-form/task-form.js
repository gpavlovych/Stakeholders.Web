angular
    .module('porlaDashboard.taskForm', [])
.component('taskForm',
    {
        transclude: true,
        //require: {
        //    activityFormController: '^activityForm',
        //    taskFormController: '^taskForm'
        //},
        controller: [
            '$rootScope', '$resource', 'dialogService', '$http', function ($rootScope, $resource, dialogService, $http) {
                var ActivityTask = $resource(
                    '/api/ActivityTasks/:id',
                    null,
                    {
                        'update': { method: 'PUT' }
                    });
                var ActivityTaskStatus = $resource('/api/ActivityTaskStatuses/:id',
                    null,
                    {
                        'update': { method: 'PUT' }
                    });
                var vm = this;
                this.task = null;
                $rootScope.$on('newTask',
                    function() {
                        vm.open();
                    });
                this.open = function () {
                    vm.task = new ActivityTask();
                }
                this.close = function () {
                    vm.task = null;
                }
                this.save = function () {
                    vm.task.$save(function () {
                        dialogService.showMessageSavedDialog(event, null);
                        $rootScope.$emit("refreshActivityTasks", vm.task);
                    });
                    vm.task = null;
                }
                this.statuses = ActivityTaskStatus.query(function(result) {
                    vm.statuses = result;
                });
            }
        ],
        templateUrl: 'task-form/task-form.html'
    })