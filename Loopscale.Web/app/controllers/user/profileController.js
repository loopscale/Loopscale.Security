'use strict';

app.controller('profileController', profileController);

profileController.$inject = ['$rootScope', '$stateParams', '$scope', 'userService', 'loginService', 'authProvider', 'constantFactory', '$location', 'commonService', 'masterService'];


function profileController($rootScope, $stateParams, $scope, userService, loginService, authProvider, constantFactory, $location, commonService, masterService)
{
    console.log('entering profileController');

    //write activate function
    function activate() {
        //alert("Here");
        $scope.ProfileId = "";
        $scope.profileTypeId = "";
        $scope.Dob = "";
        $scope.Email = "";
        $scope.HomePhone = "";
        $scope.Mobile = "";
        $scope.FamilyName = "";
        $scope.FirstName = "";
        $scope.LastName = "";
        $scope.Gender = "";
        $scope.HomeAddressLine1 = "";
        $scope.HomeAddressLine2 = "";
        $scope.City = "";
        $scope.StateId = "";
        $scope.Zip = "";
        $scope.EmployerName = "";
        $scope.Occupation = "";
        $scope.OfficeAddressLine1 = "";
        $scope.OfficeAddressLine2 = "";
        $scope.OfficeStateId = "";
        $scope.OfficeCity = "";
        $scope.OfficeZip = "";
        $scope.OfficePhone = "";
        $scope.OfficeEmail = "";
        $scope.$isUserNameExists = false;
        $scope.$isEmailExists = false

        $scope.action = $stateParams.action;
        
        masterService.getAllStates(onStates, errorCallback);
        getProfile();
    }
        
    function onStates(data) {
        $scope.states = [];
        $scope.officeStates = [];
        $.each(data.data, function (i, obj) {
            var item = new masterModel();
            item.update(obj.key, obj.value);
            $scope.states.push(item);
            $scope.officeStates.push(item);
        });
    }

    function getProfile()
    {
        console.log('entering getProfile() controller');

        userService.getProfile(profileloadSuccessCallBack, profileLoadErrorCallBack);
    }

    function profileloadSuccessCallBack(d) {
        //console.log(d.data.profileTypeId);
        var profile = new profileModel();
        profile.update(d.data);

        $scope.profile = profile;

        console.log($scope.profile);
        //showHideField();
        //getProfileImage(d.data.imageId);

        //console.log('-----' + data.Email);
    }

    function getProfileImage(imageId) {
        if (imageId != null && imageId !== "") {
            userService.getImageData(imageId, function (d) {
                $scope.base64BgImagePath = "data:image/png;base64," + d.data;
            }, profileLoadErrorCallBack);
        }
        else {
            if (d != null && d.data != null) {
                $scope.base64BgImagePath = d.data.photo != null
                    ? "data:image/png;base64," + d.data.photo
                    : $scope.defaultProfilePic;
            } else {
                $scope.base64BgImagePath = $scope.defaultProfilePic;
            }
        }
    }

    function profileLoadErrorCallBack(d) {
        $rootScope.alerts.push({
            'type': 'danger',
            'msg': "Unable to load Profile detail"
        });
        clearProfileData();
    }

    function errorCallback(d) {
        $scope.alerts.push({
            'type': 'danger',
            'msg': "Unable to process"
        });
    }

    function clearProfileData() {
        var data = new profileModel();
        //$scope.profile = data;
        data.ProfileId = 0;
        showHideField();
    }

    activate();
}