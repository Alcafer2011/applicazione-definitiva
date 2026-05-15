namespace AIEnterpriseOS.Project.Service.Scheduling;

public interface ISchedulingEngine
{
    int EstimateDuration(string title, string description);
    DateTime AutoSchedule(DateTime start, int hours);
}

public class BasicSchedulingEngine : ISchedulingEngine
{
    public int EstimateDuration(string title, string description)
    {
        return description.Length < 50 ? 4 : 12;
    }

    public DateTime AutoSchedule(DateTime start, int hours)
    {
        return start.AddHours(hours);
    }
}
