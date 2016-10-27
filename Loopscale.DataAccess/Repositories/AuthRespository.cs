
using Loopscale.DataAccess.DbContexts;
using Loopscale.DataAccess.EFModel;
using Loopscale.Shared.ViewModels;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;

namespace Loopscale.DataAccess.Repositories
{
    public class AuthRepository : IDisposable
    {
        private readonly LS_SecurityEntities _db = new LS_SecurityEntities();

        private UserManager<IdentityUser> _userManager;

        public AuthRepository()
        {            
            _userManager = new UserManager<IdentityUser>(new UserStore<IdentityUser>(_db));
        }

        public async Task<IdentityResult> RegisterUser(UserModel userModel)
        {
            IdentityUser user = new IdentityUser
            {
                UserName = userModel.UserName
            };

            var result = await _userManager.CreateAsync(user, userModel.Password);

            return result;
        }

        public async Task<IdentityUser> FindUser(string userName, string password)
        {
            IdentityUser user = await _userManager.FindAsync(userName, password);
            return user;
        }

        public Client FindClient(string clientId)
        {
            var client = _db.Clients.Find(clientId);
            return client;
        }

        public async Task<bool> AddRefreshToken(RefreshToken token)
        {

            var existingToken = _db.RefreshTokens.Where(r => r.Subject == token.Subject && r.ClientId == token.ClientId).SingleOrDefault();
            if (existingToken != null)
            {
                var result = await RemoveRefreshToken(existingToken);
            }
            _db.RefreshTokens.Add(token);
            return await _db.SaveChangesAsync() > 0;
        }

        public async Task<bool> RemoveRefreshToken(string refreshTokenId)
        {
            var refreshToken = await _db.RefreshTokens.FindAsync(refreshTokenId);
            if (refreshToken != null)
            {
                _db.RefreshTokens.Remove(refreshToken);
                return await _db.SaveChangesAsync() > 0;
            }
            return false;
        }

        public async Task<bool> RemoveRefreshToken(RefreshToken refreshToken)
        {
            _db.RefreshTokens.Remove(refreshToken);
            return await _db.SaveChangesAsync() > 0;
        }

        public async Task<RefreshToken> FindRefreshToken(string refreshTokenId)
        {
            var refreshToken = await _db.RefreshTokens.FindAsync(refreshTokenId);
            return refreshToken;
        }

        public List<RefreshToken> GetAllRefreshTokens()
        {
            return _db.RefreshTokens.ToList();
        }

        public void Dispose()
        {
            _db.Dispose();
            _userManager.Dispose();

        }
    }
}