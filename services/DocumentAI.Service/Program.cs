using AIEnterpriseOS.DocumentAI.Service.Services;
using AIEnterpriseOS.DocumentAI.Service.OCR;
using AIEnterpriseOS.DocumentAI.Service.Classifiers;
using AIEnterpriseOS.DocumentAI.Service.Extractors;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddSingleton<IOcrEngine, FakeOcrEngine>();
builder.Services.AddSingleton<IDocumentClassifier, BasicDocumentClassifier>();
builder.Services.AddSingleton<IFieldExtractor, BasicFieldExtractor>();
builder.Services.AddSingleton<IDocumentEngine, InMemoryDocumentEngine>();

var app = builder.Build();

app.UseSwagger();
app.UseSwaggerUI();

app.MapControllers();

app.Run();
