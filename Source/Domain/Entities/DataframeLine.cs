namespace Graphite.Source.Domain.Entities
{
    public class DataframeLine
    {
        public int Id { get; set; }
        public int DataframeId { get; set; }
        public int DonatorId { get; set; }
        public DateTime Date { get; set; }
        public decimal Value { get; set; }

        public virtual Dataframe Dataframe { get; set; } = null!;


    }
}
