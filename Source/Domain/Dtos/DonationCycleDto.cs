namespace Graphite.Source.Domain.Dtos
{
    public class DonationCycleDto
    {
        public int DonorId { get; set; }
        public int TotalDonations { get; set; }
        public double AverageDaysBetweenDonations { get; set; }
    }
}
