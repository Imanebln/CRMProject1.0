using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CRMServer.Models.Parameters {
	public class AccountParameters {
		public Guid? AccountId { get; set; }
		public string? Name { get; set; }
		public string? Fax { get; set; }
	}
}
