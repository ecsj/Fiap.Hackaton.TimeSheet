﻿namespace Domain.Requests;

public class Filter
{
    public int Month { get; set; } = DateTime.UtcNow.Month;
    public int Year { get; set; } = DateTime.UtcNow.Year;

}
