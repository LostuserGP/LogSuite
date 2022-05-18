using LogSuite.Shared.Helpers;
using System;

namespace LogSuite.Shared.Models.DailyReview
{
    public class GisInputValueDTO : DayValue
    {
        public int GisId { get; set; }
        public GisDTO Gis { get; set; }
    }
}
