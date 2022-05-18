using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogSuite.Shared.Models
{
    public class RatingDTO
    {
        public int Id { get; set; }
        public int Score { get; set; }
        public string Name { get; set; }
        public RatingGroupDTO RatingGroup { get; set; }
    }
}
