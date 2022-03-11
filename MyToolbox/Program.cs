using System.Text.Json.Serialization;
using Microsoft.EntityFrameworkCore;
using MyToolbox.BusinessLayer.MapperProfiles;
using MyToolbox.DataAccessLayer;
using TinyHelpers.Json.Serialization;

var builder = WebApplication.CreateBuilder(args);

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
    });

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<DataContext>(options =>
{
    options.UseSqlServer("a", sqlOptions =>
    {
        //sqlOptions.UseQuerySplittingBehavior(QuerySplittingBehavior.SplitQuery);
    });
});

builder.Services.AddScoped<IReadOnlyDataContext>(services => services.GetRequiredService<DataContext>());
builder.Services.AddScoped<IDataContext>(services => services.GetRequiredService<DataContext>());

builder.Services.AddAutoMapper(typeof(PersonMapperProfile).Assembly);

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
