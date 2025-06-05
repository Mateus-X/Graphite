namespace Graphite.Source.Domain.Entities
{
    public class Dataframe
    {
        public int Id { get; set; }
        //nullable foreign key para V1 (1 dataframe => 1 html)
        public int? OrganizationId { get; set; }
        public required string File { get; set; }

        public virtual Organization Organization { get; set; } = null!;
        public virtual ICollection<DataframeLine> DataframeLines { get; set; } = new List<DataframeLine>();
    }
}
