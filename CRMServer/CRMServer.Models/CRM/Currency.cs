using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace CRMServer.Models.CRM {
	public class Currency : ICrmEntity {
		[JsonPropertyName("transactioncurrencyid")]
	    public Guid CurrencyId { get; set; }
		public string? CurrencyName { get; set; }
		public string? IsoCurrencyCode { get; set; }
		public string? CurrencySymbol { get; set; }
		public Guid GetId() {
			throw new NotImplementedException();
		}

		public string GetUnique() {
			throw new NotImplementedException();
		}

		public override string? ToString() {
			return $"Currency({CurrencyId},{CurrencyName},{IsoCurrencyCode})";
		}
	}
}
