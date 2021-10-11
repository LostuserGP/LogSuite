using LogSuite.Shared.Helpers;
using System;

namespace LogSuite.Shared.Models.DailyReview
{
    public class GisOutputValueDTO
    {
        public int Id { get; set; }
        public int GisId { get; set; }
        public GisDTO Gis { get; set; }
        public DateTime DateReport { get; set; }
        public decimal RequstedValue { get; set; }
        public int? RequestedValueTimeId { get; set; }
        public InputFileLogDTO RequestedValueTime { get; set; }
        public decimal AllocatedValue { get; set; }
        public int? AllocatedValueTimeId { get; set; }
        public InputFileLogDTO AllocatedValueTime { get; set; }
        public decimal EstimatedValue { get; set; }
        public int? EstimatedValueTimeId { get; set; }
        public InputFileLogDTO EstimatedValueTime { get; set; }
        public decimal FactValue { get; set; }
        public int? FactValueTimeId { get; set; }
        public InputFileLogDTO FactValueTime { get; set; }
        public string RequstedValueString
        {
            get => RequstedValue.ToString();
            set
            {
                RequstedValue = StringParser.TryGetDecimal(value);
            }
        }
        public string AllocatedValueString
        {
            get => AllocatedValue.ToString();
            set
            {
                AllocatedValue = StringParser.TryGetDecimal(value);
            }
        }
        public string EstimatedValueString
        {
            get => EstimatedValue.ToString();
            set
            {
                EstimatedValue = StringParser.TryGetDecimal(value);
            }
        }
        public string FactValueString
        {
            get => FactValue.ToString();
            set
            {
                FactValue = StringParser.TryGetDecimal(value);
            }
        }
    }
}
