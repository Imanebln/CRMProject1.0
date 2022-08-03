namespace CRMServer.DTO
{
    public class OpportunityDTO
    {
		public Guid OpportunityId { get; set; }
		public string Name { get; set; } = string.Empty;
		public string? StepName { get; set; }
		public string? EstimatedClosedate { get; set; }
		public int? CloseProbability { get; set; }
		public string? CustomerNeed { get; set; }
		public decimal? EstimatedValue { get; set; }
		public string? Description { get; set; }
		public string? Email { get; set; }
		public string? ProposedSolution { get; set; }
		public string? CurrentSituation { get; set; }
		public decimal? TotalAmount { get; set; }
	}
}
