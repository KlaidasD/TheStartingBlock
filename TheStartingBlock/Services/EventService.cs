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
    public class EventService : IEventService
    {
        private readonly IEventRepository _eventRepository;
        
        public EventService(IEventRepository eventRepository)
        {
            _eventRepository = eventRepository;
        }

        public async Task AddEventAsync(Event newEvent)
        {
            try
            {
                Log.Information("Calling AddEventAsync from eventRepository at {Time}", DateTime.UtcNow);
                await _eventRepository.AddEventAsync(newEvent);
            }
            catch (Exception ex)
            {
                Log.Error("Error calling AddEventAsync from eventRepository: {Error}", ex.Message);
                throw;
            }
        }

        public async Task<bool> AddParticipantToEventAsync(int eventId, int participantId)
        {
            try
            {
                Log.Information("Calling AddParticipantToEventAsync from eventRepository at {Time}", DateTime.UtcNow);
                return await _eventRepository.AddParticipantToEventAsync(eventId, participantId);
            }
            catch (Exception ex)
            {
                Log.Error("Error calling AddParticipantToEventAsync from eventRepository: {Error}", ex.Message);
                throw;
            }
        }

        public async Task DeleteEventAsync(int eventId)
        {
            try
            {
                Log.Information("Calling DeleteEventAsync from eventRepository at {Time}", DateTime.UtcNow);
                await _eventRepository.DeleteEventAsync(eventId);
            }
            catch (Exception ex)
            {
                Log.Error("Error calling DeleteEventAsync from eventRepository: {Error}", ex.Message);
                throw;
            }
        }

        public async Task<Event> GetEventByIdAsync(int eventId)
        {
            try
            {
                Log.Information("Calling GetEventByIdAsync from eventRepository at {Time}", DateTime.UtcNow);
                return await _eventRepository.GetEventByIdAsync(eventId);
            }
            catch (Exception ex)
            {
                Log.Error("Error calling GetEventByIdAsync from eventRepository: {Error}", ex.Message);
                throw;
            }
        }

        public async Task<List<Event>> GetEventsAsync()
        {
            try
            {
                Log.Information("Calling GetEventsAsync from eventRepository at {Time}", DateTime.UtcNow);
                return await _eventRepository.GetEventsAsync();
            }
            catch (Exception ex)
            {
                Log.Error("Error calling GetEventsAsync from eventRepository: {Error}", ex.Message);
                throw;
            }
        }

        public async Task UpdateEventAsync(Event updatedEvent)
        {
            try
            {
                Log.Information("Calling UpdateEventAsync from eventRepository at {Time}", DateTime.UtcNow);
                await _eventRepository.UpdateEventAsync(updatedEvent);
            }
            catch (Exception ex)
            {
                Log.Error("Error calling UpdateEventAsync from eventRepository: {Error}", ex.Message);
                throw;
            }
        }
    }
}
