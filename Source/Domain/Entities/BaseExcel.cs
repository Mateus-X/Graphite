namespace Graphite.Source.Domain.Entities
{
    public class BaseExcel
    {
        public required string DonorId { get; set; }
        public required DateTime DonationDate { get; set; }
        public DateTime? ExpirationDate { get; set; }
        public required decimal Value { get; set; }
        public string? PaymentMethod { get; set; }
    }
}
