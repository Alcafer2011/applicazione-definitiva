namespace AIEnterpriseOS.Search.Service.Models;

public class SearchResult
{
    public string Id { get; set; } = string.Empty;
    public string Module { get; set; } = string.Empty;
    public string Title { get; set; } = string.Empty;
    public string Snippet { get; set; } = string.Empty;
    public double Score { get; set; }
}

public class SearchQuery
{
    public string Query { get; set; } = string.Empty;
}
