namespace Graphite.Source.Domain.Dtos
{
    public class LostDonorsDto : TimeAnalysisDto
    {
        public int Quantity { get; set; }
        public decimal EvasionPercentage { get; set; } 
    }
}
