using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace LogSuite.Shared.Models
{
    public class ReviewValueInputDTO
    {
        public enum InputType
        {
            Input,
            Output,
            Addon,
            Country
        }

        public enum ValueType
        {
            Requsted,
            Allocated,
            Estimated,
            Fact
        }

        public int GisId { get; set; }
        public InputType inType { get; set; }
        public ValueType valType { get; set; }
        public int ValueId { get; set; }
        public DateTime ReportDate { get; set; }
        [Column(TypeName = "decimal(8, 8)")]
        public double Value { get; set; }
    }
}
