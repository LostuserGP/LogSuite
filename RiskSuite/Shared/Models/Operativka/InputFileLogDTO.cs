using LogSuite.Shared.Authorization;
using System;

namespace LogSuite.Shared.Models.Operativka
{
    public class InputFileLogDTO
	{
        public int Id { get; set; }
        public string Filename { get; set; }
        public string UserId { get; set; }
        public UserDTO User { get; set; }
        public DateTime TimeInput { get; set; }
        public DateTime DateFile { get; set; }
        public DateTime TimeFile { get; set; }
    }
}
