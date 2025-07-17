using Graphite.Database;
using Graphite.Source.Domain.Entities;
using Graphite.Source.Domain.Interfaces;
using Graphite.Source.Domain.Models;
using Graphite.Source.Infrastructure.Repositories;
using Graphite.Source.Infrastructure.Services;
using Hangfire;
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
        private readonly ApplicationDatabaseContext _context;
        private readonly UserManager<User> _userManager;

        public DataframeController(ApplicationDatabaseContext context, UserManager<User> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        [Authorize]
        [HttpPost("import-xlsx")]
        [RequestFormLimits(MultipartBodyLengthLimit = 1209715200)]
        [RequestSizeLimit(1209715200)]
        public async Task<IActionResult> ImportXlsx([FromForm] DataframeModel request)
        {
            List<string> availableFileExtensions = new List<string> { ".xlsx", ".xls", ".xlsb", ".csv" };

            var fileExtension = Path.GetExtension(request.File.FileName);

            if (!availableFileExtensions.Contains(fileExtension))
                return BadRequest("Formato Inválido.");

            var tempFilePath = Path.Combine(Path.GetTempPath(), Guid.NewGuid() + fileExtension);

            try
            {
                var userId = _userManager.GetUserId(User);

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
                    Id = Guid.NewGuid(),
                    UserId = userId,
                    SpreadsheetFilePath = tempFilePath,
                };

                await _context.AddAsync(dataframe);

                await _context.SaveChangesAsync();

                BackgroundJob.Enqueue<DataframeProcessorService>(service =>
                    service.Process(dataframe));

                return Accepted(new { JobId = dataframe.Id, Message = "Processamento iniciado em segundo plano." });
            }
            catch (Exception ex)
            {
                if (System.IO.File.Exists(tempFilePath))
                {
                    System.IO.File.Delete(tempFilePath);
                }

                return BadRequest(ex.Message);
            }
        }
    }
}