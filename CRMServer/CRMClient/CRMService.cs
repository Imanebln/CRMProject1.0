using CRMClient.contracts;
using CRMClient.Impl;
using Microsoft.Extensions.Configuration;


namespace CRMClient {
	public class CRMService {
		private readonly CRMProvider _context;
		public IContactService contacts;
		public IAccountService accounts;
		public ILeadService leads;
		public IOpportunityService opportunities;
		public IAddressService address;

		public CRMService(IConfiguration configuration) {
			_context= new CRMProvider(configuration);
			contacts = new ContactService(_context);
			accounts = new AccountService(_context);
			leads = new LeadService(_context);
			opportunities = new OpportunityService(_context);
			address = new AddressService(_context);
		}
	}
}
