using BookingEngineV1.Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BookingEngineV1.Models.Interfaces
{
    public interface IBookingItemResourceAssignmentRepository
    {
        List<BookingItemResourceAssignment> AssignResourceToBookingItem(int resourceID, long bookingItemID, string comment);
        List<BookingItemResourceAssignment> GetBookingItemResourceAssignments(string companyID, DateTime dateOfArrival);
        void RemoveBookignItemResourceAssignement(BookingItemResourceAssignment bookingItemResourceAssignment);
        List<Resource> GetResourcesAvailableForAssignment(BookingItem bookingItem);
        List<BookingItem> GetBookingItemsByArrivalDate(string companyID, DateTime dateOfArrival);
        List<BookingItemResourceAssignment> GetDateEffectiveBookingItemResourceAssignments(string companyID, DateTime dateEffective);
    }
}
