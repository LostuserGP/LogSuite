using System;
using System.Collections.Generic;
using System.Text;

namespace RiskSuite.DataAccess.CredRisk
{
    public class GuaranteeReport
    {
        public int Id { get; set; }
        public int GuaranteeId { get; set; }
        public Guarantee Guarantee { get; set; }
        public DateTime DateReport { get; set; }
        public DateTime? DateExpiration { get; set; }
        public long Amount { get; set; }
        public long AmountOperation { get; set; }

    }
}
