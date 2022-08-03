using System.Text.Json.Serialization;


namespace CRMServer.Models.CRM {
	public class Lead : ICrmEntity {
		public Guid LeadId { get; set; }
		public string? Subject { get; set; }
		public string? Fullname { get; set; }
		public string? Firstname { get; set; }
		public string? Lastname	{ get; set; }
		[JsonPropertyName("emailaddress1")]
		public string Email { get; set; } = String.Empty;
		[JsonPropertyName("address1_composite")]
		public string? Address { get; set; }
		public string? JobTitle	{ get; set; }
		public Account? Account { get; set; }

		public Guid GetId() {
			return LeadId;
		}

		public string GetUnique() {
			return Email;
		}

		public override string? ToString() {
			return $"Lead({LeadId}, {Subject}, {Fullname}, {Email}, {JobTitle}, {Account})";
		}
	}
}
