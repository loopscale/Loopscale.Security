var profileModel = profileModel || function () {

    var self = this;

    self.key = "";
    self.description = "";

    self.AddNewUser = false;
    self.ImageId = "";
    self.ProfileId = "";
    self.AspNetUserId = "";
    self.RelationshipId = "";
    self.RelationshipDisabled = false;
    self.ProfileTypeDisabled = false;
    self.ProfileTypeId = "";
    self.FamilyId = "";
    self.Dob = "";
    self.Email = "";
    self.HomePhone = "";
    self.Mobile = "";
    self.Gender = "Male";
    self.FullName
    self.FirstName = "";
    self.LastName = "";
    self.HomeAddressLine1 = "";
    self.HomeAddressLine2 = "";
    self.City = "";
    self.StateId = "";
    self.Zip = "";
    self.EmployeeName = "";
    self.Occupation = "";
    self.OfficeAddressLine1 = "";
    self.OfficeAddressLine2 = "";
    self.OfficeCity = "";
    self.OfficeStateId = "";
    self.OfficeZip = "";
    self.OfficePhone = "";
    self.OfficeEmail = "";

    self.updateProfileDropdown = function (key, desc) {

        self.key = key;
        self.description = desc;
    };

    self.update = function (data) {

        self.ImageId = data.imageId;
        self.ProfileId = data.profileId;
        self.AspNetUserId = data.aspNetUserId;
        self.RelationshipId = data.relationshipId;
        self.ProfileTypeId = data.profileTypeId;
        self.FamilyId = data.familyId;
        self.Dob = data.dob;
        self.Email = data.email;
        self.HomePhone = data.homePhone;
        self.Mobile = data.mobile;
        self.Gender = data.gender;
        self.FullName = data.fullName;
        self.FirstName = data.firstName;
        self.LastName = data.lastName;
        self.HomeAddressLine1 = data.homeAddressLine1;
        self.HomeAddressLine2 = data.homeAddressLine2;
        self.City = data.city;
        self.StateId = data.stateId;
        self.Zip = data.zip;
        self.EmployeeName = data.employeeName;
        self.Occupation = data.occupation;
        self.OfficeAddressLine1 = data.officeAddressLine1;
        self.OfficeAddressLine2 = data.officeAddressLine2;
        self.OfficeCity = data.officeCity;
        self.OfficeStateId = data.officeStateId;
        self.OfficeZip = data.officeZip;
        self.OfficePhone = data.officePhone;
        self.OfficeEmail = data.officeEmail;
    };
};