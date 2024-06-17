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
    public class Result
    {
        [Key]
        [BsonElement("id")]
        [JsonPropertyName("id")]
        public int? ResultId { get; set; }
        [ForeignKey("eventId")]
        [BsonElement("eventId")]
        [JsonPropertyName("eventId")]
        public Event Event { get; set; }
        [ForeignKey("participantId")]
        [BsonElement("participantId")]
        [JsonPropertyName("participantId")]
        public Participant Participant { get; set; }
        [BsonElement("resultValue")]
        [JsonPropertyName("resultValue")]
        public decimal ResultValue { get; set; }
        [BsonElement("position")]
        [JsonPropertyName("position")]
        public int Position { get; set; }

        [BsonId]
        [NotMapped]
        [BsonIgnoreIfDefault]
        [JsonPropertyName("_id")]
        [BsonRepresentation(BsonType.ObjectId)]
        public ObjectId? id { get; set; }
    }
}
