'use strict'

app.controller('commonController', commonController);

commonController.$inject = ['$scope', 'commonService', '$location', '$rootScope', 'constantFactory'];

function commonController($scope, commonService, $location, $rootScope, constantFactory) {

    $scope.navigateHome = function () {
        commonService.getRole(getRoleSuccessCallback, getRoleErrorCallback);
    }
    //TODO:Load from config
    constantFactory.visitAllowedDays = [1, 3, 5];
    constantFactory.schoolTimeZone = "US";
    function getRoleSuccessCallback(result) {

        if (result.data === "Admin") {
            $location.path("/dashboard");
        }
        else if (result.data === "Visitor") {
            $location.path("/dashboard");
        }
        else {
            $location.path("/login");
        }
    }

    function getRoleErrorCallback(result) {
        console.log(result);
    }

    $scope.navigateDashboard = function () {
        $location.path("/dashboard");
    }
}