using Graphite.Source.Domain.Dtos;
using Graphite.Source.Domain.Models;
using Graphite.Source.Infrastructure.Repositories;
using Graphite.Source.Infrastructure.Services;
using Hangfire;
using Microsoft.AspNetCore.Mvc;

namespace Graphite.Source.Http.Controllers
{
    [ApiController]
    [Route("D")]
    public class DataframeLinesController : ControllerBase
    {
        private readonly DataframeRepository _dataframeRepository;

        private readonly DataframeLinesRepository _dataframeLinesRepository;

        public DataframeLinesController(DataframeRepository dataframeRepository, DataframeLinesRepository dataframeLinesRepository)
        {
            _dataframeRepository = dataframeRepository;
            _dataframeLinesRepository = dataframeLinesRepository;
        }

        [HttpPost("new-donors")]
        public async Task<IActionResult> GetNewDonors([FromQuery] string userId)
        {
            try
            {
                var dataframeUserId = await _dataframeRepository.GetByUserIdAsync(userId)
                    ?? throw new Exception();

                NewDonorsDto newDonorsDto = (NewDonorsDto) await _dataframeLinesRepository
                    .GetNewDonorsPerYearAsync(dataframeUserId.Id);

                return Accepted(new { Message = "ok", Data = newDonorsDto });
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}