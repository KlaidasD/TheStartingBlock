using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MongoDB.Driver;
using TheStartingBlock.Models;
using TheStartingBlock.Contracts;
using TheStartingBlock.Services;

namespace TheStartingBlock.Repositories
{
    public class MongoRepository : IMongoRepository
    {
        private readonly IMongoCollection<Event> _Events;
        private readonly IMongoCollection<Participant> _Participants;
        private readonly IMongoCollection<Result> _Results;
        private readonly IEventService _eventService;
        private readonly IParticipantService _participantService;
        private readonly IResultService _resultService;
        
        public MongoRepository(IMongoClient mongoClient)
        {
            var database = mongoClient.GetDatabase("TheStartingBlock");
            _Events = database.GetCollection<Event>("Events");
            _Participants = database.GetCollection<Participant>("Participants");
            _Results = database.GetCollection<Result>("Results");
        }

        public async Task AddEventAsync(Event newEvent)
        {
            await _Events.InsertOneAsync(newEvent);
        }

        public async Task AddParticipantAsync(Participant newParticipant)
        {
            await _Participants.InsertOneAsync(newParticipant);
        }

        public async Task AddResultAsync(Result newResult)
        {
            await _Results.InsertOneAsync(newResult);
        }

        public async Task<List<Event>> GetEventsAsync()
        {
            return await _Events.Find(events => true).ToListAsync();
        }

        public async Task<Event> GetEventByIdAsync(int eventId)
        {
            return await _Events.Find<Event>(events => events.EventId == eventId).FirstOrDefaultAsync();
        }

        public async Task<List<Participant>> GetParticipantsAsync()
        {
            return await _Participants.Find(participants => true).ToListAsync();
        }

        public async Task<Participant> GetParticipantByIdAsync(int participantId)
        {
            return await _Participants.Find<Participant>(participants => participants.ParticipantId == participantId).FirstOrDefaultAsync();
        }
       
        public async Task<List<Result>> GetResultsAsync()
        {
            return await _Results.Find(results => true).ToListAsync();
        }

        public async Task<Result> GetResultByIdAsync(int resultId)
        {
            return await _Results.Find<Result>(results => results.ResultId == resultId).FirstOrDefaultAsync();
        }
    }
}
