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
    public class EventController : Controller
    {
        private readonly IEventService _eventService;
        public EventController(IEventService eventService)
        {
            _eventService = eventService;
        }

        [HttpPost("AddEvent")]
        public async Task AddEvent([FromForm]Event newEvent)
        {
            try
            {
                Log.Information("Endpoint AddEvent called at {Time}", DateTime.UtcNow);
                await _eventService.AddEventAsync(newEvent);
            }
            catch (Exception ex)
            {
                Log.Error("Error calling endpoint AddEvent: {Error}", ex.Message);
                throw;
            }
        }

        [HttpGet("GetAllEvents")]
        public async Task<List<Event>> GetEvents()
        {
            try
            {
                Log.Information("Endpoint GetAllEvents called at {Time}", DateTime.UtcNow);
                return await _eventService.GetEventsAsync();
                
            }
            catch (Exception ex)
            {
                Log.Error("Error calling endpoint GetAllEvents: {Error}", ex.Message);
                throw;
            }
        }

        [HttpPost("GetEventById")]
        public async Task<Event> GetEventById([FromForm]int eventId)
        {
            try
            {
                Log.Information("Endpoint GetEventById called at {Time}", DateTime.UtcNow);
                return await _eventService.GetEventByIdAsync(eventId);
            }
            catch (Exception ex)
            {
                Log.Error("Error calling endpoint GetEventById: {Error}", ex.Message);
                throw;
            }
        }
       
        [HttpPut("UpdateEvent")]
        public async Task UpdateEvent([FromForm]Event updatedEvent)
        {
            try
            {
                Log.Information("Endpoint UpdateEvent called at {Time}", DateTime.UtcNow);
                await _eventService.UpdateEventAsync(updatedEvent);
            }
            catch (Exception ex)
            {
                Log.Error("Error calling endpoint UpdateEvent: {Error}", ex.Message);
                throw;
            }
        }

        [HttpDelete("DeleteEvent")]
        public async Task DeleteEvent([FromForm]int eventId)
        {
            try
            {
                Log.Information("Endpoint DeleteEvent called at {Time}", DateTime.UtcNow);
                await _eventService.DeleteEventAsync(eventId);
            }
            catch (Exception ex)
            {
                Log.Error("Error calling endpoint DeleteEvent: {Error}", ex.Message);
                throw;
            }
        }

        [HttpPost("AddParticipantToEvent")]
        public async Task<bool> AddParticipantToEvent([FromForm] int eventId,[FromForm] int participantId)
        {
            try
            {
                Log.Information("Endpoint AddParticipantToEvent called at {Time}", DateTime.UtcNow);
                return await _eventService.AddParticipantToEventAsync(eventId, participantId);
            }
            catch (Exception ex)
            {
                Log.Error("Error calling endpoint AddParticipantToEvent: {Error}", ex.Message);
                throw;
            }
        }
    } 
}
