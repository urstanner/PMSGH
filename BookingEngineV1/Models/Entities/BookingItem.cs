using BookingEngineV1.Models.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace BookingEngineV1.Models
{
    [Table("tBookingItem")]
    public class BookingItem
    {
        [Key]
        public long BookingItemID { get; set; }
        public long BookingIDPMS { get; set; }
        public int ResourceTypeID { get; set; }
       
        public ResourceType ResourceType { get; set; }

        public RateComposition RateComposition { get; set; }
        public int RateCompositionID { get; set; }
        public DateTime DateOfArrival { get; set; }
        public DateTime DateOfDeparture { get; set; }
        public string GuestNames { get; set; }
        public int NumberOfUnits { get; set; }
        public decimal NotusedPricePerUnit { get; set; }
        public int NumberOfGuests { get; set; }
        public Booking Booking { get; set; }
        public List<BookingItemDay> BookingItemDays { get; set; }
        public List<BookingItemResourceAssignment> BookingItemResourceAssignments { get; set; }
        [NotMapped]
        public virtual List<Resource> ResourcesAvailableForAssignment { get; set; }

        public decimal AveragePricePerNight { get
            {
                return BookingItemDays.Average(x => x.PriceAfterTaxPerUnit);
            } }

        [NotMapped]
        public int NumberOfNights
        {
            get
            {
                return (int)(DateOfDeparture - DateOfArrival).TotalDays;
            }
        }

        [NotMapped]
        public decimal BaseOrDerivedPricePerUnit
        {
            get
            {
                return BookingItemDays.Sum(x => x.BaseOrDerivedPricePerUnit);
            }
        }

        [NotMapped]
        public decimal BaseOrDerivedPrice
        {
            get
            {
                return NumberOfUnits * BaseOrDerivedPricePerUnit;
            }
        }

        [NotMapped]
        public decimal LastMinutePromotionPerUnit
        {
            get
            {
                return BookingItemDays.Sum(x => x.LastMinutePromotionPerUnit);
            }
        }

        [NotMapped]
        public decimal EarlyBookingPromotionPerUnit
        {
            get
            {
                return BookingItemDays.Sum(x => x.EarlyBookingPromotionPerUnit);
            }
        }

        [NotMapped]
        public decimal LongStayPromotionPerUnit
        {
            get
            {
                return BookingItemDays.Sum(x => x.LongStayPromotionPerUnit);
            }
        }

        [NotMapped]
        public decimal TotalPromotionPerUnit
        {
            get
            {
                return BookingItemDays.Sum(x => x.TotalPromotionPerUnit);
            }
        }

        [NotMapped]
        public decimal TotalPromotion
        {
            get
            {
                return NumberOfUnits * TotalPromotionPerUnit;
            }
        }

        [NotMapped]
        public decimal PriceBeforeTaxPerUnit
        {
            get
            {
                return PriceAfterTaxPerUnit - VATPerUnit;
            }
        }

        [NotMapped]
        public decimal PriceBeforeTax
        {
            get
            {
                return NumberOfUnits * PriceBeforeTaxPerUnit;
            }
        }

        [NotMapped]
        public decimal VATPercentage
        {
            get
            {
                return 0.037M;
            }
        }

        [NotMapped]
        public decimal VATPerUnit
        {
            get
            {
                return BookingItemDays.Sum(x => x.VATPerUnit);
            }
        }

        [NotMapped]
        public decimal VAT
        {
            get
            {
                return NumberOfUnits * VATPerUnit;
            }
        }

        [NotMapped]
        public decimal PriceAfterTaxPerUnit
        {
            get
            {
                //return BaseOrDerivedPricePerUnit + TotalPromotionPerUnit;
                return this.BookingItemDays.Sum(x => x.PriceAfterTaxPerUnit);
            }
        }

        [NotMapped]
        public decimal PriceAfterTax
        {
            get
            {
                return NumberOfUnits * PriceAfterTaxPerUnit;
            }
        }

    }
}
