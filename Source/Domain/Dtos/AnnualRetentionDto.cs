namespace Graphite.Source.Domain.Dtos
{
    public class AnnualRetentionDto
    {
        public int Year { get; set; }
        public decimal RetentionRate { get; set; }
        public string Classification { get; set; } = string.Empty; 
    }
}