using Graphite.Database;
using Graphite.Source.Domain.Entities;
using Graphite.Source.Domain.Interfaces;
using Graphite.Source.Infrastructure.Wrappers;
using Hangfire;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data;
using System.Diagnostics;
using System.Reflection;

namespace Graphite.Source.Infrastructure.Repositories
{
    public class DataframeRepository(ApplicationDatabaseContext context) : Repository<Dataframe>(context), IDataframeRepository
    {

        public async Task<Dataframe?> GetByUserIdAsync(string userId)
        {
            return await context.Dataframes
                .FirstOrDefaultAsync(df => df.UserId == userId);
        }

        public async Task BulkInsertDataframeLinesFile(Dataframe dataframe)
        {
            var start = Stopwatch.StartNew();

            using var dataReader = new SkippingInvalidRowsDataReader(dataframe.CastSpreadsheetToDataReader());

            using (var bulkCopy = new SqlBulkCopy(context.Database.GetConnectionString()))
            {
                bulkCopy.DestinationTableName = typeof(DataframeLine).GetCustomAttribute<TableAttribute>()?.Name;

                for (int i = 0; i < dataReader.FieldCount; i++)
                {
                    var columnName = dataReader.GetName(i);

                    bulkCopy.ColumnMappings.Add(columnName, columnName);
                }

                bulkCopy.BulkCopyTimeout = 0;
                bulkCopy.BatchSize = 1000;

                bulkCopy.SqlRowsCopied += (sender, e) =>
                {
                    Console.WriteLine($"Copiadas {e.RowsCopied} linhas...");
                };

                bulkCopy.NotifyAfter = 100000;

                await bulkCopy.WriteToServerAsync(dataReader);
            }

            Console.WriteLine(start.Elapsed);
            start.Stop();
        }
    }
};
