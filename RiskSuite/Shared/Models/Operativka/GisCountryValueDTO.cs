using System;

namespace LogSuite.Shared.Models.DailyReview
{
    public class GisCountryValueDTO
	{
        public int Id { get; set; }
        public int GisCountryId { get; set; }
        public GisCountryDTO GisCountry { get; set; }
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
    }
}
