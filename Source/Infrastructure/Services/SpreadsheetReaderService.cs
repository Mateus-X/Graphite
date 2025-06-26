//using System.Data;
//using Sylvan.Data.Csv;
//using Sylvan.Data.Excel;
//using Graphite.Source.Domain.Entities;

//public static class SpreadsheetReader
//{
//    public static Sylvan.Data.Excel.ExcelDataReader SpreadsheetToDataTable(string filePath)
//    {
//        System.Text.Encoding.RegisterProvider(System.Text.CodePagesEncodingProvider.Instance);

//        using var stream = new FileStream(filePath, FileMode.Open, FileAccess.Read);

//        if (Path.GetExtension(filePath).Equals(".csv", StringComparison.OrdinalIgnoreCase))
//            return Sylvan.Data.Excel.ExcelDataReader.Create(filePath);


//        return ;
//    }

//    private static DataTable ReadExcelToDataTable(Stream excelStream)
//    {
//        using var reader = ExcelReaderFactory.CreateReader(excelStream);
//        return reader.AsDataSet(new ExcelDataSetConfiguration()
//        {
//            ConfigureDataTable = (_) => new ExcelDataTableConfiguration()
//            {
//                UseHeaderRow = true,
//                FilterColumn = (rowReader, colIndex) => colIndex < 3
//            }
//        }).Tables[0];
//    }

//    private static DataTable ReadCsvToDataTable(Stream csvStream)
//    {
//        using var reader = ExcelReaderFactory.CreateCsvReader(csvStream);
//        return reader.AsDataSet(new ExcelDataSetConfiguration()
//        {
//            ConfigureDataTable = (_) => new ExcelDataTableConfiguration()
//            {
//                UseHeaderRow = true,
//                FilterColumn = (rowReader, colIndex) => colIndex < 3
//            }
//        }).Tables[0];
//    }
//}