using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CRMServer.Models.Parameters {
	public class LeadParameters {
		public Guid? LeadId { get; set; }
		public string? Fullname { get; set; }
		public string? Firstname { get; set; }
		public string? Lastname { get; set; }
		public string? EmailAddress1 { get; set; }
	}
}
