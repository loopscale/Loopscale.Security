'use strict';

angular
    .module('app')
    .factory('commonService', commonService);

commonService.$inject = ['$http', 'loginService', 'constantFactory', '$q', '$rootScope', 'authProvider'];

function commonService($http, loginService, constantFactory, $q, $rootScope, authProvider) {

    var service = {
        AutheticateUser: authenticateUser,
        getRole: getRole,
        Logout: logOut,
        showSuccessMsg: showSuccessMsg,
        showErrorMsg: showErrorMsg,
        setRegistrationAction: setRegistrationAction,
        getRegistrationAction: getRegistrationAction,
    };

    return service;

    var baseUrl = constantFactory.serviceBaseUrl();
    $rootScope.alerts = [];
    function authenticateUser(userName, password) {
        loginService.AutheticateUser(userName, password);
    };

    function getRole(successCallback, errorCallback) {

        //console.log("http://localhost/ChildCareManagement.Services/api/Users/GetRole");

        var req = {
            method: 'GET',
            url: baseUrl + "/api/Users/GetRole",
            headers: {
                'Content-Type': "application/json"
            },
            xhrFields: {
                withCredentials: true
            },
        }
        $http(req).then(successCallback, errorCallback);
    };

    function logOut() {
        loginService.Logout();
    };

    function showSuccessMsg(msg) {
        $rootScope.alerts = [];
        $rootScope.alerts.push({
            'type': 'success',
            'msg': msg
        });
    };

    function showErrorMsg(msg, d) {
        $rootScope.alerts = [];
        $rootScope.alerts.push({
            'type': 'danger',
            'msg': "Error: " + msg + " Please contact administrator"
        });
    };
}