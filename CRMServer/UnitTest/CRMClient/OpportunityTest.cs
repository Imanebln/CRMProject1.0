using CRMClient;
using CRMServer.Models.CRM;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit.Abstractions;

namespace UnitTest.CRMClient {
	public class OpportunityTest : CRMBaseTest<Opportunity> {
		public OpportunityTest(CRMService crmService, ITestOutputHelper output) : base(crmService, output) {
			CrudFunctions = new CrudFunctions<Opportunity>(
				crmService.opportunities.InsertOpportunity,
				crmService.opportunities.GetOpportunityByName,
				crmService.opportunities.UpdateOpportunity,
				crmService.opportunities.DeleteOpportunity
			);
			entity = new Opportunity() {
				CreatedOn = "2022-08-03",
				CurrentSituation = "It s not good out there",
				Description = "This is a huge description",
				Email = "test@gmail.com",
				Name = "10 Coffe machine",
				ProposedSolution = "Proposed Solution"
			};
		}

		protected override string? SimpleUpdate(ref Opportunity entityEdited) {
			entityEdited.ProposedSolution = "Better Solution";
			return entityEdited.ProposedSolution;
		}

		protected override string? GetUpdatetField(Opportunity entityEdited) {
			return entityEdited.ProposedSolution;
		}
	}
}
