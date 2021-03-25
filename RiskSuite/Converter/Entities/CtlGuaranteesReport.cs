using System;
using System.Collections.Generic;

#nullable disable

namespace RiskSuite.Converter
{
    public partial class CtlGuaranteesReport
    {
        public int Id { get; set; }
        public int? DaughterId { get; set; }
        public string ReportDate { get; set; }
        public int? BankId { get; set; }
        public int? CounterpartyId { get; set; }
        public string Number { get; set; }
        public string StartDate { get; set; }
        public string FinishDate { get; set; }
        public int? CurrencyId { get; set; }
        public string FirstAmount { get; set; }
        public string StartMonthAmount { get; set; }
        public string ChangeAmount { get; set; }
        public string EndMonthAmount { get; set; }
        public string OperationAmount { get; set; }
        public string GuaranteeType { get; set; }
        public string BenificiaryName { get; set; }
        public int? BenificiaryCode { get; set; }
        public string ApprovalType { get; set; }
        public string ApprovalDate { get; set; }
        public string ApprovalNumber { get; set; }
        public string Note { get; set; }
    }
}
