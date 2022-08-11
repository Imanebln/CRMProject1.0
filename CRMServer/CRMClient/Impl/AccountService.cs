using CRMClient.contracts;
using CRMServer.Models.CRM;
using CRMServer.Models.Parameters;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using NJsonSchema.Infrastructure;
using System.Reflection;
using System.Text;

namespace CRMClient.Impl {
	public class AccountService : CRMBaseService<Account>, IAccountService {
		public readonly string BaseQuery = "/api/data/v9.1/accounts?$expand=primarycontactid,contact_customer_accounts";
		public AccountService(CRMProvider context) : base(context) {
		}

		public IEnumerable<Account> GetAllAccounts() {
			return IsPrimaryPopulator(GetFromCrm(BaseQuery));
		}
		public IEnumerable<Account> GetAccountsWhere(AccountParameters account) {
			string query = GetFilterQuery(BaseQuery, account);
			return IsPrimaryPopulator(GetFromCrm(query));
		}

		public Account? GetAccountByName(string name){
			AccountParameters parameters = new() { Name = name};
			string filteredQuery = GetFilterQuery(BaseQuery, parameters);
			return IsPrimaryPopulator(GetFromCrm(filteredQuery)).FirstOrDefault();
		}

		public Account? GetAccountById(Guid id) {
			AccountParameters parameters = new() { AccountId =  id};
			string filteredQuery = GetFilterQuery(BaseQuery, parameters);
			return IsPrimaryPopulator(GetFromCrm(filteredQuery)).FirstOrDefault();
		}

		public async Task<Account?> InsertAccount(Account account) {
			return await Insert(account, GetAccountByName, BaseQuery);
		}

		public async Task<Account?> UpdateAccount(Account account) {
			return await Update(account, GetAccountById, BaseQuery);
		}

		public async Task<Account?> DeleteAccount(Account account) {
			return await Delete(account, GetAccountById, BaseQuery);
		}

		protected override PropertyRenameAndIgnoreSerializerContractResolver GetJsonResolver() {
			PropertyRenameAndIgnoreSerializerContractResolver jsonResolver = new();
			Type type = typeof(Account);

			jsonResolver.NamingStrategy = new LowercaseNamingStrategy();
			jsonResolver.IgnoreProperty(type, "accountid");
			jsonResolver.IgnoreProperty(type, "_primarycontactid_value");
			jsonResolver.IgnoreProperty(type, "primarycontactid");
			jsonResolver.IgnoreProperty(type, "contact_customer_accounts");
			jsonResolver.IgnoreProperty(typeof(Contact), "contactid");
			jsonResolver.IgnoreProperty(typeof(Contact), "parentcustomerid_account");
			jsonResolver.IgnoreProperty(typeof(Contact), "isprimary");

			return jsonResolver;
		}

		private IEnumerable<Account> IsPrimaryPopulator(IEnumerable<Account> accounts) {
			accounts.ToList().ForEach(account => {
				Guid? guid = account?.PrimaryContact?.ContactId;
				account?.Contacts?.ToList().ForEach(contact => {
					contact.IsPrimary = guid == contact.ContactId;
				});
			});
			return accounts;
		}
	}
}
