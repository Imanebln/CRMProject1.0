using CRMServer.Models.CRM;
using CRMServer.Models.Parameters;

namespace CRMClient.contracts {
	public interface ILeadService : ICRMBaseService<Lead> {
		IEnumerable<Lead> GetAllLeads();
		IEnumerable<Lead> GetLeadsWhere(LeadParameters lead);
		Lead? GetLeadById(Guid id);
		Lead? GetLeadByEmail(string name);
		Task<Lead?> InsertLead(Lead lead);
		Task<Lead?> UpdateLead(Lead lead);
		Task<Lead?> DeleteLead(Lead lead);
	}
}
