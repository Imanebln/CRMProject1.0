using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CRMServer.Models.Parameters {
	public class OpportunityParameters {
		public Guid OpportunityId { get; set; }
		public string? Name { get; set; }
		public string? Email { get; set; }
		public string? EstimatedClosedate { get; set; }
		public int? CloseProbability { get; set; }
	}
}
