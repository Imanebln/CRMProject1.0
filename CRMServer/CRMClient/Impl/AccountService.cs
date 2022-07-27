using CRMClient.contracts;
using CRMServer.Models.CRM;
using CRMServer.Models.Parameters;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Reflection;
using System.Text;

namespace CRMClient.Impl {
	public class AccountService : CRMBaseService<Account>, IAccountService {
		private readonly string BaseQuery = "/api/data/v9.1/accounts?$expand=primarycontactid,contact_customer_accounts";
		public AccountService(CRMProvider context) : base(context) {}

		public IEnumerable<Account> GetAllAccounts() {
			return this.GetFromCrm(BaseQuery);
		}

		public Account? GetAccountByName(string name){
			AccountParameters parameters = new() { Name = name};
			string filteredQuery = GetFilterQuery(BaseQuery, parameters);
			return GetFromCrm(filteredQuery).FirstOrDefault();
		}

		public Account? GetAccountById(Guid id) {
			AccountParameters parameters = new() { AccountId =  id};
			string filteredQuery = GetFilterQuery(BaseQuery, parameters);
			return GetFromCrm(filteredQuery).FirstOrDefault();
		}

		public IEnumerable<Account> GetAccountsWhere(AccountParameters account) {
			string query = GetFilterQuery(BaseQuery, account);
			return GetFromCrm(query);
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


		protected override void Populate(ref List<Account> entities, JObject doc) {
		    for(int i=0; i<entities.Count; i++){
				string? JsonPrimaryContact = doc.SelectToken($"value[{i}].primarycontactid")?.ToString();
				string? JsonContacts = doc.SelectToken($"value[{i}].contact_customer_accounts")?.ToString();
				if (JsonPrimaryContact != null)
					entities[i].PrimaryContact = JsonConvert.DeserializeObject<Contact>(JsonPrimaryContact);
				if (JsonContacts != null)
					entities[i].Contacts = JsonConvert.DeserializeObject<List<Contact>>(JsonContacts);
			}
		}
	}
}
