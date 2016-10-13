// app.js
    // create the module and name it scotchApp
    // also include ngRoute for all our routing needs
var app = angular.module('app', ['ngRoute', 'LocalStorageModule', 'angular-loading-bar']);

    // configure our routes
    app.config(function($routeProvider) {
        $routeProvider
            // route for the home page
            .when('/', {
                templateUrl : 'app/pages/home.html',
                controller  : 'mainController'
            })

             // route for the Login page
            .when('/login', {
                templateUrl: 'app/pages/login.html',
                controller: 'loginController'
            })

             // route for the order page
            .when('/orders', {
                templateUrl: 'app/pages/orders.html',
                controller: 'ordersController'
            })

            // route for the about page
            .when('/about', {
                templateUrl : 'app/pages/about.html',
                controller  : 'aboutController'
            })

            // route for the contact page
            .when('/contact', {
                templateUrl : 'app/pages/contact.html',
                controller  : 'contactController'
            });

        $routeProvider.otherwise({ redirectTo: "app/pages/home.html" });
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

