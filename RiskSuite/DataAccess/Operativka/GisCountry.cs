using LogSuite.DataAccess.References;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogSuite.DataAccess.Operativka
{
    public class GisCountry
    {
        public int Id { get; set; }
        public int CountryId { get; set; }
        public Country Country { get; set; }
        public bool IsHidden { get; set; }
        public bool IsCalculated { get; set; }
        public int Multiplicator { get; set; }
        public List<GisCountryResource> Resources { get; set; } = new List<GisCountryResource>();
        public int GisId { get; set; }
        public Gis Gis { get; set; }
        public List<GisCountryValue> Values { get; set; } = new List<GisCountryValue>();
    }
}
