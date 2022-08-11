namespace CRMServer.DTO
{
    public class ContactDTO
    {
		public Guid ContactId { get; set; }
		public string? Firstname { get; set; }
		public string? Lastname { get; set; }
		public string? Birthdate { get; set; }
		public string Email { get; set; } = String.Empty;
		public string? ImageUrl { get; set; }
		public string? MobilePhone { get; set; }
		public string? Fax { get; set; }
		public string? JobTitle { get; set; }
		public bool IsPrimary { get; set; } 

	}
}
