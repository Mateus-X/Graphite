using Graphite.Source.Domain.Entities;
using Microsoft.Data.SqlClient;
using Sylvan.Data;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data;
using System.Data.Common;
using System.Diagnostics;
using System.Reflection;

namespace Graphite.Source.Infrastructure.Services
{
    public static class SqlBulkCopySpreadsheetService
    {
        public static async Task InsertDataframeLines(DbTransaction transaction, string filePath, Guid dataframeId)
        {
            var start = Stopwatch.StartNew();

            var connection = transaction.Connection as SqlConnection;

            using IDataReader dataReader = ProcessSpreadsheetData(filePath, dataframeId);

            using (var bulkCopy = new SqlBulkCopy(connection, SqlBulkCopyOptions.Default, (SqlTransaction)transaction))
            {
                bulkCopy.DestinationTableName = typeof(DataframeLine).GetCustomAttribute<TableAttribute>()?.Name;

                for (int i = 0; i < dataReader.FieldCount; i++)
                {
                    var columnName = dataReader.GetName(i);
                    Console.WriteLine(columnName);
                    bulkCopy.ColumnMappings.Add(columnName, columnName);
                }

                bulkCopy.BulkCopyTimeout = 0;
                bulkCopy.BatchSize = 10000;

                bulkCopy.SqlRowsCopied += (sender, e) =>
                {
                    Console.WriteLine($"Copiadas {e.RowsCopied} linhas...");
                };
                bulkCopy.NotifyAfter = 10000; 

                await bulkCopy.WriteToServerAsync(dataReader);
            }

            Console.WriteLine(start.Elapsed);
            start.Stop();
        }

        private static IDataReader ProcessSpreadsheetData(string filePath, Guid dataframeId)
        {
            if (Path.GetExtension(filePath).Equals(".csv", StringComparison.OrdinalIgnoreCase))
            {
                var csv = Sylvan.Data.Csv.CsvDataReader.Create(filePath);

                return csv.WithColumns(
                    new CustomDataColumn<Guid>("DataframeId", r => dataframeId)
                );
            }

            var xlsx = Sylvan.Data.Excel.ExcelDataReader.Create(filePath);

            return xlsx.WithColumns(
                new CustomDataColumn<Guid>("DataframeId", r => dataframeId)
            );
        }
    }
}