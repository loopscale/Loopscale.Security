using Loopscale.DataAccess.DbContexts;
using Loopscale.Authentication.API.Infrastructure;
using Loopscale.Shared.Exceptions;
using Loopscale.Shared.ViewModels;
using Microsoft.AspNet.Identity;
using System.Linq;

namespace Beginnings.Services.ServiceHelpers
{
    public static class UserHelper
    {
        public static bool AddAspNetUser(ApplicationUserManager userManager, UserModel userModel)
        {
            ApplicationUser currentUser = new ApplicationUser();

            var newUser = new ApplicationUser
            {
                UserName = userModel.UserName,
                Email = userModel.Email,
            };

            var result = userManager.Create(newUser, userModel.Password);

            if (!result.Succeeded)
            {
                throw new CreateUserException(result.Errors.FirstOrDefault());
            }

            return result.Succeeded;
        }

        public static void DeleteAspNetUser(ApplicationUserManager userManager, UserModel userModel)
        {
            try
            {
                ApplicationUser currentUser = new ApplicationUser();

                var newUser = new ApplicationUser
                {
                    UserName = userModel.UserName,
                    Email = userModel.Email,
                };

                userManager.DeleteAsync(newUser);
            }
            catch
            {
            }
        }

        public static void AddRoleToUser(ApplicationUserManager userManager, UserModel userModel)
        {
            var result = userManager.AddToRole(userModel.Id, userModel.Role);

            if (!result.Succeeded)
            {
                throw new CreateUserException(result.Errors.FirstOrDefault());
            }
        }

        public static void RemoveRoleFromUser(ApplicationUserManager userManager, UserModel userModel)
        {
            try
            {
                var result = userManager.RemoveFromRole(userModel.Id, userModel.Role);

                if (!result.Succeeded)
                {
                    throw new CreateUserException(result.Errors.FirstOrDefault());
                }
            }
            catch
            {
            }
        }
    }
}