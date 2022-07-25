using CRMServer.Models.CRM;
using CRMServer.Models.Parameters;

namespace CRMClient.contracts {
	public interface IAccountService : ICRMBaseService<Account> {
		IEnumerable<Account> GetAllAccounts();
		IEnumerable<Account> GetAccountsWhere(AccountParameters account);
	}
}