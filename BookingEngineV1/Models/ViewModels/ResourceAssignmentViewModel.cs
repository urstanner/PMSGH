using BookingEngineV1.Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BookingEngineV1.Models.ViewModels
{
    public class ResourceAssignmentViewModel
    {
        public DateTime DateOfArrival { get; set; }
        public string CompanyID { get; set; }
        public List<BookingItem> BookingItemsByArrivalDate { get; set; }
        public List<Resource> ResourcesAvailableForAssignment { get; set; }
        public List<BookingItemResourceAssignment> BookingItemsResourceAssignments { get;set;}

    }
}
