using AIEnterpriseOS.Tenant.Service.Services;
using AIEnterpriseOS.Tenant.Service.Provisioning;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddSingleton<ITenantEngine, InMemoryTenantEngine>();
builder.Services.AddSingleton<IUsageEngine, InMemoryUsageEngine>();
builder.Services.AddSingleton<IMarketplaceEngine, InMemoryMarketplaceEngine>();
builder.Services.AddSingleton<IProvisioningEngine, BasicProvisioningEngine>();

var app = builder.Build();

app.UseSwagger();
app.UseSwaggerUI();

app.MapControllers();

app.Run();
