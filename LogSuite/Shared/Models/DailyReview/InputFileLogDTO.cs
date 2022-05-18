using LogSuite.Shared.Authorization;
using System;

namespace LogSuite.Shared.Models.DailyReview
{
    public class InputFileLogDTO
	{
        public long Id { get; set; }
        public string Filename { get; set; }
        public string UserId { get; set; }
        public UserDTO User { get; set; }
        public DateTime InputTime { get; set; }
        public DateOnly FileDate { get; set; }
        public DateTime FileTime { get; set; }
    }
}
