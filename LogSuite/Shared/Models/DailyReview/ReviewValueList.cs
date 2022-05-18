using System;
using System.Collections.Generic;

namespace LogSuite.Shared.Models.DailyReview;

public class ReviewValueList
{
    public List<ReviewValueInput> Values { get; set; }
    public string Filename { get; set; }
    public DateTime FileTimeStamp { get; set; }
    public string Option { get; set; }
}