using System;
using System.ComponentModel.DataAnnotations;

namespace LogSuite.DataAccess.Operativka
{
    public class InputFileLog
    {
        public int Id { get; set; }
        public string Filename { get; set; }
        public string UserId { get; set; }
        public ApplicationUser User { get; set; }
        public DateTime TimeInput { get; set; }
        [DataType(DataType.Date)]
        public DateTime DateFile { get; set; }
        public DateTime TimeFile { get; set; }
    }
}
