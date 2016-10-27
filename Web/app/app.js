/// <reference path="C:\Users\Loopscale\documents\visual studio 2015\Projects\Loopscale.Security\Web\scripts/angular.js" />

var app = angular.module('AngularAuthApp', ['ui.router', 'LocalStorageModule', 'angular-loading-bar']);

app.config(function ($stateProvider, $urlRouterProvider) {
    $urlRouterProvider.otherwise('home');

    $stateProvider
    .state('home', {
        url: '/home',
        controller: 'homeController',
        templateUrl: '/app/views/home.html'
    })

    .state('login', {
        url: '/login',
        controller: 'loginController',
        templateUrl: '/app/views/login.html'
    })

    .state('signup', {
        url: '/signup',
        controller: 'signupController',
        templateUrl: '/app/views/signup.html'
    })

     .state('refresh', {
         url: '/refresh',
         controller: 'refreshController',
         templateUrl: '/app/views/refresh.html'
     })

    .state('dashboard', {
        url: '/dashboard',
        controller: 'dashboardController',
        templateUrl: '/app/views/dashboard.html'
    })

     .state('form1', {
         url: '/form1',
         //controller: 'dashboardController',
         templateUrl: '/app/views/forms/dashboard.html'
     })

    .state('orders', {
        url: '/orders',
        controller: 'ordersController',
        templateUrl: '/app/views/orders.html'
    });

});

// create the controller and inject Angular's $scope
var serviceBase = 'http://localhost:52926/';
//var serviceBase = 'http://ngauthenticationapi.azurewebsites.net/';
app.constant('ngAuthSettings', {
    apiServiceBaseUri: serviceBase,
    clientId: 'ngAuthApp'
});

app.config(function ($httpProvider) {
    $httpProvider.interceptors.push('authInterceptorService');
});

app.run(['authService', function (authService) {
    authService.fillAuthData();
}]);
