using Microsoft.Data.SqlClient;
using Sylvan.Data;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data;
using System.Diagnostics;
using System.Reflection;

namespace Graphite.Source.Domain.Entities
{
    [Table("Dataframes")]
    public class Dataframe
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public required string UserId { get; set; }
        public required string SpreadsheetFilePath { get; set; }
        public string? HtmlReportFilePath { get; set; }

        public virtual User User { get; set; } = null!;
        public virtual ICollection<DataframeLine> DataframeLines { get; set; } = new List<DataframeLine>();

        /* 
         * <summary>
         * Converts the spreadsheet stored in FilePath to a DataReader.
         * </summary>
         */
        public IDataReader CastSpreadsheetToDataReader()
        {
            if (Path.GetExtension(SpreadsheetFilePath).Equals(".csv", StringComparison.OrdinalIgnoreCase))
            {
                var csv = Sylvan.Data.Csv.CsvDataReader.Create(SpreadsheetFilePath);

                return csv.WithColumns(
                    new CustomDataColumn<Guid>("DataframeId", r => Id)
                );
            }

            var xlsx = Sylvan.Data.Excel.ExcelDataReader.Create(SpreadsheetFilePath);

            return xlsx.WithColumns(
                new CustomDataColumn<Guid>("DataframeId", r => Id)
            );
        }
    }
}
