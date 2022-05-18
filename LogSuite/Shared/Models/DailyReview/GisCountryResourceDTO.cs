using LogSuite.Shared.Helpers;
using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.Globalization;

namespace LogSuite.Shared.Models.DailyReview
{
    public class GisCountryResourceDTO
	{
        public int Id { get; set; }
        public int GisCountryId { get; set; }
        public GisCountryDTO GisCountry { get; set; }
        public DateOnly Month { get; set; }
        [Column(TypeName = "decimal(16, 8)")]
        public decimal Value { get; set; }
        public string ValueString
        {
            get => Value.ToString(CultureInfo.CurrentCulture);
            set => Value = StringParser.TryGetDecimal(value);
        }
    }
}
