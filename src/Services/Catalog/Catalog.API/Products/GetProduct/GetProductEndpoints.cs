
namespace Catalog.API.Products.GetProduct
{
    //public record GetProductRequest();
    public record GetProductResponse(IEnumerable<Product> Products);

    public class GetProductEndpoints : ICarterModule
    {
        public void AddRoutes(IEndpointRouteBuilder app)
        {
            app.MapGet("/products",
                async (ISender sender) => 
                {
                    try
                    {
                        var result = await sender.Send(new GetProductQuery());

                        var response = result.Adapt<GetProductResponse>();

                        return Results.Ok(response);
                    }
                    catch (ProductNotFoundException ex)
                    {

                        return Results.NotFound<string>("Product not found!");
                    }
                })
                .WithName("GetProducts")
                .Produces<GetProductResponse>(StatusCodes.Status200OK)
                .ProducesProblem(StatusCodes.Status400BadRequest)
                .WithSummary("Get Products")
                .WithDescription("Get Products");
        }
    }
}
