namespace MyToolbox.Api.Endpoints;

public class ProductEndpoints : IEndpointRouteHandlerBuilder
{
    public static void MapEndpoints(IEndpointRouteBuilder endpoints)
    {
        endpoints.MapGet("/api/products", () => TypedResults.Ok(new[] { "Laptop", "Smartphone", "Tablet" }))
            .WithName("GetProducts")
            .WithTags("Products");
    }
}
