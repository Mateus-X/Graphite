using Graphite.Database;
using Graphite.Source.Domain.Entities;
using Graphite.Source.Domain.Interfaces;

namespace Graphite.Source.Infrastructure.Repositories
{
    public class DataframeRepository(ApplicationDatabaseContext context) : Repository<Dataframe>(context), IDataframeRepository
    {
    }
};
