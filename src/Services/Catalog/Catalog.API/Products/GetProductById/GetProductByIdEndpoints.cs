﻿
namespace Catalog.API.Products.GetProductById
{
    //public record GetProductByIdRequest();

    public record GetProductByIdResponse(Product Product);

    public class GetProductByIdEndpoints : ICarterModule
    {
        public void AddRoutes(IEndpointRouteBuilder app)
        {
            app.MapGet("/products/{id}",
                async (Guid id, ISender sender) =>
                {
                    try
                    {
                        var result = await sender.Send(new GetProductByIdQuery(id));

                        var response = result.Adapt<GetProductByIdResponse>();

                        return Results.Ok(response);
                    }
                    catch (ProductNotFoundException ex)
                    {

                        return Results.NotFound(ex.Message);

                    }
                })
                .WithName("GetProductById")
                .Produces<GetProductByIdResponse>(StatusCodes.Status200OK)
                .ProducesProblem(StatusCodes.Status400BadRequest)
                .WithSummary("Get product by id")
                .WithDescription("Get product by id");
        }
    }
}
