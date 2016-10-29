﻿(function () {
    'use strict';

    angular
        .module('app')
        .controller('userController', userController);

    userController.$inject = ['$rootScope', '$routeParams','$scope', 'userService', 'loginService', 'authProvider', 'constantFactory', '$location', 'commonService'];

    function userController($rootScope, $routeParams,$scope, userService, loginService, authProvider, constantFactory, $location, commonService) {

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
            $scope.$isEmailExists = false;
            $scope.$isInvalidCaptcha = false;

            $scope.action = $routeParams.action;

            userService.getCaptchaImage(captchaSuccessCallback, captchaErrorCallback)
        }

        function captchaSuccessCallback(d) {
            if (d != null) {
                $scope.captchaImagePath = "data:image/png;base64," + d.data.captchaText;
                $scope.captchaOriginalText = d.data.captchaOriginalText;
            }
        }
        function captchaErrorCallback(d) {
            console.log(d);

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
            $scope.$isInvalidCaptcha = false;
            $scope.UserForm.$setPristine()
            $scope.UserForm.$setUntouched();
        }

        $scope.refreshCaptcha=function() {
            userService.getCaptchaImage(captchaSuccessCallback, captchaErrorCallback)
        }

        $scope.readCaptcha = function () {
            userService.readCaptcha($scope.captchaOriginalText, readCaptchasuccessCallback, readCaptchaerrorCallback)
        }

        function readCaptchasuccessCallback(d) {
            console.log(d);
        }

        function readCaptchaerrorCallback(d) {
            console.log(d);
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
            else if ($scope.$isInvalidCaptcha)
            {
                $scope.$errorMessage = "Are you Human!!. Please enter valid captcha.";
                return;
            }

            if (isValid) {
                userService.createUser($scope.UserId, $scope.Password, $scope.FirstName, $scope.LastName, $scope.Email, $scope.Captcha, $scope.Mobile, $scope.Phone, successCallback, errorCallback);
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
            $scope.loginUser($scope.UserId, $scope.Password);
            //$location.path("/registerconfirmation");
        }

        $scope.loginUser = function (userName, passwd) {
            $rootScope.spinner = true;
            loginService.AutheticateUser(userName, passwd).then(
                function (d) {
                    $rootScope.alerts = [];
                    authProvider.setUser(d);
                    $rootScope.firstName = d.FirstName;
                    $rootScope.lastName = d.LastName;
                    $rootScope.role = d.Role;
                    $rootScope.Id = d.ProfileId;
                    //$cookies.put(d.data.sessionIdCookieName, d.data.sessionAuthorization);
                    $scope.$errorMessage = "";
                    if ($scope.action == 'b') {
                        commonService.setRegistrationAction(true, false);
                        $location.path("/profile/f/" + $rootScope.Id);
                    }
                    else {
                        commonService.setRegistrationAction(false, true);
                        $location.path("/dashboard");
                    }

                    $rootScope.spinner = false;
                    $rootScope.login = false;

                }, errorCallback);
        }

        function errorCallback(e) {
            console.log(e);

            if (e.status === 412 ) {
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
})();