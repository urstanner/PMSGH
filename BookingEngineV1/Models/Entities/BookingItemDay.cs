using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace BookingEngineV1.Models.Entities
{
    [Table("tBookingItemDay")]
    public class BookingItemDay
    {
        public long BookingItemDayID { get; set; }
        public long BookingItemID { get; set; }
        public BookingItem BookingItem { get; set; }
        public DateTime DateEffective { get; set; }
        public decimal BaseOrDerivedPricePerUnit { get; set; }
        public decimal TotalPromotionPerUnit { get; set; }
        public decimal PriceBeforeTaxPerUnit { get; set; }
        public decimal VATPercentage { get; set; }
        public decimal VATPerUnit { get; set; }
        public decimal PriceAfterTaxPerUnit { get; set; }
        public List<BookingItemDayPromotion> BookingItemDayPromotions { get; set; }
        [NotMapped]
        public List<BookingItemResourceAssignment> BookingItemResourceAssignments { get; set; }

        [NotMapped]
        public decimal LastMinutePromotionPerUnit
        {
            get
            {
                BookingItemDayPromotion biDP = new BookingItemDayPromotion();
                biDP = BookingItemDayPromotions.Where(x => x.PromotionTypeID == "PT02").SingleOrDefault();
                if (biDP != null)
                {
                    return biDP.Amount;
                }
                else
                {
                    return 0;
                }
            }
        }

        [NotMapped]
        public decimal EarlyBookingPromotionPerUnit
        {
            get
            {
                BookingItemDayPromotion biDP = new BookingItemDayPromotion();
                biDP = BookingItemDayPromotions.Where(x => x.PromotionTypeID == "PT01").SingleOrDefault();
                if (biDP != null)
                {
                    return biDP.Amount;
                }
                else
                {
                    return 0;
                }
            }
        }

        [NotMapped]
        public decimal LongStayPromotionPerUnit
        {
            get
            {
                BookingItemDayPromotion biDP = new BookingItemDayPromotion();
                biDP = BookingItemDayPromotions.Where(x => x.PromotionTypeID == "PT03").SingleOrDefault();
                if (biDP != null)
                {
                    return biDP.Amount;
                }
                else
                {
                    return 0;
                }
            }
        }

        [NotMapped]
        public decimal TotalPromotion
        {
            get
            {
                return TotalPromotionPerUnit * this.BookingItem.NumberOfUnits;
            }
        }

        [NotMapped]
        public decimal PriceBeforeTax
        {
            get
            {
                return PriceBeforeTaxPerUnit * this.BookingItem.NumberOfUnits;
            }
        }

        [NotMapped]
        public decimal PriceAfterTax
        {
            get
            {
                return PriceAfterTaxPerUnit * this.BookingItem.NumberOfUnits;
            }
        }


    }
}
