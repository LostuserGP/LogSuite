using System;
using System.Collections.Generic;
using System.Text;

namespace RiskSuite.DataAccess.CredRisk
{
    public class Rating
    {
        public int Id { get; set; }
        public int Score { get; set; }
        public string Name { get; set; }
        public int RatingGroupId { get; set; }
        public RatingGroup RatingGroup { get; set; }
    }
}
