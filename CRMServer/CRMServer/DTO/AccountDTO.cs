namespace CRMServer.DTO
{
    public class AccountDTO
    {
        public Guid AccountId { get; set; }
        public string Name { get; set; } = string.Empty;
        public string? WebsiteUrl { get; set; }
        public string? Description { get; set; }
        public string? Fax { get; set; }
    }
}
