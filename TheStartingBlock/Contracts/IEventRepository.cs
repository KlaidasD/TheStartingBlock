using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TheStartingBlock.Models;

namespace TheStartingBlock.Contracts
{
    public interface IEventRepository
    {
        Task<List<Event>> GetEventsAsync();
        Task<Event> GetEventByIdAsync(int eventId);
        Task AddEventAsync(Event newEvent);
        Task UpdateEventAsync(Event updatedEvent);
        Task DeleteEventAsync(int eventId);
        Task<bool> AddParticipantToEventAsync(int eventId, int participantId);
    }
}
