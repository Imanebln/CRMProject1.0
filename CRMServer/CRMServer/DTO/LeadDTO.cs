using System.Text.Json.Serialization;

namespace CRMServer.DTO
{
    public class LeadDTO
    {
		public Guid LeadId { get; set; }
		public string? Subject { get; set; }
		public string? Firstname { get; set; }
		public string? Lastname { get; set; }
		public string Email { get; set; } = String.Empty;
		public string? Address { get; set; }
		public string? JobTitle { get; set; }
	}
}
