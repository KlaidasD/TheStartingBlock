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
using MongoDB.Driver;


namespace TheStartingBlock.Repositories
{
    public class ResultRepository : IResultRepository
    {
        private readonly ApplicationDbContext _context;
        private readonly IMongoRepository _mongoRepository;
        public ResultRepository(ApplicationDbContext context, IMongoRepository mongoRepository)
        {
            _context = context;
            _mongoRepository = mongoRepository;
        }
        public async Task AddResultAsync(ResultInputModel newResultInput)
        {
            try
            {
                if (newResultInput == null || newResultInput.ResultValue <= 0)
                {
                    throw new ArgumentException("Invalid result input");
                }

                var @event = await _context.Events.FindAsync(newResultInput.EventId);
                var participant = await _context.Participants.FindAsync(newResultInput.ParticipantId);
                if (@event == null || participant == null)
                {
                    throw new ArgumentException("Event or participant not found");
                }

                var newResult = new Result
                {
                    Event = @event,
                    Participant = participant,
                    ResultValue = newResultInput.ResultValue,
                    Position = 1
                };

                _context.Results.Add(newResult);
                await _context.SaveChangesAsync();

                await UpdatePositionsForEventAsync(newResultInput.EventId);
            }
            catch (Exception ex)
            {
                Log.Error("Error adding result: {Error}", ex.Message);
                throw;
            }
        }

        public async Task UpdatePositionsForEventAsync(int eventId)
        {
            try
            {
                var eventType = await _context.Events
                    .Where(e => e.EventId == eventId)
                    .Select(e => e.Type)
                    .FirstOrDefaultAsync();

                var results = await _context.Results
                    .Where(r => r.Event.EventId == eventId)
                    .ToListAsync();

                switch (eventType)
                {
                    case EventType._1KM:
                        results = results.OrderBy(r => r.ResultValue).ToList();
                        break;
                    case EventType._5KM:
                        results = results.OrderBy(r => r.ResultValue).ToList();
                        break;
                    case EventType._10KM:
                        results = results.OrderBy(r => r.ResultValue).ToList();
                        break;
                    case EventType.HalfMarathon:
                        results = results.OrderBy(r => r.ResultValue).ToList();
                        break;
                    case EventType.Marathon:
                        results = results.OrderBy(r => r.ResultValue).ToList();
                        break;
                    case EventType.HighJump:
                        results = results.OrderByDescending(r => r.ResultValue).ToList();
                        break;
                    case EventType.LongJump:
                        results = results.OrderByDescending(r => r.ResultValue).ToList();
                        break;
                    default:
                        Log.Information("No sorting logic defined for event type {Type}", eventType);
                        break;
                }

                for (int i = 0; i < results.Count; i++)
                {
                    results[i].Position = i + 1;
                }

                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                Log.Error("Error updating positions for event with id {Id}: {Error}", eventId, ex.Message);
                throw;
            }
        }

        public async Task DeleteResultAsync(int resultId)
        {
            try
            {
                Log.Information("Deleting result with id {Id} at {Time}", resultId, DateTime.UtcNow);
                var resultToDelete = await _context.Results.FindAsync(resultId);
                if (resultToDelete != null)
                {
                    _context.Results.Remove(resultToDelete);
                    await _context.SaveChangesAsync();
                }
            }
            catch (Exception ex)
            {
                Log.Error("Error deleting result with id {Id}: {Error}", resultId, ex.Message);
                throw;
            }
        }

        public async Task<Result> GetResultByIdAsync(int resultId)
        {
            try
            {
                    Log.Information("Results not found in mongoDB trying to search in MSSQL");
                    var results = await _context.Results.Include(x => x.Event).Include(x => x.Participant).FirstOrDefaultAsync(x => x.ResultId == resultId);

                return results;
            }
            catch (Exception ex)
            {
                Log.Error("Error getting result with id {Id}: {Error}", resultId, ex.Message);
                throw;
            }
        }

        public async Task<List<Result>> GetResultsAsync()
        {
            try
            {
                    Log.Information("Trying to get results from MSSQL");
                    var results = await _context.Results.ToListAsync();
                    if (results != null)
                    {
                        Log.Information("Results not found in mongoDB, trying to get from MSSQL");

                        results = await _context.Results
                            .Include(r => r.Event)
                            .Include(r => r.Participant)
                            .ToListAsync();
                    }
                return results;
            }
            catch (Exception ex)
            {
                Log.Error("Error getting results: {Error}", ex.Message);
                throw;
            }
        }

        public async Task UpdateResultAsync(ResultInputModel updatedResultInput)
        {
            try
            {
                Log.Information("Updating result with id {Id} at {Time}", updatedResultInput.ResultId, DateTime.UtcNow);

                var existingResult = await _context.Results
                    .Include(r => r.Event)
                    .Include(r => r.Participant)
                    .FirstOrDefaultAsync(r => r.ResultId == updatedResultInput.ResultId);

                if (existingResult == null)
                {
                    throw new ArgumentException($"Result with id {updatedResultInput.ResultId} not found");
                }

                existingResult.ResultValue = updatedResultInput.ResultValue;

                if (updatedResultInput.EventId != null)
                {
                    var @event = await _context.Events.FindAsync(updatedResultInput.EventId);
                    if (@event == null)
                    {
                        throw new ArgumentException($"Event with id {updatedResultInput.EventId} not found");
                    }
                    existingResult.Event = @event;
                }

                if (updatedResultInput.ParticipantId != null)
                {
                    var participant = await _context.Participants.FindAsync(updatedResultInput.ParticipantId);
                    if (participant == null)
                    {
                        throw new ArgumentException($"Participant with id {updatedResultInput.ParticipantId} not found");
                    }
                    existingResult.Participant = participant;
                }

                await UpdatePositionsForEventAsync(updatedResultInput.EventId);
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                Log.Error("Error updating result with id {Id}: {Error}", updatedResultInput.ResultId, ex.Message);
                throw;
            }
        }

        public async Task<string> GenerateReportAsync()
        {
            try
            {
                Log.Information("Generating report at {Time}", DateTime.UtcNow);
                var sb = new StringBuilder();

                var events = await _context.Events.ToListAsync();
                sb.AppendLine("Events Report:");
                foreach (var ev in events)
                {
                    sb.AppendLine($"Event ID: {ev.EventId}, Name: {ev.Name}, Start Date: {ev.StartDate}, Location: {ev.Location}");
                }
                sb.AppendLine();

                var participants = await _context.Participants.ToListAsync();
                sb.AppendLine("Participants Report:");
                foreach (var participant in participants)
                {
                    sb.AppendLine($"Participant ID: {participant.ParticipantId}, Name: {participant.Name}, Birth Year: {participant.BirthYear}, Gender: {participant.Gender}");
                }
                sb.AppendLine();

                var results = await _context.Results.ToListAsync();
                sb.AppendLine("Results Report:");
                foreach (var result in results)
                {
                    sb.AppendLine($"Result ID: {result.ResultId}, Event ID: {result.Event.EventId}, Participant ID: {result.Participant.ParticipantId}, Position: {result.Position}");
                }

                return sb.ToString();
            }
            catch (Exception ex)
            {
                Log.Error("Error generating report: {Error}", ex.Message);
                throw;
            }
        }
    }
}

    


