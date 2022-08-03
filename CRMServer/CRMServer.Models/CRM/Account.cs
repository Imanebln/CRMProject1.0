using System.Text.Json.Serialization;

namespace CRMServer.Models.CRM {
	public class Account : ICrmEntity {
		public Guid AccountId { get; set; }
		public string Name { get; set; } = string.Empty;
		public string? WebsiteUrl { get; set; }
		public string? Description { get; set; }
		public string? Fax { get; set; }
		[JsonPropertyName("_primarycontactid_value")]
		public string? PrimaryContactId { get; set; }
		public Contact? PrimaryContact { get; set; }
		public IList<Contact>? Contacts { get; set;} = new List<Contact>();

		public Guid GetId() {
			return AccountId;
		}

		public string GetUnique() {
			return Name;
		}

		public override string ToString() {
			return $"Account({AccountId}, {Name}, {WebsiteUrl}, {Description?[..10] + "..."}, {Fax})";
		}

	}
}
