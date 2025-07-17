using Graphite.Database;
using Graphite.Source.Domain.Entities;
using Graphite.Source.Domain.Interfaces;
using Graphite.Source.Infrastructure.Repositories;
using Microsoft.AspNetCore.Html;
using System.Net.Http;
using System.Text;
using System.Text.Json;

namespace Graphite.Source.Infrastructure.Services
{
    public class DataframeProcessorService(DataframeRepository dataframeRepository, ApplicationDatabaseContext context) : IDataframeProcessorService
    {
        private readonly DataframeRepository _dataframeRepository = dataframeRepository;
        private readonly HttpClient _httpClient = new HttpClient();
        private readonly IConfiguration _configuration;
        private readonly ApplicationDatabaseContext _context = context;

        public async Task Process(Dataframe dataframe)
        {
            await _dataframeRepository.BulkInsertDataframeLinesFile(dataframe);

            var serviceEndpoint = _configuration.GetConnectionString("GraphiteDataframeService");

            HttpContent dataframePayload = new StringContent(
                JsonSerializer.Serialize(new { userId = dataframe.UserId }),
                Encoding.UTF8,
                "application/json"
            );

            HttpResponseMessage file = await _httpClient.PostAsync(serviceEndpoint, dataframePayload);

            string plotContent = await file.Content.ReadAsStringAsync();

            string filePath = Path.Combine(Path.GetTempPath(), $"{dataframe.UserId}_report.html");

            await File.WriteAllTextAsync(filePath, plotContent, Encoding.UTF8);

            dataframe.HtmlReportFilePath = filePath;

            _context.Dataframes.Update(dataframe);
        }
    }
}
