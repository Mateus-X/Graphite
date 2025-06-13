namespace Graphite.Source.Domain.Dtos
{
    public class DonationCycleStatisticsDto
    {
        public double AverageDaysBetweenDonations { get; set; }
        public double MedianDaysBetweenDonations { get; set; }
        public List<DonationCycleDto> DonationCycles { get; set; } = new List<DonationCycleDto>();

    }
}
