using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace LogSuite.DataAccess.Operativka
{
    public class GisCountryResource
    {
        public int Id { get; set; }
        public int GisCountryId { get; set; }
        public GisCountry GisCountry { get; set; }
        public DateTime Month { get; set; }
        [Column(TypeName = "decimal(16, 8)")]
        public decimal Value { get; set; }
    }
}
