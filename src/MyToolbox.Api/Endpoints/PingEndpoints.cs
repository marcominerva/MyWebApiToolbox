namespace MyToolbox.Api.Endpoints;

public class PingEndpoints
{
    public static void MapEndpoints(IEndpointRouteBuilder app)
    {
        app.MapGet("/api/ping", Ping)
            .WithName("Ping")
            .WithTags("Health");
    }

    private static IResult Ping()
        => TypedResults.Ok();
}
