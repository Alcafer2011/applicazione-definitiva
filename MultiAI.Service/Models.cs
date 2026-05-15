namespace MultiAI.Service.Models;

public class AIRequest
{
    public string Goal { get; set; } = "";
    public Dictionary<string, object> Context { get; set; } = new();
}

public class AIResponse
{
    public string Agent { get; set; } = "";
    public string Result { get; set; } = "";
}