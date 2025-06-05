namespace Graphite.Source.Domain.Entities
{
    public class Organization
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public User User { get; set; } = null!;

        public virtual ICollection<Dataframe> Dataframes { get; set; } = new List<Dataframe>();
    }
}