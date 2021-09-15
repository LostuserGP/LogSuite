using System;
using System.Collections.Generic;
using System.Text;

namespace LogSuite.DataAccess.CredRisk
{
    public class GuaranteeApprovalDoc
    {
        public int Id { get; set; }
        public int GuaranteeId { get; set; }
        public Guarantee Guarantee { get; set; }
        public DateTime DateApproval { get; set; }
        public int GuaranteeApprovalDocTypeId { get; set; }
        public GuaranteeApprovalDocType GuaranteeApprovalDocType { get; set; }
        public string Number { get; set; }
    }
}
