using LogSuite.Shared.Helpers;
using System;

namespace LogSuite.Shared.Models.DailyReview
{
    public class GisAddonValueDTO : DayValue
	{
        public int GisAddonId { get; set; }
        public GisAddonDTO GisAddon { get; set; }
    }
}
