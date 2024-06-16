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
    public class ParticipantController : Controller
    {
        private readonly IParticipantService _participantService;
        public ParticipantController(IParticipantService participantService)
        {
            _participantService = participantService;
        }

        [HttpPost("AddParticipant")]
        public async Task AddParticipant([FromForm] Participant newParticipant)
        {
            try
            {
                Log.Information("Endpoint AddParticipant called at {Time}", DateTime.UtcNow);
                await _participantService.AddParticipantAsync(newParticipant);
            }
            catch (Exception ex)
            {
                Log.Error("Error calling endpoint AddParticipant: {Error}", ex.Message);
                throw;
            }
        }

        [HttpGet("GetAllParticipants")]
        public async Task<List<Participant>> GetParticipants()
        {
            try
            {
                Log.Information("Endpoint GetAllParticipants called at {Time}", DateTime.UtcNow);
                return await _participantService.GetParticipantsAsync();

            }
            catch (Exception ex)
            {
                Log.Error("Error calling endpoint GetAllParticipants: {Error}", ex.Message);
                throw;
            }
        }

        [HttpPost("GetParticipantByPersonalId")]
        public async Task<Participant> GetParticipantByPersonalId([FromForm] string personalId)
        {
            try
            {
                Log.Information("Endpoint GetParticipantByPersonalId called at {Time}", DateTime.UtcNow);
                return await _participantService.GetParticipantByPersonalIdAsync(personalId);
            }
            catch (Exception ex)
            {
                Log.Error("Error calling endpoint GetParticipantById: {Error}", ex.Message);
                throw;
            }
        }

        [HttpPut("UpdateParticipant")]
        public async Task UpdateParticipant([FromForm] Participant updatedParticipant)
        {
            try
            {
                Log.Information("Endpoint UpdateParticipant called at {Time}", DateTime.UtcNow);
                await _participantService.UpdateParticipantAsync(updatedParticipant);
            }
            catch (Exception ex)
            {
                Log.Error("Error calling endpoint UpdateParticipant: {Error}", ex.Message);
                throw;
            }
        }

        [HttpDelete("DeleteParticipant")]
        public async Task DeleteParticipant([FromForm] int participantId)
        {
            try
            {
                Log.Information("Endpoint DeleteParticipant called at {Time}", DateTime.UtcNow);
                await _participantService.DeleteParticipantAsync(participantId);
            }
            catch (Exception ex)
            {
                Log.Error("Error calling endpoint DeleteParticipant: {Error}", ex.Message);
                throw;
            }
        }

        [HttpPost("GenerateRandomParticipant")]
        public async Task<Participant> GenerateRandomParticipant()
        {
            try
            {
                Log.Information("Endpoint GenerateRandomParticipant called at {Time}", DateTime.UtcNow);
                return await _participantService.GenerateRandomParticipantAsync();
            }
            catch (Exception ex)
            {
                Log.Error("Error calling endpoint GenerateRandomParticipant: {Error}", ex.Message);
                throw;
            }
        }
    }
}
