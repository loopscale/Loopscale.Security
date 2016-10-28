    'use strict';

    app.factory('loginService', loginService);

    loginService.$inject = ['$http', 'constantFactory', '$q', 'localStorageService', '$rootScope'];

    function loginService($http, constantFactory, $q, localStorageService, $rootScope) {
        var service = {
            AutheticateUser: authenticateUser,
            Logout: _logOut
        };

        var baseUrl = constantFactory.serviceBaseUrl();
        /**
       * Logout call
       */
        function _logOut() {

            $rootScope.isAuth = false;
            //_authentication.userName = "";
            //_authentication.authorities = null;
            //_authentication.schoolInfo = null;
            localStorageService.set('authorizationData', null);

            // we need to have a server call to logout the user

        };

        function authenticateUser(userName, password) {
            var url = baseUrl + 'oauth/token';

            var data = "grant_type=password&username=" + userName + "&password=" + password;
            var deferred = $q.defer();

            $http.post(serviceBase + 'oauth/token', data, {
                headers: { 'Content-Type': 'application/x-www-form-urlencoded' },
                xhrFields: {
                    withCredentials: true
                }
            }).success(function (response) {
                console.log("success");
                localStorageService.set('authorizationData', { token: response.access_token, userName: userName });
                $rootScope.isAuth = true;
                $rootScope.userName = userName;

                deferred.resolve(response);

            }).error(function (err, status) {
                _logOut();
                deferred.reject(err);
            });

            return deferred.promise;
        }

        return service;
    }
