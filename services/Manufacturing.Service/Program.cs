using AIEnterpriseOS.Manufacturing.Service.BOM;
using AIEnterpriseOS.Manufacturing.Service.Routing;
using AIEnterpriseOS.Manufacturing.Service.WorkOrders;
using AIEnterpriseOS.Manufacturing.Service.Capacity;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddSingleton<IBomEngine, InMemoryBomEngine>();
builder.Services.AddSingleton<IRoutingEngine, InMemoryRoutingEngine>();
builder.Services.AddSingleton<IWorkOrderEngine, InMemoryWorkOrderEngine>();
builder.Services.AddSingleton<ICapacityEngine, BasicCapacityEngine>();

var app = builder.Build();

app.UseSwagger();
app.UseSwaggerUI();

app.MapControllers();

app.Run();
