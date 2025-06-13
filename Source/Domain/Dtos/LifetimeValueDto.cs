namespace Graphite.Source.Domain.Dtos
{
    public class LifetimeValueDto
    {
        public int AcquisitionYear { get; set; }
        public decimal AverageLTV { get; set; }
        public decimal TotalLTV { get; set; }
        public int DonorCount { get; set; }
    }
}