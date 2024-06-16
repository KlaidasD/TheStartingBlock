using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TheStartingBlock.Models;

namespace TheStartingBlock.Contracts
{
    public interface IParticipantRepository
    {
        Task<List<Participant>> GetParticipantsAsync();
        Task<Participant> GetParticipantByPersonalIdAsync(string personalId);
        Task AddParticipantAsync(Participant newParticipant);
        Task UpdateParticipantAsync(Participant updatedParticipant);
        Task DeleteParticipantAsync(int participantId);
        Task<Participant> GenerateRandomParticipantAsync();
    }
}
