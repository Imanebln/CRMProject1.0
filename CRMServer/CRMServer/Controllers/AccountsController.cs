using AutoMapper;
using CRMClient;
using CRMServer.DTO;
using CRMServer.Models.CRM;
using CRMServer.Models.Parameters;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace CRMServer.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountsController : ControllerBase
    {
        private readonly CRMService _crmService;
        private readonly IMapper _mapper;

        public AccountsController(CRMService crmService, IMapper mapper)
        {
            _crmService = crmService;
            _mapper = mapper;
        }

        // GET: api/Accounts
        [HttpGet]
        /*[Authorize(Roles = "Admin")]*/
        public IEnumerable<Account> GetAccounts()
        {
            return _crmService.accounts.GetAllAccounts();
        }

        // GET: api/Accounts/ById
        [HttpGet("{id}")]
        [Authorize(Roles = "Primary , User, Admin")]
        public ActionResult<Account?> GetAccountById(Guid id)
        {
            return _crmService.accounts.GetAccountById(id);
        }

        // GET: api/Accounts/ByName
        [HttpGet("GetAccountByName")]
        [Authorize(Roles = "Primary , User, Admin")]
        public ActionResult<Account?> GetAccountByName(string name)
        {
            return _crmService.accounts.GetAccountByName(name);
        }

        // GET: api/Accounts/Where
        [HttpGet("GetAccountWhere")]
        [Authorize(Roles = "Primary , User, Admin")]
        public IEnumerable<Account> GetAccountWhere(AccountParameters account)
        {
            return _crmService.accounts.GetAccountsWhere(account);
        }

        // PUT: api/Accounts
        [HttpPut("UpdateAccount")]
        /*[Authorize(Roles = "Primary")]*/
        public IActionResult UpdateAccount(AccountDTO accountdto)
        {
            Account? account = _mapper.Map<Account>(accountdto);
            if (account == null)
            {
                return BadRequest("This Account does not exist!");
            }
            _ = _crmService.accounts.UpdateAccount(account).Result;
            return Ok(new { message = "Account updated sucessfully!" });
        }

        // POST: api/Accounts
        [HttpPost]
        [Authorize(Roles = "Admin")]
        public ActionResult<Account> InsertAccount(AccountDTO accountdto)
        {
            Account? account = _mapper.Map<Account>(accountdto);
            account = _crmService.accounts.InsertAccount(account).Result;
            if (account == null)
            {
                return BadRequest("This name is taken by another account!");
            }
            return CreatedAtAction("GetAccountById", new { id = account.AccountId }, account);
        }

        // DELETE: api/Accounts
        [HttpDelete("DeleteAccount")]
        [Authorize(Roles = "Primary, Admin")]
        public IActionResult DeleteAccount(Guid id)
        {
            Account? account = _crmService.accounts.GetAccountById(id);
            if(account == null)
            {
               return NotFound("This Account does not exist!");
            }
            _ = _crmService.accounts.DeleteAccount(account).Result;

            return Ok("Account deleted successfully!");
        }

    }
}
