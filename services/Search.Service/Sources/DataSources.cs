using AIEnterpriseOS.Search.Service.Models;

namespace AIEnterpriseOS.Search.Service.Sources;

public interface IDataSource
{
    IEnumerable<SearchResult> Search(string query);
}

public class CustomerHubSource : IDataSource
{
    public IEnumerable<SearchResult> Search(string query)
    {
        if (query.Length < 3) yield break;

        yield return new SearchResult
        {
            Id = "cust-1",
            Module = "CustomerHub.Service",
            Title = "Cliente Rossi SRL",
            Snippet = "Contatto, note, messaggi recenti",
            Score = 0.8
        };
    }
}

public class FinanceSource : IDataSource
{
    public IEnumerable<SearchResult> Search(string query)
    {
        if (!query.Any(char.IsDigit)) yield break;

        yield return new SearchResult
        {
            Id = "inv-123",
            Module = "Finance.Service",
            Title = "Fattura 123",
            Snippet = "Totale 1.200€, cliente Rossi SRL",
            Score = 0.9
        };
    }
}

public class ProjectSource : IDataSource
{
    public IEnumerable<SearchResult> Search(string query)
    {
        if (query.Contains("progetto", StringComparison.OrdinalIgnoreCase))
        {
            yield return new SearchResult
            {
                Id = "proj-1",
                Module = "Project.Service",
                Title = "Progetto Carpenteria 2024",
                Snippet = "Task, costi, avanzamento",
                Score = 0.7
            };
        }
    }
}
