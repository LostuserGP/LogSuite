using System;
using System.Collections.Generic;
using System.Text;

namespace RiskSuite.DataAccess.CredRisk
{
    public class RatingGroup
    {
        public int Id { get; set; }
        public int Number { get; set; }
        public long GroupLimit { get; set; }
        public long GroupLimitBank { get; set; }
        public ICollection<Rating> Ratings { get; set; }
    }
}
