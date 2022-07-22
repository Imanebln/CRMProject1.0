namespace CRMServer.Models.CRM {
	public class Account : ICrmEntity {
		public Guid AccountId { get; set; }
		public string? Name { get; set; }
		public string? WebsiteUrl { get; set; }
		public string? Description { get; set; }
		public string? Fax { get; set; }
		public string? _primarycontactid_value { get; set; }
		public Contact? PrimaryContact { get; set; }
		public IEnumerable<Contact>? Contacts { get; set;}


		public override string ToString() {
			return $"Account({AccountId}, {Name}, {WebsiteUrl}, {Description?[..10] + "..."}, {Fax})";
		}

	}
}
