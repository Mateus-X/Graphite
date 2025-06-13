namespace Graphite.Source.Domain.Dtos
{
    public class Post2020ComparisonDto
    {
        public string Metric { get; set; } = string.Empty; 
        public decimal ValuePre2020 { get; set; }
        public decimal ValuePost2020 { get; set; }
        public decimal PercentageChange { get; set; }
    }
}