using CRMServer.Data;
using CRMServer.Models;
using CRMServer.Models;
using EmailService;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.WebUtilities;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Web;

namespace CRMServer.Services
{
    public class AuthService: IAuthService
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly CRMContext _context;
        private readonly IConfiguration _configuration;
        private readonly IEmailSender _emailSender;

        public AuthService(UserManager<AppUser> userManager, RoleManager<IdentityRole> roleManager, CRMContext context, IConfiguration configuration, IEmailSender emailSender)
        {
            _userManager = userManager;
            _roleManager = roleManager;
            _context = context;
            _configuration = configuration;
            _emailSender = emailSender;
        }

        //Get Token
        public async Task<AuthModel> GetTokenAsync(LoginModel model)
        {
            var authModel = new AuthModel();

            var user = await _userManager.FindByEmailAsync(model.Email);

            if (!await _userManager.IsEmailConfirmedAsync(user))
            {
                authModel.Message = "Email not confirmed yet, please confirm your email!";
                await ValidationEmail(user);
                return authModel;

            }
            if (user is null || !await _userManager.CheckPasswordAsync(user, model.Password))
            {
                authModel.Message = "Email or Password is incorrect!";
                return authModel;
            }
            var jwtSecurityToken = await CreateJwtToken(user);
            var rolesList = await _userManager.GetRolesAsync(user);

            authModel.IsAuthenticated = true;
            authModel.Token = new JwtSecurityTokenHandler().WriteToken(jwtSecurityToken);
            authModel.Email = user.Email;
            authModel.ExpiresOn = jwtSecurityToken.ValidTo;
            authModel.Roles = rolesList.ToList();

            return authModel;
        }

        //Create JWT Token
        public async Task<JwtSecurityToken> CreateJwtToken(AppUser user)
        {
            var userClaims = await _userManager.GetClaimsAsync(user);
            var roles = await _userManager.GetRolesAsync(user);
            var roleClaims = new List<Claim>();

            foreach (var role in roles)
                roleClaims.Add(new Claim("roles", role));

            var claims = new[]
            {
                new Claim(JwtRegisteredClaimNames.Sub, user.UserName),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                new Claim(JwtRegisteredClaimNames.Email, user.Email),
                new Claim("uid", user.Id)
            }
            .Union(userClaims)
            .Union(roleClaims);

            var symmetricSecurityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["JWT:Secret"]));
            var signingCredentials = new SigningCredentials(symmetricSecurityKey, SecurityAlgorithms.HmacSha256);

            var jwtSecurityToken = new JwtSecurityToken(
                issuer: _configuration["JWT:ValidIssuer"],
                audience: _configuration["JWT:ValidAudience"],
                claims: claims,
                expires: DateTime.Now.AddHours(3),
                signingCredentials: signingCredentials);

            return jwtSecurityToken;
        }

        //Register 
        public async Task<AuthModel> RegisterAsync(RegisterModel model)
        {
            if (await _userManager.FindByEmailAsync(model.Email) is not null)
                return new AuthModel { Message = "Email is already registered!" };

            var user = new AppUser
            {
                UserName = model.Email,
                Email = model.Email,
                FullName = "heheheh"
            };
            var result = await _userManager.CreateAsync(user, model.Password);

            if (!result.Succeeded)
            {
                var errors = string.Empty;
                foreach (var error in result.Errors)
                    errors += $"{error.Description},";

                return new AuthModel { Message = errors };
            }

            if (!await _roleManager.RoleExistsAsync(UserRoles.User))
                await _roleManager.CreateAsync(new IdentityRole(UserRoles.User));
            if (await _roleManager.RoleExistsAsync(UserRoles.User))
            {
                await _userManager.AddToRoleAsync(user, UserRoles.User);
            }

            var jwtSecurityToken = await CreateJwtToken(user);
            await ValidationEmail(user);
            
            return new AuthModel
            {
                Email = user.Email,
                ExpiresOn = jwtSecurityToken.ValidTo,
                IsAuthenticated = false,
                Roles = new List<string> { "User" },
                Token = new JwtSecurityTokenHandler().WriteToken(jwtSecurityToken)
            };
        }

        // Send email to confirm CRM
        public async Task<string> ValidationEmail(AppUser user)
        {
            var ctoken = HttpUtility.UrlEncode(await _userManager.GenerateEmailConfirmationTokenAsync(user));

            // API Emailvalidation
            var confirmationlink = "https://localhost:7270/api/Auth/ValidationEmail?token=" + ctoken + "&email=" + user.Email;

            var message = new Email();
            message.To = user.Email;
            message.Subject = "CRM Email Confirmation";
            message.Content = "Please confirm your email!";
            message.Link = confirmationlink;

            await _emailSender.SendEmailAsync(message);

            return "Success";
        }


    }
}
