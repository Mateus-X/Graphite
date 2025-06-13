using System.ComponentModel.DataAnnotations.Schema;

namespace Graphite.Source.Domain.Entities
{
    [Table("Dataframes")]
    public class Dataframe
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public required string UserId { get; set; }
        public required string File { get; set; }

        public virtual User User { get; set; } = null!;
        public virtual ICollection<DataframeLine> DataframeLines { get; set; } = new List<DataframeLine>();
    }
}
