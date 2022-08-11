using CRMClient.contracts;
using CRMServer.Models.CRM;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using CRMServer.Models.Parameters;
using System.Text;
using NJsonSchema.Infrastructure;
using System.Net;

namespace CRMClient.Impl {
	public class ContactService : CRMBaseService<Contact>, IContactService {
		private readonly string BaseQuery = "/api/data/v9.1/contacts?$expand=parentcustomerid_account,Contact_CustomerAddress";
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
		public async Task<Contact?> InsertContact(Contact contact) {
			return await Insert(contact, GetContactByEmail, BaseQuery);
		}

		public async Task<Contact?> UpdateContact(Contact contact) {
			return await Update(contact, GetContactById, BaseQuery);
		}

		public async Task<Contact?> DeleteContact(Contact contact) {
			return await Delete(contact, GetContactById, BaseQuery);
		}

		public async Task<byte[]?> ResolveImage(string link){
			HttpResponseMessage response = await _client.GetAsync(link);
			if (response.IsSuccessStatusCode) {
				byte[] bytes = await response.Content.ReadAsByteArrayAsync();
				return bytes;
			}else{
				return null;
			}

		}

		protected override PropertyRenameAndIgnoreSerializerContractResolver GetJsonResolver() {
			PropertyRenameAndIgnoreSerializerContractResolver jsonResolver = new();
			Type type = typeof(Contact);
			jsonResolver.NamingStrategy = new LowercaseNamingStrategy();
			jsonResolver.IgnoreProperty(type, "contactid");
			jsonResolver.IgnoreProperty(type, "isprimary");
			jsonResolver.IgnoreProperty(type, "parentcustomerid_account");
			jsonResolver.IgnoreProperty(type, "Contact_CustomerAddress");

			return jsonResolver;
			
		}
	}
}
