﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using CRMServer.Data;
using CRMServer.Models;
using Microsoft.AspNetCore.Identity;
using CRMServer.Services;
using System.Web;
using CRMClient;
using CRMServer.Models.CRM;

namespace CRMServer.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly CRMContext _context;
        private readonly UserManager<AppUser> userManager;
        private readonly RoleManager<IdentityRole> roleManager;
        private readonly IConfiguration _configuration;
        private readonly IAuthService _authService;
        private readonly CRMService _crm;

        public AuthController(CRMContext context, UserManager<AppUser> userManager, RoleManager<IdentityRole> roleManager, IConfiguration configuration, IAuthService authService, CRMService crmService)
        {
            _context = context;
            this.userManager = userManager;
            this.roleManager = roleManager;
            _configuration = configuration;
            _authService = authService;
            _crm = crmService;
        }

        //GET: CRMVerification
        [HttpGet("CRMVerification")]
        public async Task<IActionResult> CRMVerification(string email)
        {
            //Inject CRMService and check if contact exists
            Contact contact = _crm.contacts.GetContactByEmail(email);

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            if (contact == null)
            {
                return BadRequest("Contact not found");
            }
            else
            {
                var result = await _authService.RegisterAsync(email);
                if (result.Token is null) { 
                    return BadRequest(result.Message);
                }
                return Ok(result);
            }
            return Ok();
        }

        //GET: Login
        [HttpPost("SignIn")]
        public async Task<IActionResult> Login([FromBody] LoginModel model)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            var result = await _authService.GetTokenAsync(model);

            if (!result.IsAuthenticated)
                return NotFound(result.Message);

            return Ok(result);
        }

        [HttpPost("SignUp")]
        public async Task<IActionResult> Register(RegisterModel model)
        {
            var user = await userManager.FindByEmailAsync(model.Email);

            if (user == null)
            {
                return BadRequest(new { str = "User not found" });
            }

            bool valid = userManager.GetSecurityStampAsync(user).Result == model.Token;
            if (!valid) return BadRequest(new { str = "Invalid token!" });

            var result1 = await userManager.ConfirmEmailAsync(user, userManager.GenerateEmailConfirmationTokenAsync(user).Result);
            if (result1.Succeeded)
            {
                var result = await _authService.ResetPasswordAsync(model.Email, model.Password);

                if (result == "Succeeded")
                {
                    return Ok(new { str = "Succeeded" });
                }
                else
                {
                    return BadRequest(new { str = "Failed" });
                }
            }
            else
            {
                return Ok(new { str = "Email not confirmed" });
            }

           

        }

        // API Validation Email
        [HttpGet("ValidationEmail")]
        public async Task<IActionResult> ValidationEmail(string email, string token)
        {
            var user = await userManager.FindByEmailAsync(email);

            if (user == null)
            {
                return BadRequest(new { str = "User not found" });
            }
            bool valid = userManager.GetSecurityStampAsync(user).Result == token;
            if (!valid) return BadRequest(new { str = "Invalid token!" });

            await userManager.ConfirmEmailAsync(user, userManager.GenerateEmailConfirmationTokenAsync(user).Result);
            return Ok(new { str = "Succeeded" });

        }

        // POST: api/RecoverPassword
        [HttpPost("RecoverPassword")]
        public async Task<ActionResult<string>> ForgotPassword([FromBody] string email)
        {
            var user = await userManager.FindByEmailAsync(email);
            if (user == null)
            {
                return BadRequest(new { str = "User not found" });
            }
            var result = await _authService.ForgotPasswordAsync(email);
            if (result == "Succeeded")
            {
                return Ok(new { str = "Succeeded" });
            }
            return BadRequest(new { str = "Failed" });
        }
        // POST: api/ResetPassword
        [HttpPost("ResetPassword")]
        public async Task<ActionResult<string>> ResetPassword(string email,string NewPassword)
        {
            var result = await _authService.ResetPasswordAsync(email, NewPassword);
            if (result == "Succeeded")
            {
                return Ok(new { str = "Succeeded" });
            }
            else if (result == "Failed")
            {
                return BadRequest(new { str = "Failed" });

            }
            return BadRequest(new { str = "user not found" });
        }
    }
}
