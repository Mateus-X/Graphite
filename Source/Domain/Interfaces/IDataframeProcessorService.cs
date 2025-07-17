using Graphite.Source.Domain.Entities;

namespace Graphite.Source.Domain.Interfaces
{
    public interface IDataframeProcessorService
    {
        public Task Process(Dataframe dataframe);
    }
}
