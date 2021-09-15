using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace LogSuite.DataAccess.Operativka
{
    public class GisInputValue
    {
        public int Id { get; set; }
        public int GisId { get; set; }
        public Gis Gis { get; set; }
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
