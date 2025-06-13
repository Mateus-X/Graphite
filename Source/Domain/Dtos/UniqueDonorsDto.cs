namespace Graphite.Source.Domain.Dtos
{
    public class UniqueDonorsDto
    {
        public int TotalUniqueDonors { get; set; }
        public decimal PercentageOfTotal { get; set; }
        public List<int> UniqueDonorIds { get; set; } = new();
    }
}