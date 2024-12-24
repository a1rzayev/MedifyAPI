using MedifyAPI.Core.Enums;

namespace MedifyAPI.Core.Models;

public class WeekDayHours{
    public Guid Id { get; set; }
    public WeekDayEnum WeekDay {get; set;}
    public UInt16 StartHour {get; set;}
    public UInt16 StartMinute {get; set;}
    public UInt16 EndHour {get; set;}
    public UInt16 EndMinute {get; set;}
}