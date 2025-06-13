namespace Graphite.Source.Domain.Dtos
{
    public class AverageDonatedValueDto : TimeAnalysisDto
    {
        public decimal GeneralAverage { get; set; }
        public decimal RetainedAverage { get; set; }
        public decimal AverageDifference { get; set; }
        public string? DominantTendency { get; set; }
    }
}
