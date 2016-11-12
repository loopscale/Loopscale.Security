'use strict';

app.factory('masterService', masterService);

    masterService.$inject = ['$http', 'loginService', 'constantFactory', '$q', '$rootScope'];

    function masterService($http, loginService, constantFactory, $q, $rootScope) {

        var service = {
            getAllStates: getAllStates,
            getAllProfileTypes: getAllProfileTypes,
        };

        var baseUrl = constantFactory.serviceBaseUrl();

        return service;

        function getAllStates(successCallback, errorCallback) {
            var postUrl = baseUrl + "api/masters/GetAllStates";
            var req = {
                method: 'POST',
                url: postUrl,
                headers: {
                    'Content-Type': "application/json",
                    "Accept": "application/json"
                },
                xhrFields: {
                    withCredentials: true
                }
            }

            $http(req).then(successCallback, errorCallback);
        }

        function getAllProfileTypes(successCallback, errorCallback) {
            var postUrl = baseUrl + "api/masters/GetAllProfileTypes";
            var req = {
                method: 'POST',
                url: postUrl,
                headers: {
                    'Content-Type': "application/json",
                    "Accept": "application/json"
                },
                dataType: 'json',
                xhrFields: {
                    withCredentials: true
                }
            }

            $http(req).then(successCallback, errorCallback);
        }
    }


