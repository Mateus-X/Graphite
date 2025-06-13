using Graphite.Database;
using Graphite.Source.Domain.Dtos;
using Graphite.Source.Domain.Entities;
using Graphite.Source.Domain.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Graphite.Source.Infrastructure.Repositories
{
    public class DataframeLinesRepository(ApplicationDatabaseContext context) : Repository<DataframeLine>(context), IDataframeLinesRepository
    {
        private readonly ApplicationDatabaseContext _context = context;

        //public async Task<IEnumerable<FundraisingGrowthDto>> GetDataframeFor(DateOnly date)
        //{
        //    return await _context.DataframeLines
        //            .GroupBy(d => d.Date.Year)
        //            .Select(g => new
        //            {
        //                Year = g.Key,
        //                TotalFundraise = g.Sum(d => d.Value),
        //                Crescimento = (g.Sum(d => d.Value) - g
        //                    .Where(d => d.Date.Year == g.Key - 1)
        //                    .Sum(d => d.Value)) /
        //                    g.Where(d => d.Date.Year == g.Key - 1)
        //                    .Sum(d => d.Value) * 100
        //            })
        //            .OrderBy(x => x.Year);
        //}
    }
}
