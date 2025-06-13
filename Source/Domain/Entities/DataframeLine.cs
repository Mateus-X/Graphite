using System.ComponentModel.DataAnnotations.Schema;

namespace Graphite.Source.Domain.Entities
{
    [Table("DataframeLines")]
    public class DataframeLine : BaseExcel
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public Guid DataframeId { get; set; }

        public virtual Dataframe Dataframe { get; set; } = null!;
    }
}
