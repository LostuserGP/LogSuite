using RiskSuite.Shared;
using System;
using System.Collections.Generic;
using System.Text;

namespace RiskSuite.DataAccess.CredRisk
{
    public class FinancialStatementStandard : IReferenceName
    {
        public int Id { get; set; }
        public string Name { get; set; }
    }
}
