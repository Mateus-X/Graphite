using System.Data;
using ExcelDataReader;
using Graphite.Source.Domain.Entities;

public static class SpreadsheetReader
{
    public static DataTable SpreadsheetToDataTable(string filePath)
    {
        System.Text.Encoding.RegisterProvider(System.Text.CodePagesEncodingProvider.Instance);

        using var stream = new FileStream(filePath, FileMode.Open, FileAccess.Read);

        if (Path.GetExtension(filePath).Equals(".csv", StringComparison.OrdinalIgnoreCase))
            return ReadCsvToDataTable(stream);


        return ReadExcelToDataTable(stream);
    }

    private static DataTable ReadExcelToDataTable(Stream excelStream)
    {
        using var reader = ExcelReaderFactory.CreateReader(excelStream);
        return reader.AsDataSet(new ExcelDataSetConfiguration()
        {
            ConfigureDataTable = (_) => new ExcelDataTableConfiguration()
            {
                UseHeaderRow = true,
                FilterColumn = (rowReader, colIndex) => colIndex < 3
            }
        }).Tables[0];
    }

    private static DataTable ReadCsvToDataTable(Stream csvStream)
    {
        using var reader = ExcelReaderFactory.CreateCsvReader(csvStream);
        return reader.AsDataSet(new ExcelDataSetConfiguration()
        {
            ConfigureDataTable = (_) => new ExcelDataTableConfiguration()
            {
                UseHeaderRow = true,
                FilterColumn = (rowReader, colIndex) => colIndex < 3
            }
        }).Tables[0];
    }
}