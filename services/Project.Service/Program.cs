using AIEnterpriseOS.Project.Service.Services;
using AIEnterpriseOS.Project.Service.Scheduling;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddSingleton<IProjectEngine, InMemoryProjectEngine>();
builder.Services.AddSingleton<ISchedulingEngine, BasicSchedulingEngine>();

var app = builder.Build();

app.UseSwagger();
app.UseSwaggerUI();

app.MapControllers();

app.Run();
