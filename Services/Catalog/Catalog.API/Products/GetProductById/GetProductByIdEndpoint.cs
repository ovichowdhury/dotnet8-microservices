using Catalog.API.Models;
using Catalog.API.Products.CreateProduct;

namespace Catalog.API.Products.GetProductById
{
    public record GetProductByIdResponse(Product Product);
    public class GetProductByIdEndpoint : ICarterModule
    {
        public void AddRoutes(IEndpointRouteBuilder app)
        {
            app.MapGet("/products/{id}", async (Guid id, ISender sender) =>
            {
                var result = await sender.Send(new GetProductByIdQuery(id));

                var response = result.Adapt<GetProductByIdResponse>();

                return Results.Ok(response);
            })
            .WithName("GetProduct")
            .Produces<CreateProductResponse>(StatusCodes.Status200OK)
            .ProducesProblem(StatusCodes.Status400BadRequest)
            .WithSummary("Get Product By ID")
            .WithDescription("Get Products By ID");
        }
    }
}
