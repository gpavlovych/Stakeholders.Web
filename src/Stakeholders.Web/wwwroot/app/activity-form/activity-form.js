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
                var User = $resource('/api/ApplicationUsers/:id',
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
                    this.dateCreated = new Date();
                    var nextMonthDate = null;
                    var now = new Date();
                    if (now.getMonth() == 11) {
                        nextMonthDate = new Date(now.getFullYear() + 1, 0, 1);
                    } else {
                        nextMonthDate = new Date(now.getFullYear(), now.getMonth() + 1, 1);
                    }
                    vm.activity = new Activity();
                    vm.activity.dateActivity = nextMonthDate;
                }
                this.close = function() {
                    vm.activity = null;
                }
                this.save = function() {
                    var observerUserIds = [];
                    for (var index = 0; index < vm.selectedObserverUsers.length; index++) {
                        var selectedObserverUser = vm.selectedObserverUsers[index];
                        observerUserIds.push(selectedObserverUser.id);
                    }
                    vm.activity.observerUserIds = observerUserIds;

                    vm.activity.$save(function () {
                        dialogService.showMessageSavedDialog();
                        $rootScope.$emit("refreshActivities", vm.activity);
                    });
                    vm.activity = null;
                }

                this.users = User.query(function (result) {
                    vm.users = result;
                });

                this.selectedObserverUsers = [];
            }
        ],
        templateUrl: 'activity-form/activity-form.html'
    })