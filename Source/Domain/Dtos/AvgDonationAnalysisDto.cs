namespace Graphite.Source.Domain.Dtos
{
    public class AvgDonationAnalysisDto
    {
        public List<AvgDonationYearlyData> YearlyData { get; set; } = new List<AvgDonationYearlyData>();
    }

    public class AvgDonationYearlyData
    {
        public int Year { get; set; }
        public decimal AllDonorsAvg { get; set; }
        public decimal AllDonorsMedian { get; set; }
        public decimal RetainedDonorsAvg { get; set; }
        public decimal RetainedDonorsMedian { get; set; }
        public int DonorCount { get; set; }
    }
}
