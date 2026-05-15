using AIEnterpriseOS.Dashboard.Service.Services;
using AIEnterpriseOS.Dashboard.Service.Widgets;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddSingleton<ILayoutEngine, InMemoryLayoutEngine>();
builder.Services.AddSingleton<IWidgetEngine, WidgetEngine>();

var app = builder.Build();

app.UseSwagger();
app.UseSwaggerUI();

app.MapControllers();

app.Run();
