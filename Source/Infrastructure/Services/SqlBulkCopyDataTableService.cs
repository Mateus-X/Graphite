using Graphite.Source.Domain.Entities;
using Microsoft.Data.SqlClient;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data;
using System.Data.Common;
using System.Reflection;

namespace Graphite.Source.Infrastructure.Services
{
    public static class SqlBulkCopyDataTableService
    {
        public static async Task InsertDataframeLines(DbTransaction transaction, DataTable dataTable, Dataframe dataframe)
        {
            var connection = transaction.Connection as SqlConnection;

            using (var bulkCopy = new SqlBulkCopy(connection, SqlBulkCopyOptions.Default, (SqlTransaction)transaction))
            {
                bulkCopy.DestinationTableName = typeof(DataframeLine).GetCustomAttribute<TableAttribute>()?.Name;

                

                DataColumn dataframeIdColumn = new DataColumn("DataframeId", typeof(Guid))
                {
                    DefaultValue = dataframe.Id
                };

                dataTable.Columns.Add(dataframeIdColumn);

                foreach (DataColumn column in dataTable.Columns)
                {
                    bulkCopy.ColumnMappings.Add(column.ColumnName, column.ColumnName);
                }

                bulkCopy.BatchSize = 10000;
                bulkCopy.BulkCopyTimeout = 0;
                await bulkCopy.WriteToServerAsync(dataTable);
            }
        }
    }
}