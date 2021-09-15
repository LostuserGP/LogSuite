using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogSuite.DataAccess.Operativka
{
    public class GisCountryValue
    {
        public int Id { get; set; }
        public int GisCountryId { get; set; }
        public GisCountry GisCountry { get; set; }
        public DateTime DateReport { get; set; }
        [Column(TypeName = "decimal(16, 8)")]
        public decimal RequstedValue { get; set; }
        public int? RequestedValueTimeId { get; set; }
        public InputFileLog RequestedValueTime { get; set; }
        [Column(TypeName = "decimal(16, 8)")]
        public decimal AllocatedValue { get; set; }
        public int? AllocatedValueTimeId { get; set; }
        public InputFileLog AllocatedValueTime { get; set; }
        [Column(TypeName = "decimal(16, 8)")]
        public decimal EstimatedValue { get; set; }
        public int? EstimatedValueTimeId { get; set; }
        public InputFileLog EstimatedValueTime { get; set; }
        [Column(TypeName = "decimal(16, 8)")]
        public decimal FactValue { get; set; }
        public int? FactValueTimeId { get; set; }
        public InputFileLog FactValueTime { get; set; }
    }
}
