using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace BookingEngineV1.Models.Entities
{
    [Table("tBookingItemResourceAssignment")]
    public class BookingItemResourceAssignment
    {
        public long BookingItemResourceAssignmentID { get; set; }
        public long BookingItemID { get; set; }
        public BookingItem BookingItem { get; set; }
        public int ResourceID { get; set; }
        public Resource Resource { get; set; }
        public string Comment { get; set; }
        
    }
}
