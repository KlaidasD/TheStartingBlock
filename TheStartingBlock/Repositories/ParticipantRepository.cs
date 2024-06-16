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
    public class ParticipantRepository : IParticipantRepository
    {
        private readonly ApplicationDbContext _context;
        private readonly IMongoRepository _mongoRepository;
        private readonly Random _random;
        public ParticipantRepository(ApplicationDbContext context, IMongoRepository mongoRepository)
        {
            _context = context;
            _mongoRepository = mongoRepository;
        }
        public async Task AddParticipantAsync(Participant newParticipant)
        {
            try
            {
                Log.Information("Adding participant with id {Id} at {Time}", newParticipant.ParticipantId, DateTime.UtcNow);
                _context.Participants.Add(newParticipant);
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                Log.Error("Error adding participant with id {Id}: {Error}", newParticipant.ParticipantId, ex.Message);
                throw;
            }
        }

        public async Task DeleteParticipantAsync(int participantId)
        {
            try
            {
                Log.Information("Deleting participant with id {Id} at {Time}", participantId, DateTime.UtcNow);
                var participantToDelete = await _context.Participants.FindAsync(participantId);
                if (participantToDelete != null)
                {
                    _context.Participants.Remove(participantToDelete);
                    await _context.SaveChangesAsync();
                }
            }
            catch (Exception ex)
            {
                Log.Error("Error deleting participant with id {Id}: {Error}", participantId, ex.Message);
                throw;
            }
        }

        public async Task<Participant> GetParticipantByPersonalIdAsync(string personalId)
        {
            try
            {
                Log.Information("Getting participant with PersonalId {PersonalId} from MongoDB at {Time}", personalId, DateTime.UtcNow);

                // Try to get participant from MongoDB
                var participant = await _mongoRepository.GetParticipantByPersonalIdAsync(personalId);

                if (participant == null)
                {
                    Log.Information("Participant not found in MongoDB, trying to search in MSSQL");

                    // If not found in MongoDB, search in MSSQL
                    participant = await _context.Participants.FirstOrDefaultAsync(p => p.PersonalCode == personalId);

                    if (participant != null)
                    {
                        Log.Information("Participant found in MSSQL, adding to MongoDB");
                        await _mongoRepository.AddParticipantAsync(participant);
                    }
                }

                return participant; // Return the found participant or null if not found
            }
            catch (Exception ex)
            {
                Log.Error("Error getting participant with PersonalId {PersonalId}: {Error}", personalId, ex.Message);
                throw;
            }
        }

        public async Task<List<Participant>> GetParticipantsAsync()
        {
            try
            {
                Log.Information("Getting all participants from mongoDB at {Time}", DateTime.UtcNow);
                var participants = await _mongoRepository.GetParticipantsAsync();
                if (participants == null || participants.Count == 0)
                {
                    Log.Information("Participants not found in mongoDB, trying to get from MSSQL");
                    participants = await _context.Participants.ToListAsync();
                    if (participants != null)
                    {
                        Log.Information("Participants found in MSSQL, adding to mongoDB");
                        foreach(var p in participants)
                        {
                            await _mongoRepository.AddParticipantAsync(p);
                        }
                    }
                }
                Log.Information("Returning participants");
                return await _context.Participants.ToListAsync();
            }
            catch (Exception ex)
            {
                Log.Error("Error getting participants: {Error}", ex.Message);
                throw;
            }
        }

        public Task UpdateParticipantAsync(Participant updatedParticipant)
        {
            try
            {
                Log.Information("Updating participant with id {Id} at {Time}", updatedParticipant.ParticipantId, DateTime.UtcNow);
                _context.Participants.Update(updatedParticipant);
                return _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                Log.Error("Error updating participant with id {Id}: {Error}", updatedParticipant.ParticipantId, ex.Message);
                throw;
            }
        }

        public async Task<Participant> GenerateRandomParticipantAsync()
        {
            var _random = new Random();

            try
            {
                Log.Information("Generating random participant at {Time}", DateTime.UtcNow);
                string[] maleFirstNames = { "John", "Michael", "David", "James", "Robert" };
                string[] femaleFirstNames = { "Mary", "Patricia", "Jennifer", "Linda", "Elizabeth" };
                string[] lastNames = { "Smith", "Johnson", "Williams", "Brown", "Jones" };

                // Generate random gender
                string gender = _random.Next(2) == 0 ? "Male" : "Female";

                // Generate random first name based on gender
                string firstName = gender == "Male" ? maleFirstNames[_random.Next(maleFirstNames.Length)] : femaleFirstNames[_random.Next(femaleFirstNames.Length)];

                // Generate random last name
                string lastName = lastNames[_random.Next(lastNames.Length)];

                // Combine first and last name
                string fullName = $"{firstName} {lastName}";

                // Generate random birth year (e.g., between 1950 and 2005)
                int birthYear = _random.Next(1950, 2006);

                // Generate random personal code (if applicable)
                string personalCode = Guid.NewGuid().ToString().Substring(0, 8);

                var newParticipant = new Participant
                {
                    Name = fullName,
                    BirthYear = birthYear,
                    Gender = gender,
                    PersonalCode = personalCode
                };

                _context.Participants.Add(newParticipant);
                await _context.SaveChangesAsync();

                return newParticipant;
            }
            catch (Exception ex)
            {
                Log.Error("Error generating random participant: {Error}", ex.Message);
                throw;
            }
        }
    }
}
