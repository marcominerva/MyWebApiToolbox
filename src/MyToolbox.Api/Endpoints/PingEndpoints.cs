using System.Globalization;

namespace MyToolbox.Api.Endpoints;

public class PingEndpoints
{
    public static void MapEndpoints(IEndpointRouteBuilder app)
    {
        app.MapGet("/api/ping", Ping)
            .WithName("Ping")
            .WithTags("Health");

        app.MapGet("/api/notfound", ()
            => TypedResults.Problem("Errore imprevisto",statusCode: StatusCodes.Status400BadRequest));

        app.MapPost("/api/exception", ()=>
            {
                throw new Exception("Errore incredibile", new ApplicationException("Errore interno"));
            });
    }

    private static IResult Ping(TimeProvider timeProvider)
    {
        var dateTime = new DateTimeOffset(2025, 9, 12, 15, 42, 0, TimeSpan.FromHours(2));

        return TypedResults.Ok(new { timestamp = dateTime });
    }
}
