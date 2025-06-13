namespace Graphite.Source.Domain.Dtos
{
    public class AtRiskCohortDto
    {
        public int AcquisitionYear { get; set; }
        public decimal EvasionRate { get; set; }
        public string RiskLevel { get; set; } = string.Empty;
    }
}