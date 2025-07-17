namespace Graphite.Source.Domain.Dtos
{
    public class NewDonorsDto
    {
        public required List<NewDonorsPerYear> NewDonorsPerYear { get; set; }
    }

    public class NewDonorsPerYear
    {
        public int Year { get; set; }
        public int NewDonors { get; set; }
    }
}
