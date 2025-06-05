using Microsoft.AspNetCore.Mvc;
using ClosedXML.Excel;
using Graphite.Database;
using Graphite.Source.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Graphite.Source.Http.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class DataframeController : ControllerBase
    {
        private readonly ILogger<DataframeController> _logger;
        private readonly ApplicationDatabaseContext _dbContext;

        public DataframeController(ILogger<DataframeController> logger, ApplicationDatabaseContext dbContext)
        {
            _logger = logger;
            _dbContext = dbContext;
        }

        [HttpPost("import-xlsx")]
        public async Task<IActionResult> ImportXlsx([FromForm] IFormFile file)
        {
            if (file == null || file.Length == 0)
                return BadRequest("Arquivo não enviado.");

            var fo

            var dataframeLines = new List<DataframeLine>();

            using (var stream = file.OpenReadStream())
            using (var workbook = new XLWorkbook(stream))
            {
                var worksheet = workbook.Worksheets.First();
                foreach (var row in worksheet.RowsUsed().Skip(1)) 
                {
                    dataframeLines.Add(new DataframeLine
                    {
                        DonatorId = int.Parse(row.Cell(1).GetString()),
                        Date = DateOnly.Parse(row.Cell(2).GetString()),
                        Value = row.Cell(3).GetString()
                    });
                }
            }

            _dbContext.AddRange(dataframeLines);
            await _dbContext.SaveChangesAsync();

            return Ok(new { count = dataframeLines.Count });
        }
    }
}