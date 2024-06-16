using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TheStartingBlock.Models
{
    public class ResultInputModel
    {
        public int EventId { get; set; }
        public int ParticipantId { get; set; }
        public int ResultId { get; set; }
        public decimal ResultValue { get; set; }
    }
}
