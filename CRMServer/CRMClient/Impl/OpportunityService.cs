using CRMClient.contracts;
using CRMServer.Models.CRM;
using CRMServer.Models.Parameters;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
			OpportunityParameters parameters = new(){ Email = email};
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

		protected override void Populate(ref List<Opportunity> entities, JObject doc) {
			for(int i=0; i<entities.Count; i++){
				string? contactJson = doc.SelectToken($"value[{i}].parentcontactid")?.ToString();
				string? accountJson = doc.SelectToken($"value[{i}].parentaccountid")?.ToString();
				string? currencyJson = doc.SelectToken($"value[{i}].transactioncurrencyid")?.ToString();
				if(contactJson != null)
					entities[i].Contact = JsonConvert.DeserializeObject<Contact>(contactJson);
				if (accountJson != null)
					entities[i].Account = JsonConvert.DeserializeObject<Account>(accountJson);
				if (currencyJson != null)
					entities[i].Currency = JsonConvert.DeserializeObject<Currency>(currencyJson);
			}
		}
	}
}
