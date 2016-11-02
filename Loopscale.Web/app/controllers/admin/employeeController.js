'use strict';

app.controller('employeeController', employeeController);

employeeController.$inject = ['$scope', 'employeeService', '$rootScope', 'constantFactory', '$location'];

function employeeController($scope, employeeService, $rootScope, constantFactory, $location) {

    activate();

    function activate() {
        $scope.firstname = "";
        $scope.lastname = "";
        $scope.email = "";
    }

    $scope.createProfile = function (isValid, form) {

        $scope.UserForm = form;

        var profile = {};
        profile.FirstName = $scope.firstname;
        profile.LastName = $scope.lastname;
        profile.Email = $scope.email;

        employeeService.createProfile(profile, successCallback, errorCallback);
    }

    function successCallback(data) {
        console.log('Success');
        $location.path("/admin/employeeProfileRegistrationConfirmation");
    }

    function errorCallback(e) {
        console.log(e);
    }
}

