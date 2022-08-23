using Newtonsoft.Json;


namespace CRMServer.Models.CRM {
	public class Account : ICrmEntity {
		public Guid AccountId { get; set; }
		public string Name { get; set; } = string.Empty;
		public string? TickerSymbol { get; set; }
		public string? WebsiteUrl { get; set; }
		[JsonProperty(PropertyName = "address1_composite")]
		public string? Address { get; set; }
		public string? Description { get; set; }
		public string? Fax { get; set; }
		public long? CreditLimit { get; set; }
		public long? Revenue { get; set; }
		[JsonProperty(PropertyName = "entityimage")]
		public string? ImageUrl { get; set; }
		[JsonProperty(PropertyName = "_primarycontactid_value")]
		public string? PrimaryContactId { get; set; }
		[JsonProperty(PropertyName = "primarycontactid")]
		public Contact? PrimaryContact { get; set; }
		[JsonProperty(PropertyName = "contact_customer_accounts")]
		public IList<Contact>? Contacts { get; set;} = new List<Contact>();

		public Guid GetId() {
			return AccountId;
		}

		public string GetUnique() {
			return Name;
		}

		public override string ToString() {
			return $"Account({AccountId}, {Name}, {WebsiteUrl}, {Description?[..10] + "..."}, {Fax}, {Contacts?.Count})"; 
		}

		public override bool Equals(object? obj) {
			if (obj == null) return false;
			if (obj.GetType()==typeof(Account)) {
				Account account = (Account)obj;
				return account.AccountId == AccountId;
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
