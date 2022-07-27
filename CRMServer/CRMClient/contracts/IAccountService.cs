using CRMServer.Models.CRM;
using CRMServer.Models.Parameters;

namespace CRMClient.contracts {
	public interface IAccountService : ICRMBaseService<Account> {
		IEnumerable<Account> GetAllAccounts();
		IEnumerable<Account> GetAccountsWhere(AccountParameters account);
		Account? GetAccountById(Guid id);
		Account? GetAccountByName(string name);
		Task<Account?> InsertAccount(Account account);
		Task<Account?> UpdateAccount(Account account);
		Task<Account?> DeleteAccount(Account account);
	}
}