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
        [BsonElement]
        [JsonPropertyName("id")]
        public int? ResultId { get; set; }
        [ForeignKey("eventId")]
        public Event Event { get; set; }
        [ForeignKey("participantId")]
        public Participant Participant { get; set; }
        public decimal ResultValue { get; set; }
        public int Position { get; set; }

        [BsonId]
        [NotMapped]
        [BsonIgnoreIfDefault]
        public ObjectId? _id { get; set; }
    }
}
