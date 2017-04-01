
porlaDashboard.factory('$activityService', ['$http', function ($http) {
   
    $http.get("/api/Activities")
   .then(function (response) {
       $scope.myWelcome = response.data;
   });
    //var activities = [
    //    {
    //        title: 'Lorem Ipsum Task',
    //        content: 'Lorem ipsum dolor sit amet, consectetur adipiscing elit, sed do eiusmod tempor incididunt.',
    //        user: 'John Appleseed',
    //        relatedtogoal: 'Loren goal',
    //        createdyear: 2017,
    //        createdmonth: 01,
    //        createdday: 01
    //    },
    //    {
    //        title: 'Lorem Ipsum Task',
    //        content: 'Lorem ipsum dolor sit amet, consectetur adipiscing elit, sed do eiusmod tempor incididunt.',
    //        user: 'John Appleseed',
    //        relatedtogoal: 'Loren goal',
    //        createdyear: 2017,
    //        createdmonth: 01,
    //        createdday: 01
    //    },
    //    {
    //        title: 'Lorem Ipsum Task',
    //        content: 'Lorem ipsum dolor sit amet, consectetur adipiscing elit, sed do eiusmod tempor incididunt.',
    //        user: 'John Appleseed',
    //        relatedtogoal: 'Loren goal',
    //        createdyear: 2017,
    //        createdmonth: 01,
    //        createdday: 01
    //    },
    //    {
    //        title: 'Lorem Ipsum Task',
    //        content: 'Lorem ipsum dolor sit amet, consectetur adipiscing elit, sed do eiusmod tempor incididunt.',
    //        user: 'John Appleseed',
    //        relatedtogoal: 'Loren goal',
    //        createdyear: 2017,
    //        createdmonth: 01,
    //        createdday: 01
    //    },
    //    {
    //        title: 'Lorem Ipsum Task',
    //        content: 'Lorem ipsum dolor sit amet, consectetur adipiscing elit, sed do eiusmod tempor incididunt.',
    //        user: 'John Appleseed',
    //        relatedtogoal: 'Loren goal',
    //        createdyear: 2017,
    //        createdmonth: 01,
    //        createdday: 01
    //    },
    //    {
    //        title: 'Lorem Ipsum Task',
    //        content: 'Lorem ipsum dolor sit amet, consectetur adipiscing elit, sed do eiusmod tempor incididunt.',
    //        user: 'John Appleseed',
    //        relatedtogoal: 'Loren goal',
    //        createdyear: 2017,
    //        createdmonth: 01,
    //        createdday: 01
    //    },
    //    {
    //        title: 'Lorem Ipsum Task',
    //        content: 'Lorem ipsum dolor sit amet, consectetur adipiscing elit, sed do eiusmod tempor incididunt.',
    //        user: 'John Appleseed',
    //        relatedtogoal: 'Loren goal',
    //        createdyear: 2017,
    //        createdmonth: 01,
    //        createdday: 01
    //    },
    //    {
    //        title: 'Lorem Ipsum Task',
    //        content: 'Lorem ipsum dolor sit amet, consectetur adipiscing elit, sed do eiusmod tempor incididunt.',
    //        user: 'John Appleseed',
    //        relatedtogoal: 'Loren goal',
    //        createdyear: 2017,
    //        createdmonth: 01,
    //        createdday: 01
    //    }
    //];
    return {
        getActivities: function () {
            return activities;
        },
        addActivity: function (obj) {
            activities.push(obj);
        },
        removeActivity: function (obj) {
            activities.pop();
        },
    };
}]);