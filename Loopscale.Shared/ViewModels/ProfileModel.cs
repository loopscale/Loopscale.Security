using System;

namespace Loopscale.Shared.ViewModels
{
    public class ProfileModel
    {
        public int? RelationshipId { get; set; }
        public string RelationshipName { get; set; }
        public bool AddNewUser { get; set; }
        public int ProfileTypeId { get; set; }
        public long? FamilyId { get; set; }
        public string ProfileTypeName { get; set; }
        public int? StateId { get; set; }
        public int? OfficeStateId { get; set; }
        public long ProfileId { get; set; }
        public string AspNetUserId { get; set; }
        public string FullName {
            get { return string.Concat(FamilyName , " ", FirstName, " " ,LastName);  }
        }
        public Nullable<DateTime> DOB { get; set; }
        public string Email { get; set; }
        public string HomePhone { get; set; }
        public string Mobile { get; set; }
        public string Gender { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string FamilyName { get; set; }
        public string HomeAddressLine1 { get; set; }
        public string HomeAddressLine2 { get; set; }
        public string City { get; set; }
        public string Zip { get; set; }
        public string EmployerName { get; set; }
        public string Occupation { get; set; }
        public string OfficeAddressLine1 { get; set; }
        public string OfficeAddressLine2 { get; set; }
        public string OfficeCity { get; set; }
        public string OfficeZip { get; set; }
        public string OfficePhone { get; set; }
        public string OfficeMobile { get; set; }
        public string OfficeEmail { get; set; }
        public string ImageId { get; set; }
        public bool IsEmailConfirmed { get; set; }
        public byte[] Photo { get; set; }
    }
}
