using BookingEngineV1.Models.DBQueries;
using BookingEngineV1.Models.Entities;
using BookingEngineV1.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BookingEngineV1.Models.Interfaces
{
    public interface IBookingRepository
    {

        List<Channel> GetChannels();
        List<RateComposition> GetRateCompositions(string companyID);
        List<ResourceType> GetResourceTypes(string companyID);
        Booking GetBookingDetails(long bookingIDPMS);

        List<Booking> GetBookings(BookingsSelection selection);

        Booking CreateBooking(string companyID, int channelID, string userID);
        Booking AddBookingItem(Booking booking, int resourceTypeID, int rateCompositionID, int numberOfUnits, int numberOfGuests, string guestNames, DateTime dateOfArrival, DateTime dateOfDeparture);
        List<BookingItemDay> AddBookingItemDays(BookingItem bookingItem);

        BookingItem IncreaseNumberOfUnits(BookingItem bookingItem, int newNumberOfUnits);
        BookingItem DecreaseNumberOfUnits(BookingItem bookingItem, int newNumberOfUnits);
        BookingItem UpgradeResourceType(BookingItem bookingItem, int newResourceTypeID);
        BookingItem DowngradeResourceType(BookingItem bookingItem, int newResourceTypeID);
        BookingItem ExtendStay(BookingItem bookingItem, DateTime newDateOfArrival, DateTime newDateOfDeparture);
        BookingItem ShortenStay(BookingItem bookingItem, DateTime newDateOfArrival, DateTime newDateOfDeparture);
        Cart MapBookingToCart(long bookingIDPMS);
        CartItem MapBookingItemToCartItem(BookingItem bi, long cartID);
        CartItemDay MapBookingItemDayToCartItemDay(BookingItemDay bid, long cartItemID);
    }
}
