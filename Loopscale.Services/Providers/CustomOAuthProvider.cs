using Microsoft.Owin.Security;
using Microsoft.Owin.Security.OAuth;
using Microsoft.AspNet.Identity.Owin;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Loopscale.Services.Infrastructure;
using Loopscale.DataAccess.DbContexts;
using Loopscale.DataAccess.Repositories.Interfaces;
using Loopscale.DataAccess.Repositories;
using Loopscale.Shared.Helpers;
using Loopscale.Shared.Logging;
using Loopscale.DataAccess.EFModel;

namespace Beginnings.Services.Providers
{
    public class CustomOAuthProvider : OAuthAuthorizationServerProvider
    {
        private readonly IUserRepository _profileRepo;
        public CustomOAuthProvider()
        {
            _profileRepo = new UserRepository(new LS_SecurityEntities());
        }
        public override Task ValidateClientAuthentication(OAuthValidateClientAuthenticationContext context)
        {
            context.Validated();
            return Task.FromResult<object>(null);
        }

        public override Task MatchEndpoint(OAuthMatchEndpointContext context)
        {
            var allowedOrigin = ConfigHelper._Instance.ServerBaseUrl;
            if (context.IsTokenEndpoint && context.Request.Method == "OPTIONS")
            {
                //context.OwinContext.Response.Headers.Add("Access-Control-Allow-Origin", new[] { allowedOrigin });
                context.OwinContext.Response.Headers.Add("Access-Control-Allow-Headers", new[] { "authorization", "content-type" });
                context.RequestCompleted();
                return Task.FromResult(0);
            }

            return base.MatchEndpoint(context);
        }
        public override async Task GrantResourceOwnerCredentials(OAuthGrantResourceOwnerCredentialsContext context)
        {

            //var allowedOrigin = "http://ccms.beginningsschool.com";
            //var allowedOrigin = "http://localhost/ChildCareManagement.Web/";

            //context.OwinContext.Response.Headers.Add("Access-Control-Allow-Methods", new [] { "GET", "POST", "OPTIONS", "HEAD" });
           // context.OwinContext.Response.Headers.Add("Access-Control-Allow-Credentials", new [] { "true" });
           //context.OwinContext.Response.Headers.Add("Access-Control-Allow-Origin", new[] { allowedOrigin });
           // context.OwinContext.Response.Headers.Add("Access-Control-Allow-Headers", new[] { "Origin", "Content-Type", "X-Auth-Token" });

            var userManager = context.OwinContext.GetUserManager<ApplicationUserManager>();
            ApplicationUser user;
            try
            {
                //throw new Exception("error message");
                user = await userManager.FindAsync(context.UserName, context.Password);


                if (user == null)
                {
                    context.SetError("invalid_grant", "The user name or password is incorrect.");
                    return;
                }

                if (!user.EmailConfirmed)
                {
                    context.SetError("invalid_grant", "User did not confirm email.");
                    return;
                }

                ClaimsIdentity oAuthIdentity = await user.GenerateUserIdentityAsync(userManager, "JWT");

                var claims = new List<Claim>();

                var profile = _profileRepo.GetProfileByEmailId(user.Email);
                var roleData = await userManager.GetRolesAsync(user.Id);
                claims.Add(new Claim("FirstName", profile.FirstName));
                claims.Add(new Claim("LastName", profile.LastName));
                claims.Add(new Claim("ProfileId", profile.ProfileId.ToString()));
                claims.Add(new Claim("FamilyId", profile.FamilyId.ToString()));

                var role = roleData.FirstOrDefault();

                claims.Add(new Claim("Role", role.ToLower()));

                oAuthIdentity.AddClaims(claims);

                var props = new AuthenticationProperties(new Dictionary<string, string>
                        {
                            {
                               "FirstName", profile.FirstName
                            },
                            {
                               "LastName", profile.LastName
                            },
                            {
                                "Role", role.ToLower()
                            },
                            //{
                            //    "UserName", profile.AspNetUser.UserName
                            //},
                            {
                                "AspNetUserId", profile.AspNetUserId
                            },
                            {
                                "ProfileId", profile.ProfileId.ToString()
                            },
                            {
                                "FamilyId", profile.FamilyId.ToString()
                            }
                        });

                var ticket = new AuthenticationTicket(oAuthIdentity, props);
                context.Validated(ticket);
            }
            catch (Exception ex)
            {
                CCMgmtLogManager.Instance.LogError(ex);
                throw ex;
            }
        }

        public override Task TokenEndpoint(OAuthTokenEndpointContext context)
        {
            foreach (KeyValuePair<string, string> property in context.Properties.Dictionary)
            {
                context.AdditionalResponseParameters.Add(property.Key, property.Value);
            }

            return Task.FromResult<object>(null);
        }
    }
}