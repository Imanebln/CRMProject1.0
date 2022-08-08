namespace CRMServer.DTO
{
    public class ContactDTO
    {
		public string? Firstname { get; set; }
		public string? Lastname { get; set; }
		public string? Birthdate { get; set; }
		public string Email { get; set; } = String.Empty;
		public string? MobilePhone { get; set; }
		public string? Fax { get; set; }
		public string? JobTitle { get; set; }
	}
}
