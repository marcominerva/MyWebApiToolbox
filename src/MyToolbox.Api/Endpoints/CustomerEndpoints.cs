using System.Net.Mime;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using MinimalHelpers.OpenApi;
using MyToolbox.Shared.Models;

namespace MyToolbox.Api.Endpoints;

public class CustomerEndpoints : IEndpointRouteHandlerBuilder
{
    public static void MapEndpoints(IEndpointRouteBuilder endpoints)
    {
        var apiGroup = endpoints.MapGroup("/api/customers")            
            .WithTags("Customers");

        apiGroup.MapGet(string.Empty, GetCustomers)            
            .Produces<IEnumerable<Customer>>(StatusCodes.Status200OK)
            .WithName("GetCustomers")
            .WithSummary("Gets the customer list")
            .WithDescription("Gets the entire list of customers as available in the database");

        apiGroup.MapGet("{id:int}", GetCustomer)
            .Produces<Customer>(StatusCodes.Status200OK)
            .ProducesProblem(StatusCodes.Status404NotFound)
            .WithName("GetCustomer");

        apiGroup.MapPost(string.Empty, () => { })
            .Produces<Customer>(StatusCodes.Status201Created)
            .ProducesDefaultProblem(StatusCodes.Status404NotFound, StatusCodes.Status400BadRequest, StatusCodes.Status409Conflict, StatusCodes.Status422UnprocessableEntity)
            .WithName("SaveCustomer");
    }

    [Authorize]
    public static IResult GetCustomers()
    {
        var customers = new List<Customer>
            {
                new("John", "Doe"),
                new("Jane", "Smith"),
                new("Alice", "Johnson")
            };

        return TypedResults.Ok(customers.AsEnumerable());
    }

    public static IResult GetCustomer(int id)
    {
        if (id == 0)
        {
            return TypedResults.NotFound();
        }

        return TypedResults.Ok(new Customer("John", "Doe"));
    }
}
