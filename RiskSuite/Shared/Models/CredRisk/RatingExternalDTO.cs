using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogSuite.Shared.Models
{
    public class RatingExternalDTO
    {
        public int Id { get; set; }
        public DateTime DateStart { get; set; }
        public int CounterpartyId { get; set; }
        public CounterpartyDTO Counterparty { get; set; }
        public int RatingAgencyId { get; set; }
        public RatingAgencyDTO RatingAgency { get; set; }
        public int RatingId { get; set; }
        public RatingDTO Rating { get; set; }
    }
}
