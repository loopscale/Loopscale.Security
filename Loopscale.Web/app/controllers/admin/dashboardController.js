'use strict';
app.controller('dashboardController', ['$scope', 'loginService', function ($scope, loginService) {
   console.log('Dashboard - ' + $scope.firstName);
}]);