using CRMClient.contracts;
using CRMServer.Models.CRM;
using CRMServer.Models.Parameters;
using NJsonSchema.Infrastructure;


namespace CRMClient.Impl {
	public class OpportunityService : CRMBaseService<Opportunity>, IOpportunityService {
		private readonly string BaseQuery = "/api/data/v9.1/opportunities?$expand=parentcontactid,parentaccountid,transactioncurrencyid";
		public OpportunityService(CRMProvider context) : base(context) {}

		public IEnumerable<Opportunity> GetAllOpportunities() {
			return GetFromCrm(BaseQuery);
		}

		public IEnumerable<Opportunity> GetOpportunitiesWhere(OpportunityParameters opportunityParameters) {
			string query = GetFilterQuery(BaseQuery, opportunityParameters);
			return GetFromCrm(query);
		}

		public Opportunity? GetOpportunityByEmail(string email) {
			OpportunityParameters parameters = new(){ EmailAddress = email};
			string query = GetFilterQuery(BaseQuery, parameters);
			return GetFromCrm(query).FirstOrDefault();
		}

		public Opportunity? GetOpportunityById(Guid guid) {
			OpportunityParameters parameters = new() { OpportunityId = guid };
			string query = GetFilterQuery(BaseQuery, parameters);
			return GetFromCrm(query).FirstOrDefault();
		}
		public Opportunity? GetOpportunityByName(string name) {
			OpportunityParameters parameters = new() { Name = name };
			string query = GetFilterQuery(BaseQuery, parameters);
			return GetFromCrm(query).FirstOrDefault();
		}

		public Task<Opportunity?> DeleteOpportunity(Opportunity opportunity) {
			return Delete(opportunity, GetOpportunityById, BaseQuery);
		}

		public Task<Opportunity?> InsertOpportunity(Opportunity opportunity) {
			return Insert(opportunity, GetOpportunityByName, BaseQuery);
		}

		public Task<Opportunity?> UpdateOpportunity(Opportunity opportunity) {
			return Update(opportunity, GetOpportunityById, BaseQuery);
		}

		protected override PropertyRenameAndIgnoreSerializerContractResolver GetJsonResolver() {
			PropertyRenameAndIgnoreSerializerContractResolver jsonResolver = new();
			jsonResolver.NamingStrategy = new LowercaseNamingStrategy();
			Type type = typeof(Opportunity);
			jsonResolver.IgnoreProperty(type, "opportunityid");
			jsonResolver.IgnoreProperty(type, "parentcontactid");
			jsonResolver.IgnoreProperty(type, "parentaccountid");
			jsonResolver.IgnoreProperty(type, "transactioncurrencyid");

			return jsonResolver;
		}
	}
}
