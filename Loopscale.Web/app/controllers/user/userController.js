'use strict';

app.controller('userController', userController);

userController.$inject = ['$rootScope', '$stateParams', '$scope', 'userService', 'loginService', 'authProvider', 'constantFactory', '$location', 'commonService'];

function userController($rootScope, $stateParams, $scope, userService, loginService, authProvider, constantFactory, $location, commonService) {

    activate();

    function activate() {
        //alert("Here");
        $scope.UserId = "";
        $scope.Password = "";
        $scope.ConfirmPassword = "";
        $scope.FirstName = "";
        $scope.LastName = "";
        $scope.Email = "";
        $scope.Mobile = "";
        $scope.$isUserNameExists = false;
        $scope.$isEmailExists = false

        $scope.action = $stateParams.action;
    }

    $scope.clear = function () {
        $scope.UserId = "";
        $scope.Password = "";
        $scope.ConfirmPassword = "";
        $scope.FirstName = "";
        $scope.LastName = "";
        $scope.Email = "";
        $scope.Mobile = "";
        $scope.$isUserNameExists = false;
        $scope.$isEmailExists = false;
        $scope.UserForm.$setPristine()
        $scope.UserForm.$setUntouched();
    }

    $scope.clearForm = function (form) {
        $scope.UserForm = form;
        $scope.clear();
    }

    $scope.isUniqueUser = function () {
        if (!$scope.ProfileId) {
            $scope.ProfileId = 0;
        }

        userService.isUniqueUser($scope.ProfileId, $scope.UserId).then(
            function (d) {
                $scope.$isUserNameExists = d.data === false;
            },
            errorCallback);
    }

    $scope.isUniqueEmail = function () {
        if (!$scope.ProfileId) {
            $scope.ProfileId = 0;
        }

        userService.isUniqueEmail($scope.ProfileId, $scope.Email).then(
            function (d) {
                console.log(d.data);
                $scope.$isEmailExists = d.data === false;
            },
            errorCallback);
    }

    $scope.createUser = function (isValid, form) {
        $scope.UserForm = form;
        if ($scope.$isEmailExists || $scope.$isUserNameExists || $scope.Password != $scope.ConfirmPassword) {
            $scope.$errorMessage = "There are still some invald values. Please resolve them to proceed";
            return;
        }

        if (isValid) {
            $scope.AddNewUser = true;
            userService.createUser($scope.UserId,
                                    $scope.Password,
                                    $scope.FirstName,
                                    $scope.LastName,
                                    $scope.Email,
                                    $scope.Mobile,
                                    $scope.Phone,
                                    $scope.AddNewUser,
                                    successCallback,
                                    errorCallback);

            form.$setPristine();
            form.$setUntouched();
        } else {
            angular.forEach($scope.userForm.$error, function (field) {
                angular.forEach(field, function (errorField) {
                    errorField.$setTouched();
                })
            });
        }
    }

    $scope.myCustomValidator = function (text) {
        return $scope.Password == text;
    };

    function successCallback(data) {
        console.log(data);
        $rootScope.alerts = [];
        $location.path("/registerconfirmation");
    }

    function errorCallback(e) {
        console.log(e);

        if (e.status === 412) {
            $scope.alerts.push({
                'type': 'danger',
                'msg': e.data
            });
        }
        else {
            $scope.alerts.push({
                'type': 'danger',
                'msg': "Error while creating user"
            });
        }

        if (typeof (e.data) != 'undefined') {

            $scope.$errorMessage = e.data;

            if (typeof (e.data.ModelError) != 'undefined') {
                $scope.$errorMessage = e.data.ModelError[0];
            }
        }

        /*if (e.data && e.data.ModelError && e.data.ModelError[0]) {
            $scope.$errorMessage = e.data.ModelError[0];
        }*/
    }

    function getDataSuccess(d) {
        console.log(d);
    }
}
