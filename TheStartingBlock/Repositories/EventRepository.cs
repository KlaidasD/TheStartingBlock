using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TheStartingBlock.Contracts;
using TheStartingBlock.Models;
using TheStartingBlock.Data;
using Microsoft.EntityFrameworkCore;
using Serilog;
using TheStartingBlock.Models.Enums;

namespace TheStartingBlock.Repositories
{
    public class EventRepository : IEventRepository
    {
        private readonly ApplicationDbContext _context;
        private readonly IMongoRepository _mongoRepository;
        public EventRepository(ApplicationDbContext context, IMongoRepository mongoRepository)
        {
            _context = context;
            _mongoRepository = mongoRepository;
        }

        public async Task AddEventAsync(Event newEvent)
        {
            try
            {
                Log.Information("Adding event at {Time}", DateTime.UtcNow);
                _context.Events.Add(newEvent);
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                Log.Error("Error adding event: {Error}", newEvent.EventId, ex.Message);
                throw;
            }
        }

        public async Task<bool> AddParticipantToEventAsync(int eventId, int participantId)
        {
            try
            {
                Log.Information("Adding participant to event with id {Id} at {Time}", eventId, DateTime.UtcNow);

                var @event = await _context.Events.FindAsync(eventId);
                var participant = await _context.Participants.FindAsync(participantId);

                if (@event == null || participant == null)
                {
                    Log.Information("Participant or event not found");
                    return false;
                }

                var existingParticipant = await _context.EventParticipants
                .Where(ep => ep.Participant.ParticipantId == participantId)
                .FirstOrDefaultAsync();

                if (existingParticipant != null)
                {
                    Log.Information("Participant already added to the event");
                    return false;
                }

                int participantAge = DateTime.UtcNow.Year - participant.BirthYear;

                switch (@event.Category)
                {
                    case EventCategory.Junior:
                        if (participantAge > 16)
                        {
                            Log.Information("Participant is too old for Junior category");
                            return false;
                        }
                        break;
                    case EventCategory.Mid:
                        if (!(participantAge >= 16 && participantAge <= 49))
                        {
                            Log.Information("Participant age does not fit Mid category");
                            return false;
                        }
                        break;
                    case EventCategory.Senior:
                        if (participantAge <= 49)
                        {
                            Log.Information("Participant is too young for Senior category");
                            return false;
                        }
                        break;
                    default:
                        return false;
                }

                var eventParticipant = new EventParticipants
                {
                    EventParticipant = @event,
                    Participant = participant
                };

                _context.EventParticipants.Add(eventParticipant);
                await _context.SaveChangesAsync();

                return true;
            }
            catch (Exception ex)
            {
                Log.Error("Error adding participant to event with id {Id}: {Error}", eventId, ex.Message);
                throw;
            }
        }

        public async Task DeleteEventAsync(int eventId)
        {
            try
            {
                Log.Information("Deleting event with id {Id} from MSSQL at {Time}", eventId, DateTime.UtcNow);
                var eventToDelete = await GetEventByIdAsync(eventId);
                if (eventToDelete != null)
                {
                    _context.Events.Remove(eventToDelete);
                    await _context.SaveChangesAsync();
                }
            }
            catch (Exception ex)
            {
                Log.Error("Error deleting event with id {Id}: {Error}", eventId, ex.Message);
                throw;
            }
        }

        public async Task<Event> GetEventByIdAsync(int eventId)
        {
            try
            {
                Log.Information("Getting event with id {Id} from MongoDB at {Time}", eventId, DateTime.UtcNow);
                var events = await _mongoRepository.GetEventByIdAsync(eventId);
                if (events == null)
                {
                    Log.Information("Event not found in MongoDB, trying to get from MSSQL");
                    events = await _context.Events.FindAsync(eventId);
                    if (events != null)
                    {
                        Log.Information("Event found in MSSQL, adding to MongoDB");
                        await _mongoRepository.AddEventAsync(events);
                    }
                }
                return await _context.Events.FindAsync(eventId);
            }
            catch (Exception ex)
            {
                Log.Error("Error getting event with id {Id}: {Error}", eventId, ex.Message);
                throw;
            }

        }

        public async Task<List<Event>> GetEventsAsync()
        {
            try
            {
                Log.Information("Getting all events from MongoDB at {Time}", DateTime.UtcNow);
                var events = await _mongoRepository.GetEventsAsync();
                if(events == null || events.Count == 0)
                {
                    Log.Information("No events found in mongoDB, trying to get from MSSQL");
                    events = await _context.Events.ToListAsync();
                    if(events != null && events.Count > 0)
                    {
                        Log.Information("Events found in MSSQL, adding to MongoDB");
                        foreach(var e in events)
                        {
                            await _mongoRepository.AddEventAsync(e);
                        }
                    }
                }
                Log.Information("Returning events");
                return events;
            }
            catch (Exception ex)
            {
                Log.Error("Error getting all events: {Error}", ex.Message);
                throw;
            }
        }

        public async Task UpdateEventAsync(Event updatedEvent)
        {
            try
            {
                Log.Information("Updating event with id {Id} at {Time}", updatedEvent.EventId, DateTime.UtcNow);
                _context.Events.Update(updatedEvent);
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                Log.Error("Error updating event with id {Id}: {Error}", updatedEvent.EventId, ex.Message);
                throw;
            }
        }
    }
}
