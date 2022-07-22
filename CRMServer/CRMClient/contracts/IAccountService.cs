using CRMServer.Models.CRM;
using CRMServer.Models.Parameters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CRMClient.contracts {
	public interface IAccountService : ICRMBaseService<Account> {
		IEnumerable<Account> GetAllAccounts();
		IEnumerable<Account> GetAccountsWhere(AccountParameters account);
	}
}
