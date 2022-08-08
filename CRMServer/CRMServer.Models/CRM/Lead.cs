using Newtonsoft.Json;


namespace CRMServer.Models.CRM {
	public class Lead : ICrmEntity {
		public Guid LeadId { get; set; }
		public string? Subject { get; set; }
		public string? Firstname { get; set; }
		public string? Lastname	{ get; set; }
		[JsonProperty(PropertyName = "emailaddress1")]
		public string Email { get; set; } = String.Empty;
		/*[JsonProperty(PropertyName = "address1_composite")]
		public string? Address { get; private set; }*/
		public string? JobTitle	{ get; set; }
		[JsonProperty(PropertyName = "parentaccountid")]
		public Account? Account { get; set; }

		public Guid GetId() {
			return LeadId;
		}

		public string GetUnique() {
			return Email;
		}

		public override string? ToString() {
			return $"Lead({LeadId}, {Subject}, {Firstname}, {Lastname}, {Email}, {JobTitle}, {Account})";
		}

		public override bool Equals(object? obj) {
			if (obj == null) return false;
			if (obj.GetType()==typeof(Lead)) {
				Lead lead = (Lead)obj;
				return lead.LeadId == LeadId;
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
