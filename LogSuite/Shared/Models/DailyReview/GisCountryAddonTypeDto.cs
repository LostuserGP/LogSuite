using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace LogSuite.Shared.Models.DailyReview;

public class GisCountryAddonTypeDto
{
    public int Id { get; set; }
    public int GisCountryAddonId { get; set; }
    public GisCountryAddonDto GisCountryAddon { get; set; }
    public DateOnly StartDate { get; set; }
    public bool IsCommGas { get; set; }
    [NotMapped]
    public DateTime StartDateTime
    {
        get => StartDate.ToDateTime(new TimeOnly(0));
        set => StartDate = DateOnly.FromDateTime(value);
    }
}