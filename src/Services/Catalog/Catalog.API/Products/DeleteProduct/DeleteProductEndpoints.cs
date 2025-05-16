
namespace Catalog.API.Products.DeleteProduct
{
    public record DeleteProductRequest(Guid id);

    public record DeleteProductResponse(bool IsSuccess);

    public class DeleteProductEndpoints() : ICarterModule
    {
        public void AddRoutes(IEndpointRouteBuilder app)
        {
            app.MapDelete("/products/{id}",
                async (Guid id, ISender sender) =>
                {
                    var command = new DeleteProductCommand(id);

                    var result = await sender.Send(command);

                    var response = result.Adapt<DeleteProductResponse>();

                    return Results.Ok(response);
                })
                .WithName("DeleteProduct")
                .Produces<DeleteProductResponse>()
                .ProducesProblem(StatusCodes.Status400BadRequest)
                .ProducesProblem(StatusCodes.Status404NotFound)
                .WithSummary("Delete Product")
                .WithDescription("Delete Product");
        }
    }
}
