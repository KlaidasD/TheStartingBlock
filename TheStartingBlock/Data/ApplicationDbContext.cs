using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TheStartingBlock.Models;
using TheStartingBlock.Models.Enums;


namespace TheStartingBlock.Data
{
    public class ApplicationDbContext : DbContext
    {
        
        public DbSet<Event> Events { get; set; }
        public DbSet<Participant> Participants { get; set; }
        public DbSet<Result> Results { get; set; }
        public DbSet<EventParticipants> EventParticipants { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Event>(entity =>
            {
                entity.HasKey(e => e.EventId);

                entity.Property(e => e.Category)
                    .HasConversion(
                        v => v.ToString(),
                        v => (EventCategory)Enum.Parse(typeof(EventCategory), v));

                entity.Property(e => e.Type)
                    .HasConversion(
                        v => v.ToString(),
                        v => (EventType)Enum.Parse(typeof(EventType), v));
            });
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Server=DESKTOP-9849SKM;Database=TheStartingBlockDB;Integrated Security=True;TrustServerCertificate=true;").EnableSensitiveDataLogging();
        }

    }
}
