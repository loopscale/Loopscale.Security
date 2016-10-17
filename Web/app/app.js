/// <reference path="C:\Users\Loopscale\documents\visual studio 2015\Projects\Loopscale.Security\Web\scripts/angular.js" />

var app = angular.module('app', ['LocalStorageModule', 'ui.router']);

app.config(function($stateProvider, $urlRouterProvider, $httpProvider) {
    $stateProvider
        .state('login', {
            url: '/login',
            templateUrl: 'app/partials/login.html',
            controller: 'loginController',
        })
        .state('dashboard', {
            url: '/dashboard',
            templateUrl: 'app/partials/dashboard.html',
            controller: 'dashboardController',
        });

    $urlRouterProvider.otherwise('login');

   $httpProvider.interceptors.push('APIInterceptor');
})

//app.run(['$rootScope', '$location', 'authProvider', function ($rootScope, $location, authProvider) {
//    $rootScope.isAuthenticated = true;
//}]);

// create the controller and inject Angular's $scope
var serviceBaseUrl = 'http://localhost:52926/';
//var serviceBase = 'http://ngauthenticationapi.azurewebsites.net/';
app.constant('constantFactory', {
    apiServiceBaseUri: serviceBaseUrl
});
