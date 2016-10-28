'use strict';

angular.module('app').directive('audioReader', ['$parse',  '$rootScope', function($parse, $rootScope) {
return {
        restrict: 'A',
        scope: {},
        transclude: true,
        link:function(scope, element, attrs) {
            
        }
    }
}])