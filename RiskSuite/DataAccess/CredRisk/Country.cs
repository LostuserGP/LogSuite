using System;
using System.Collections.Generic;
using System.Text;

namespace RiskSuite.DataAccess.CredRisk
{
    public class Country
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string NameEn { get; set; }
        public string ShortName { get; set; }
        public string ticker { get; set; }
    }
}
