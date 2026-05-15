using AIEnterpriseOS.Search.Service.Models;
using AIEnterpriseOS.Search.Service.Sources;

namespace AIEnterpriseOS.Search.Service.Services;

public interface ISearchEngine
{
    IEnumerable<SearchResult> Search(string query);
}

public class SearchEngine : ISearchEngine
{
    private readonly List<IDataSource> _sources;

    public SearchEngine()
    {
        _sources = new List<IDataSource>
        {
            new CustomerHubSource(),
            new FinanceSource(),
            new ProjectSource()
        };
    }

    public IEnumerable<SearchResult> Search(string query)
    {
        return _sources
            .SelectMany(s => s.Search(query))
            .OrderByDescending(r => r.Score);
    }
}
