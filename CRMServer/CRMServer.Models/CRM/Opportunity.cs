using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.Json.Serialization;

namespace CRMServer.Models.CRM {
	public class Opportunity : ICrmEntity {
		public Guid OpportunityId { get; set; }
		public string Name { get; set; } = string.Empty;
		public string? StepName { get; set; }
		public string? EstimatedClosedate { get; set; }
		public int? CloseProbability { get; set; }
		public string? CustomerNeed { get; set; }
		[JsonPropertyName("estimatedvalue_base")]
		public decimal? EstimatedValue { get; set; }
		public string? Description { get; set; }
		[JsonPropertyName("emailaddress")]
		public string? Email { get; set; }
		public string? ProposedSolution { get; set; }
		public string? CurrentSituation	{ get; set; }	
		[JsonPropertyName("totallineitemamount_base")]
		public decimal TotalAmount { get; set; }
		public string? CreatedOn { get; set; }
		public Contact? Contact { get; set; }
		public Account? Account { get; set; }
		public Currency? Currency { get; set; }



		public Guid GetId() {
			return OpportunityId;
		}

		public string GetUnique() {
			return Name;
		}

		public override string? ToString() {
			return $"Opportunity({OpportunityId},{Name},{CustomerNeed},{CurrentSituation},\n\tContact({Contact}),\n\tAccount({Account}),\n\tCurrency({Currency})\n)";
		}
	}
}
