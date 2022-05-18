using System.Collections.Generic;

namespace LogSuite.Shared.Models.CredRisk
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
