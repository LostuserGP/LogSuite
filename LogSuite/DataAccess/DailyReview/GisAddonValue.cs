using System;
using System.ComponentModel.DataAnnotations.Schema;
using LogSuite.Shared.Models;

namespace LogSuite.DataAccess.DailyReview
{
    public class GisAddonValue : DayValue
    {
        public int GisAddonId { get; set; }
        public GisAddon GisAddon { get; set; }
    }
}
