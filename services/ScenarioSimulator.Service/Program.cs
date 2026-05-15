using AIEnterpriseOS.ScenarioSimulator.Service.Services;
using AIEnterpriseOS.ScenarioSimulator.Service.Combined;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddSingleton<IScenarioEngine, InMemoryScenarioEngine>();
builder.Services.AddSingleton<ICombinedScenarioEngine, CombinedScenarioEngine>();

var app = builder.Build();

app.UseSwagger();
app.UseSwaggerUI();

app.MapControllers();

app.Run();
