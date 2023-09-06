using Microsoft.Extensions.Azure;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.


builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();


var azureConfig = builder.Configuration.GetSection("Azure").Get<AzureConfig>() ?? throw new Exception("Azure configuration is missing");
if (azureConfig == AzureConfig.Empty) throw new Exception("Azure configuration is missing or incomplete");

// See: https://learn.microsoft.com/en-us/dotnet/azure/sdk/dependency-injection?tabs=web-app-builder
builder.Services.AddAzureClients(azure => {
    azure.AddServiceBusClient(azureConfig.SharedServiceBus); // This is the default
    azure.AddServiceBusClient(azureConfig.PrivateServiceBus)
        .WithName("PrivateServiceBus");
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
