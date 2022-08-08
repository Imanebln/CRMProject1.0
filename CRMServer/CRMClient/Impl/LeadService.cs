using CRMClient.contracts;
using CRMServer.Models.CRM;
using CRMServer.Models.Parameters;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using NJsonSchema.Infrastructure;

namespace CRMClient.Impl {
	public class LeadService : CRMBaseService<Lead>, ILeadService {
		private readonly string BaseQuery = "/api/data/v9.1/leads?$expand=parentaccountid";

		public LeadService(CRMProvider context) : base(context) {}
		public IEnumerable<Lead> GetAllLeads() {
			return GetFromCrm(BaseQuery);
		}

		public IEnumerable<Lead> GetLeadsWhere(LeadParameters lead) {
			string query = GetFilterQuery(BaseQuery, lead);
			return GetFromCrm(query);
		}

		public Lead? GetLeadByEmail(string email) {
			LeadParameters parameters = new() { EmailAddress1 = email};
			string query = GetFilterQuery(BaseQuery, parameters);
			return GetFromCrm(query).FirstOrDefault();
		}

		public Lead? GetLeadById(Guid id) {
			LeadParameters parameters = new() { LeadId = id };
			string query = GetFilterQuery(BaseQuery, parameters);
			return GetFromCrm(query).FirstOrDefault();
		}

		public Task<Lead?> DeleteLead(Lead lead) {
			return Delete(lead, GetLeadById, BaseQuery);
		}

		public Task<Lead?> InsertLead(Lead lead) {
			return Insert(lead, GetLeadByEmail, BaseQuery);
		}

		public Task<Lead?> UpdateLead(Lead lead) {
			return Update(lead, GetLeadById, BaseQuery);
		}

		protected override PropertyRenameAndIgnoreSerializerContractResolver GetJsonResolver() {
			PropertyRenameAndIgnoreSerializerContractResolver jsonResolver = new();
			jsonResolver.NamingStrategy = new LowercaseNamingStrategy();
			Type type = typeof(Lead);
			jsonResolver.IgnoreProperty(type, "leadid");
			return jsonResolver;
		}
	}
}
