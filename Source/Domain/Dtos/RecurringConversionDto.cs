namespace Graphite.Source.Domain.Dtos
{
    public class RecurringConversionDto : TimeAnalysisDto
    {
        public int NewDonors { get; set; }
        public int ConvertedToRecurring { get; set; }
        public decimal ConversionRate { get; set; }
    }
}