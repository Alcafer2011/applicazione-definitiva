using Docker.DotNet;
using Docker.DotNet.Models;
using AnalyticsHub.Service.Models;
using MongoDB.Driver;

namespace AnalyticsHub.Service.Core;

public class AnalyticsCore
{
    private readonly DockerClient _docker;
    private readonly IMongoCollection<MetricSnapshot> _metrics;

    public AnalyticsCore()
    {
        _docker = new DockerClientConfiguration(new Uri("unix:///var/run/docker.sock")).CreateClient();

        var client = new MongoClient("mongodb://mongo:27017");
        var db = client.GetDatabase("bos-analytics");
        _metrics = db.GetCollection<MetricSnapshot>("metrics");
    }

    public async Task<List<MetricSnapshot>> Collect()
    {
        var list = new List<MetricSnapshot>();
        var containers = await _docker.Containers.ListContainersAsync(new ContainersListParameters());

        foreach (var c in containers)
        {
            // var stats = await _docker.Containers.GetContainerStatsAsync(c.ID, new ContainerStatsParameters { Stream = false });

            var snap = new MetricSnapshot
            {
                Service = c.Names.First().Trim('/'),
                Cpu = 0, // stats.CPUStats.CPUUsage.TotalUsage,
                Memory = 0, // stats.MemoryStats.Usage,
                Uptime = (DateTime.UtcNow - c.Created).TotalSeconds
            };

            list.Add(snap);
            await _metrics.InsertOneAsync(snap);
        }

        return list;
    }

    public async Task<List<MetricSnapshot>> Latest()
    {
        return await _metrics.Find(_ => true)
            .SortByDescending(m => m.Timestamp)
            .Limit(50)
            .ToListAsync();
    }
}