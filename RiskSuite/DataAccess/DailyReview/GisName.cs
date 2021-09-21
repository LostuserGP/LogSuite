using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogSuite.DataAccess.DailyReview
{
    public class GisName
    {
        public int Id { get; set; }
        public int GisId { get; set; }
        public Gis Gis { get; set; }
        public string Name { get; set; }
    }
}
