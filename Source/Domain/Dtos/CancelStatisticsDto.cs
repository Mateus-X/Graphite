namespace Graphite.Source.Domain.Dtos
{
    public class CancelStatisticsDto
    {
        public double AverageDaysUntilCancel { get; set; }
        public int ModeTimeCategory { get; set; } 
        public List<CancelTimerDto> DonorDetails { get; set; } = new();
    }
}