'use strict';

angular
    .module('app')
    .factory('userService', userService);

userService.$inject = ['$http', 'constantFactory'];

function userService($http, constantFactory) {
    var service = {
        editUser: editUser,
        updateUser: updateUser,
        createUser: createUser,
        isUniqueUser: IsUniqueUser,
        isUniqueEmail: IsUniqueEmail,
        createProfile: createProfile,
        registerNewParentProfile: registerNewParentProfile,
        listProfile: listProfile,
        getProfileDetail: getProfileDetail,
        getCaptchaImage: getCaptchaImage,
        readCaptcha: readCaptcha,
        getImageData: getImageData,
        getAllUserProfiles: getAllUserProfiles,
        getAllRegisteredProfiles: getAllRegisteredProfiles,
        activateProfile: activateProfile,
        searchProfiles: searchProfiles,
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

    function createUser(userId, password, firstName, lastName, email, captcha, mobile, phone, successCallback, errorCallback) {
        var user = {
            "UserName": userId,
            "Password": password,
            "FirstName": firstName,
            "LastName": lastName,
            "Email": email,
            "DOB": null,
            "Phone": phone,
            "Mobile": mobile,
            "Captcha": captcha
        }
        var postUrl = baseUrl + "api/Users/Add";
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

    function getCaptchaImage(successCallback, errorCallback) {
        var postUrl = baseUrl + "api/auth/GetCaptchaText";
        var req = {
            method: 'Get',
            url: postUrl,
            headers: {
                'Content-Type': "application/json",

            },
            xhrFields: {
                withCredentials: true
            }
        }

        $http(req).then(successCallback, errorCallback);
    }

    function readCaptcha(captchaText, successCallback, errorCallback) {
        var postUrl = baseUrl + "api/auth/GetCaptchaAudio";
        var req = {
            method: 'Get',
            url: postUrl,
            params: { captcha: captchaText },
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
}