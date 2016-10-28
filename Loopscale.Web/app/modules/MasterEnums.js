(function () {
    'use strict';

    angular
        .module('app')
        .factory('masterEnums', masterEnums)
        .constant('EnrolmentEnum', {
            Value: {
                admission_form_submitted_150_fee_pending: 1,
                enrolment_agreement_pending_generation_by_admin: 2,
                enrolment_agreement_rejected_by_parent: 3,
                enrolment_agreement_pending_approval_by_parent: 4,
                enrolment_agreement_approved_by_parent_2000_fee_pending: 5,
                enrolment_agreement_suspended_due_to_24_hrs_limit: 6,
                enrolment_agreement_pending_approval_by_admin: 7,
                class_schedule_in_progress: 8,
                class_schedule_pending_start_in_future_date: 9,
                class_schedule_in_waiting_list: 10,
                school_year_over: 11,
                class_schedule_suspended: 12,
                class_Schedule_Change_Request_initiated_by_parent: 13,
                class_Schedule_Change_Request_cancelled_by_parent: 14,
                class_Schedule_Change_Request_disapproved_by_admin: 15,
                class_Schedule_Change_Request_approved_by_admin: 16
            }
        })
        .constant('RelationshipEnum', {
            Value: {
                self: 1,
                aunt: 2,
                brother: 3,
                father: 4,
                guardian: 5,
                grandFather: 6,
                grandMother: 7,
                mother: 8,
                neighbour: 9,
                sister: 10,
                notSpecified: 11
            }
        })
        .constant('GenderEnum', {
            Value: {
                male: 0,
                female: 1,
                notSpecified: 2
            }
        })
        .constant('ProfileTypeEnum', {
            Value: {
                child: 1,
                employee: 2,
                parent: 3,
                vendor: 4,
                admin: 5
            }
        })
        .constant('FeeTypeEnum', {
            Value: {
                siblingDiscount: 1,
                referralDiscount: 2,
                earlyPaymentDiscount: 3,
                scholarshipDiscount: 4,
                customDiscount: 5,
                specialCareSurcharge: 6,                
                westonTeacher5ChildDiscount: 7,
                westonTeacher10ChildDiscount: 8,
                westonTeacher15ChildDiscount: 9,
                earlyDropoffSurcharge: 10,
                lateFeesSurcharge: 11,
                latePickupSurcharge: 12,
                tuitionFees: 13,
                initialDeposit: 14,
                initialDepositDiscount: 15,
                admissionFormFees: 16,
                extraSurcharge: 17,
                all: 18
            }
        })
        .constant('OptionMasterEnum', {
            Value: {
                Days_3: 1,
                Days_4: 2,
                Days_5: 3
            }
        })
        .constant('PickupTimeEnum', {
            Value: {
                pickUpTime_12: 1,
                pickUpTime_4: 2,
                pickUpTime_6: 3 
            }
        })
        .constant('DropoffTimeEnum', {
            Value: {
                normalDropoffTime_7_45: 1,
                earlyDropoffTime_7: 2
            }
        }).constant('MasterScheduleEnum', {
            Value: {
                Month_12: 1,
                Month_10: 2
            }
        });

    function masterEnums() {
        var service = {
            
        };

        return service;
    }
})();
