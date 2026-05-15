using AIEnterpriseOS.ActionAI.Service.IntentEngine;
using AIEnterpriseOS.ActionAI.Service.AgentOrchestrator;
using AIEnterpriseOS.ActionAI.Service.WorkflowExecutor;
using AIEnterpriseOS.ActionAI.Service.MemoryConnector;
using AIEnterpriseOS.ActionAI.Service.SecurityLayer;
using AIEnterpriseOS.ActionAI.Service.SimulationLayer;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddSingleton<IIntentEngine, BasicIntentEngine>();
builder.Services.AddSingleton<IAgentOrchestrator, AgentOrchestrator>();
builder.Services.AddSingleton<IWorkflowExecutor, WorkflowExecutor>();
builder.Services.AddSingleton<IMemoryConnector, InMemoryMemoryConnector>();
builder.Services.AddSingleton<ISecurityLayer, BasicSecurityLayer>();
builder.Services.AddSingleton<ISimulationLayer, BasicSimulationLayer>();

var app = builder.Build();

app.UseSwagger();
app.UseSwaggerUI();

app.MapControllers();

app.Run();
