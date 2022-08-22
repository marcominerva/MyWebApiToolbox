using System.Text.Json.Serialization;
using FluentValidation;
using FluentValidation.AspNetCore;
using Hellang.Middleware.ProblemDetails;
using MicroElements.Swashbuckle.FluentValidation.AspNetCore;
using Microsoft.EntityFrameworkCore;
using MyToolbox.BusinessLayer.MapperProfiles;
using MyToolbox.BusinessLayer.Services;
using MyToolbox.BusinessLayer.Validations;
using MyToolbox.DataAccessLayer;
using Serilog;
using TinyHelpers.Json.Serialization;

var builder = WebApplication.CreateBuilder(args);

//builder.Host.ConfigureLogging(logging =>
//{
//    logging.AddNotepad();
//});

builder.Host.UseSerilog((hostingContext, loggerConfiguration) =>
{
    loggerConfiguration.ReadFrom.Configuration(hostingContext.Configuration);
});

// Add services to the container.

builder.Services.AddControllers()
    //.AddNewtonsoftJson(options => 
    //{
    //    options.SerializerSettings.DateTimeZoneHandling = Newtonsoft.Json.DateTimeZoneHandling.Utc;
    //});
    .AddJsonOptions(options =>
    {
        options.JsonSerializerOptions.DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingDefault;
        options.JsonSerializerOptions.Converters.Add(new UtcDateTimeConverter());
        options.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());
    });

builder.Services.AddAutoMapper(typeof(PersonMapperProfile).Assembly);

builder.Services.AddFluentValidationAutoValidation(options =>
{
    options.DisableDataAnnotationsValidation = true;
});

builder.Services.AddValidatorsFromAssemblyContaining<SaveOrderRequestValidator>();

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();

builder.Services.AddSwaggerGen()
.AddFluentValidationRulesToSwagger(options =>
{
    options.SetNotNullableIfMinLengthGreaterThenZero = true;
});

builder.Services.AddDbContext<DataContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("SqlConnection"), sqlOptions =>
    {
        //sqlOptions.UseQuerySplittingBehavior(QuerySplittingBehavior.SplitQuery);
    });
});

builder.Services.AddScoped<IReadOnlyDataContext>(services => services.GetRequiredService<DataContext>());
builder.Services.AddScoped<IDataContext>(services => services.GetRequiredService<DataContext>());

//builder.Services.AddScoped<IPeopleService, PeopleService>();
//builder.Services.AddScoped<IOrderService, OrderService>();

builder.Services.Scan(scan => scan.FromAssemblyOf<PeopleService>()
    .AddClasses(classes => classes.InNamespaceOf<PeopleService>())
    .AsImplementedInterfaces()
    .WithScopedLifetime()
);

builder.Services.AddProblemDetails(options =>
{
    options.Map<ApplicationException>(ex =>
    new StatusCodeProblemDetails(StatusCodes.Status503ServiceUnavailable)
    {
        Title = "Services Unavailable"
    });
});

var app = builder.Build();

app.UseHttpsRedirection();

app.UseProblemDetails();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseSerilogRequestLogging(options =>
{
    options.IncludeQueryInRequestPath = true;
});

app.UseAuthorization();

app.MapControllers();

app.Run();
