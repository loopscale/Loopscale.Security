
'use strict';

angular.module('app')
        .directive('thValidatepwd', function () {
            return {
                require: 'ngModel',
                restrict: 'A',
                link: function (scope, elem, attrs, ctrl) {
                    scope.count = [];
                    scope.$watch(function () {

                        if (ctrl.$pristine || angular.isUndefined(ctrl.$viewValue))
                            return;

                        ctrl.$render();

                        //Validation for any three of four categories.
                        var cnt = 0;
                        var regExArray = [/(?=.*\d)/, /(?=.*[a-z])/, /(?=.*[A-Z])/, /(?=.*[~!@#$])/];

                        for (var x = 0; x < regExArray.length; x++) {
                            scope.count[x] = regExArray[x].test(ctrl.$viewValue);
                        }

                        for (var j = 0; j < scope.count.length; j++) {
                            if (scope.count[j] == true)
                                cnt += 1;
                        }

                        // Validation for characters.
                        var passwordRegEx = /(?:([a-z])|([A-Z])|([0-9])|([~!@#$]))$/;

                        var valid = passwordRegEx.test(ctrl.$viewValue);

                        //Validation for restricted values
                        if (valid && cnt >= 3 && ctrl.$viewValue.length >= 8) {
                            ctrl.$setValidity("validity", true);
                        } else {
                            ctrl.$setValidity("validity", false);
                        }
                    });

                    $(".clspassword").keypress(function (e) {

                        if (e.which == 32) {
                            e.preventDefault();
                            return false;
                        }
                    });
                }
            };
        })
        .directive('thMatch', ["$parse", function ($parse) {

            return {
                require: 'ngModel',
                restrict: 'A',
                link: function (scope, elem, attrs, ctrl) {

                    if (!ctrl)
                        return;

                    if (!attrs["thMatch"])
                        return;

                    var firstPassword = $parse(attrs["thMatch"]);

                    var validator = function (value) {

                        var temp = firstPassword(scope), matched = false;

                        if (value) {
                            matched = value == temp;
                        } else {
                            matched = true;
                        }

                        ctrl.$setValidity('match', matched);
                        return value;
                    };

                    ctrl.$parsers.unshift(validator);
                    ctrl.$formatters.push(validator);
                    attrs.$observe("thMatch", function () {
                        validator(ctrl.$viewValue);
                    });

                    scope.$watch(attrs["thMatch"], function () {
                        validator(ctrl.$viewValue);
                    });

                }
            };
        }])
        .directive('thEmail', [function () {
            return {
                require: 'ngModel',
                restrict: 'A',
                scope: {
                },
                link: function (scope, elem, attrs, ctrl) {
                    scope.$watch(function () {
                        if (ctrl.$viewValue) {
                            //var emailRegEx = /[^\.]([a-zA-Z0-9!#$%&'\*\\+\-\/=\?\^_`\{\}|~\.])*[^\.]*@([a-zA-Z0-9\-]+\.)+[a-zA-Z0-9\-]+/;
                            var emailRegEx = /^[_A-Za-z0-9-]+(\.[_A-Za-z0-9-]+)*@[-A-Za-z0-9]+(\.[_A-Za-z0-9-]{2,})+$/;
                            var doubleDot = /\.{2}/;
                            return (ctrl.$pristine && angular.isUndefined(ctrl.$viewValue)) || (ctrl.$viewValue.match(emailRegEx) != null && ctrl.$viewValue.match(doubleDot) == null);
                        }
                        return true;
                    }, function (currentValue) {
                        ctrl.$setValidity('validEmail', currentValue);
                    });
                }
            };
        }])
        .directive('numbersOnly', [function(){
            return {
                require: 'ngModel',
                restrict: 'A',
                scope: {
                },
                link: function(scope, element, attrs, modelCtrl) {
                    modelCtrl.$parsers.push(function (inputValue) {
                        // this next if is necessary for when using ng-required on your input. 
                        // In such cases, when a letter is typed first, this parser will be called
                        // again, and the 2nd time, the value will be undefined
                        if (inputValue == undefined) return '' 
                        var transformedInput = inputValue.replace(/[^0-9]/g, ''); 
                        if (transformedInput!=inputValue) {
                            modelCtrl.$setViewValue(transformedInput);
                            modelCtrl.$render();
                        }         

                        return transformedInput;         
                    });
                }
            }
        }])
        .directive('pwCheck', [function () {
            return {
                require: 'ngModel',
                link: function (scope, elem, attrs, ctrl) {
                    var firstPassword = '#' + attrs.pwCheck;
                    elem.add(firstPassword).on('keyup', function () {
                        scope.$apply(function () {
                            var v = elem.val() === $(firstPassword).val();
                            ctrl.$setValidity('pwmatch', v);
                        });
                    });
                }
            }
        }])
        .directive("validateDate", function () {
            return {
                require: 'ngModel',
                link: function (scope, elm, attrs, ctrl) {
                    ctrl.$validators.validateDate = function (modelValue, viewValue) {
                        var date = new Date(modelValue);
                        console.log(date);
                        //var dateReg = /^\d{2}[./-]\d{2}[./-]\d{4}$/;
                        //var testresult = dateReg.test(dateReg);
                        //console.log(testresult);
                        if(date == 'Invalid Date')
                        {
                            return false
                        }
                        else
                        {
                            return true;
                        }
                    };
                }
            };
        });

        
