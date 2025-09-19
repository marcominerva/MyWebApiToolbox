using System.Text.Json.Serialization;
using EntityFramework.Exceptions.SqlServer;
using Microsoft.EntityFrameworkCore;
using MyToolbox.Api.Swagger;
using MyToolbox.BusinessLayer.Settings;
using MyToolbox.DataAccessLayer;
using TinyHelpers.AspNetCore.Extensions;
using TinyHelpers.AspNetCore.OpenApi;
using TinyHelpers.Json.Serialization;

var builder = WebApplication.CreateBuilder(args);
builder.Configuration.AddJsonFile("appsettings.local.json", optional: true, reloadOnChange: true);

// Add services to the container.
builder.Services.AddSingleton(TimeProvider.System);
builder.Services.AddHttpContextAccessor();

builder.Services.AddDbContext<ApplicationDbContext>(options =>
{
    var connectionString = builder.Configuration.GetConnectionString("SqlConnection")!;
    if (connectionString.Contains("database.net"))
    {
        options.UseAzureSql(connectionString);
    }
    else
    {
        options.UseSqlServer(connectionString);
    }

    options.UseExceptionProcessor();
    options.UseQueryTrackingBehavior(QueryTrackingBehavior.NoTracking);
});

builder.Services.ConfigureHttpJsonOptions(options =>
{
    options.SerializerOptions.DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull;
    options.SerializerOptions.Converters.Add(new JsonStringEnumConverter());
    options.SerializerOptions.Converters.Add(new UtcDateTimeConverter());
});

var appSettings = builder.Services.ConfigureAndGet<AppSettings>(builder.Configuration, nameof(AppSettings)) ?? new();
var swaggerSettings = builder.Services.ConfigureAndGet<SwaggerSettings>(builder.Configuration, nameof(SwaggerSettings)) ?? new();

builder.Services.AddRequestLocalization(appSettings.SupportedCultures.Distinct().ToArray());

builder.Services.AddDefaultProblemDetails();
builder.Services.AddDefaultExceptionHandler();

if (swaggerSettings.IsEnabled)
{
    builder.Services.AddOpenApi(options =>
    {
        options.AddAcceptLanguageHeader();
        options.AddDefaultProblemDetailsResponse();
    });
}

var app = builder.Build();

// Configure the HTTP request pipeline.
app.UseHttpsRedirection();

app.UseExceptionHandler();
app.UseStatusCodePages();

if (swaggerSettings.IsEnabled)
{
    app.UseMiddleware<SwaggerBasicAuthenticationMiddleware>();

    app.MapOpenApi();

    app.UseSwaggerUI(options =>
    {
        options.SwaggerEndpoint("/openapi/v1.json", "v1");        
    });
}

app.UseRouting();
app.UseRequestLocalization();

//app.UseAuthentication();
//app.UseAuthorization();

app.MapEndpoints();

app.Run();
