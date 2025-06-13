namespace Graphite.Source.Domain.Dtos
{
    public class DonationFrequencyDto : TimeAnalysisDto
    {
        public double AverageDonationsPerDonor { get; set; }
        public string Trend { get; set; } = string.Empty; 
    }
}