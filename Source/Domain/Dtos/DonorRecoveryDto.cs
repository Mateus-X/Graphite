namespace Graphite.Source.Domain.Dtos
{
    public class DonorRecoveryDto
    {
        public int RecoverableDonors { get; set; }
        public decimal EstimatedValue { get; set; }
        public decimal AnnualRevenuePercentage { get; set; }
    }
}