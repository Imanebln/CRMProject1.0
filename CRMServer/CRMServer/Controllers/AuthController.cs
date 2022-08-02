using Microsoft.AspNetCore.Mvc;
using CRMServer.Models;
using Microsoft.AspNetCore.Identity;
using CRMServer.Services;
using CRMClient;
using CRMServer.Models.CRM;
using System.Security.Claims;
using CRMServer.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authorization;

namespace CRMServer.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly UserManager<AppUser> userManager;
        private readonly IAuthService _authService;
        private readonly CRMService _crm;
        private readonly CRMContext _context;

        public AuthController(UserManager<AppUser> userManager, IAuthService authService, CRMService crmService, CRMContext context)
        {
            this.userManager = userManager;
            _authService = authService;
            _crm = crmService;
            _context = context;
        }

        //GET: CRMVerification
        [HttpGet("CRMVerification")]
        public async Task<IActionResult> CRMVerification(string email)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            //Inject CRMService and check if contact exists
            Contact? contact = _crm.contacts.GetContactByEmail(email);

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
            if(!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
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
        public async Task<ActionResult<string>> ForgotPassword(string email)
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

        //GET: api/Current User
        [HttpGet("GetCurrentUser")]
        public ActionResult<Contact?> GetCurrentUser()
        {
            var userEmail = User.FindFirstValue(ClaimTypes.NameIdentifier);
            return _crm.contacts.GetContactByEmail(userEmail);
        }
        //GET: api/Get contact's account
        [HttpGet("GetContactsAccount")]
        public ActionResult<Account?> GetContactsAccount()
        {
            var userEmail = User.FindFirstValue(ClaimTypes.NameIdentifier);
            return _crm.contacts.GetContactByEmail(userEmail)?.Account;
        }
    }
}
