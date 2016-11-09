'use strict';

app.controller('profileController', profileController);

profileController.$inject = ['$rootScope', '$stateParams', '$scope', 'userService', 'loginService', 'authProvider', 'constantFactory', '$location', 'commonService'];


function profileController($rootScope, $stateParams, $scope, userService, loginService, authProvider, constantFactory, $location, commonService)
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
    }

    $scope.getProfile = function getProfile()
    {
        console.log('entering getProfile() controller');

        userService.getProfile(profileloadSuccessCallBack, profileLoadErrorCallBack);
    }

    function profileloadSuccessCallBack(d) {
        //console.log(d.data.profileTypeId);
        var data = new profileModel();
        data.ProfileId = parseInt(d.data.profileId);
        if (d.data.relationshipId) {
            data.RelationshipId = d.data.relationshipId.toString();
        }
        //data.RelationshipId = d.data.relationshipId.toString();
        data.ProfileTypeId = d.data.profileTypeId;
        data.Dob = new Date(d.data.dob);
        data.Email = d.data.email;
        data.HomePhone = d.data.homePhone;
        data.Mobile = d.data.mobile;
        data.FamilyName = d.data.familyName;
        data.FirstName = d.data.firstName;
        data.LastName = d.data.lastName;
        data.Gender = d.data.gender;
        data.HomeAddressLine1 = d.data.homeAddressLine1;
        data.HomeAddressLine2 = d.data.homeAddressLine2;
        if (d.data.stateId) {
            data.StateId = d.data.stateId.toString();
        }
        data.City = d.data.city;
        data.Zip = d.data.zip;
        data.EmployerName = d.data.employerName;
        data.Occupation = d.data.occupation;
        data.OfficeAddressLine1 = d.data.officeAddressLine1;
        data.OfficeAddressLine2 = d.data.officeAddressLine2;
        if (d.data.officeStateId) {
            data.OfficeStateId = d.data.officeStateId.toString();
        }
        data.OfficeCity = d.data.officeCity;
        data.OfficeZip = d.data.officeZip;
        data.OfficePhone = d.data.officePhone;
        data.OfficeEmail = d.data.officeEmail;
        $scope.profile = data;

        showHideField();
        getProfileImage(d.data.imageId);

        console.log('-----' + data.Email);
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

    function clearProfileData() {
        var data = new profileModel();
        //$scope.profile = data;
        data.ProfileId = 0;
        showHideField();
    }

    activate();
}