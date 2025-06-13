namespace Graphite.Source.Domain.Dtos
{
    public class FundraisingLossDto : TimeAnalysisDto
    {
        public decimal LossValue { get; set; }
        public decimal LossPercentage { get; set; }
        public string? PossibleCauses { get; set; }

    }
}
