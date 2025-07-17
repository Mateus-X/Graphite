using Graphite.Source.Domain.Entities;

namespace Graphite.Source.Domain.Interfaces
{
    public interface IDataframeRepository : IRepository<Dataframe>
    {
        public Task BulkInsertDataframeLinesFile(Dataframe dataframe);
    }
}
