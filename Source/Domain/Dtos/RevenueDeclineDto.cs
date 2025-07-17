namespace Graphite.Source.Domain.Dtos
{
    public class RevenueDeclineDto
    {
        public List<YearlyDeclineData> DeclineYears { get; set; } = new List<YearlyDeclineData>();
    }

    public class YearlyDeclineData
    {
        public int Year { get; set; }
        public decimal RevenueChange { get; set; }
        public double DonorChange { get; set; }
        public double DonationChange { get; set; }
        public decimal AvgDonationChange { get; set; }
    }
}
