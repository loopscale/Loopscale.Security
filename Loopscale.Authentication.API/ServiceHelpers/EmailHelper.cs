//using Loopscale.DataAccess.EFModel;
//using Loopscale.Authentication.API.Infrastructure;
//using Loopscale.Shared.MasterEnums;
//using Loopscale.Shared.RabbitMqSubscribers;
//using Loopscale.Shared.ViewModels;
//using System;
//using System.Collections.Generic;
//using System.Configuration;
//using System.Text;
//using System.Threading.Tasks;
//using Loopscale.Authentication.API.Models;

//namespace Loopscale.Authentication.API.ServiceHelpers
//{
//    public class EmailHelper
//    {
//        public static async Task<bool> SendWelcomeEmail(ApplicationUserManager userManager, Shared.ViewModels.UserModel userModel)
//        {
//            //var token = userManager.GenerateEmailConfirmationTokenAsync(userModel.Id);

//            ////+ is creating problem soreplacing it wih other characters
//            //var stringToEscape = token.Result.Replace("+", "__444__");
//            //var body = GenerateWelcomeEMailBody(userModel, Uri.EscapeUriString(stringToEscape));
//            var body = GenerateWelcomeEMailBodyWithoutToken(userModel);
//            //Send an email with this link
//            return MailSubscription.SendEmailAsync(userModel.Email, "Welcome Email!", body);
//        }

//        public static string GenerateWelcomeEMailBody(UserModel userModel, string token)
//        {
//            //TODO
//            var callbackUrl = string.Format("{0}/User/Activate?email={1}&key={2}", ConfigurationManager.AppSettings["uiServerBaseUrl"], userModel.Email, token); //Url.Action("ValidateEmail", "Users", new { email = cc.Id, validationKey = token }, protocol: HttpContext.Request.Scheme);

//            var sb = new StringBuilder("Hello [[FIRSTNAME]] [[LASTNAME]], <br />Thank you for registering with us. <br/>Please click <a href='[[CallbackUrl]]' >here</a> to activate your account");
//            sb.Replace("[[FIRSTNAME]]", userModel.FirstName);
//            sb.Replace("[[LASTNAME]]", userModel.LastName);
//            sb.Replace("[[CallbackUrl]]", callbackUrl);
//            sb.Replace("[[EMAIL]]", userModel.Email);

//            return sb.ToString();
//        }

//        public static string GenerateWelcomeEMailBodyWithoutToken(UserModel userModel)
//        {   var sb = new StringBuilder("Hello [[FIRSTNAME]] [[LASTNAME]], <br />Greetings from Beginnings School.<br/>Thank you for registering with us.<br />");
//            sb.Replace("[[FIRSTNAME]]", userModel.FirstName);
//            sb.Replace("[[LASTNAME]]", userModel.LastName);
//            sb.Replace("[[EMAIL]]", userModel.Email);

//            return sb.ToString();
//        }

//        public static async Task<bool> SendInvite(string emailId, TourAppointmentModel appointment)
//        {
//            return MailSubscription.SendAppointmentAsync(emailId, "Appointment Confirmed", appointment.Description,
//                appointment.StartTime, appointment.EndTime);
//        }

//        /*
//        public static async Task<bool> SendEnrolmentEmail(ChildSchedule schedule, Profile adminProfile)
//        {        
//            var emails = new List<string>();
//            var body = new StringBuilder();
//            var statusEnum = (Enums.EnrolmentStatusEnum)Enum.Parse(typeof(Enums.EnrolmentStatusEnum), schedule.StatusId.ToString());
//            switch (statusEnum)
//            {
//               //case Beginnings.Shared.MasterEnums.Enums.EnrolmentStatusEnum.Enrolment_agreement_pending_approval_by_admin:
//                //    body.AppendFormat("Dear Admin <br />Class schedule has been accepted for Child {0} {1}m Child Id. Please review and Sign", schedule.ChildProfile.FirstName, schedule.ChildProfile.LastName, schedule.ChildProfileId);
//                //    emails.Add(schedule.ChildProfile.Email);
//                //    break;
             
//                default:
//                    return true;
//            }
//            if (emails.Count > 0)
//            {
//                return MailSubscription.SendMultipleEmailAsync(emails, "Beginnings School", body.ToString());
//            }

//            return true;
//        }
//        */
//    }
//}
