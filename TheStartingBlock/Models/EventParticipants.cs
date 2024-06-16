using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace TheStartingBlock.Models
{
    public class EventParticipants
    {
        [Key]
        public int? RecordId { get; set; }
        [ForeignKey("EventId")]
        public Event EventParticipant { get; set; }
        [ForeignKey("ParticipantId")]
        public Participant Participant { get; set; }
        

        [BsonId]
        [NotMapped]
        [BsonIgnoreIfDefault]
        public ObjectId _id { get; set; }
    }
}
