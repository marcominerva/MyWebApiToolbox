namespace MyToolbox.Api.Endpoints;

public class PingEndpoints : IEndpointRouteHandlerBuilder
{
    public static void MapEndpoints(IEndpointRouteBuilder app)
    {
        app.MapGet("/api/ping", Ping)
            .WithName("Ping")
            .WithTags("Health");
    }

    private static IResult Ping(TimeProvider timeProvider)
    {
        var dateTime = new DateTimeOffset(2025, 9, 12, 15, 42, 0, TimeSpan.FromHours(2));

        return TypedResults.Ok(new { timestamp = dateTime });
    }
}
