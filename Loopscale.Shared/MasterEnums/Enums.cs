using System;
using System.Reflection;

namespace Loopscale.Shared.MasterEnums
{
    public class Enums
    {
        public enum ApplicationTypes: int
        {
            JavaScript = 0,
            NativeConfidential = 1
        };
        public enum Relationship: int
        {
            Self = 1,
            Aunt = 2,
            Brother = 3,
            Father = 4,
            Guardian = 5,
            GrandFather = 6,
            GrandMother = 7,
            Mother = 8,
            Neighbour = 9,
            Sister = 10,
            NotSpecified = 11
        }

        public enum Months : int
        {
            January = 1,
            February = 2,
            March = 3,
            April = 4,
            May = 5,
            June = 6,
            July = 7,
            August = 8,
            September = 9,
            October = 10,
            November = 11,
            December = 12
        }

        public enum PaymentModeEnum : int
        {
            CreditCard = 1,
            ECheque = 2,
            Cash = 3,
            PaperCheque = 4
        }

        public enum Gender
        {
            Male = 0,
            Female = 1,
            NotSpecified = 2
        }

        public enum ProfileTypesEnum: int
        {
            Child = 1,
            Employee = 2,
            Parent = 3,
            Vendor = 4,
            Admin = 5
        }

        public enum EnrolmentStatusEnum: int
        {
            [StringValue("Cancelled admission form - failed payment")]
            Admission_form_submitted_150_fee_pending = 1,
            [StringValue("Enrolment agreement pending generation by admin")]
            Enrolment_agreement_pending_generation_by_admin = 2,           
            [StringValue("Enrolment agreement rejected by parent")]
            Enrolment_agreement_rejected_by_parent = 3,
            [StringValue("Enrolment agreement pending approval by parent")]
            Enrolment_agreement_pending_approval_by_parent = 4,        
            [StringValue("Enrolment agreement approved by parent, 2000$ fee pending")]
            Enrolment_agreement_approved_by_parent_2000_fee_pending = 5,
            [StringValue("Enrolment agreement suspended due to 24 hrs approval limit")]
            Enrolment_agreement_suspended_due_to_24_hrs_limit = 6,
            [StringValue("Enrolment agreement pending approval by admin")]
            Enrolment_agreement_pending_approval_by_admin = 7,
            [StringValue("Class schedule in progress")]
            Class_schedule_in_progress = 8,            
            [StringValue("Class schedule pending start in future date")]
            Class_schedule_pending_start_in_future_date = 9,
            [StringValue("Class schedule in waiting list")]
            Class_schedule_in_waiting_list = 10,
            [StringValue("School year over")]
            School_year_over = 11,
            [StringValue("Class schedule suspended")]
            Class_schedule_suspended = 12,
            [StringValue("Schedule change request initiated by parent")]
            Class_Schedule_Change_Request_initiated_by_parent = 13,
            [StringValue("Schedule change request cancelled by parent")]
            Class_Schedule_Change_Request_cancelled_by_parent = 14,
            [StringValue("Schedule change request disapproved by admin")]
            Class_Schedule_Change_Request_disapproved_by_admin = 15,
            [StringValue("Schedule change request approved by admin")]
            Class_Schedule_Change_Request_approved_by_admin = 16

        }

        public enum FeeTypeEnum: int
        {
            SiblingDiscount = 1,
            ReferralDiscount = 2,
            EarlyPaymentDiscount= 3,
            ScholarshipDiscount = 4,
            CustomDiscount = 5,
            SpecialCareSurcharge = 6,            
            WestonTeacher5ChildDiscount = 7,
            WestonTeacher10ChildDiscount = 8,
            WestonTeacher15ChildDiscount = 9,
            EarlyDropoffSurcharge = 10,           
            LateFeesSurcharge = 11,
            LatePickupSurcharge = 12,
            TuitionFees = 13,
            InitialDeposit = 14,
            InitialDepositDiscount = 15,
            AdmissionFormFees = 16,
            ExtraSurcharge = 17,
            All = 18
        }

        public enum ClassRoomEnum : int
        {
            Seedling = 1,
            Rosebud = 2,
            Songbird = 3,
            Sprout = 4,
            Blossom = 5,
            Star = 6,
            Moon = 7,
            Spark = 8,
            Seedling_Summer = 9,
            Junior_Pathfinders_1 = 10,
            Junior_Pathfinders_2 = 11,
            Pathfinders_Room_1 = 12,
            Pathfinders_Room_2 = 13,
            Jr_Explorer = 14,
            Explorer = 15,
            Adventurer = 16
        }

        public enum OptionMasterEnum : int
        {
            Days_3 = 1,
            Days_4 = 2,
            Days_5 = 3
        }

        public enum PickupTimeEnum : int
        {
            PickUpTime_12 = 1,
            PickUpTime_4 = 2,
            PickUpTime_6 = 3           
        }

        public enum DropoffTimeEnum : int
        {
            EarlyDropoffTime_7 = 1,
            NormalDropoffTime_7_45 = 2,
            
        }

        public enum InvoiceTypeEnum : int
        {
            All = 0,
            EnrollmentInvoice = 1,
            AdmissionInvoice = 2,
            MonthlyInvoice = 3,
            LateFeeInvoice = 4,
            OtherInvoice = 5
        }

        public enum MasterScheduleEnum : int
        {
            Month_12 = 1,
            Month_10 = 2
        }

        public enum RosterEnum : int
        {
            Schedule_10_Month = 1,
            Schedule_Summer_Month = 2
        }

        public class StringValue : System.Attribute
        {
            private readonly string _value;

            public StringValue(string value)
            {
                _value = value;
            }

            public string Value
            {
                get { return _value; }
            }
        }

        public static class StringEnum
        {
            public static string GetStringValue(Enum value)
            {
                string output = null;
                Type type = value.GetType();

                //Check first in our cached results...
                //Look for our 'StringValueAttribute' 
                //in the field's custom attributes

                FieldInfo fi = type.GetField(value.ToString());
                StringValue[] attrs =
                   fi.GetCustomAttributes(typeof(StringValue),
                                           false) as StringValue[];
                if (attrs.Length > 0)
                {
                    output = attrs[0].Value;
                }

                return output;
            }
        }
    }
}