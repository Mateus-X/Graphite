namespace Graphite.Source.Domain.Dtos
{
    public class EvasionImpactDto : TimeAnalysisDto
    {
        public int LostDonors { get; set; }
        public decimal LostValue { get; set; }
        public decimal ImpactPercentage { get; set; } 

    }
}
