namespace Graphite.Source.Domain.Dtos
{
    public class DonorClassificationDto
    {
        public string Type { get; set; } = string.Empty; 
        public decimal Percentage { get; set; }
        public int Quantity { get; set; }
    }
}