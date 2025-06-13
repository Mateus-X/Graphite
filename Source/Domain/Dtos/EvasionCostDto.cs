namespace Graphite.Source.Domain.Dtos
{
    public class EvasionCostDto : TimeAnalysisDto
    {
        public decimal FutureLostValue { get; set; }
        public decimal PercentageOfTotal { get; set; }
    }
}