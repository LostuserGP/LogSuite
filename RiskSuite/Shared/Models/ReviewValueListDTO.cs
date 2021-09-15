using System.Collections.Generic;

namespace LogSuite.Shared.Models
{
    public class ReviewValueListDTO
    {
        public List<ReviewValueInputDTO> Values { get; set; }
        public string Filename { get; set; }
        public string Message { get; set; }
    }
}
