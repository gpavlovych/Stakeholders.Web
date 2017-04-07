﻿angular
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
                var Company = $resource('/api/Companies/:id',
                    null,
                    {
                        'update': { method: 'PUT' }
                    });

                var vm = this;
                this.dateCreated = new Date();
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
                    var observerCompanyIds = [];
                    for (var index = 0; index < vm.selectedObserverCompanies.length; index++) {
                        var selectedObserverCompany = vm.selectedObserverCompanies[index];
                        observerCompanyIds.push(selectedObserverCompany.id);
                    }
                    vm.activity.observerCompanyIds = observerCompanyIds;

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

                this.companies = Company.query(function(result) {
                    vm.companies = result;
                });

                this.selectedObserverCompanies = [];
                this.selectedObserverUsers = [];
            }
        ],
        templateUrl: 'activity-form/activity-form.html'
    })