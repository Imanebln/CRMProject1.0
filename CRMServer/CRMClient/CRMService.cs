using CRMClient.contracts;
using CRMClient.Impl;
using Microsoft.Extensions.Configuration;


namespace CRMClient {
	public class CRMService {
		private readonly CRMContext _context;
		public IContactService contacts;
		public IAccountService accounts;

		public CRMService(IConfiguration configuration) {
			_context= new CRMContext(configuration);
			contacts = new ContactService(_context);
			accounts = new AccountService(_context);
		}
	}
}
