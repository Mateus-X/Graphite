namespace Graphite.Source.Domain.Dtos
{
    public class DonorAttritionDto
    {
        public List<DonorAttritionYearlyData> YearlyData { get; set; } = new List<DonorAttritionYearlyData>();
    }

    public class DonorAttritionYearlyData
    {
        public int Year { get; set; }
        public int AttritedDonors { get; set; }
        public int NewDonors { get; set; }
        public int NetChange { get; set; }
    }
}
