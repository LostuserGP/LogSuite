using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogSuite.Shared.Models
{
    public class RatingGroupDTO
    {
        public int Id { get; set; }
        public int Number { get; set; }
        public long GroupLimit { get; set; }
        public long GroupLimitBank { get; set; }
        public ICollection<RatingDTO> Ratings { get; set; }
    }
}
