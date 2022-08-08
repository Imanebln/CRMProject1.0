

using Newtonsoft.Json;

namespace CRMServer.Models.CRM {
	public class Address : ICrmEntity {
		[JsonProperty(PropertyName = "customeraddressid")]
		public Guid AddressId { get; set; }
		[JsonProperty]
		public string? Composite { get; private set; }
		public string? StateOrProvince { get; set; }
		public string? Country { get; set; }
		public string? City { get; set; }
		public string? Line1 { get; set; }
		public string? Line2 { get; set; }

		public override string? ToString() {
			return $"Address({Line1},{Line2},{Country},{StateOrProvince}, {City})";
		}

		public Guid GetId() {
			return AddressId;
		}

		public string GetUnique() {
			return AddressId.ToString();
		}
	}
}
