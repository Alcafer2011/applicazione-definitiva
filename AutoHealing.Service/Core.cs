using Docker.DotNet;
using Docker.DotNet.Models;
using AutoHealing.Service.Models;

namespace AutoHealing.Service.Core;

public class AutoHealingCore
{
    private readonly DockerClient _client;

    public AutoHealingCore()
    {
        _client = new DockerClientConfiguration(new Uri("unix:///var/run/docker.sock")).CreateClient();
    }

    public async Task<List<AutoHealing.Service.Models.ServiceStatus>> CheckAllAsync()
    {
        var result = new List<AutoHealing.Service.Models.ServiceStatus>();
        var containers = await _client.Containers.ListContainersAsync(new ContainersListParameters());

        foreach (var c in containers)
        {
            var state = c.State;
            result.Add(new AutoHealing.Service.Models.ServiceStatus
            {
                Name = c.Names.First().Trim('/'),
                State = state,
                LastCheck = DateTime.UtcNow
            });

            if (state != "running")
            {
                await RestartAsync(c.ID);
            }
        }

        return result;
    }

    public async Task RestartAsync(string containerId)
    {
        await _client.Containers.RestartContainerAsync(containerId, new ContainerRestartParameters());
    }
}