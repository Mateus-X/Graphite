namespace Graphite.Source.Domain.Dtos
{
    public class DonorReactivationDto
    {
        public int DonorId { get; set; }
        public decimal HistoricalValue { get; set; }
        public int DaysSinceLastDonation { get; set; }
        public string Potential { get; set; } = string.Empty; // "High", "Medium", "Low"
    }
}