using System;
using System.Collections.Generic;

#nullable disable

namespace RiskSuite.Converter
{
    public partial class CtlRatingLimit
    {
        public int Id { get; set; }
        public int? GroupNumber { get; set; }
        public int? RatingId { get; set; }
        public string GroupLimit { get; set; }
        public string BankLimit { get; set; }
    }
}
