'use strict';
app.controller('mainController', ['$scope', '$location', 'authService', function ($scope, $location, authService) {
    $scope.message = 'Contact us! JK. This is just a demo.';

    $scope.logOut = function () {
        authService.logOut();
        $location.path('/home');
    }


    $scope.authentication = authService.authentication;

}]);