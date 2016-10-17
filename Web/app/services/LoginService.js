/// <reference path="C:\Users\Loopscale\documents\visual studio 2015\Projects\Loopscale.Security\Web\scripts/angular.js" />

'use strict';

app.factory('loginService', ['$http', '$q', 'localStorageService', 'constantFactory', function ($http, $q, localStorageService, constantFactory) {

    var service = {
        Login: Login,
        Logout: Logout
    };

    var serviceBase = constantFactory.apiServiceBaseUri;

    function Login(loginData) {

        var data = "grant_type=password&username=" + loginData.userName + "&password=" + loginData.password;
        var deferred = $q.defer();

        $http.post(serviceBase + 'token', data, { headers: { 'Content-Type': 'application/x-www-form-urlencoded' } }).success(function (response) {

            if (loginData.useRefreshTokens) {
                loginData.refreshToken = response.refresh_token;
                localStorageService.set('authorizationData', { token: response.access_token, userDetails: loginData, useRefreshTokens: true });
            }
            else {
                loginData.refreshToken = "";
                localStorageService.set('authorizationData', { token: response.access_token, userDetails: loginData, useRefreshTokens: false });
            }
           
            deferred.resolve(response);

        }).error(function (err, status) {
            LogOut();
            deferred.reject(err);
        });

        return deferred.promise;

    };

    function Logout () {
        localStorageService.remove('authorizationData');
    };

    
    return service;
}]);