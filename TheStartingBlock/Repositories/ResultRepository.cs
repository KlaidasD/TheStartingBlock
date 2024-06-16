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
    public class ResultRepository : IResultRepository
    {
        private readonly ApplicationDbContext _context;
        private readonly IMongoRepository _mongoRepository;
        public ResultRepository(ApplicationDbContext context, IMongoRepository mongoRepository)
        {
            _context = context;
            _mongoRepository = mongoRepository;
        }
        public async Task AddResultAsync(Result newResult)
        {
            try
            {
                Log.Information("Adding new result with {Id} at {Time}", newResult.ResultId, DateTime.UtcNow);
                _context.Results.Add(newResult);
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                Log.Error("Error adding result with id {Id}: {Error}", newResult.ResultId, ex.Message);
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
                Log.Information("Trying to get results with id {Id} from mongoDB at {Time}", resultId, DateTime.UtcNow);
                var results = await _mongoRepository.GetResultByIdAsync(resultId);
                if (results == null)
                {
                    Log.Information("Results not found in mongoDB trying to search in MSSQL");
                    results = await _context.Results.FindAsync(resultId);
                    if (results != null)
                    {
                        Log.Information("Results found in MSSQL, adding to mongoDB");
                        await _mongoRepository.AddResultAsync(results);
                    }
                }
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
                Log.Information("Trying to get results from mongoDB at {Time}", DateTime.UtcNow);
                var results = await _mongoRepository.GetResultsAsync();
                if (results == null || results.Count == 0)
                {
                    Log.Information("Results not found in mongoDB, trying to get from MSSQL");
                    results = await _context.Results.ToListAsync();
                    if (results != null)
                    {
                        Log.Information("Results found in MSSQL, adding to mongoDB");
                        foreach (var r in results)
                        {
                            await _mongoRepository.AddResultAsync(r);
                        }
                    }
                }
                return results;
            }
            catch (Exception ex)
            {
                Log.Error("Error getting results: {Error}", ex.Message);
                throw;
            }
        }

        public async Task UpdateResultAsync(Result updatedResult)
        {
            try
            {
                Log.Information("Updating result with id {Id} at {Time}", updatedResult.ResultId, DateTime.UtcNow);
                _context.Results.Update(updatedResult);
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                Log.Error("Error updating result with id {Id}: {Error}", updatedResult.ResultId, ex.Message);
                throw;
            }
        }

        public async Task<bool> GenerateRandomResultsAsync(int eventId, List<int> participantIds)
        {
            var random = new Random();

            try
            {
                Log.Information("Generating random results for event {EventId} at {Time}", eventId, DateTime.UtcNow);
                foreach (int participantId in participantIds)
                {
                    // Generate random result value (e.g., for running events)
                    double randomResultValue = 0.0;
                    if (eventId == (int)EventType._1KM || eventId == (int)EventType._5KM || eventId == (int)EventType._10KM)
                    {
                        randomResultValue = random.NextDouble() * 10; // Random value between 0 and 10 (for example)
                    }
                    else if (eventId == (int)EventType.LongJump || eventId == (int)EventType.HighJump)
                    {
                        randomResultValue = random.NextDouble() * 5; // Random value between 0 and 5 (for example)
                    }
                    else if (eventId == (int)EventType.HalfMarathon)
                    {
                        randomResultValue = random.NextDouble() * 21; // Random value between 0 and 21 (for example)
                    }
                    else if (eventId == (int)EventType.Marathon)
                    {
                        randomResultValue = random.NextDouble() * 42; // Random value between 0 and 42 (for example)
                    }

                    var result = new Result
                    {
                        EventId = eventId,
                        ParticipantId = participantId,
                        ResultValue = (decimal)randomResultValue
                    };

                    _context.Results.Add(result);
                }
                return true;
            }
            catch (Exception ex)
            {
                Log.Error("Error generating random results: {Error}", ex.Message);
                throw;
            }

        }
    }
}

