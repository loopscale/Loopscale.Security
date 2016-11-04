using Loopscale.DataAccess.DbContexts;
using Loopscale.DataAccess.Repositories;
using Loopscale.DataAccess.Repositories.Interfaces;
using Loopscale.Shared.Helpers;
using System.Threading.Tasks;
using System.Web.Http;

namespace Loopscale.Authentication.API.Controllers
{
    [RoutePrefix("api/auth")]
    public class AuthController : BaseController
    {
        private readonly IUserRepository _profileRepo = new UserRepository();

        [AllowAnonymous]
        [HttpGet]
        [Route("GetCaptchaAudio")]
        public byte[] GetCaptchaAudio(string captcha)
        {
            var captchaHelper = new CaptchaHelper();
            return captchaHelper.CaptchaReader(captcha);
        }

        [AllowAnonymous]
        [HttpPost]
        [Route("validateemail", Name = "ValidateEmail")]
        public async Task<IHttpActionResult> ValidateEmail([FromBody] AuthModel model)
        {
            ApplicationUser user = await this.AppUserManager.FindByEmailAsync(model.Email);
            var token = model.Token.Replace("__444__", "+");
            if (user != null)
            {
                var result = await this.AppUserManager.ConfirmEmailAsync(user.Id, token);
                if (result.Succeeded)
                {
                    return Ok(result);
                }
            }

            return NotFound();
        }
    }

    public class AuthModel
    {
        public string Email { get; set; }
        public string Token { get; set; }
    }
}
