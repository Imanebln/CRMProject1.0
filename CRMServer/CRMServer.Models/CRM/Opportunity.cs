using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.Json.Serialization;
using Newtonsoft.Json;

namespace CRMServer.Models.CRM {
	public class Opportunity : ICrmEntity {
		public Guid OpportunityId { get; set; }
		public string Name { get; set; } = string.Empty;
		public string? StepName { get; set; }
		public string? EstimatedClosedate { get; set; }
		public int? CloseProbability { get; set; }
		public string? CustomerNeed { get; set; }
		[JsonProperty(PropertyName ="estimatedvalue_base")]
		public decimal? EstimatedValue { get; set; }
		public string? Description { get; set; }
		[JsonProperty(PropertyName = "emailaddress")]
		public string? Email { get; set; }
		public string? ProposedSolution { get; set; }
		public string? CurrentSituation	{ get; set; }	
		[JsonProperty(PropertyName = "totallineitemamount_base")]
		public decimal TotalAmount { get; set; }
		public string? CreatedOn { get; set; }
		[JsonProperty(PropertyName = "parentcontactid")]
		public Contact? Contact { get; set; }
		[JsonProperty(PropertyName = "parentaccountid")]
		public Account? Account { get; set; }
		[JsonProperty(PropertyName = "transactioncurrencyid")]
		public Currency? Currency { get; set; }



		public Guid GetId() {
			return OpportunityId;
		}

		public string GetUnique() {
			return Name;
		}

		public override string? ToString() {
			return $"Opportunity({Email},{Name},{CustomerNeed},{CurrentSituation},\n\tContact({Contact}),\n\tAccount({Account}),\n\tCurrency({Currency})\n)";
		}

		public override bool Equals(object? obj) {
			if (obj == null) return false;
			if (obj.GetType()==typeof(Opportunity)) {
				Opportunity opportunity = (Opportunity)obj;
				return opportunity.OpportunityId == OpportunityId;
			}
			else {
				return false;
			}
		}

		public override int GetHashCode() {
			return base.GetHashCode();
		}
	}
}
