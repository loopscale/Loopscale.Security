'use strict';

app.factory('employeeService', employeeService);

employeeService.$inject = ['$http', 'constantFactory'];

function employeeService($http, constantFactory) {

    var service = {
        getAllEmployees: getAllEmployees,
        createProfile: createProfile,
        getAllVisitorProfile: getAllVisitorProfile,
        updateVisitorToRegistrant: updateVisitorToRegistrant
    };

    var baseUrl = constantFactory.serviceBaseUrl();

    return service;

    function getAllEmployees(successCallback, errorCallback) {

        var req = {
            method: 'GET',
            url: baseUrl + "api/admin/employee/GetAllEmployees",
            headers: {
                'Content-Type': "application/json"
            },
            xhrFields: {
                withCredentials: true
            },
        }

        $http(req).then(successCallback, errorCallback);
    };

    function getAllVisitorProfile(successCallback, errorCallback) {

        var req = {
            method: 'GET',
            url: baseUrl + "api/users/GetVisitorProfiles",
            headers: {
                'Content-Type': "application/json"
            },
            xhrFields: {
                withCredentials: true
            },
        }

        $http(req).then(successCallback, errorCallback);
    };

    function updateVisitorToRegistrant(id, successCallback, errorCallback) {

        var req = {
            method: 'GET',
            url: baseUrl + "api/users/UpdateVisitorToRegistrant",
            params: { id: id },
            headers: {
                'Content-Type': "application/json"
            },
            xhrFields: {
                withCredentials: true
            },
        }

        $http(req).then(successCallback, errorCallback);
    };

    //function updateVisitorToRegistrant(id, successCallback, errorCallback) {

    //    //var data = {
    //    //    id: id
    //    //};

    //    var postUrl = baseUrl + "api/users/UpdateVisitorToRegistrant";
    //    var req = {
    //        method: 'POST',
    //        url: postUrl,
    //        headers: {
    //            'Content-Type': "application/json"
    //        },
    //        xhrFields: {
    //            withCredentials: true
    //        },
    //        data: id
    //    }

    //    $http(req).then(successCallback, errorCallback);
    //};

    function createProfile(profile, successCallback, errorCallback) {

        var data = {
            FirstName: profile.FirstName,
            LastName: profile.LastName,
            Email: profile.Email
        };

        var postUrl = baseUrl + "api/admin/Employee/AddProfile";
        var req = {
            method: 'POST',
            url: postUrl,
            headers: {
                'Content-Type': "application/json"
            },
            xhrFields: {
                withCredentials: true
            },
            data: data
        }

        $http(req).then(successCallback, errorCallback);
    };
}