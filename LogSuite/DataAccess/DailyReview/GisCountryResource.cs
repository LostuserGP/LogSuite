using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace LogSuite.DataAccess.DailyReview
{
    public class GisCountryResource
    {
        public int Id { get; set; }
        public int GisCountryId { get; set; }
        public GisCountry GisCountry { get; set; }
        public DateOnly Month { get; set; }
        [Column(TypeName = "decimal(16, 8)")]
        public decimal Value { get; set; }
        [NotMapped]
        public DateTime MonthTime
        {
            get => Month.ToDateTime(new TimeOnly(0));
            set => Month = DateOnly.FromDateTime(value);
        }
    }
}
