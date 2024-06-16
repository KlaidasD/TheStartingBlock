using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace TheStartingBlock.Models
{
    public class Participant
    {
        [Key]
        [BsonElement]
        [JsonPropertyName("id")]
        public int ParticipantId { get; set; }
        [BsonElement]
        [JsonPropertyName("name")]
        public string Name { get; set; }
        [BsonElement]
        [JsonPropertyName("gender")]
        public string Gender { get; set; }
        [BsonElement]
        [JsonPropertyName("personalCode")]
        public string PersonalCode { get; set; }
        [BsonElement]
        [JsonPropertyName("birthYear")]
        public int BirthYear { get; set; }

        [BsonId]
        [NotMapped]
        [JsonIgnore]
        public ObjectId _id { get; set; }

        public List<EventParticipants> EventParticipants { get; set; } = new List<EventParticipants>();
        public List<Result> Results { get; set; } = new List<Result>();
    }
}
