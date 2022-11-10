using Azure.Identity;
using Ofgem.API.BUS.Applications.Api;
using Ofgem.API.BUS.Applications.Core;
using Ofgem.API.BUS.Applications.Providers.DataAccess;
using Ofgem.API.BUS.BusinessAccounts.Client.ServiceExtensions;
using Ofgem.API.BUS.PropertyConsents.Client;
using Ofgem.Lib.BUS.AuditLogging.Extensions;
using Ofgem.Lib.BUS.Logging;
using System.Reflection;
using System.Text.Json.Serialization;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddOfgemCloudApplicationInsightsTelemetry();
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddHealthChecks();
builder.Services.AddApplicationsDataAccess(builder.Configuration);
builder.Services.AddHttpClient();
builder.Services.AddMvc()
    .AddApplicationPart(typeof(Ofgem.API.BUS.Applications.Api.Controllers.ApplicationsController).GetTypeInfo().Assembly);
builder.Services.AddAuditLoggingServices();

// Azure Key Vault configuration
builder.Configuration.AddAzureKeyVault(
    new Uri($"https://{builder.Configuration["KeyVaultName"]}.vault.azure.net/"),
    new DefaultAzureCredential());

builder.Services.AddApplicationsConfigurations(builder.Configuration);

builder.Services.AddBusinessAccountsAPI(builder.Configuration, "BusinessAccountsAPIBaseAddress");
builder.Services.AddPropertyConsentClient(builder.Configuration, "PropertyConsentAPIBaseAddress");

builder.Services.AddMvc().AddJsonOptions(x => x.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles);

// Addition of comment to test PR pipeline
var app = builder.Build();

app.Use((context, next) =>
{
    context.Request.EnableBuffering();
    return next(context);
});

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.MapHealthChecks("/health");
app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();
app.UseTelemetryMiddleware();
app.Run();
