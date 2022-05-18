using System;
using System.ComponentModel.DataAnnotations.Schema;
using LogSuite.Shared.Models.DailyReview;

namespace LogSuite.Shared.Models;

public class DayValue
{
    public long Id { get; set; }
    public DateOnly ReportDate { get; set; }
    [Column(TypeName = "decimal(16, 8)")]
    public decimal RequestedValue { get; set; }
    public long? RequestedValueTimeId { get; set; }
    public InputFileLogDTO RequestedValueTime { get; set; }
    [Column(TypeName = "decimal(16, 8)")]
    public decimal AllocatedValue { get; set; }
    public long? AllocatedValueTimeId { get; set; }
    public InputFileLogDTO AllocatedValueTime { get; set; }
    [Column(TypeName = "decimal(16, 8)")]
    public decimal EstimatedValue { get; set; }
    public long? EstimatedValueTimeId { get; set; }
    public InputFileLogDTO EstimatedValueTime { get; set; }
    [Column(TypeName = "decimal(16, 8)")]
    public decimal FactValue { get; set; }
    public long? FactValueTimeId { get; set; }
    public InputFileLogDTO FactValueTime { get; set; }
    [NotMapped]
    public DateTime ReportDateTime
    {
        get => ReportDate.ToDateTime(new TimeOnly(0));
        set => ReportDate = DateOnly.FromDateTime(value);
    }
}