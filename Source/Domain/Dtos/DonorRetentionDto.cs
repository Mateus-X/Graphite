namespace Graphite.Source.Domain.Dtos
{
    public class DonorRetentionDto
    {
        public int ConquerYear { get; set; }
        public Dictionary<decimal, double>? RetentionPercentagePerYear { get; set; }
    }
}
