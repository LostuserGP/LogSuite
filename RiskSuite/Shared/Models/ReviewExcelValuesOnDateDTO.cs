using System;
using System.Collections.Generic;

namespace LogSuite.Shared.Models
{
    public class ReviewExcelValuesOnDateDTO
    {
        public DateTime DateReport { get; set; }
        public List<ReviewExcelValueDTO> Values { get; set; }
    }
}
