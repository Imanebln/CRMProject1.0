using CRMServer.Models.CRM;
using CRMServer.Models.Parameters;

namespace CRMClient.contracts {
	public interface IContactService : ICRMBaseService<Contact> {
		IEnumerable<Contact> GetAllContacts();
		IEnumerable<Contact> GetContactsWhere(ContactParameters contact);
		Contact? GetContactByEmail(string email);
		Contact? GetContactByPhone(string phone);
		Contact? GetContactById(Guid guid);
	}
}
