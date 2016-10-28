'use strict';

angular.module('app').directive('remotevalidator', [
    '$http', 'constantFactory', '$parse', function ($http, constantFactory, $parse) {
        var toId;
        var baseUrl = constantFactory.serviceBaseUrl();
        return {
            restrict: 'A',
            scope: false,
            require: 'ngModel',
            link: function (scope, elem, attr, ngModel) {
                //when the scope changes, check the email.
                elem.on('change', function () {
                    // if there was a previous attempt, stop it.
                    if (toId) clearTimeout(toId);
                    toId = setTimeout(function () {
                        // call to some API that returns { isValid: true } or { isValid: false 

                        $http.get(baseUrl + 'api/' + attr.remoteUrl + elem.val()+"/").success(function (data) {

                            //set the validity of the field
                            ngModel.$setValidity('remotevalidator', data);
                        });
                    }, 200);
                })
            }
        }
    }
]);