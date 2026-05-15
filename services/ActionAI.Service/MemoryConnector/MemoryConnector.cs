namespace AIEnterpriseOS.ActionAI.Service.MemoryConnector;

public interface IMemoryConnector
{
    void SaveDecision(string intent, string result);
}

public class InMemoryMemoryConnector : IMemoryConnector
{
    private readonly List<string> _memory = new();

    public void SaveDecision(string intent, string result)
    {
        _memory.Add($"{DateTime.UtcNow}: {intent} -> {result}");
    }
}
