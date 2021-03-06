﻿'use strict';

app.controller('loginController', loginController);

loginController.$inject = ['$scope', 'loginService', 'constantFactory', '$window', '$location', 'authProvider', '$cookies', '$cookieStore', '$state', '$stateParams', '$rootScope'];

function loginController($scope, loginService, constantFactory, $window, $location, authProvider, $cookies, $cookieStore, $state, $stateParams, $rootScope) {

    $scope.title = 'loginController';
    //$scope.baseUrl = constantFactory.getBaseUrl;
    var serviceBaseUrl = constantFactory.serviceBaseUrl();
    var uiBaseUrl = constantFactory.uiBaseUrl();

    $scope.uiBaseUrl = uiBaseUrl;

    activate();

    function activate() {
        authProvider.logout();
        
    }
    $scope.navigateToRegister = function () {
    }

    $scope.loginUser = function (isValid, form) {
        if (isValid) {
            $rootScope.spinner = true;
            loginService.AutheticateUser($scope.UserName, $scope.Password).then(
                function (d) {
                    $rootScope.alerts = [];
                    authProvider.setUser(d);
                    $rootScope.firstName = d.FirstName;
                    $rootScope.lastName = d.LastName;
                    $rootScope.role = d.Role;
                    //$cookies.put(d.data.sessionIdCookieName, d.data.sessionAuthorization);
                    $scope.$errorMessage = "";
                    //alert($rootScope.role);                                              
                    var url = $scope.uiBaseUrl + "#/dashboard";
                    //var url = $scope.uiBaseUrl + "admin/#";
                    console.log("url to log-in");
                    console.log(url);
                    $window.location.href = url;
                    //$location.path("/dashboard");

                    $rootScope.spinner = false;
                    $rootScope.login = false;

                }, errorResult);
        } else {
            angular.forEach($scope.loginForm.$error, function (field) {
                angular.forEach(field, function (errorField) {
                    errorField.$setTouched();
                })
            });
        }
    }

    function errorResult(e) {
        $rootScope.spinner = false;
        //console.log(e);
        var msg = "Invalid Credentials";
        if (e.error == "invalid_grant") {
            msg = e.error_description;
        }
        $rootScope.alerts = [];
        $rootScope.alerts.push({
            'type': 'danger',
            'msg': msg
        });
        // $scope.$errorMessage = "Invalid Credentials";
        // constantFactory.showErrorMsg("Invalid Credentials");
    }
}