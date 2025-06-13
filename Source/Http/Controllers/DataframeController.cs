using Graphite.Database;
using Graphite.Source.Domain.Entities;
using Graphite.Source.Domain.Interfaces;
using Graphite.Source.Domain.Models;
using Graphite.Source.Infrastructure.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Storage;
using System.Data;
using System.Data.Common;

namespace Graphite.Source.Http.Controllers
{
    [ApiController]
    [Route("api/dataframe")]
    public class DataframeController : ControllerBase
    {
        private readonly ILogger<DataframeController> _logger;
        private readonly ApplicationDatabaseContext _context;
        private readonly IDataframeRepository _dataframeRepository;
        private readonly UserManager<User> _userManager;

        public DataframeController(ILogger<DataframeController> logger, ApplicationDatabaseContext context, IDataframeRepository repository, UserManager<User> userManager)
        {
            _logger = logger;
            _context = context;
            _dataframeRepository = repository;
            _userManager = userManager;
        }

        [Authorize]
        [HttpPost("import-xlsx")]
        [RequestFormLimits(MultipartBodyLengthLimit = 1209715200)]
        [RequestSizeLimit(1209715200)]
        public async Task<IActionResult> ImportXlsx([FromForm] DataframeModel request)
        {
            using var transaction = _context.Database.BeginTransaction();

            List<string> availableFileExtensions = new List<string> { ".xlsx", ".xls", ".xlsb", ".csv" };

            if (!availableFileExtensions.Contains(Path.GetExtension(request.File.FileName)))
                return BadRequest("Formato Inválido.");

            var fileExtension = Path.GetExtension(request.File.FileName);
            var tempFilePath = Path.Combine(Path.GetTempPath(), Guid.NewGuid() + fileExtension);

            try
            {

                var userId = _userManager.GetUserId(User);
                Console.WriteLine(userId);

                if (string.IsNullOrEmpty(userId))
                    return Unauthorized("Usuário não autenticado.");

                if (request.File.Length > 0)
                {
                    using (var stream = new FileStream(tempFilePath, FileMode.Create))
                    {
                        await request.File.CopyToAsync(stream);
                    }
                }

                var dataframe = new Dataframe
                {
                    UserId = userId,
                    File = request.File.FileName,
                };

                await _context.AddAsync(dataframe);

                await _context.SaveChangesAsync();

                DataTable dataTable = SpreadsheetReader.SpreadsheetToDataTable(tempFilePath);

                await SqlBulkCopyDataTableService.InsertDataframeLines(
                    transaction: transaction.GetDbTransaction(),
                    dataTable: dataTable,
                    dataframe: dataframe
                );

                await transaction.CommitAsync();
            }
            catch (Exception ex)
            {
                await transaction.RollbackAsync();

                return BadRequest(ex.Message);

            }
            finally
            {
                if (System.IO.File.Exists(tempFilePath))
                {
                    System.IO.File.Delete(tempFilePath);
                }
            }

            return Ok();
        }
    }
}