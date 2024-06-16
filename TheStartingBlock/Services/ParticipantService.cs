using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TheStartingBlock.Models;
using TheStartingBlock.Contracts;
using Serilog;

namespace TheStartingBlock.Services
{
    public class ParticipantService : IParticipantService
    {
        private readonly IParticipantRepository _participantRepository;

        public ParticipantService(IParticipantRepository participantRepository)
        {
            _participantRepository = participantRepository;
        }

        public async Task AddParticipantAsync(Participant newParticipant)
        {
            try
            {
                Log.Information("Calling AddParticipantAsync from participantRepository at {Time}", DateTime.UtcNow);
                await _participantRepository.AddParticipantAsync(newParticipant);
            }
            catch (Exception ex)
            {
                Log.Error("Error calling AddParticipantAsync from participantRepository: {Error}", ex.Message);
                throw;
            }
        }

        public async Task DeleteParticipantAsync(int participantId)
        {
            try
            {
                Log.Information("Calling DeleteParticipantAsync from participantRepository at {Time}", DateTime.UtcNow);
                await _participantRepository.DeleteParticipantAsync(participantId);
            }
            catch (Exception ex)
            {
                Log.Error("Error calling DeleteParticipantAsync from participantRepository: {Error}", ex.Message);
                throw;
            }
        }

        public async Task<Participant> GetParticipantByPersonalIdAsync(string personalId)
        {
            try
            {
                Log.Information("Calling GetParticipantByPersonalIdAsync from participantRepository at {Time}", DateTime.UtcNow);
                return await _participantRepository.GetParticipantByPersonalIdAsync(personalId);
            }
            catch (Exception ex)
            {
                Log.Error("Error calling GetParticipantByPersonalIdAsync from participantRepository: {Error}", ex.Message);
                throw;
            }
        }

        public async Task<List<Participant>> GetParticipantsAsync()
        {
            try
            {
                Log.Information("Calling GetParticipantsAsync from participantRepository at {Time}", DateTime.UtcNow);
                return await _participantRepository.GetParticipantsAsync();
            }
            catch (Exception ex)
            {
                Log.Error("Error calling GetParticipantsAsync from participantRepository: {Error}", ex.Message);
                throw;
            }
        }

        public async Task UpdateParticipantAsync(Participant updatedParticipant)
        {
            try
            {
                Log.Information("Calling UpdateParticipantAsync from participantRepository at {Time}", DateTime.UtcNow);
                await _participantRepository.UpdateParticipantAsync(updatedParticipant);
            }
            catch (Exception ex)
            {
                Log.Error("Error calling UpdateParticipantAsync from participantRepository: {Error}", ex.Message);
                throw;
            }
        }

        public Task<Participant> GenerateRandomParticipantAsync()
        {
            try
            {
                Log.Information("Calling GenerateRandomParticipantAsync from participantRepository at {Time}", DateTime.UtcNow);
                return _participantRepository.GenerateRandomParticipantAsync();
            }
            catch (Exception ex)
            {
                Log.Error("Error calling GenerateRandomParticipantAsync from participantRepository: {Error}", ex.Message);
                throw;
            }
        }
    }
}
