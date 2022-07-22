using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CRMServer.Models.Parameters {
	public class ContactParameters {
		public Guid? ContactId { get; set; }
		public string? Firstname { get; set; }
		public string? Lastname { get; set; }
		public string? EmailAddress1 { get; set; }
		public string? MobilePhone { get; set; }
	}
}
