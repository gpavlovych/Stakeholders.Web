porlaDashboard.factory('$authenticationService',
    function ($http, $window) {
        return {
            login: function (username, password, callback) {
                $http.defaults.headers.post["Content-Type"] = "application/x-www-form-urlencoded";
                $http.post('/token', { email: username, password: password })
                    .then(function(response) {
                        var token = response.data.access_token;
                        // login successful if there's a token in the response
                        if (token) {
                            // store username and token in local storage to keep user logged in between page refreshes
                            $window.localStorage.currentUser = { username: username, token: token };

                            // add jwt token to auth header for all requests made by the $http service
                            $http.defaults.headers.common.Authorization = 'Bearer ' + token;

                            // execute callback with true to indicate successful login
                            callback(true);
                        } else {
                            // execute callback with false to indicate failed login
                            callback(false);
                        }
                    });
            },

            logout: function() {
                // remove user from local storage and clear http auth header
                delete localStorage.currentUser;
                $http.defaults.headers.common.Authorization = '';
            }
        };
    });