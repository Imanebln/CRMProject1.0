using CRMServer.Models.CRM;
using CRMServer.Models.Parameters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CRMClient.contracts {
	public interface IOpportunityService : ICRMBaseService<Opportunity> {
		IEnumerable<Opportunity> GetAllOpportunities();
		IEnumerable<Opportunity> GetOpportunitiesWhere(OpportunityParameters opportunityParameters);
		Opportunity? GetOpportunityByEmail(string email);
		Opportunity? GetOpportunityById(Guid guid);
		Opportunity? GetOpportunityByName(string name);

		Task<Opportunity?> InsertOpportunity(Opportunity opportunity);
		Task<Opportunity?> UpdateOpportunity(Opportunity opportunity);
		Task<Opportunity?> DeleteOpportunity(Opportunity opportunity);
	}
}
