(function () {
    'use strict';

    angular
        .module('app')
        .factory('authProvider', authProvider);

    authProvider.$inject = ['$http', '$window', '$rootScope', '$location'];

    function authProvider($http, $window, $rootScope, $location) {
        var AuthTicketKey = "AuthTicket";

        var user;

        var service = {
            setUser: setUser,
            isLoggedIn: isLoggedIn,
            redirectToLoggedInIfNotAuth: redirectToLoggedInIfNotAuth,
            logout: logout,
            saveInStorage: saveInStorage,
            getFromStorage: getFromStorage,
            removeFromStorage: removeFromStorage,
        };

        return service;

        function setUser(u) {
            user = u;
            $window.sessionStorage.setItem(AuthTicketKey, JSON.stringify(user));
            $rootScope.IsLoggedIn = true;
            //$rootScope.DisplayName = user.Email;
            //$rootScope.FirstName = user.FirstName;
            //$rootScope.LastName = user.LastName;
            //$rootScope.Role = user.UserRole;
                        $rootScope.firstName = user.FirstName;
                        $rootScope.lastName = user.LastName;
                        $rootScope.role = user.Role;

        }

        function isLoggedIn() {
            var u = $window.sessionStorage.getItem(AuthTicketKey);
            if (u) {
                user = JSON.parse(u);
                $rootScope.IsLoggedIn = true;
                //$rootScope.DisplayName = user.Email;
                //$rootScope.FirstName = user.FirstName;
                //$rootScope.LastName = user.LastName;
                //$rootScope.Role = user.UserRole;
                 $rootScope.firstName = user.FirstName;
                        $rootScope.lastName = user.LastName;
                        $rootScope.role = user.Role;
                        $rootScope.Id = user.ProfileId;
            }
            else {
                $rootScope.IsLoggedIn = false;
                //$rootScope.DisplayName = '';
                //$rootScope.FirstName = '';
                //$rootScope.LastName = '';
                //$rootScope.Role = '';
                 $rootScope.firstName = '';
                        $rootScope.lastName = '';
                        $rootScope.role = '';
                        $rootScope.Id = '';
            }
            return (user) ? user : false;
        }

        function redirectToLoggedInIfNotAuth() {
            console.log("authprovider - redirectToLoggedInIfNotAuth");
            var u = $window.sessionStorage.getItem(AuthTicketKey);
            if (u) {
                user = JSON.parse(u);
            }
            //console.log(user);

            if (user) {
                console.log("authprovider - authenticated");
                $rootScope.IsLoggedIn = true;
                $rootScope.firstName = user.FirstName;
                $rootScope.lastName = user.LastName;
                $rootScope.role = user.Role;
                $rootScope.Id = user.ProfileId;
            } else {
                console.log("authprovider - un-authenticated");
                $rootScope.IsLoggedIn = false;
                $rootScope.showTopNav = false;
                $rootScope.showFooter = false;
                $rootScope.authenticated = false;
                $rootScope.firstName = '';
                $rootScope.lastName = '';
                $rootScope.role = '';
                $location.path("/");

            }
        }

        function logout() {
            $rootScope.IsLoggedIn = false;
            $rootScope.DisplayName = '';
             $rootScope.authenticated = false;
                $rootScope.firstName = '';
                $rootScope.lastName = '';
                $rootScope.role = '';
                $rootScope.Id = '';
                $window.sessionStorage.removeItem(AuthTicketKey, JSON.stringify(user));
                var allKeys = getFromStorage("All_Keys");
                if (allKeys != null && allKeys != undefined) {
                    $.each(allKeys, function (i, obj) {
                        $window.sessionStorage.removeItem(obj);
                    });
                }
                $window.sessionStorage.removeItem("All_Keys");
        }

        function saveInStorage(key, val) {
            var allKeys = getFromStorage("All_Keys");
            if (allKeys == null || allKeys == undefined) {
                allKeys = [];
            }
            allKeys.push(key);
            $window.sessionStorage.setItem("All_Keys", JSON.stringify(allKeys));
            $window.sessionStorage.setItem(key, JSON.stringify(val));
        }

        function getFromStorage(key) {
            var val = $window.sessionStorage.getItem(key);
            if (val) {
                return JSON.parse(val);
            }
        }

        function removeFromStorage(key) {
            $window.sessionStorage.removeItem(key);
        }
    }
})();