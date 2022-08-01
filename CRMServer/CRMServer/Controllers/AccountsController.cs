using AutoMapper;
using CRMClient;
using CRMServer.DTO;
using CRMServer.Models.CRM;
using CRMServer.Models.Parameters;
using Microsoft.AspNetCore.Mvc;

namespace CRMServer.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountsController : ControllerBase
    {
        private readonly CRMService _crmService;
        private readonly IMapper _mapper;

        public AccountsController(CRMService crmService,IMapper mapper)
        {
            _crmService = crmService;
            _mapper = mapper;
        }

        // GET: api/Accounts
        [HttpGet]
        public IEnumerable<Account> GetAccounts()
        {
            return _crmService.accounts.GetAllAccounts();
        }

        // GET: api/Accounts/ById
        [HttpGet("{id}")]
        public ActionResult<Account?> GetAccountById(Guid id)
        {
            return _crmService.accounts.GetAccountById(id);
        }

        // GET: api/Accounts/ByEmail
        [HttpGet("GetAccountByName")]
        public ActionResult<Account?> GetAccountByName(string name)
        {
            return _crmService.accounts.GetAccountByName(name);
        }

        // GET: api/Accounts/ByPhone
        [HttpGet("GetAccountWhere")]
        public IEnumerable<Account> GetAccountWhere(AccountParameters account)
        {
            return _crmService.accounts.GetAccountsWhere(account);
        }

        // PUT: api/Accounts
        [HttpPut("UpdateAccount")]
        public IActionResult UpdateAccount(AccountDTO accountdto)
        {
            Account? a = _crmService.accounts.GetAccountByName(accountdto.Name);
            Account? account = _mapper.Map<Account>(accountdto);
            account.Name = a.Name;
            account.AccountId = a.AccountId;
            _ = _crmService.accounts.UpdateAccount(account).Result;
            if (a == null)
            {
                return BadRequest("This Account does not exist!");
            }
            return Ok("Account updated successfully!");   
        }

        // POST: api/Accounts
        [HttpPost]
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
        public IActionResult DeleteAccount(Guid id)
        {
            Account? account = _crmService.accounts.GetAccountById(id);
            if(account == null)
            {
               return BadRequest("This Account does not exist!");
            }
            _ = _crmService.accounts.DeleteAccount(account).Result;

            return Ok("Account deleted successfully!");
        }
    }
}
