using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace LogSuite.Shared.Models.DailyReview;

public class ReviewValueInput
{
    public enum InputType
    {
        Input,
        Output,
        Addon,
        Country,
        CountryAddon
    }

    public enum ValueType
    {
        Requested,
        Allocated,
        Estimated,
        Fact
    }

    public bool LikeValue(ReviewValueInput value)
    {
        return GisId == value.GisId
               && ValueId == value.ValueId
               && InType == value.InType
               && ValType == value.ValType
               && ReportDate == value.ReportDate;
    }

    public int GisId { get; set; }
    public InputType InType { get; set; }
    public ValueType ValType { get; set; }
    public int ValueId { get; set; }
    public DateOnly ReportDate { get; set; }
    [Column(TypeName = "decimal(16, 8)")]
    public double Value { get; set; }
}