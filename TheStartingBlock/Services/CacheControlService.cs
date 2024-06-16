using TheStartingBlock.Models;
using MongoDB.Driver;
using Serilog;

namespace TheStartingBlock.Services
{
    public class CacheControlService
    {
        private readonly IMongoDatabase _database;

        public CacheControlService(IMongoClient mongoClient)
        {
            _database = mongoClient.GetDatabase("TheStartingBlock");
        }

        public async Task RunCleanupJob()
        {
            while (true)
            {
                try
                {
                    Log.Information("Deleting Events cache");
                    await _database.GetCollection<Event>("Events").DeleteManyAsync(_ => true);
                    Log.Information("Deleting Participants cache");
                    await _database.GetCollection<Participant>("Participants").DeleteManyAsync(_ => true);
                    Log.Information("Deleting Results cache");
                    await _database.GetCollection<Result>("Results").DeleteManyAsync(_ => true);
                    Log.Information("Waiting for 2 minutes before running Cache cleaning");
                    await Task.Delay(TimeSpan.FromMinutes(2));
                }
                catch (Exception e)
                {
                    Log.Error(e, "Error while cleaning cache {Error}", e.Message);
                }

            }
        }
    }
}