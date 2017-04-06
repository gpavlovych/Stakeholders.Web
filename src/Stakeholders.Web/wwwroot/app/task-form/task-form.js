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
            '$rootScope', '$resource', 'dialogService', '$http', function($rootScope, $resource, dialogService, $http) {
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
                var User = $resource('/api/ApplicationUsers/:id',
                    null,
                    {
                        'update': { method: 'PUT' }
                    });
                var Contact = $resource('/api/Contacts/:id',
                    null,
                    {
                        'update': { method: 'PUT' }
                    });
                var Organization = $resource('/api/Organizations/:id',
                    null,
                    {
                        'update': { method: 'PUT' }
                    });
                var Goal = $resource('/api/Goals/:id',
                    null,
                    {
                        'update': { method: 'PUT' }
                    });
                var vm = this;
                this.dateCreated = new Date();
                this.task = null;
                $rootScope.$on('newTask',
                    function() {
                        vm.open();
                    });
                this.open = function() {
                    vm.task = new ActivityTask();
                };

                this.close = function() {
                    vm.task = null;
                };

                this.save = function () {
                    vm.task.statusId = vm.selectedStatus != null ? vm.selectedStatus.id : null;
                    vm.task.goalId = vm.selectedGoal != null ? vm.selectedGoal.id : null;
                    vm.task.assignToId = vm.owner != null ? vm.owner.id : null;

                    var organizationIds = [];
                    for (var index = 0; index < vm.selectedOrganizations.length; index++) {
                        var selectedOrganization = vm.selectedOrganizations[index];
                        organizationIds.push(selectedOrganization.id);
                    }
                    //TODO vm.task.organizationIds = organizationIds;

                    var contactIds = [];
                    for (var index = 0; index < vm.selectedContacts.length; index++) {
                        var selectedContact = vm.selectedContacts[index];
                        contactIds.push(selectedContact.id);
                    }
                    vm.task.contactIds = contactIds;

                    vm.task.$save(function () {
                        dialogService.showMessageSavedDialog();
                        $rootScope.$emit("refreshActivityTasks", vm.task);
                    });
                    vm.task = null;
                };

                this.statuses = ActivityTaskStatus.query(function(result) {
                    vm.statuses = result;
                });

                this.users = User.query(function(result) {
                    vm.users = result;
                });

                this.selectedContacts = [];
                this.contacts = Contact.query(function(result) {
                    vm.contacts = result;
                });

                this.selectedOrganizations = [];
                this.organizations = Organization.query(function(result) {
                    vm.organizations = result;
                });

                this.goals = Goal.query(function(result) {
                    vm.goals = result;
                });
            }
        ],
        templateUrl: 'task-form/task-form.html'
    })