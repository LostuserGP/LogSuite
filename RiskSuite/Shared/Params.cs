using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogSuite.Shared
{
    public class Params
    {
        private const int MaxPageSize = 100;
        public int PageNumber { get; set; } = 1;
        private int pageSize = 50;
        public int PageSize
        {
            get { return pageSize; }
            set { pageSize = (value > MaxPageSize) ? MaxPageSize : value; }
        }
        public string FilterColumn { get; set; }
        public string Filter { get; set; }
        public string Order { get; set; }
        public bool OrderAsc { get; set; }
        public string Type { get; set; }
        public string Status { get; set; }
    }
}
