namespace Graphite.Source.Domain.Dtos
{
    public class RetentionImprovementImpactDto
    {
        public decimal CurrentRate { get; set; }
        public decimal ProjectedRate { get; set; }
        public decimal IncrementalValue { get; set; }
        public int AdditionalDonors { get; set; }
    }
}