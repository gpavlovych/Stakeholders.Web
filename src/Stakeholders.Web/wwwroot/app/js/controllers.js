porlaDashboard.controller('dirDashboard', ['$scope', '$location', '$timeout', '$translate', function ($scope, $location, $timeout, $translate) {
    $scope.msg = 'rtl';
    $translate.use("he");
    $scope.changeRTL = function () {
        $scope.msg = 'rtl';
        $translate.use("he");
        $scope.urlPage = $location.absUrl();
    }
    $scope.changeLTR = function () {
        $scope.msg = 'ltr';
        $translate.use("en");
        $scope.urlPage = $location.absUrl();
    }
}]);

porlaDashboard.controller('appDashboard', ['$rootScope', '$scope', '$window', '$mdDialog', '$editTaskFormService', '$editActivityFormService', function ($rootScope, $scope, $window, $mdDialog, $editTaskFormService, $editActivityFormService) {
    $rootScope.title = 'PORIA DASHBOARD';
    $rootScope.pathImage = '/app/images/';
    $rootScope.pathUser = '/app/user/';

    $scope.highChk = false;
    $scope.status = '';
    $scope.customFullscreen = false;

    $scope.showError = function (ev) {
        $mdDialog.show(
            $mdDialog.alert()
                .parent(angular.element(document.querySelector('#popupContainer')))
                .clickOutsideToClose(true)
                .title('Error')
                .textContent('Something went wrong. Try again.')
                .ariaLabel('Error')
                .ok('Ok')
                .targetEvent(ev)
        );
    };

    // Config HOME
    $scope.toggleHomeTask = true;
    $scope.toggleFilterHomeTask = function () {
        $scope.toggleHomeTask = $scope.toggleHomeTask === false ? true : false;
    }
    $scope.toggleHomeActiv = true;
    $scope.toggleFilterHomeActiv = function () {
        $scope.toggleHomeActiv = $scope.toggleHomeActiv === false ? true : false;
    }
    $scope.toggleHomeTask = true;
    $scope.toggleCloseHomeTask = function () {
        $scope.toggleHomeTask = $scope.toggleHomeTask === false ? true : false;
    }
    $scope.toggleHomeActiv = true;
    $scope.toggleCloseHomeActiv = function () {
        $scope.toggleHomeActiv = $scope.toggleHomeActiv === false ? true : false;
    }

    // Config CEO
    $scope.toggleCeoTask = true;
    $scope.toggleFilterCeoTask = function () {
        $scope.toggleCeoTask = $scope.toggleCeoTask === false ? true : false;
    }
    $scope.toggleCeoActiv = true;
    $scope.toggleFilterCeoActiv = function () {
        $scope.toggleCeoActiv = $scope.toggleCeoActiv === false ? true : false;
    }
    $scope.toggleCeoTask = true;
    $scope.toggleCloseCeoTask = function () {
        $scope.toggleCeoTask = $scope.toggleCeoTask === false ? true : false;
    }
    $scope.toggleCeoActiv = true;
    $scope.toggleCloseCeoActiv = function () {
        $scope.toggleCeoActiv = $scope.toggleCeoActiv === false ? true : false;
    }

    // Config MANAGER
    $scope.toggleManagerTask = true;
    $scope.toggleFilterManagerTask = function () {
        $scope.toggleManagerTask = $scope.toggleManagerTask === false ? true : false;
    }
    $scope.toggleManagerActiv = true;
    $scope.toggleFilterManagerActiv = function () {
        $scope.toggleManagerActiv = $scope.toggleManagerActiv === false ? true : false;
    }
    $scope.toggleManagerTask = true;
    $scope.toggleCloseManagerTask = function () {
        $scope.toggleManagerTask = $scope.toggleManagerTask === false ? true : false;
    }
    $scope.toggleManagerActiv = true;
    $scope.toggleCloseManagerActiv = function () {
        $scope.toggleManagerActiv = $scope.toggleManagerActiv === false ? true : false;
    }

    // Config TASKS
    $scope.toggleTsTask = true;
    $scope.toggleFilterTsTask = function () {
        $scope.toggleTsTask = $scope.toggleTsTask === false ? true : false;
    }
    $scope.toggleTsActiv = true;
    $scope.toggleFilterTsActiv = function () {
        $scope.toggleTsActiv = $scope.toggleTsActiv === false ? true : false;
    }
    $scope.toggleTsTask = true;
    $scope.toggleCloseTsTask = function () {
        $scope.toggleTsTask = $scope.toggleTsTask === false ? true : false;
    }
    $scope.toggleTsActiv = true;
    $scope.toggleCloseTsActiv = function () {
        $scope.toggleTsActiv = $scope.toggleTsActiv === false ? true : false;
    }
    $scope.toggleTasksEdit = true;
    $scope.toggleFilterTasksEdit = function () {
        $scope.toggleTasksEdit = $scope.toggleTasksEdit === false ? true : false;
    }
    $scope.toggleTasksFilter = true;
    $scope.toggleTasksFilterSet = function () {
        $scope.toggleTasksFilter = $scope.toggleTasksFilter === false ? true : false;
    }
    $scope.toggleTsDropdownFilter = true;
    $scope.toggleTsFilterSet = function () {
        $scope.toggleTsDropdownFilter = $scope.toggleTsDropdownFilter === false ? true : false;
    }
    $scope.toggleTasksFilter = true;
    $scope.toggleCloseTask = function () {
        $scope.toggleTasksFilter = $scope.toggleTasksFilter === false ? true : false;
    }

    // Config ACTIVITIES
    $scope.toggleActvTask = $editTaskFormService.getStatusModal;
    $scope.toggleFilterActvTask = $editTaskFormService.toggleModal;

    $scope.toggleActvActiv = $editActivityFormService.getStatusModal;
    $scope.toggleFilterActvActiv = $editActivityFormService.toggleModal;

    $scope.toggleCloseActvTask = true;
    $scope.toggleCloseActvTask = function () {
        $scope.toggleCloseActvTask = $scope.toggleCloseActvTask === false ? true : false;
    }
    $scope.toggleActvCloseActiv = true;
    $scope.toggleCloseActvActiv = function () {
        $scope.toggleActvCloseActiv = $scope.toggleActvCloseActiv === false ? true : false;
    }
    $scope.toggleActvEdit = true;
    $scope.toggleFilterActvEdit = function () {
        $scope.toggleActvEdit = $scope.toggleActvEdit === false ? true : false;
    }
    $scope.toggleActvFilter = true;
    $scope.toggleActvFilterSet = function () {
        $scope.toggleActvFilter = $scope.toggleActvFilter === false ? true : false;
    }
    $scope.toggleChangePanel = true;
    $scope.toggleChangePanelButton = function () {
        $scope.toggleChangePanel = $scope.toggleChangePanel === false ? true : false;
    }
    $scope.toggleConnectPanel = true;
    $scope.toggleConnectPanelButton = function () {
        $scope.toggleConnectPanel = $scope.toggleConnectPanel === false ? true : false;
    }

    // Config ORGANIZATIONS
    $scope.toggleOrgTask = true;
    $scope.toggleFilterOrgTask = function () {
        $scope.toggleOrgTask = $scope.toggleOrgTask === false ? true : false;
    }
    $scope.toggleOrgActiv = true;
    $scope.toggleFilterOrgActiv = function () {
        $scope.toggleOrgActiv = $scope.toggleOrgActiv === false ? true : false;
    }
    $scope.toggleOrgTask = true;
    $scope.toggleCloseOrgTask = function () {
        $scope.toggleOrgTask = $scope.toggleOrgTask === false ? true : false;
    }
    $scope.toggleOrgActiv = true;
    $scope.toggleCloseOrgActiv = function () {
        $scope.toggleOrgActiv = $scope.toggleOrgActiv === false ? true : false;
    }
    $scope.toggleOrgEdit = true;
    $scope.toggleFilterOrgEdit = function () {
        $scope.toggleOrgEdit = $scope.toggleOrgEdit === false ? true : false;
    }

    // Config CONTACTS
    $scope.toggleContTask = true;
    $scope.toggleFilterContTask = function () {
        $scope.toggleContTask = $scope.toggleContTask === false ? true : false;
    }
    $scope.toggleContActiv = true;
    $scope.toggleFilterContActiv = function () {
        $scope.toggleContActiv = $scope.toggleContActiv === false ? true : false;
    }
    $scope.toggleContTask = true;
    $scope.toggleCloseContTask = function () {
        $scope.toggleContTask = $scope.toggleContTask === false ? true : false;
    }
    $scope.toggleContActiv = true;
    $scope.toggleCloseContActiv = function () {
        $scope.toggleContActiv = $scope.toggleContActiv === false ? true : false;
    }
    $scope.toggleContEdit = true;
    $scope.toggleFilterContEdit = function () {
        $scope.toggleContEdit = $scope.toggleContEdit === false ? true : false;
    }

    // Config COMPANIES
    $scope.toggleCompTask = true;
    $scope.toggleFilterCompTask = function () {
        $scope.toggleCompTask = $scope.toggleCompTask === false ? true : false;
    }
    $scope.toggleCompActiv = true;
    $scope.toggleFilterCompActiv = function () {
        $scope.toggleCompActiv = $scope.toggleCompActiv === false ? true : false;
    }
    $scope.toggleCompEdit = true;
    $scope.toggleFilterCompEdit = function () {
        $scope.toggleCompEdit = $scope.toggleCompEdit === false ? true : false;
    }

    // Config USERS
    $scope.toggleUsersTask = true;
    $scope.toggleFilterUsersTask = function () {
        $scope.toggleUsersTask = $scope.toggleUsersTask === false ? true : false;
    }
    $scope.toggleUsersActiv = true;
    $scope.toggleFilterUsersActiv = function () {
        $scope.toggleUsersActiv = $scope.toggleUsersActiv === false ? true : false;
    }
    $scope.toggleUsersEdit = true;
    $scope.toggleFilterUsersEdit = function () {
        $scope.toggleUsersEdit = $scope.toggleUsersEdit === false ? true : false;
    }

    // Config CATEGORIES
    $scope.toggleCatTask = true;
    $scope.toggleFilterCatTask = function () {
        $scope.toggleCatTask = $scope.toggleCatTask === false ? true : false;
    }
    $scope.toggleCatActiv = true;
    $scope.toggleFilterCatActiv = function () {
        $scope.toggleCatActiv = $scope.toggleCatActiv === false ? true : false;
    }
    $scope.toggleCatEdit = true;
    $scope.toggleFilterCatEdit = function () {
        $scope.toggleCatEdit = $scope.toggleCatEdit === false ? true : false;
    }
}]);

porlaDashboard.controller('formLogin', ['$scope', function ($scope) {
    $scope.master = {};
    $scope.user = {};
    $scope.update = function (user) {
        $scope.master = angular.copy(user);
        $scope.user = {};
        console.log(user);
    };
}]);

porlaDashboard.controller('headerController', ['$scope', '$window', function ($scope, $window) {
    $scope.users = angular.fromJson($window.localStorage.getItem("poria_users"));
    if ((!$scope.users) || ($scope.users.length == 0)) {
        $window.location.assign("/app/login.html");
    }
}]);

porlaDashboard.controller('filterController', [function () {
    $(function () {
        $('.filterTabOne').on('click', function () {
            $('.filterTabTwo, .filterTabThree, .filterTabFour').removeClass('current_page_item');
            $(this).toggleClass('current_page_item');
            $('#magic-line').animate({ left: '0px', width: '60px' });
        });
        $('.filterTabTwo').on('click', function () {
            $('.filterTabOne, .filterTabThree, .filterTabFour').removeClass('current_page_item');
            $(this).toggleClass('current_page_item');
            $('#magic-line').animate({ left: '111px', width: '81px' });
        });
        $('.filterTabThree').on('click', function () {
            $('.filterTabOne, .filterTabTwo, .filterTabFour').removeClass('current_page_item');
            $(this).toggleClass('current_page_item');
            $('#magic-line').animate({ left: '245px', width: '73px' });
        });
        $('.filterTabFour').on('click', function () {
            $('.filterTabOne, .filterTabTwo, .filterTabThree').removeClass('current_page_item');
            $(this).toggleClass('current_page_item');
            $('#magic-line').animate({ left: '367px', width: '73px' });
        });
    });
}]);

porlaDashboard.controller('dialController', [function () {
      this.topDirections = ['left', 'up'];
      this.bottomDirections = ['down', 'right'];
      this.isOpen = false;
      this.tooltipsVisible = true;
      this.availableModes = ['md-fling', 'md-scale'];
      this.selectedMode = 'md-scale';
      this.availableDirections = ['up', 'down', 'left', 'right'];
      this.selectedDirection = 'left';

      $('.add').on('click', function () {
          $('.settings').toggleClass('opacitySet');
      });
}]);

porlaDashboard.controller('chipsSelector', ['$scope', function ($scope) {
    $scope.selectedOrganizations = [];
    $scope.organizationsList = [
        { organization: "Apple", id: 0 },
        { organization: "Netflix", id: 1 },
        { organization: "Microsoft", id: 2 },
        { organization: "Facebook", id: 3 }
    ];
    $scope.selectedContacts = [];
    $scope.contactsList = [
        { contact: "Peter", id: 0 },
        { contact: "Jannet", id: 1 },
        { contact: "Elis", id: 2 },
        { contact: "John", id: 3 }
    ];
}]);

porlaDashboard.controller('dateController', ['$scope', function ($scope) {
    $scope.myDeadline = new Date();
    $scope.myComplete = new Date();
    $scope.isOpen = false;
}]);

porlaDashboard.controller('dropdownController', ['$rootScope', '$scope', function($rootScope, $scope) {
    $scope.ddSelectOptionsType = [
        { text: 'In Progress', value: 'inprogress' },
        { text: 'Done', value: 'done' },
        { text: 'Overdue', value: 'overdue' },
        { text: 'Delayed', value: 'delayed' }
    ];
    $scope.ddSelectSelectedType = { text: "Type" };

    $scope.ddSelectOptionsActiv = [
        { text: 'Phone Call', value: 'inprogress' },
        { text: 'Email', value: 'done' },
        { text: 'Meeting', value: 'overdue' }
    ];
    $scope.ddSelectSelectedActiv = { text: "Options" };

    $scope.ddSelectOptionsStatus = [
        { text: 'Status One', value: 'statusone' },
        { text: 'Status Two', value: 'statustwo' },
        { text: 'Status Three', value: 'statusthree' },
        { text: 'Status Four', value: 'statusfour' }
    ];
    $scope.ddSelectSelectedStatus = { text: "Status" };

    $scope.ddSelectOptionsAssign = [
        { text: 'Assign One', value: 'assignone' },
        { text: 'Assign Two', value: 'assigntwo' },
        { text: 'Assign Three', value: 'assignthree' },
        { text: 'Assign Four', value: 'assignfour' }
    ];
    $scope.ddSelectSelectedAssign = { text: "Assign to" };

    $scope.ddSelectOptionsDone = [
        { text: 'Done One', value: 'doneone' },
        { text: 'Done Two', value: 'donetwo' },
        { text: 'Done Three', value: 'donethree' },
        { text: 'Done Four', value: 'donefour' }
    ];
    $scope.ddSelectSelectedDone = { text: "Done by" };

    $scope.ddSelectOptionsCat = [
        { text: 'Governmental', value: 'Governmental' },
        { text: 'Clients', value: 'Clients' },
        { text: 'Employees', value: 'Employees' },
        { text: 'Competitors', value: 'Competitors' },
        { text: 'Suppliers', value: 'Suppliers' },
        { text: 'NGOs', value: 'NGOs' },
        { text: 'Community', value: 'Community' },
        { text: 'Distributors', value: 'Distributors' },
        { text: 'Partners', value: 'Partners' },
        { text: 'Share', value: 'Share' },
        { text: 'Media', value: 'Media' },
        { text: 'Research', value: 'Research' },
        { text: 'Elected', value: 'Elected' },
        { text: 'Opinion', value: 'Opinion' }
    ];
    $scope.ddSelectSelectedCat = { text: "by categories" };

    $scope.ddSelectOptionsOrg = [
        { text: 'Apple', value: 'Apple' },
        { text: 'Adobe', value: 'done' },
        { text: 'Facebook', value: 'Facebook' },
        { text: 'Samsung', value: 'Samsung' },
        { text: 'Sony', value: 'Sony' },
        { text: 'Mail Chimp', value: 'Mail Chimp' },
        { text: 'Nintendo', value: 'Nintendo' },
        { text: 'HP', value: 'HP' },
        { text: 'Microsoft', value: 'Microsoft' },
        { text: 'Disney', value: 'Disney' },
        { text: 'Marvel', value: 'Marvel' },
        { text: 'Invision', value: 'Invision' }
    ];
    $scope.ddSelectSelectedOrg = { text: "Organizations" };

    $scope.ddSelectOptionsContact = [
        { text: 'Gisele Bin', value: 'Gisele Bin' },
        { text: 'John Petrucci', value: 'John Petrucci' },
        { text: 'Janet Oliver', value: 'Janet Oliver' },
        { text: 'Jesse James', value: 'Jasse James' }
    ];
    $scope.ddSelectSelectedContact = { text: "Contacts" };

    $scope.ddSelectOptionsOwner = [
        { text: 'Gisele Bin', value: 'Gisele Bin' },
        { text: 'John Petrucci', value: 'John Petrucci' },
        { text: 'Janet Oliver', value: 'Janet Oliver' },
        { text: 'Jesse James', value: 'Jasse James' }
    ];
    $scope.ddSelectSelectedOwner = { text: "Owner" };

    $scope.ddSelectOptionsCreatedBy = [
        { text: 'Gisele Bin', value: 'Gisele Bin' },
        { text: 'John Petrucci', value: 'John Petrucci' },
        { text: 'Janet Oliver', value: 'Janet Oliver' },
        { text: 'Jesse James', value: 'Jasse James' }
    ];
    $scope.ddSelectSelectedCreatedBy = { text: "Created BY" };

    $scope.ddSelectOptionsRole = [
        { text: 'Role One', value: 'Role One' },
        { text: 'Role Two', value: 'Role Two' },
        { text: 'Role Three', value: 'Role Three' },
        { text: 'Role Four', value: 'Role Four' }
    ];
    $scope.ddSelectSelectedRole = { text: "Role" };

    $scope.ddSelectOptionsCompany = [
        { text: 'Apple', value: 'Apple' },
        { text: 'Adobe', value: 'done' },
        { text: 'Facebook', value: 'Facebook' },
        { text: 'Samsung', value: 'Samsung' },
        { text: 'Sony', value: 'Sony' },
        { text: 'Mail Chimp', value: 'Mail Chimp' },
        { text: 'Nintendo', value: 'Nintendo' },
        { text: 'HP', value: 'HP' },
        { text: 'Microsoft', value: 'Microsoft' },
        { text: 'Disney', value: 'Disney' },
        { text: 'Marvel', value: 'Marvel' },
        { text: 'Invision', value: 'Invision' }
    ];
    $scope.ddSelectSelectedCompany = {
        text: "Company"
    };
}]);

porlaDashboard.controller('DoughnutCtrl', ['$scope', function ($scope) {
    $scope.labels = ["Task in progress", "Tasks Completed", "Tasks ready to start"];
    $scope.data = [50, 30, 20];
    $scope.datainfo = [
        {
            title: 'Improve the relationship with the ministry of education',
            valueProcess: 3,
            percentProcess: 30,
            valueCompleted: 5,
            percentCompleted: 50,
            valueReady: 2,
            percentReady: 20
        },
        {
            title: 'Improve relationship with the neighbors of the factory',
            valueProcess: 3,
            percentProcess: 30,
            valueCompleted: 5,
            percentCompleted: 50,
            valueReady: 2,
            percentReady: 20
        },
        {
            title: 'Renegotiate budget with partners and suppliers',
            valueProcess: 3,
            percentProcess: 30,
            valueCompleted: 5,
            percentCompleted: 50,
            valueReady: 2,
            percentReady: 20
        },
        {
            title: 'Research new markets for expansion',
            valueProcess: 3,
            percentProcess: 30,
            valueCompleted: 5,
            percentCompleted: 50,
            valueReady: 2,
            percentReady: 20
        },
        {
            title: 'Improve relationship with the neighbors of the factory',
            valueProcess: 3,
            percentProcess: 30,
            valueCompleted: 5,
            percentCompleted: 50,
            valueReady: 2,
            percentReady: 20
        },
        {
            title: 'Renegotiate budget with partners and suppliers',
            valueProcess: 3,
            percentProcess: 30,
            valueCompleted: 5,
            percentCompleted: 50,
            valueReady: 2,
            percentReady: 20
        }
    ];
}]);

porlaDashboard.controller('fileControllerEditor', [function () {
    var inputseditor = document.querySelectorAll('.inputfile');
    Array.prototype.forEach.call(inputseditor, function (input) {
        var label = input.nextElementSibling,
            labelValEditor = label.innerHTML;
        input.addEventListener('change', function (e) {
            var fileName = '';
            if (this.files && this.files.length > 1)
                fileName = (this.getAttribute('data-multiple-caption') || '').replace('{count}', this.files.length);
            else
                fileName = e.target.value.split('\\').pop();

            if (fileName)
                label.querySelector('span.labelValEditor').innerHTML = fileName;
            else
                label.innerHTML = labelValEditor;
        });
    });
}]);

porlaDashboard.controller('fileController', [function () {
    var inputs = document.querySelectorAll('.inputfile');
    Array.prototype.forEach.call(inputs, function (input) {
        var label = input.nextElementSibling,
            labelVal = label.innerHTML;
        input.addEventListener('change', function (e) {
            var fileName = '';
            if (this.files && this.files.length > 1)
                fileName = (this.getAttribute('data-multiple-caption') || '').replace('{count}', this.files.length);
            else
                fileName = e.target.value.split('\\').pop();

            if (fileName)
                label.querySelector('span').innerHTML = fileName;
            else
                label.innerHTML = labelVal;
        });
    });
}]);

porlaDashboard.controller('fileControllerTask', [function () {
    var inputstask = document.querySelectorAll("#attachedTask > .inputfile");

    Array.prototype.forEach.call(inputstask, function (input) {
        var label = input.nextElementSibling,
            labelValTask = label.innerHTML;
        input.addEventListener('change', function (e) {
            var fileName = '';
            if (this.files && this.files.length > 1)
                fileName = (this.getAttribute('data-multiple-caption') || '').replace('{count}', this.files.length);
            else
                fileName = e.target.value.split('\\').pop();

            if (fileName)
                label.querySelector('span.labelValTask').innerHTML = fileName;
            else
                label.innerHTML = labelValTask;
        });
    });
}]);

porlaDashboard.controller('fileControllerActiv', [function () {
    var inputsactiv = document.querySelectorAll("#attachedActiv > .inputfile");

    Array.prototype.forEach.call(inputsactiv, function (input) {
        var label = input.nextElementSibling,
            labelValAtiv = label.innerHTML;
        input.addEventListener('change', function (e) {
            var fileName = '';
            if (this.files && this.files.length > 1)
                fileName = (this.getAttribute('data-multiple-caption') || '').replace('{count}', this.files.length);
            else
                fileName = e.target.value.split('\\').pop();

            if (fileName)
                label.querySelector('span').innerHTML = fileName;
            else
                label.innerHTML = labelValAtiv;
        });
    });
}]);

porlaDashboard.controller('homeController', ['$rootScope', '$scope', '$location', function ($rootScope, $scope, $location) {
    $rootScope.activetab = $location.path();
    $(".dashboardContent").ready(function(){
          $("footer").removeClass("visibleFooter");
   });
}]);

porlaDashboard.controller('ceoController', ['$rootScope', '$scope', '$location', function ($rootScope, $scope, $location) {
    $rootScope.activetab = $location.path();
    $(".dashboardContent").ready(function(){
          $("footer").removeClass("visibleFooter");
   });
}]);

porlaDashboard.controller('sidebarCeo', ['$scope', function ($scope) {
    $scope.activities = [
        {
            listingIcon: 'people',
            listingOne: 'Lorem Ipsum content',
            listingTwo: 'Lorem Ipsum content',
            listingThree: 'Lorem Ipsum content'
        },
        {
            listingIcon: 'hands',
            listingOne: 'Lorem Ipsum content',
            listingTwo: 'Lorem Ipsum content',
            listingThree: 'Lorem Ipsum content'
        },
        {
            listingIcon: 'stats',
            listingOne: 'Lorem Ipsum content',
            listingTwo: 'Lorem Ipsum content',
            listingThree: 'Lorem Ipsum content'
        },
        {
            listingIcon: 'search',
            listingOne: 'Lorem Ipsum content',
            listingTwo: 'Lorem Ipsum content',
            listingThree: 'Lorem Ipsum content'
        },
        {
            listingIcon: 'company',
            listingOne: 'Lorem Ipsum content',
            listingTwo: 'Lorem Ipsum content',
            listingThree: 'Lorem Ipsum content'
        }
    ];
}]);

porlaDashboard.controller('usersActivities', ['$scope', function ($scope) {
    $scope.users = [
        {
            title: 'John Appleseed',
            userimage: 'john.png',
            ocupation: 'User title',
            taskcomp: '87',
            activities: '25'
        },
        {
            title: 'John Appleseed',
            userimage: 'john.png',
            ocupation: 'User title',
            taskcomp: '87',
            activities: '25'
        },
        {
            title: 'John Appleseed',
            userimage: 'john.png',
            ocupation: 'User title',
            taskcomp: '87',
            activities: '25'
        },
        {
            title: 'John Appleseed',
            userimage: 'john.png',
            ocupation: 'User title',
            taskcomp: '87',
            activities: '25'
        },
        {
            title: 'John Appleseed',
            userimage: 'john.png',
            ocupation: 'User title',
            taskcomp: '87',
            activities: '25'
        },
        {
            title: 'John Appleseed',
            userimage: 'john.png',
            ocupation: 'User title',
            taskcomp: '87',
            activities: '25'
        },
        {
            title: 'John Appleseed',
            userimage: 'john.png',
            ocupation: 'User title',
            taskcomp: '87',
            activities: '25'
        },
        {
            title: 'John Appleseed',
            userimage: 'john.png',
            ocupation: 'User title',
            taskcomp: '87',
            activities: '25'
        },
        {
            title: 'John Appleseed',
            userimage: 'john.png',
            ocupation: 'User title',
            taskcomp: '87',
            activities: '25'
        },
        {
            title: 'John Appleseed',
            userimage: 'john.png',
            ocupation: 'User title',
            taskcomp: '87',
            activities: '25'
        },
        {
            title: 'John Appleseed',
            userimage: 'john.png',
            ocupation: 'User title',
            taskcomp: '87',
            activities: '25'
        },
        {
            title: 'John Appleseed',
            userimage: 'john.png',
            ocupation: 'User title',
            taskcomp: '87',
            activities: '25'
        },
        {
            title: 'John Appleseed',
            userimage: 'john.png',
            ocupation: 'User title',
            taskcomp: '87',
            activities: '25'
        },
        {
            title: 'John Appleseed',
            userimage: 'john.png',
            ocupation: 'User title',
            taskcomp: '87',
            activities: '25'
        },
        {
            title: 'John Appleseed',
            userimage: 'john.png',
            ocupation: 'User title',
            taskcomp: '87',
            activities: '25'
        },
        {
            title: 'John Appleseed',
            userimage: 'john.png',
            ocupation: 'User title',
            taskcomp: '87',
            activities: '25'
        }
    ];
}]);

porlaDashboard.controller('managersController', ['$rootScope', '$scope', '$location', function ($rootScope, $scope, $location) {
    $rootScope.activetab = $location.path();
    $(".dashboardContent").ready(function(){
          $("footer").removeClass("visibleFooter");
   });
}]);

porlaDashboard.controller('sidebarManager', ['$scope', function ($scope) {
    $scope.deadline = [
        {
            taskid: '1',
            title: 'Lorem Ipsum content',
            optionone: 'Lorem Ipsum content',
            optiontwo: 'Lorem Ipsum content',
            optionthree: 'Lorem Ipsum content',
            deadlinestatus: 'red'
        },
        {
            taskid: '2',
            title: 'Lorem Ipsum content',
            optionone: 'Lorem Ipsum content',
            optiontwo: 'Lorem Ipsum content',
            optionthree: 'Lorem Ipsum content',
            deadlinestatus: 'red'
        },
        {
            taskid: '3',
            title: 'Lorem Ipsum content',
            optionone: 'Lorem Ipsum content',
            optiontwo: 'Lorem Ipsum content',
            optionthree: 'Lorem Ipsum content',
            deadlinestatus: 'red'
        },
        {
            taskid: '4',
            title: 'Lorem Ipsum content',
            optionone: 'Lorem Ipsum content',
            optiontwo: 'Lorem Ipsum content',
            optionthree: 'Lorem Ipsum content',
            deadlinestatus: 'orange'
        },
        {
            taskid: '5',
            title: 'Lorem Ipsum content',
            optionone: 'Lorem Ipsum content',
            optiontwo: 'Lorem Ipsum content',
            optionthree: 'Lorem Ipsum content',
            deadlinestatus: 'orange'
        },
        {
            taskid: '6',
            title: 'Lorem Ipsum content',
            optionone: 'Lorem Ipsum content',
            optiontwo: 'Lorem Ipsum content',
            optionthree: 'Lorem Ipsum content',
            deadlinestatus: 'yellow'
        },
        {
            taskid: '7',
            title: 'Lorem Ipsum content',
            optionone: 'Lorem Ipsum content',
            optiontwo: 'Lorem Ipsum content',
            optionthree: 'Lorem Ipsum content',
            deadlinestatus: 'green'
        },
        {
            taskid: '8',
            title: 'Lorem Ipsum content',
            optionone: 'Lorem Ipsum content',
            optiontwo: 'Lorem Ipsum content',
            optionthree: 'Lorem Ipsum content',
            deadlinestatus: 'green'
        }
    ];
}]);

porlaDashboard.controller('categoriesOrganizations', ['$scope', function ($scope) {
    $scope.organizations = [
        {
            categorie: 'Governmental',
            categoryimage: 'governmental',
            ocupation: 'User title',
            task: 10,
            taskcomp: 40,
            taskcompunit: 4,
            activities: 25
        },
        {
            categorie: 'Clients',
            categoryimage: 'clients',
            ocupation: 'User title',
            task: 10,
            taskcomp: 40,
            taskcompunit: 4,
            activities: 25
        },
        {
            categorie: 'Employees',
            categoryimage: 'employees',
            ocupation: 'User title',
            task: 10,
            taskcomp: 40,
            taskcompunit: 4,
            activities: 25
        },
        {
            categorie: 'Competitors',
            categoryimage: 'competitors',
            ocupation: 'User title',
            task: 10,
            taskcomp: 40,
            taskcompunit: 4,
            activities: 25
        },
        {
            categorie: 'Suppliers',
            categoryimage: 'suppliers',
            ocupation: 'User title',
            task: 10,
            taskcomp: 40,
            taskcompunit: 4,
            activities: 25
        },
        {
            categorie: 'NGOs',
            categoryimage: 'ngos',
            ocupation: 'User title',
            task: 10,
            taskcomp: 40,
            taskcompunit: 4,
            activities: 25
        },
        {
            categorie: 'Community',
            categoryimage: 'community',
            ocupation: 'User title',
            task: 10,
            taskcomp: 40,
            taskcompunit: 4,
            activities: 25
        },
        {
            categorie: 'Distributors',
            categoryimage: 'distributors',
            ocupation: 'User title',
            task: 10,
            taskcomp: 40,
            taskcompunit: 4,
            activities: 25
        },
        {
            categorie: 'Partners',
            categoryimage: 'partners',
            ocupation: 'User title',
            task: 10,
            taskcomp: 40,
            taskcompunit: 4,
            activities: 25
        },
        {
            categorie: 'Share',
            categoryimage: 'share',
            ocupation: 'User title',
            task: 10,
            taskcomp: 40,
            taskcompunit: 4,
            activities: 25
        },
        {
            categorie: 'Media',
            categoryimage: 'media',
            ocupation: 'User title',
            task: 10,
            taskcomp: 40,
            taskcompunit: 4,
            activities: 25
        },
        {
            categorie: 'Research',
            categoryimage: 'research',
            ocupation: 'User title',
            task: 10,
            taskcomp: 40,
            taskcompunit: 4,
            activities: 25
        },
        {
            categorie: 'Elected',
            categoryimage: 'elected',
            ocupation: 'User title',
            task: 10,
            taskcomp: 40,
            taskcompunit: 4,
            activities: 25
        },
        {
            categorie: 'Opinion',
            categoryimage: 'opinion',
            ocupation: 'User title',
            task: 10,
            taskcomp: 40,
            taskcompunit: 4,
            activities: 25
        }
    ];
}]);

porlaDashboard.controller('tasksController', ['$rootScope', '$scope', '$location', function ($rootScope, $scope, $location) {
    $rootScope.activetab = $location.path();
    $scope.checked = [];
    $(".dashboardContent").ready(function(){
          $("footer").removeClass("visibleFooter");
   });
}]);

porlaDashboard.controller('tasksResult', ['$rootScope', '$scope', '$taskService', '$dialogServiceFactory', function ($rootScope, $scope, $taskService, $dialogServiceFactory) {
    $rootScope.scopeTaskResult = $scope;
    $scope.tasks = $taskService.getTasks();
    $scope.goRelated = false;

    $scope.refreshList = function () {
        $scope.tasks = $taskService.getTasks();
    };

    $scope.removeTask = function (event, index) {
        $dialogServiceFactory.showConfirmationDeleteDialog(event,
            function () {
                $taskService.removeTask(index);
                $scope.refreshList();
            }, null);
    };
}]);

porlaDashboard.controller('activitiesController', ['$rootScope', '$scope', '$location', function ($rootScope, $scope, $location) {
    $rootScope.activetab = $location.path();
    $(".dashboardContent").ready(function(){
          $("footer").removeClass("visibleFooter");
   });
}]);

porlaDashboard.controller('activitiesResult', ['$rootScope', '$scope', '$activityService', '$dialogServiceFactory', function ($rootScope, $scope, $activityService, $dialogServiceFactory) {
    $scope.activities = $activityService.getActivities();

    $scope.removeActivity = function (event, index) {
        $dialogServiceFactory.showConfirmationDeleteDialog(event,
            function () {
                $activityService.removeActivity(index);
                $scope.activities = $activityService.getActivities();
            }, null);
    };
}]);

porlaDashboard.controller('organizationsController', ['$rootScope', '$scope', '$location', function ($rootScope, $scope, $location) {
    $rootScope.activetab = $location.path();
    $(".dashboardContent").ready(function(){
          $("footer").removeClass("visibleFooter");
   });
}]);

porlaDashboard.controller('organizationsResult', ['$scope', '$dialogServiceFactory', function ($scope, $dialogServiceFactory) {
    $scope.orderByField = 'ownerField';
    $scope.orderByField = 'categoryField';
    $scope.orderByField = 'typeField';
    $scope.orderByField = 'nameField';
    $scope.reverseSort = false;
    $scope.data = {
        organizations: [
            {
                title: 'APPLE',
                category: 'Technology',
                type: 'GovernamentalOne',
                ownership: 'Kevin Salamanda'
            },
            {
                title: 'MICROSOFT',
                category: 'Technology',
                type: 'GovernamentalTwo',
                ownership: 'Kevin Salamanda'
            },
            {
                title: 'APPLE',
                category: 'Technology',
                type: 'GovernamentalThree',
                ownership: 'Kevin Salamanda'
            },
            {
                title: 'MICROSOFT',
                category: 'Technology',
                type: 'GovernamentalFour',
                ownership: 'Kevin Salamanda'
            },
            {
                title: 'APPLE',
                category: 'Technology',
                type: 'GovernamentalFive',
                ownership: 'Kevin Salamanda'
            },
            {
                title: 'MICROSOFT',
                category: 'Technology',
                type: 'GovernamentalSix',
                ownership: 'Kevin Salamanda'
            },
            {
                title: 'APPLE',
                category: 'Technology',
                type: 'GovernamentalSeven',
                ownership: 'Kevin Salamanda'
            },
            {
                title: 'MICROSOFT',
                category: 'Technology',
                type: 'GovernamentalEight',
                ownership: 'Kevin Salamanda'
            },
            {
                title: 'MICROSOFT',
                category: 'Technology',
                type: 'GovernamentalNine',
                ownership: 'Kevin Salamanda'
            },
            {
                title: 'MICROSOFT',
                category: 'Technology',
                type: 'GovernamentalTen',
                ownership: 'Kevin Salamanda'
            },
            {
                title: 'MICROSOFT',
                category: 'Technology',
                type: 'Governamental',
                ownership: 'Kevin Salamanda'
            },
            {
                title: 'MICROSOFT',
                category: 'Technology',
                type: 'Governamental',
                ownership: 'Kevin Salamanda'
            }
        ]
    };

    $scope.remover = function (event, index) {
        $dialogServiceFactory.showConfirmationDeleteDialog(event, function () { $scope.data.organizations.splice(index, 1); }, null);
    };
}]);

porlaDashboard.controller('contactsController', ['$rootScope', '$scope', '$location', function ($rootScope, $scope, $location) {
    $rootScope.activetab = $location.path();
    $(".dashboardContent").ready(function(){
          $("footer").removeClass("visibleFooter");
   });
}]);

porlaDashboard.controller('contactsResult', ['$scope', '$dialogServiceFactory', function ($scope, $dialogServiceFactory) {
    $scope.orderByField = 'ownerField';
    $scope.orderByField = 'categoryField';
    $scope.orderByField = 'typeField';
    $scope.orderByField = 'nameField';
    $scope.reverseSort = false;
    $scope.data = {
        contacts: [
            {
                firstname: 'John',
                familyname: 'Petrucci',
                title: 'Loren Title',
                organization: 'Loren Title',
                userimage: 'john.png',
                organization: 'Dream Theater',
                ownership: 'Ryan Carter',
                phone: '(895) 84932-4324',
                email: 'john@dt.com',
                taskcomp: '87',
                activities: '25'
            },
            {
                firstname: 'John',
                familyname: 'Petrucci',
                title: 'Loren Title',
                organization: 'Loren Title',
                userimage: 'john.png',
                organization: 'Dream Theater',
                ownership: 'Ryan Carter',
                phone: '(895) 84932-4324',
                email: 'john@dt.com',
                taskcomp: '87',
                activities: '25'
            },
            {
                firstname: 'John',
                familyname: 'Petrucci',
                title: 'Loren Title',
                organization: 'Loren Title',
                userimage: 'john.png',
                organization: 'Dream Theater',
                ownership: 'Ryan Carter',
                phone: '(895) 84932-4324',
                email: 'john@dt.com',
                taskcomp: '87',
                activities: '25'
            },
            {
                firstname: 'John',
                familyname: 'Petrucci',
                title: 'Loren Title',
                organization: 'Loren Title',
                userimage: 'john.png',
                organization: 'Dream Theater',
                ownership: 'Ryan Carter',
                phone: '(895) 84932-4324',
                email: 'john@dt.com',
                taskcomp: '87',
                activities: '25'
            },
            {
                firstname: 'John',
                familyname: 'Petrucci',
                title: 'Loren Title',
                organization: 'Loren Title',
                userimage: 'john.png',
                organization: 'Dream Theater',
                ownership: 'Ryan Carter',
                phone: '(895) 84932-4324',
                email: 'john@dt.com',
                taskcomp: '87',
                activities: '25'
            },
            {
                firstname: 'John',
                familyname: 'Petrucci',
                title: 'Loren Title',
                organization: 'Loren Title',
                userimage: 'john.png',
                organization: 'Dream Theater',
                ownership: 'Ryan Carter',
                phone: '(895) 84932-4324',
                email: 'john@dt.com',
                taskcomp: '87',
                activities: '25'
            },
            {
                firstname: 'John',
                familyname: 'Petrucci',
                title: 'Loren Title',
                organization: 'Loren Title',
                userimage: 'john.png',
                organization: 'Dream Theater',
                ownership: 'Ryan Carter',
                phone: '(895) 84932-4324',
                email: 'john@dt.com',
                taskcomp: '87',
                activities: '25'
            },
            {
                firstname: 'John',
                familyname: 'Petrucci',
                title: 'Loren Title',
                organization: 'Loren Title',
                userimage: 'john.png',
                organization: 'Dream Theater',
                ownership: 'Ryan Carter',
                phone: '(895) 84932-4324',
                email: 'john@dt.com',
                taskcomp: '87',
                activities: '25'
            },
            {
                firstname: 'John',
                familyname: 'Petrucci',
                title: 'Loren Title',
                organization: 'Loren Title',
                userimage: 'john.png',
                organization: 'Dream Theater',
                ownership: 'Ryan Carter',
                phone: '(895) 84932-4324',
                email: 'john@dt.com',
                taskcomp: '87',
                activities: '25'
            },
            {
                firstname: 'John',
                familyname: 'Petrucci',
                title: 'Loren Title',
                organization: 'Loren Title',
                userimage: 'john.png',
                organization: 'Dream Theater',
                ownership: 'Ryan Carter',
                phone: '(895) 84932-4324',
                email: 'john@dt.com',
                taskcomp: '87',
                activities: '25'
            },
            {
                firstname: 'John',
                familyname: 'Petrucci',
                title: 'Loren Title',
                organization: 'Loren Title',
                userimage: 'john.png',
                organization: 'Dream Theater',
                ownership: 'Ryan Carter',
                phone: '(895) 84932-4324',
                email: 'john@dt.com',
                taskcomp: '87',
                activities: '25'
            },
            {
                firstname: 'John',
                familyname: 'Petrucci',
                title: 'Loren Title',
                organization: 'Loren Title',
                userimage: 'john.png',
                organization: 'Dream Theater',
                ownership: 'Ryan Carter',
                phone: '(895) 84932-4324',
                email: 'john@dt.com',
                taskcomp: '87',
                activities: '25'
            }
        ]
    };

    $scope.remover = function (event, index) {
        $dialogServiceFactory.showConfirmationDeleteDialog(event, function () { $scope.data.contacts.splice(index, 1); }, null);
    };
}]);

porlaDashboard.controller('companiesController', ['$rootScope', '$scope', '$location', function ($rootScope, $scope, $location) {
    $rootScope.activetab = $location.path();
    $(".dashboardContent").ready(function(){
          $("footer").removeClass("visibleFooter");
   });
}]);

porlaDashboard.controller('companiesResult', ['$scope', '$dialogServiceFactory', function ($scope, $dialogServiceFactory) {
    $scope.companies = [
        { title: 'Apple', brandimage: 'apple' },
        { title: 'Adobe', brandimage: 'adobe' },
        { title: 'Facebook', brandimage: 'facebook' },
        { title: 'Samsung', brandimage: 'samsung' },
        { title: 'Sony', brandimage: 'sony' },
        { title: 'Mail Chimp', brandimage: 'mailchimp' },
        { title: 'Nintendo', brandimage: 'nintendo' },
        { title: 'HP', brandimage: 'hp' },
        { title: 'Microsoft', brandimage: 'microsoft' },
        { title: 'Disney', brandimage: 'disney' },
        { title: 'Marvel', brandimage: 'marvel' },
        { title: 'Invision', brandimage: 'invision' }
    ];

    $scope.remover = function (event, index) {
        $dialogServiceFactory.showConfirmationDeleteDialog(event, function () { $scope.companies.splice(index, 1); }, null);
    };
}]);

porlaDashboard.controller('categoriesController', ['$rootScope', '$scope', '$location', function ($rootScope, $scope, $location) {
    $rootScope.activetab = $location.path();
    $(".dashboardContent").ready(function(){
          $("footer").removeClass("visibleFooter");
   });
}]);

porlaDashboard.controller('categoriesResult', ['$scope', '$location', '$dialogServiceFactory', function ($scope, $location, $dialogServiceFactory) {
    $scope.orderByField = 'categoryField';
    $scope.orderByField = 'companyField';
    $scope.orderByField = 'influencedField';
    $scope.orderByField = 'influencingField';
    $scope.orderByField = 'nameField';
    $scope.reverseSort = false;
    $scope.data = {
        categories: [
            {
                firstname: 'John',
                familyname: 'Appleseed',
                influencing: 'Loren Title',
                influenced: 'john.png',
                company: 'Apple',
                brandimage: 'apple',
                category: 'governmental'
            },
            {
                firstname: 'John',
                familyname: 'Appleseed',
                influencing: 'Loren Title',
                influenced: 'john.png',
                company: 'Apple',
                brandimage: 'apple',
                category: 'employees'
            },
            {
                firstname: 'John',
                familyname: 'Appleseed',
                influencing: 'Loren Title',
                influenced: 'john.png',
                company: 'Apple',
                brandimage: 'apple',
                category: 'distributors'
            },
            {
                firstname: 'John',
                familyname: 'Appleseed',
                influencing: 'Loren Title',
                influenced: 'john.png',
                company: 'Apple',
                brandimage: 'apple',
                category: 'suppliers'
            },
            {
                firstname: 'John',
                familyname: 'Appleseed',
                influencing: 'Loren Title',
                influenced: 'john.png',
                company: 'Apple',
                brandimage: 'apple',
                category: 'ngos'
            },
            {
                firstname: 'John',
                familyname: 'Appleseed',
                influencing: 'Loren Title',
                influenced: 'john.png',
                company: 'Apple',
                brandimage: 'apple',
                category: 'community'
            },
            {
                firstname: 'John',
                familyname: 'Appleseed',
                influencing: 'Loren Title',
                influenced: 'john.png',
                company: 'Apple',
                brandimage: 'apple',
                category: 'competitors'
            },
            {
                firstname: 'John',
                familyname: 'Appleseed',
                influencing: 'Loren Title',
                influenced: 'john.png',
                company: 'Apple',
                brandimage: 'apple',
                category: 'partners'
            },
            {
                firstname: 'John',
                familyname: 'Appleseed',
                influencing: 'Loren Title',
                influenced: 'john.png',
                company: 'Apple',
                brandimage: 'apple',
                category: 'clients'
            },
            {
                firstname: 'John',
                familyname: 'Appleseed',
                influencing: 'Loren Title',
                influenced: 'john.png',
                company: 'Apple',
                brandimage: 'apple',
                category: 'media'
            },
            {
                firstname: 'John',
                familyname: 'Appleseed',
                influencing: 'Loren Title',
                influenced: 'john.png',
                company: 'Apple',
                brandimage: 'apple',
                category: 'opinion'
            },
            {
                firstname: 'John',
                familyname: 'Appleseed',
                influencing: 'Loren Title',
                influenced: 'john.png',
                company: 'Apple',
                brandimage: 'apple',
                category: 'elected'
            }
        ]
    };

    $scope.remover = function (event, index) {
        $dialogServiceFactory.showConfirmationDeleteDialog(event, function () { $scope.data.categories.splice(index, 1); }, null);
    };
}]);

porlaDashboard.controller('usersController', ['$rootScope', '$scope', '$location', function ($rootScope, $scope, $location) {
    $rootScope.activetab = $location.path();
    $(".dashboardContent").ready(function(){
          $("footer").removeClass("visibleFooter");
   });
}]);

porlaDashboard.controller('usersResult', ['$scope', '$location', '$dialogServiceFactory', function ($scope, $location, $dialogServiceFactory) {
    $scope.orderByField = 'companyField';
    $scope.orderByField = 'roleField';
    $scope.orderByField = 'titleField';
    $scope.orderByField = 'nameField';
    $scope.reverseSort = false;
    $scope.data = {
        users: [
            {
                firstname: 'John',
                familyname: 'Appleseed',
                title: 'Loren Title',
                role: 'Loren Role',
                company: 'Apple',
                brandimage: 'apple'
            },
            {
                firstname: 'John',
                familyname: 'Appleseed',
                title: 'Loren Title',
                role: 'Loren Role',
                company: 'Apple',
                brandimage: 'apple'
            },
            {
                firstname: 'John',
                familyname: 'Appleseed',
                title: 'Loren Title',
                role: 'Loren Role',
                company: 'Apple',
                brandimage: 'apple'
            },
            {
                firstname: 'John',
                familyname: 'Appleseed',
                title: 'Loren Title',
                role: 'Loren Role',
                company: 'Apple',
                brandimage: 'apple'
            },
            {
                firstname: 'John',
                familyname: 'Appleseed',
                title: 'Loren Title',
                role: 'Loren Role',
                company: 'Apple',
                brandimage: 'apple'
            },
            {
                firstname: 'John',
                familyname: 'Appleseed',
                title: 'Loren Title',
                role: 'Loren Role',
                company: 'Apple',
                brandimage: 'apple'
            },
            {
                firstname: 'John',
                familyname: 'Appleseed',
                title: 'Loren Title',
                role: 'Loren Role',
                company: 'Apple',
                brandimage: 'apple'
            },
            {
                firstname: 'John',
                familyname: 'Appleseed',
                title: 'Loren Title',
                role: 'Loren Role',
                company: 'Apple',
                brandimage: 'apple'
            },
            {
                firstname: 'John',
                familyname: 'Appleseed',
                title: 'Loren Title',
                role: 'Loren Role',
                company: 'Apple',
                brandimage: 'apple'
            },
            {
                firstname: 'John',
                familyname: 'Appleseed',
                title: 'Loren Title',
                role: 'Loren Role',
                company: 'Apple',
                brandimage: 'apple'
            },
            {
                firstname: 'John',
                familyname: 'Appleseed',
                title: 'Loren Title',
                role: 'Loren Role',
                company: 'Apple',
                brandimage: 'apple'
            },
            {
                firstname: 'John',
                familyname: 'Appleseed',
                title: 'Loren Title',
                role: 'Loren Role',
                company: 'Apple',
                brandimage: 'apple'
            }
        ]
    };

    $scope.remover = function (event, index) {
        $dialogServiceFactory.showConfirmationDeleteDialog(event, function () { $scope.data.users.splice(index, 1); }, null);
    };
}]);
