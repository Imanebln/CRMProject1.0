using CRMClient;
using CRMServer.Models.CRM;
using Xunit.Abstractions;

namespace UnitTest.CRMClient {
	public class LeadTest : CRMBaseTest<Lead> {
		public LeadTest(CRMService crmService, ITestOutputHelper output) : base(crmService, output) {
			CrudFunctions = new CrudFunctions<Lead>(
				crmService.leads.InsertLead,
				crmService.leads.GetLeadByEmail,
				crmService.leads.UpdateLead,
				crmService.leads.DeleteLead
			);
			entity = new Lead() {
				Email = "test@gmail.com",
				Firstname = "Mehdi",
				Lastname = "Elmess",
				JobTitle = "IT Engineer",
				Subject = "10 Coffee machine"
			};
		}

		protected override string? SimpleUpdate(ref Lead entityEdited) {
			entityEdited.Firstname = "Ahmed";
			return entityEdited.Firstname;
		}
		protected override string? GetUpdatetField(Lead entityEdited) {
			return entityEdited.Firstname;
		}

	}
}
