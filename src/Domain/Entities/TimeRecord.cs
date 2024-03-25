namespace Domain.Entities;
public class TimeRecord
{
    public TimeRecord(string employeeId)
    {
        EmployeeId = employeeId;
        AddEntryTimeRecord();
    }
    public Guid Id { get; set; }
    public DateTime EntryTime { get; private set; }
    public DateTime ExitTime { get; private set; }
    public List<Intervals> Intervals { get; private set; } = new List<Intervals>();
    public TimeSpan TotalHours { get; private set; }
    public TimeSpan TotalHoursIntervals { get; private set; }
    public string EmployeeId { get; private set; }

    public void AddEntryTimeRecord()
    {
        EntryTime = DateTime.UtcNow.AddHours(-2);
    }

    public void AddEntryTimeInterval()
    {
        Intervals.Add(new Intervals()
        {
            EntryTime = DateTime.UtcNow,
            IntervalType = IntervalType.Lunch
        });

    }
    public void AddExitTimeInterval(Intervals intervals)
    {
        intervals.ExitTime = DateTime.UtcNow;

    }
    public void AddExitTimeRecord()
    {
        ExitTime = DateTime.UtcNow;

        CalculateTotalHours();
    }

    private void CalculateTotalHours()
    {
        TotalHours = ExitTime - EntryTime;

        if (Intervals != null)
        {
            foreach (var interval in Intervals)
            {
                if(interval.ExitTime is null) throw new Exception("Não foi possivel calcular as horas pois existe intervalo em aberto");

                TotalHoursIntervals = (interval.ExitTime ?? DateTime.UtcNow) - interval.EntryTime;
                TotalHours -= TotalHoursIntervals;
            }
        }
    }
}

public class Intervals
{
    public Guid Id { get; set; }
    public IntervalType IntervalType { get; set; }
    public DateTime EntryTime { get; set; }
    public DateTime? ExitTime { get; set; }
}

public enum IntervalType
{
    Lunch
}