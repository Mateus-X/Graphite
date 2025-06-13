namespace Graphite.Source.Domain.Dtos
{
    public class AverageTicketDto
    {
        public string DonorType { get; set; } = string.Empty;
        public decimal AverageValue { get; set; }
        public decimal AnnualVariation { get; set; } // In %
    }
}