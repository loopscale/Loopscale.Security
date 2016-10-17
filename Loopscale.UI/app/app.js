/// <reference path="C:\Users\Loopscale\documents\visual studio 2015\Projects\Loopscale.Security\Loopscale.UI\scripts/angular.js" />

// app.js
// create the module and name it scotchApp
// also include ngRoute for all our routing needs
var app = angular.module('app', ['ui.router', 'LocalStorageModule', 'angular-loading-bar']);

// configure our routes
app.config(function ($stateProvider, $urlRouterProvider) {
    $urlRouterProvider.otherwise('home');

    $stateProvider
        // route for the home page
        .state('home', {
            url:'/home',
            templateUrl: 'partials/home.html',
            controller: 'mainController'
        })

         // route for the Login page
        .state('login', {
            url: '/login',
            templateUrl: 'partials/login.html',
            controller: 'loginController'
        })

         // route for the order page
        .state('orders', {
            url: '/orders',
            templateUrl: 'partials/orders.html',
            controller: 'ordersController'

        })

         // route for the dashboard page
        .state('dashboard', {
            url: '/dashboard',
            templateUrl: 'partials/dashboard.html',
            controller: 'dashboardController'
        })

        // route for the about page
        .state('about', {
            url: '/about',
            templateUrl: 'partials/about.html',
            controller: 'aboutController'
        })

        // route for the contact page
        .state('contact', {
            url: '/contact',
            templateUrl: 'partials/contact.html',
            controller: 'contactController',
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
