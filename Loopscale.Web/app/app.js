/// <reference path="C:\Users\Loopscale\documents\visual studio 2015\Projects\Loopscale.Security\Web\scripts/angular.js" />
'use strict';

var app = angular.module('app', ['ui.router', 'LocalStorageModule', 'angular-loading-bar']);


app.config(function ($stateProvider, $urlRouterProvider) {
    $urlRouterProvider.otherwise('login');

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

    .state('dashboard', {
        url: '/dashboard',
        controller: 'dashboardController',
        templateUrl: '/app/views/admin/dashboard.html'
    });
});

// create the controller and inject Angular's $scope
var serviceBase = 'http://localhost:52926/';
//var serviceBase = 'http://ngauthenticationapi.azurewebsites.net/';
app.constant('ngAuthSettings', {
    apiServiceBaseUri: serviceBase,
    clientId: 'ngAuthApp'
});

var baseUrl = 'http://localhost:54601/';
//var serviceBase = '';
app.constant('CCMSettings', {
    apiServiceBaseUri: serviceBase,
    uiBaseUri: baseUrl,
    clientId: 'ccmApp'
});

app.config(function ($httpProvider) {
    $httpProvider.interceptors.push('authServiceInterceptor');
});

//app.run(['authService', function (authService) {
 //   authService.fillAuthData();
//}]);

app.run(['$rootScope', '$location', 'authProvider', function ($rootScope, $location, authProvider) {

        $rootScope.showUserIcon = false;
        $rootScope.alerts = [];
        $rootScope.logout = function () {
            authProvider.logout();
            $location.path("/");
        }

        $rootScope.closeAlert = function (index) {
            $rootScope.alerts.splice(index, 1);
        };
        $rootScope.login = function () {
            $location.path("/");
        }
        $rootScope.navigate = function (route) {
            $location.path(route);
        }
        $rootScope.$on("$routeChangeStart", function (event, next, current) {
            $rootScope.spinner = false;
            $rootScope.$errorMessage = '';
            $rootScope.alerts = [];
            $rootScope.routeloading = true;
            var routeController = null;

            //console.log('typeof undefined: ' + typeof (next.$$route.controller));

            if (typeof (next.$$route.controller) != 'undefined') {
                routeController = next.$$route.controller.toLowerCase();
            };

            if (routeController === "logincontroller") {
                $rootScope.login = true;
            } else {
                $rootScope.login = false;
            }

            console.log("app.js --> routerController is:");
            console.log(routeController);

            if (routeController === "logincontroller") {
                //$rootScope.showTopNav = false;
                //$rootScope.showFooter = false;
                $rootScope.authenticated = false;
                // authProvider.logout()

            } else {
                //$rootScope.showTopNav = true;
                //$rootScope.showFooter = true;
                console.log("app.js --> inside authenticated");
                $rootScope.authenticated = true;
                //First check authservice whether user is logged in or not
                //if not logged in then redirect to login page
                authProvider.redirectToLoggedInIfNotAuth();
            }

            console.log("app.js --> $rootScope.authenticated");
            console.log($rootScope.authenticated);
            //console.log(event, next, current)
        })

        $rootScope.$on("$routeChangeSuccess", function (event, current, previous) {
            $rootScope.routeloading = false;
        });

        $rootScope.$on("$routeChangeError", function (event, current, previous, rejection) {
            $rootScope.routeloading = false;
        });
    }]);


