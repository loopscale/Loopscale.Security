'use strict';
app.controller('indexController',indexController);

indexController.$inject = ['$scope', 'loginService', 'constantFactory', '$window', '$location', 'authProvider',  '$state', '$stateParams', '$rootScope'];

function indexController($scope, loginService, constantFactory, $window, $location, authProvider, $state, $stateParams, $rootScope)
{
    console.log('indexController');

    $scope.logoutUser = function () {
        console.log('Logged Out');
        loginService.Logout();
        $location.path('#/login');
    }

    //$scope.authentication = authService.authentication;

}