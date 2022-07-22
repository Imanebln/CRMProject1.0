using CRMClient.contracts;
using CRMServer.Models.CRM;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Reflection;
using CRMServer.Models.Parameters;
using System.Text;

namespace CRMClient.Impl {
	public class ContactService : CRMBaseService<Contact>, IContactService {
		private readonly string BaseQuery = "/api/data/v9.1/contacts?$expand=parentcustomerid_account";
		public ContactService(CRMProvider context) : base(context){}

		public IEnumerable<Contact> GetAllContacts() {
			return GetFromCrm(BaseQuery);
		}

		public IEnumerable<Contact> GetContactsWhere(ContactParameters contact) {
			string query = GetFilterQuery(BaseQuery, contact);
			return GetFromCrm(query);
		}
		public Contact? GetContactByEmail(string email) {
			ContactParameters parameters = new() {  EmailAddress1 = email};
			string query = GetFilterQuery(BaseQuery, parameters);
			return GetFromCrm(query).FirstOrDefault();
		}
		public Contact? GetContactByPhone(string phone) {
			ContactParameters parameters = new() { MobilePhone = phone };
			string query = GetFilterQuery(BaseQuery, parameters);
			return GetFromCrm(query).FirstOrDefault();
		}

		public Contact? GetContactById(Guid guid) {
			ContactParameters parameters = new() { ContactId = guid };
			string query = GetFilterQuery(BaseQuery, parameters);
			return GetFromCrm(query).FirstOrDefault();
		}



		protected override void Populate(ref List<Contact> entities, JObject doc) {
			for (int i = 0; i < entities?.Count; i++) {
				string? account = doc.SelectToken($"value[{i}].parentcustomerid_account")?.ToString();
				if (account != null)
					entities[i].Account = JsonConvert.DeserializeObject<Account>(account);
			}
		}
	}
}
