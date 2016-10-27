using Loopscale.Shared.Exceptions;
using Loopscale.Shared.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Http.Filters;

namespace Loopscale.Authentication.API.Filters
{
    public class GlobalExceptionFilterAttribute : ExceptionFilterAttribute
    {
        public override void OnException(HttpActionExecutedContext context)
        {
            string value = null;

            if (context.Exception is InvalidCaptchaException)
            {
                value = "Invalid Captcha";
            }
            else if (context.Exception is CreateUserException)
            {
                value = (context.Exception.InnerException != null) ? context.Exception.InnerException.Message : context.Exception.Message;

                if (value.Contains("Password"))
                {
                    value = "Invalid Password";
                }
            }
            else
            {
                LSLogManager.Instance.LogError("Error", context.Exception);

                if (context.Exception.InnerException != null)
                {
                    value = context.Exception.InnerException.Message;
                }
                else
                {
                    value = context.Exception.Message;
                }
            }

            context.Response = context.Request.CreateResponse(HttpStatusCode.PreconditionFailed, value);
        }
    }
}