using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using TheStartingBlock.Models.Enums;

namespace TheStartingBlock.Models
{
    public class Event
    {
        [Key]
        [BsonElement]
        [JsonPropertyName("id")]
        public int? EventId { get; set; }
        [BsonElement]
        [JsonPropertyName("name")]
        public string Name { get; set; }
        [BsonElement]
        [JsonPropertyName("startDate")]
        public DateTime StartDate { get; set; }
        [BsonElement]
        [JsonPropertyName("location")]
        public string Location { get; set; }
        [BsonElement]
        [JsonPropertyName("prizeInformation")]
        public string PrizeInformation { get; set; }
        [BsonElement]
        [JsonPropertyName("category")]
        [JsonConverter(typeof(JsonStringEnumConverter))]
        public EventCategory Category { get; set; }
        [BsonElement]
        [JsonPropertyName("type")]
        [JsonConverter(typeof(JsonStringEnumConverter))]
        public EventType Type { get; set; }
        [BsonElement]
        [JsonPropertyName("rules")]
        public string Rules { get; set; }
        [BsonElement]
        [JsonPropertyName("requirements")]
        public string Requirements { get; set; }

        [BsonId]
        [NotMapped]
        [BsonIgnoreIfDefault]
        public ObjectId? _id { get; set; }

        public List<EventParticipants> EventParticipants { get; set; } = new List<EventParticipants>();
        public List<Result> Results { get; set; } = new List<Result>();
    }

}
