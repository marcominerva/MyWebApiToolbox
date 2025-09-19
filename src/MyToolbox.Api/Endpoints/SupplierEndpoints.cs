namespace MyToolbox.Api.Endpoints;

public class SupplierEndpoints : IEndpointRouteHandlerBuilder
{
    public static void MapEndpoints(IEndpointRouteBuilder endpoints)
    {
        endpoints.MapGet("/api/suppliers", () => TypedResults.Ok(new[] { "Supplier A", "Supplier B", "Supplier C" }))
            .WithName("GetSuppliers")
            .WithTags("Suppliers");
    }
}
