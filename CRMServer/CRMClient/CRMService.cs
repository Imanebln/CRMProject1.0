using CRMClient.contracts;
using CRMClient.Impl;
using Microsoft.Extensions.Configuration;


namespace CRMClient {
	public class CRMService {
		private readonly CRMProvider _context;
		public IContactService contacts;
		public IAccountService accounts;

		public CRMService(IConfiguration configuration) {
			_context= new CRMProvider(configuration);
			contacts = new ContactService(_context);
			accounts = new AccountService(_context);
		}
	}
}
