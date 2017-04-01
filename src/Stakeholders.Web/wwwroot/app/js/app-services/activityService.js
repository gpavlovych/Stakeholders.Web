
porlaDashboard.factory('ActivityService', ['$http', function ($http) {

    return {
        get: function(start, count) {
            var url = "/api/Activities?start=" + start + "&count=" + count;
            return $http.get(url).then(handleSuccess, handleError('Error getting activities'));
        },
        getById: function(id) {
            var url = "/api/Activities/" +id;
            return $http.get(url).then(handleSuccess, handleError('Error getting activities by id'));
        },
        count: function () {
            var url = "/api/Activities/count";
            return $http.get(url).then(handleSuccess, handleError('Error getting activities count'));
        },
        create: function (activity) {
            var url = "/api/Activities";
            return $http.post(url, activity).then(handleSuccess, handleError('Error creating activity'));
        },
        update: function (activity, id) {
            var url = "/api/Activities/"+id;
            return $http.put(url, activity).then(handleSuccess, handleError('Error updating activity'));
        },
        remove: function(id) {
            var url = "/api/Activities/" + id;
            return $http.delete(url).then(handleSuccess, handleError('Error deleting activity'));
        }
    };

    function handleSuccess(res) {
        return { success: true, data: res.data };
    }

    function handleError(error) {
        return function () {
            return { success: false, message: error };
        };
    }
    // $http.get("/api/Activities")
    //.then(function (response) {
    //    $scope.myWelcome = response.data;
    //});
    // //var activities = [
    // //    {
    // //        title: 'Lorem Ipsum Task',
    // //        content: 'Lorem ipsum dolor sit amet, consectetur adipiscing elit, sed do eiusmod tempor incididunt.',
    // //        user: 'John Appleseed',
    // //        relatedtogoal: 'Loren goal',
    // //        createdyear: 2017,
    // //        createdmonth: 01,
    // //        createdday: 01
    // //    },
    // //    {
    // //        title: 'Lorem Ipsum Task',
    // //        content: 'Lorem ipsum dolor sit amet, consectetur adipiscing elit, sed do eiusmod tempor incididunt.',
    // //        user: 'John Appleseed',
    // //        relatedtogoal: 'Loren goal',
    // //        createdyear: 2017,
    // //        createdmonth: 01,
    // //        createdday: 01
    // //    },
    // //    {
    // //        title: 'Lorem Ipsum Task',
    // //        content: 'Lorem ipsum dolor sit amet, consectetur adipiscing elit, sed do eiusmod tempor incididunt.',
    // //        user: 'John Appleseed',
    // //        relatedtogoal: 'Loren goal',
    // //        createdyear: 2017,
    // //        createdmonth: 01,
    // //        createdday: 01
    // //    },
    // //    {
    // //        title: 'Lorem Ipsum Task',
    // //        content: 'Lorem ipsum dolor sit amet, consectetur adipiscing elit, sed do eiusmod tempor incididunt.',
    // //        user: 'John Appleseed',
    // //        relatedtogoal: 'Loren goal',
    // //        createdyear: 2017,
    // //        createdmonth: 01,
    // //        createdday: 01
    // //    },
    // //    {
    // //        title: 'Lorem Ipsum Task',
    // //        content: 'Lorem ipsum dolor sit amet, consectetur adipiscing elit, sed do eiusmod tempor incididunt.',
    // //        user: 'John Appleseed',
    // //        relatedtogoal: 'Loren goal',
    // //        createdyear: 2017,
    // //        createdmonth: 01,
    // //        createdday: 01
    // //    },
    // //    {
    // //        title: 'Lorem Ipsum Task',
    // //        content: 'Lorem ipsum dolor sit amet, consectetur adipiscing elit, sed do eiusmod tempor incididunt.',
    // //        user: 'John Appleseed',
    // //        relatedtogoal: 'Loren goal',
    // //        createdyear: 2017,
    // //        createdmonth: 01,
    // //        createdday: 01
    // //    },
    // //    {
    // //        title: 'Lorem Ipsum Task',
    // //        content: 'Lorem ipsum dolor sit amet, consectetur adipiscing elit, sed do eiusmod tempor incididunt.',
    // //        user: 'John Appleseed',
    // //        relatedtogoal: 'Loren goal',
    // //        createdyear: 2017,
    // //        createdmonth: 01,
    // //        createdday: 01
    // //    },
    // //    {
    // //        title: 'Lorem Ipsum Task',
    // //        content: 'Lorem ipsum dolor sit amet, consectetur adipiscing elit, sed do eiusmod tempor incididunt.',
    // //        user: 'John Appleseed',
    // //        relatedtogoal: 'Loren goal',
    // //        createdyear: 2017,
    // //        createdmonth: 01,
    // //        createdday: 01
    // //    }
    // //];
    // return {
    //     getActivities: function () {
    //         return activities;
    //     },
    //     addActivity: function (obj) {
    //         activities.push(obj);
    //     },
    //     removeActivity: function (obj) {
    //         activities.pop();
    //     },
    // };
}]);