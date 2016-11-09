/// <reference path="C:\Users\Loopscale\documents\visual studio 2015\Projects\Loopscale.Security\Web\scripts/angular.js" />
'use strict';

var app = angular.module('app', ['ui.router', 'LocalStorageModule', 'angular-loading-bar', 'ngCookies']);

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

     .state('register', {
         url: '/register',
         controller: 'userController',
         templateUrl: '/app/views/registration.html'
     })

    .state('profile', {
        url: '/profile',
        controller: 'employeeController',
        templateUrl: '/app/views/admin/employeeprofile.html'
    })

    .state('editProfile', {
         url: '/editprofile',
         controller: 'profileController',
         templateUrl: '/app/views/profile.html'
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
    console.log("inside app");
    console.log(baseUrl);

    $httpProvider.defaults.headers.common = {};
    $httpProvider.defaults.headers.post = {};
    $httpProvider.defaults.headers.put = {};
    $httpProvider.defaults.headers.patch = {};
    $httpProvider.defaults.withCredentials = true;
    $httpProvider.defaults.headers.Accept = "application/json";

    $httpProvider.interceptors.push('authServiceInterceptor');
});

//app.run(['authService', function (authService) {
 //   authService.fillAuthData();
//}]);

app.run(['$rootScope', '$location', 'authProvider', function ($rootScope, $location, authProvider) {

        console.log("inside app run");

        $rootScope.showUserIcon = false;
        $rootScope.alerts = [];

        $rootScope.logout = function () {
            console.log("inside app run 2");
            authProvider.logout();
            $location.path("/");
        }

        $rootScope.closeAlert = function (index) {
            console.log("inside app run 3");
            $rootScope.alerts.splice(index, 1);
        };
        $rootScope.login = function () {
            console.log("inside app run ");
            $location.path("/");
        }
        $rootScope.navigate = function (route) {
            $location.path(route);
        }

        $rootScope.$on('$stateChangeStart', function(event, toState, toParams, fromState, fromParams){
            $rootScope.spinner = false;
            $rootScope.$errorMessage = '';
            $rootScope.alerts = [];
            $rootScope.routeloading = true;
            var routeController = null;

            if (toState.controller != 'undefined')
            {
                routeController = toState.controller.toLowerCase();
            }

            if (routeController === "logincontroller") {
                $rootScope.login = true;
            } else {
                $rootScope.login = false;
            }

            console.log("app.js --> routerController is:");
            console.log(routeController);

            if (routeController === "logincontroller" || routeController == "userController") {
                //$rootScope.showTopNav = false;
                //$rootScope.showFooter = false;
                $rootScope.authenticated = false;
                // authProvider.logout()
                console.log('routerController - userController');
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

        //$rootScope.$on("$routeChangeSuccess", function (event, current, previous) {
        //    $rootScope.routeloading = false;
        //});

        //$rootScope.$on("$routeChangeError", function (event, current, previous, rejection) {
        //    $rootScope.routeloading = false;
        //});
    }]);


