using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using TheStartingBlock.Contracts;
using TheStartingBlock.Models;
using MongoDB.Driver;
using Serilog;


namespace TheStartingBlockAPI.Controllers
{
    [ApiController]
    [Route("controller")]
    public class ResultController : Controller
    {
        private readonly IResultService _resultService;
        public ResultController(IResultService resultService)
        {
            _resultService = resultService;
        }

        [HttpPost("AddResult")]
        public async Task AddResult([FromForm] Result newResult)
        {
            try
            {
                Log.Information("Endpoint AddResult called at {Time}", DateTime.UtcNow);
                await _resultService.AddResultAsync(newResult);
            }
            catch (Exception ex)
            {
                Log.Error("Error calling endpoint AddResult: {Error}", ex.Message);
                throw;
            }
        }

        [HttpGet("GetAllResults")]
        public async Task<List<Result>> GetResults()
        {
            try
            {
                Log.Information("Endpoint GetAllResults called at {Time}", DateTime.UtcNow);
                return await _resultService.GetResultsAsync();
            }
            catch (Exception ex)
            {
                Log.Error("Error calling endpoint GetAllResults: {Error}", ex.Message);
                throw;
            }
        }

        [HttpGet("GetResultById")]
        public async Task<Result> GetResultById(int resultId)
        {
            try
            {
                Log.Information("Endpoint GetResultById called at {Time}", DateTime.UtcNow);
                return await _resultService.GetResultByIdAsync(resultId);
            }
            catch (Exception ex)
            {
                Log.Error("Error calling endpoint GetResultById: {Error}", ex.Message);
                throw;
            }
        }

        [HttpPut("UpdateResult")]
        public async Task UpdateResult([FromForm] Result result)
        {
            try
            {
                Log.Information("Endpoint UpdateResult called at {Time}", DateTime.UtcNow);
                await _resultService.UpdateResultAsync(result);
            }
            catch (Exception ex)
            {
                Log.Error("Error calling endpoint UpdateResult: {Error}", ex.Message);
                throw;
            }
        }

        [HttpDelete("DeleteResult")]
        public async Task DeleteResult([FromForm] int resultId)
        {
            try
            {
                Log.Information("Endpoint DeleteResult called at {Time}", DateTime.UtcNow);
                await _resultService.DeleteResultAsync(resultId);
            }
            catch (Exception ex)
            {
                Log.Error("Error calling endpoint DeleteResult: {Error}", ex.Message);
                throw;
            }
        }

        [HttpPost("GenerateRandomResults")]
        public async Task<bool> GenerateRandomResults(int eventId, List<int> participantIds)
        {
            try
            {
                Log.Information("Endpoint GenerateRandomResults called at {Time}", DateTime.UtcNow);
                return await _resultService.GenerateRandomResultsAsync(eventId, participantIds);
            }
            catch (Exception ex)
            {
                Log.Error("Error calling endpoint GenerateRandomResults: {Error}", ex.Message);
                throw;
            }
        }
    }
}
