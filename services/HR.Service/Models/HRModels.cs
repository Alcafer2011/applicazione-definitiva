namespace AIEnterpriseOS.HR.Service.Models;

public class Employee
{
    public string Id { get; set; } = "";
    public string FullName { get; set; } = "";
    public string Role { get; set; } = "";
    public bool External { get; set; }
}

public class HrEvent
{
    public string Id { get; set; } = "";
    public string EmployeeId { get; set; } = "";
    public DateTime Date { get; set; }
    public string Type { get; set; } = ""; // PRESENCE / ABSENCE / SICKNESS / INJURY / VACATION / PERMIT
    public string Notes { get; set; } = "";
}
