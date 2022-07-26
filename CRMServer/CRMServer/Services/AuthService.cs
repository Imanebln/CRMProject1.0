﻿using CRMClient;
using CRMServer.Data;
using CRMServer.Models;
using CRMServer.Models.CRM;
using EmailService;
using Microsoft.AspNetCore.Identity;
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
        private readonly IPrettyEmail _emailSender;
        private readonly CRMService _crmService;

        public AuthService(UserManager<AppUser> userManager, RoleManager<IdentityRole> roleManager, CRMContext context, IConfiguration configuration, IPrettyEmail emailSender,CRMService crmService)
        {
            _userManager = userManager;
            _roleManager = roleManager;
            _context = context;
            _configuration = configuration;
            _emailSender = emailSender;
            _crmService = crmService;
        }

        //Get Token
        public async Task<AuthModel> GetTokenAsync(LoginModel model)
        {
            var authModel = new AuthModel();

            var user = await _userManager.FindByEmailAsync(model.Email);
            
            if (user is null || !await _userManager.CheckPasswordAsync(user, model.Password))
            {
                authModel.Message = "Email or Password is incorrect!";
                return authModel;
            }
            if (!await _userManager.IsEmailConfirmedAsync(user))
            {
                authModel.Message = "Email not confirmed yet, please confirm your email!";
                await ValidationEmail(user);
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
        public async Task<AuthModel> RegisterAsync(string email)
        {
            if (await _userManager.FindByEmailAsync(email) is not null)
                return new AuthModel { Message = "Email is already registered!" };

            Contact? c = _crmService.contacts.GetContactByEmail(email);
            
            var user = new AppUser
            {
                UserName = email,
                Email = email,
                FullName = c.Firstname+" "+c.Lastname,
            };

            var Pass = "CRMContact@2022";

            var result = await _userManager.CreateAsync(user, Pass);

            if (!result.Succeeded)
            {
                var errors = string.Empty;
                foreach (var error in result.Errors)
                    errors += $"{error.Description},";

                return new AuthModel { Message = errors };
            }
            var role = "User";
            if (c.IsPrimary)
            {
                role = "Primary";
                if (!await _roleManager.RoleExistsAsync(UserRoles.PrimaryUser))
                    await _roleManager.CreateAsync(new IdentityRole(UserRoles.PrimaryUser));
                if (await _roleManager.RoleExistsAsync(UserRoles.PrimaryUser))
                {
                    await _userManager.AddToRoleAsync(user, UserRoles.PrimaryUser);
                }
            }
            else
            {
                if (!await _roleManager.RoleExistsAsync(UserRoles.User))
                    await _roleManager.CreateAsync(new IdentityRole(UserRoles.User));
                if (await _roleManager.RoleExistsAsync(UserRoles.User))
                {
                    await _userManager.AddToRoleAsync(user, UserRoles.User);
                }
            }

            var jwtSecurityToken = await CreateJwtToken(user);
            await ValidationEmail(user);

            await _context.SaveChangesAsync();
            
            return new AuthModel
            {
                Email = user.Email,
                ExpiresOn = jwtSecurityToken.ValidTo,
                IsAuthenticated = false,
                Roles = new List<string> { role },
                Token = new JwtSecurityTokenHandler().WriteToken(jwtSecurityToken)
            };
        }

        // Send email to confirm CRM
        public async Task<string> ValidationEmail(AppUser user)
        {
            // Verify user token
            var token = HttpUtility.UrlEncode(await _userManager.GetSecurityStampAsync(user));

            // API Emailvalidation
            var confirmationlink = "http://localhost:4200/newpassword?token=" + token + "&email=" + user.Email;
            _emailSender.SendRegister(user.Email, confirmationlink);
            
            return "Success";
        }

        // API Validation Email
        public async Task<string> ValidationEmail(string email, string token)
        {
            //Verify user exists!
            var user = await _userManager.FindByEmailAsync(email);
            if (user == null)
            {
                return "User not found";
            }
            
            var result = await _userManager.ConfirmEmailAsync(user, token);
            if (!result.Succeeded)
            {
                return "Failed" ;
            }
            return "Succeeded";
        }

        //Forgot password
        public async Task<string> ForgotPasswordAsync(string email)
        {
            //Verify user exists!
            var user = await _userManager.FindByEmailAsync(email);
            if (user is not null)
            {
                // Verify user token
                var token = HttpUtility.UrlEncode(await _userManager.GetSecurityStampAsync(user));

                var confirmationlink = "http://localhost:4200/newpassword?token=" + token + "&email=" + user.Email;
                _emailSender.SendPasswordReset(user.Email, confirmationlink);
                return "Succeeded";
            }
            return "Failed";
        }

        //Reset Password
         public async Task<string> ResetPasswordAsync(string email, string password)
        {
            var user = await _userManager.FindByEmailAsync(email);
            var ptoken = await _userManager.GeneratePasswordResetTokenAsync(user);
            if (user == null)
            {
                return "User not found";
            }
            //Reset password to new password
            var result = await _userManager.ResetPasswordAsync(user, ptoken, password);
            if (result.Succeeded)
            {
                return "Succeeded";
            }
            return "Failed";
        }


    }
}
