(function () {
    'use strict';

    angular
        .module('app')
        .factory('constantFactory', constantFactory)
        /*.constant('Constants', {
            Roles: {
                admin: 'SYS_ADMIN',
                user: 'SYS_USER',
                guest: 'SYS_GUEST'
            }
        });*/
        //.constant('someStringConstant', 'someConstantValue');      

    constantFactory.$inject = ['$http', 'CCMSettings'];


    function constantFactory($http, CCMSettings) {

        var service = {
            serviceBaseUrl: serviceBaseUrl,
            uiBaseUrl:uiBaseUrl,
            showSuccessMsg : showSuccess,
            showErrorMsg: showError,
            hideSuccessMsg: hideSuccess,
            hideErrorMsg:hideError
        };

        var baseUrl = CCMSettings.apiServiceBaseUri;

        var baseUiUrl = CCMSettings.uiBaseUri;

        //console.log("inside constant factory");
        //console.log(baseUiUrl);

        return service;

        function uiBaseUrl() {
            return baseUiUrl;
        }

        function serviceBaseUrl() {
            return baseUrl;
        }

        function showError(msg) {
            $("#dvErrorMsg").text(msg);
            $("#dvError").show();
        }

        function hideError()
        {
            $("#dvErrorMsg").text('');
            $("#dvError").hide();
        }

        function showSuccess(msg) {
            $("#dvSuccessMsg").text(msg);
            $("#dvSuccess").show();
        }

        function hideSuccess() {
            $("#dvSuccessMsg").text('');
            $("#dvSuccess").hide();
        }
    }
})();