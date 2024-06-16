using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TheStartingBlock.Models;

namespace TheStartingBlock.Contracts
{
    public interface IMongoRepository
    {
        Task<List<Event>> GetEventsAsync();
        Task<Event> GetEventByIdAsync(int eventId);
        Task AddEventAsync(Event newEvent);
        Task<List<Participant>> GetParticipantsAsync();
        Task<Participant> GetParticipantByPersonalIdAsync(string personalId);
        Task AddParticipantAsync(Participant newParticipant);
        Task<List<Result>> GetResultsAsync();
        Task<Result> GetResultByIdAsync(int resultId);
        Task AddResultAsync(Result newResult);
    }
}
