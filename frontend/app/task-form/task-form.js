angular
    .module('porlaDashboard.taskForm', [])
.component('taskForm',
    {
        transclude: true,
        controller: [
            '$rootScope', '$resource', 'dialogService', '$http', function($rootScope, $resource, dialogService, $http) {
                var ActivityTask = $resource(
                    '/api/ActivityTasks/:id',
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

                var vm = this;
                this.dateCreated = new Date();
                this.task = null;
                $rootScope.$on('newTask',
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
                    vm.task = new ActivityTask();
                    vm.task.dateDeadline = nextMonthDate;
                    vm.task.dateEnd = nextMonthDate;
                };

                this.close = function() {
                    vm.task = null;
                };

                this.save = function () {
                    var organizationIds = [];
                    for (var index = 0; index < vm.selectedOrganizations.length; index++) {
                        var selectedOrganization = vm.selectedOrganizations[index];
                        organizationIds.push(selectedOrganization.id);
                    }
                    vm.task.organizationIds = organizationIds;

                    var contactIds = [];
                    for (var index = 0; index < vm.selectedContacts.length; index++) {
                        var selectedContact = vm.selectedContacts[index];
                        contactIds.push(selectedContact.id);
                    }
                    vm.task.contactIds = contactIds;

                    vm.task.$save(function () {
                        dialogService.showMessageSavedDialog();
                        $rootScope.$emit("refresh", vm.task);
                    });
                    vm.task = null;
                };

                this.selectedContacts = [];
                this.contacts = Contact.query(function(result) {
                    vm.contacts = result;
                });

                this.selectedOrganizations = [];
                this.organizations = Organization.query(function(result) {
                    vm.organizations = result;
                });
            }
        ],
        templateUrl: 'task-form/task-form.html'
    })