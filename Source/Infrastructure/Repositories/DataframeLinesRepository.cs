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

        public async Task<IEnumerable<NewDonorsPerYear>> GetNewDonorsPerYearAsync(Guid dataframeId)
        {
            return await _context.DataframeLines
               .AsNoTracking()
               .Where(line => line.DataframeId == dataframeId)
               .GroupBy(line => line.DonorId)
               .Select(g => new
               {
                   DonorId = g.Key,
                   FirstYear = g.Min(x => x.DonationDate.Year)
               })
               .GroupBy(x => x.FirstYear)
               .Select(g => new NewDonorsPerYear
               {
                   Year = g.Key,
                   NewDonors = g.Select(x => x.DonorId).Count()
               })
               .OrderBy(x => x.Year)
               .ToListAsync();
        }

        public async Task<object> GetChurnsPerYear(Guid dataframeId)
        {
            var donorsPerYear = await _context.DataframeLines
                .AsNoTracking()
                .Where(line => line.DataframeId == dataframeId)
                .GroupBy(line => line.DonationDate.Year)
                .Select(g => new
                {
                    Year = g.Key,
                    Donors = g.Select(x => x.DonorId).Distinct()
                })
                .OrderBy(x => x.Year)
                .ToListAsync();

            return donorsPerYear
                .Skip(1)
                .Select((yearData, idx) => new
                {
                    yearData.Year,
                    ChurnCount = donorsPerYear[idx].Donors.Except(yearData.Donors).Count()
                })
                .ToList();
        }

        //public async Task<RevenueTrendDto> GetRevenueTrend(Guid dataframeId)
        //{
        //    var query = await _context.DataframeLines
        //         .Include(q => q.Dataframe)
        //         .Where(q => q.Dataframe.dataframeId == dataframeId)
        //         .GroupBy(x => new { x.Date.Year, x.Date.Month })
        //         .Select(g => new
        //         {
        //             g.Key.Year,
        //             g.Key.Month,
        //             TotalValue = g.Sum(x => x.Value),
        //             DonorCount = g.Select(x => x.DonorId).Distinct().Count()
        //         })
        //         .OrderBy(x => x.Year)
        //         .ThenBy(x => x.Month)
        //         .ToListAsync();

        //    var result = new RevenueTrendDto
        //    {
        //        MonthlyData = query.Select(x => new MonthlyRevenueData
        //        {
        //            YearMonth = $"{x.Year}-{x.Month:D2}",
        //            TotalRevenue = x.TotalValue,
        //            DonorCount = x.DonorCount,
        //            AvgDonation = x.TotalValue / x.DonorCount
        //        }).ToList()
        //    };

        //    return result;
        //}

        //public async Task<RevenueDeclineDto> GetRevenueDecline(Guid dataframeId)
        //{
        //    var yearlyData = await _context.DataframeLines
        //        .GroupBy(x => x.Date.Year)
        //        .Select(g => new
        //        {
        //            Year = g.Key,
        //            TotalRevenue = g.Sum(x => x.Value),
        //            DonorCount = g.Select(x => x.DonorId).Distinct().Count(),
        //            DonationCount = g.Count(),
        //            AvgDonation = g.Average(x => x.Value)
        //        })
        //        .OrderBy(x => x.Year)
        //        .ToListAsync();

        //    var result = new RevenueDeclineDto();

        //    for (int i = 1; i < yearlyData.Count; i++)
        //    {
        //        var current = yearlyData[i];
        //        var previous = yearlyData[i - 1];

        //        if (current.TotalRevenue < previous.TotalRevenue)
        //        {
        //            result.DeclineYears.Add(new YearlyDeclineData
        //            {
        //                Year = current.Year,
        //                RevenueChange = (current.TotalRevenue - previous.TotalRevenue) / previous.TotalRevenue * 100,
        //                DonorChange = (current.DonorCount - previous.DonorCount) / (double)previous.DonorCount * 100,
        //                DonationChange = (current.DonationCount - previous.DonationCount) / (double)previous.DonationCount * 100,
        //                AvgDonationChange = (current.AvgDonation - previous.AvgDonation) / previous.AvgDonation * 100
        //            });
        //        }
        //    }

        //    return result;
        //}

        //public async Task<AvgDonationAnalysisDto> AnalyzeAvgDonationGrowth(Guid dataframeId)
        //{
        //    var yearlyAllDonors = await _context.DataframeLines
        //        .Include(q => q.Dataframe)
        //        .Where(q => q.Dataframe.dataframeId == dataframeId)
        //        .GroupBy(x => x.Date.Year)
        //        .Select(g => new
        //        {
        //            Year = g.Key,
        //            AvgDonation = g.Average(x => x.Value),
        //            MedianDonation = g.Select(x => x.Value).Average(),
        //            DonorCount = g.Select(x => x.DonorId).Distinct().Count()
        //        })
        //        .OrderBy(x => x.Year)
        //        .ToListAsync();

        //    var retainedDonorsStats = await _context.DataframeLines
        //        .Include(q => q.Dataframe)
        //        .Where(q => q.Dataframe.dataframeId == dataframeId)
        //        .GroupBy(x => new { x.DonorId, x.Date.Year })
        //        .Select(g => new { g.Key.DonorId, g.Key.Year })
        //        .ToListAsync();

        //    var retainedDonors = retainedDonorsStats
        //        .GroupBy(x => x.DonorId)
        //        .Where(g => g.Count() > 1)
        //        .SelectMany(g => g)
        //        .ToList();

        //    var yearlyRetainedDonors = await _context.DataframeLines
        //        .Include(q => q.Dataframe)
        //        .Where(x => q.Dataframe.dataframeId == dataframeId && retainedDonors.Contains(new { x.DonorId, x.Date.Year }))
        //        .GroupBy(x => x.Date.Year)
        //        .Select(g => new
        //        {
        //            Year = g.Key,
        //            AvgDonation = g.Average(x => x.Value),
        //            MedianDonation = g.Select(x => x.Value).Average()
        //        })
        //        .OrderBy(x => x.Year)
        //        .ToListAsync();

        //    var result = new AvgDonationAnalysisDto();

        //    foreach (var year in yearlyAllDonors)
        //    {
        //        var retainedStat = yearlyRetainedDonors.FirstOrDefault(y => y.Year == year.Year);

        //        result.YearlyData.Add(new AvgDonationYearlyData
        //        {
        //            Year = year.Year,
        //            AllDonorsAvg = year.AvgDonation,
        //            AllDonorsMedian = year.MedianDonation,
        //            RetainedDonorsAvg = retainedStat?.AvgDonation ?? 0,
        //            RetainedDonorsMedian = retainedStat?.MedianDonation ?? 0,
        //            DonorCount = year.DonorCount
        //        });
        //    }

        //    return result;
        //}

        //public async Task<DonorAttritionDto> GetDonorAttritionByYear(Guid dataframeId)
        //{
        //    var donorYears = await _context.DataframeLines
        //        .Include(q => q.Dataframe)
        //        .Where(q => q.Dataframe.dataframeId == dataframeId)
        //        .Select(x => new { x.DonorId, x.Date.Year })
        //        .Distinct()
        //        .ToListAsync();

        //    var donorsByYear = donorYears
        //        .GroupBy(x => x.DonorId)
        //        .Select(g => new
        //        {
        //            DonorId = g.Key,
        //            Years = g.Select(x => x.Year).OrderBy(y => y).ToList()
        //        })
        //        .ToList();

        //    var attritionByYear = new Dictionary<int, DonorAttritionYearlyData>();

        //    foreach (var donor in donorsByYear)
        //    {
        //        for (int i = 1; i < donor.Years.Count; i++)
        //        {
        //            var previousYear = donor.Years[i - 1];
        //            var currentYear = donor.Years[i];

        //            if (currentYear > previousYear + 1)
        //            {
        //                for (int y = previousYear + 1; y < currentYear; y++)
        //                {
        //                    if (!attritionByYear.ContainsKey(y))
        //                    {
        //                        attritionByYear[y] = new DonorAttritionYearlyData { Year = y };
        //                    }
        //                    attritionByYear[y].AttritedDonors++;
        //                }
        //            }
        //        }

        //        var lastYear = donor.Years.Last();
        //        if (!attritionByYear.ContainsKey(lastYear + 1))
        //        {
        //            attritionByYear[lastYear + 1] = new DonorAttritionYearlyData { Year = lastYear + 1 };
        //        }
        //        attritionByYear[lastYear + 1].AttritedDonors++;
        //    }

        //    var newDonorsByYear = donorYears
        //        .GroupBy(x => x.Year)
        //        .Select(g => new { Year = g.Key, Count = g.Count() })
        //        .ToDictionary(x => x.Year, x => x.Count);

        //    var result = new DonorAttritionDto();

        //    foreach (var year in attritionByYear.Keys.OrderBy(y => y))
        //    {
        //        newDonorsByYear.TryGetValue(year, out int newDonors);

        //        result.YearlyData.Add(new DonorAttritionYearlyData
        //        {
        //            Year = year,
        //            AttritedDonors = attritionByYear[year].AttritedDonors,
        //            NewDonors = newDonors,
        //            NetChange = newDonors - attritionByYear[year].AttritedDonors
        //        });
        //    }

        //    return result;
        //}

    }
}
