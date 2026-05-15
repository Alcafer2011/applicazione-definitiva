using MultiAI.Service.Models;

namespace MultiAI.Service.Core;

public interface IAgent
{
    string Name { get; }
    bool CanHandle(string goal);
    string Execute(AIRequest request);
}