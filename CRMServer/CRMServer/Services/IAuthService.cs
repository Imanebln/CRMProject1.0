using CRMServer.Models;
using System.IdentityModel.Tokens.Jwt;

namespace CRMServer.Services
{
    public interface IAuthService
    {
        public Task<AuthModel> GetTokenAsync(LoginModel model);
        public Task<JwtSecurityToken> CreateJwtToken(AppUser user);
        public Task<AuthModel> RegisterAsync(RegisterModel model);
        public Task<string> ValidationEmail(AppUser user);
    }
}
