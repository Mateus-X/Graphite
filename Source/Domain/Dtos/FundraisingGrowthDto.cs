namespace Graphite.Source.Domain.Dtos
{
    public class FundraisingGrowthDto : TimeAnalysisDto
    {
        public decimal TotalFundraise { get; set; }
        public decimal GrowthPercentage { get; set; } 
        public string Tendency { get; set; } 

    }
}
