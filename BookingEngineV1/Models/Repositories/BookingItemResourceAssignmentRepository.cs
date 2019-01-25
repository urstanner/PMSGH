using BookingEngineV1.Models.Entities;
using BookingEngineV1.Models.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace BookingEngineV1.Models.Repositories
{
    public class BookingItemResourceAssignmentRepository : IBookingItemResourceAssignmentRepository
    {
        private readonly DataContext context;
        public BookingItemResourceAssignmentRepository(DataContext ctx) => context = ctx;

        public List<BookingItem> GetBookingItemsByArrivalDate(string companyID, DateTime dateOfArrival)
        {
            List<BookingItem> aBI = new List<BookingItem>();
            aBI = context.BookingItems
                .Where(x => x.DateOfArrival == dateOfArrival)
                .Include(x=>x.ResourceType)
                .Include(y=>y.RateComposition)
                .Include(a=>a.BookingItemResourceAssignments).ThenInclude(a=>a.Resource)
                .Include(b=>b.BookingItemDays)
                .Include(c=>c.Booking)
                .Where(x=>x.NumberOfUnits!=0)
                .ToList();

            foreach(BookingItem bi in aBI)
            {
                bi.ResourcesAvailableForAssignment = GetResourcesAvailableForAssignment(bi);
            }

            return aBI;
        }

        public List<BookingItemResourceAssignment> GetBookingItemResourceAssignments(string companyID, DateTime dateOfArrival)
        {
            List<BookingItemResourceAssignment> biRAs = new List<BookingItemResourceAssignment>();
            context.BookingItemResourceAssignments
                .Include(x => x.Resource).ToList();
   
                
            return biRAs;
        }

        public List<Resource> GetResourcesAvailableForAssignment(BookingItem bookingItem)
        {
            IQueryable<Resource> queryResourceLines;
            List<Resource> resourcesAvailable = new List<Resource>();
            

            //DateTime sDateOfArrival = dateOfArrival.ToString("yyyy-MM-dd");
            SqlParameter pDateOfArrival = new SqlParameter("@DateOfArrival", bookingItem.DateOfArrival);
            SqlParameter pDateOfDeparture = new SqlParameter("@DateOfDeparture", bookingItem.DateOfDeparture);
            SqlParameter pCompanyID = new SqlParameter("@CompanyID", bookingItem.Booking.CompanyID);
            string sqlResourcesAvailable = $@"select ResourceID, Name, CompanyID, ResourceTypeID, BedType, CapacityMax, Floor, SizeInM2, Description
                    from fnResourceAvailableForAssignment({pDateOfArrival},{pDateOfDeparture},{pCompanyID})";
            //RA
            //                    inner join tBookingItem BI on RA.BookingItemID = BI.BookingItemID
            //                    Where (BI.DateOfArrival <= '{dateOfArrival}' and BI.DateOfDeparture >= DateAdd(day, 1, '{dateOfArrival}'))  and CompanyID = {pCompanyID})";

            queryResourceLines = context.Resources.FromSql(sqlResourcesAvailable, pDateOfArrival, pDateOfDeparture, pCompanyID);
            queryResourceLines = queryResourceLines.Include(x => x.ResourceType).Where(y => y.ResourceTypeID == bookingItem.ResourceTypeID);
            return resourcesAvailable = queryResourceLines.ToList();
        }


        public List<BookingItemResourceAssignment> AssignResourceToBookingItem(int resourceID, long bookingItemID, string comment)
        {
            BookingItemResourceAssignment biRA = new BookingItemResourceAssignment()
            {
                BookingItemID = bookingItemID,
                ResourceID = resourceID,
                Comment = comment
            };

            context.BookingItemResourceAssignments.Add(biRA);
            context.SaveChanges();

            return context.BookingItemResourceAssignments.Include(x=>x.Resource).ToList();
        }

        public void RemoveBookignItemResourceAssignement(BookingItemResourceAssignment bookingItemResourceAssignment)
        {
            //BookingItemResourceAssignment ra = context.BookingItemResourceAssignments.Where(x => x.BookingItemResourceAssignmentID == bookingItemResourceAssignmentID).SingleOrDefault();
            context.BookingItemResourceAssignments.Remove(bookingItemResourceAssignment);
            context.SaveChanges();
        }

        public List<BookingItemResourceAssignment> GetDateEffectiveBookingItemResourceAssignments(string companyID, DateTime dateEffective)
        {

            IQueryable<BookingItemResourceAssignment> dateEffectiveResourceAssignments;
            //= new IQueryable<BookingItemResourceAssignment>();

            SqlParameter pDateEffective1 = new  SqlParameter("@DateEffective", dateEffective);
            SqlParameter pCompanyID1 = new SqlParameter("@CompanyID", companyID);

            string sql = $@"select BookingItemResourceAssignmentID, BookingItemID, ResourceID, Comment, BookingItemDayID
            from fnDateEffectiveBookingItemResourceAssignments({pCompanyID1}, {pDateEffective1})";
            dateEffectiveResourceAssignments = context.BookingItemResourceAssignments.FromSql(sql, pCompanyID1, pDateEffective1);
            pDateEffective1 = null;
            pCompanyID1 = null;
            return dateEffectiveResourceAssignments.Include(x => x.BookingItem).ThenInclude(b => b.Booking).ToList();
            //        .ThenInclude(BookingItem => BookingItem.BookingItemDays.Select(b=>b.DateEffective==dateEffective).FirstOrDefault()).ToList();
            //return dateEffectiveResourceAssignments;
        }

    }
}
