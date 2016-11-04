'use strict';

app.factory('userService', userService);

userService.$inject = ['$http', 'constantFactory'];

function userService($http, constantFactory) {
    var service = {
        editUser: editUser,
        updateUser: updateUser,
        createUser: createUser,
        isUniqueUser: IsUniqueUser,
        isUniqueEmail: IsUniqueEmail,
        createProfile: createProfile,
        listProfile: listProfile,
        getProfileDetail: getProfileDetail,
        getImageData: getImageData,
        getAllUserProfiles: getAllUserProfiles,
        getAllRegisteredProfiles: getAllRegisteredProfiles,
        activateProfile: activateProfile,
        searchProfiles: searchProfiles,
        resetPassword: resetPassword
    };

    var baseUrl = constantFactory.serviceBaseUrl();

    return service;

    function editUser(id, successCallback, errorCallback) {
        var postUrl = baseUrl + "api/Users/" + id;
        var req = {
            method: 'GET',
            url: postUrl,
            headers: {
                'Content-Type': "application/json"
            },
            //data: user
        }

        $http(req).then(successCallback, errorCallback);
    }

    function createUser(userId, password, firstName, lastName, email, mobile, phone, addNewUser, successCallback, errorCallback) {
        var user = {
            "UserName": userId,
            "Password": password,
            "FirstName": firstName,
            "LastName": lastName,
            "Email": email,
            "DOB": null,
            "Phone": phone,
            "Mobile": mobile,
            "AddNewUser": addNewUser
        }
        var postUrl = baseUrl + "api/Users/RegisterNewProfile";
        var req = {
            method: 'POST',
            url: postUrl,
            headers: {
                'Content-Type': "application/json"
            },
            xhrFields: {
                withCredentials: true
            },
            data: user
        }

        $http(req).then(successCallback, errorCallback);
    }

    function updateUser(user, successCallback, errorCallback) {
        var postUrl = baseUrl + "api/users/" + user.Id;
        var req = {
            method: 'PUT',
            url: postUrl,
            headers: {
                'Content-Type': "application/json"
            },
            xhrFields: {
                withCredentials: true
            },
            data: user
        };

        $http(req).then(successCallback, errorCallback);
    }

    function IsUniqueUser(id, name) {
        var url = baseUrl + "api/users/uniqueuser/" + name;
        var req = {
            method: 'GET',
            url: url,
            headers: {
                'Content-Type': "application/json"
            },
            //data: user
        }

        return $http(req);
    }

    function listProfile(successcallback, errorcallBack) {
        var url = baseUrl + "api/users/ListProfile";

        var req = {
            method: 'GET',
            url: url,
            headers: {
                'Content-Type': "application/json"
            },
        }
        $http(req).then(successcallback, errorcallBack);
    }

    function getAllUserProfiles(successcallback, errorcallBack) {
        var url = baseUrl + "api/users/GetAllUserProfiles";

        var req = {
            method: 'GET',
            url: url,
            headers: {
                'Content-Type': "application/json"
            },
        }
        $http(req).then(successcallback, errorcallBack);
    }

    function IsUniqueEmail(id, email) {
        var url = baseUrl + "users/UniqueEMail/" + email;
        var req = {
            method: 'GET',
            url: url,
            headers: {
                'Content-Type': "application/json"
            },
            //data: user
        }

        return $http(req);
    }

    function registerNewProfile(profile, successCallback, errorCallback) {

        var postUrl = baseUrl + "api/users/RegisterNewProfile/";
        var req = {
            method: 'POST',
            url: postUrl,
            headers: {
                'Content-Type': "application/json"
            },
            xhrFields: {
                withCredentials: true
            },
            data: profile
        }

        $http(req).then(successCallback, errorCallback);
    }

    function createProfile(profile, successCallback, errorCallback) {

        console.log(profile);

        var postUrl = baseUrl + "api/users/CreateProfile/";
        var req = {
            method: 'POST',
            url: postUrl,
            headers: {
                'Content-Type': "application/json"
            },
            xhrFields: {
                withCredentials: true
            },
            data: profile
        }

        $http(req).then(successCallback, errorCallback);
    }

    function getProfileDetail(profileId, successCallback, errorCallback) {
        var postUrl = baseUrl + "api/users/GetProfileDetail";
        var req = {
            method: 'Get',
            url: postUrl,
            params: { profileId: profileId },
            headers: {
                'Content-Type': "application/json"
            },
            xhrFields: {
                withCredentials: true
            }
        }

        $http(req).then(successCallback, errorCallback);

    }

    function getImageData(imageId, successCallback, errorCallback) {
        var postUrl = baseUrl + "api/users/GetProfileImageContent/";
        var req = {
            method: 'Get',
            url: postUrl,
            params: { imageid: imageId },
            headers: {
                'Content-Type': "application/json"
            },
            xhrFields: {
                withCredentials: true
            }
        }

        $http(req).then(successCallback, errorCallback);
    }

    function getAllRegisteredProfiles(email, successcallback, errorcallBack) {
        var url = baseUrl + "api/users/GetAllRegisteredProfiles";

        var req = {
            method: 'POST',
            url: url,
            headers: {
                'Content-Type': "application/json"
            },
            data: JSON.stringify(email)
        }

        $http(req).then(successcallback, errorcallBack);
    }

    function activateProfile(userId, successCallback, errorCallback) {
        var postUrl = baseUrl + "api/Users/ActivateProfile";
        var req = {
            method: 'POST',
            url: postUrl,
            headers: {
                'Content-Type': "application/json"
            },
            xhrFields: {
                withCredentials: true
            },
            data: userId
        }

        $http(req).then(successCallback, errorCallback);
    }

    function searchProfiles(filter, successCallback, errorCallback) {

        var postUrl = baseUrl + "api/users/SearchProfiles/";
        var req = {
            method: 'POST',
            url: postUrl,
            headers: {
                'Content-Type': "application/json"
            },
            xhrFields: {
                withCredentials: true
            },
            data: filter
        }

        $http(req).then(successCallback, errorCallback);
    }

    function resetPassword(passwordModel, successcallback, errorcallBack) {
        var url = baseUrl + "api/users/ResetPassword";

        var req = {
            method: 'POST',
            url: url,
            headers: {
                'Content-Type': "application/json"
            },
            data: JSON.stringify(passwordModel)
        }

        $http(req).then(successcallback, errorcallBack);
    }
}
