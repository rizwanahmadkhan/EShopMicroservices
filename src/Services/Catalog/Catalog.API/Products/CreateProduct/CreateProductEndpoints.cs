﻿namespace Catalog.API.Products.CreateProduct
{
    public record CreateProductRequest(string Name, string Description, decimal Price, string ImageFile, List<string> Categories);

    public record CreateProductResponse(Guid Id);

    public class CreateProductEndpoints : ICarterModule
    {
        public void AddRoutes(IEndpointRouteBuilder app)
        {
            app.MapPost("/products",
                async (CreateProductRequest request, ISender sender) =>
                {
                    var command = request.Adapt<CreateProductCommand>();

                    var result = await sender.Send(command);

                    var response = result.Adapt<CreateProductResponse>();

                    return Results.Created($"/products/{response.Id}", response);
            })
            .WithName("CreateProduct")
            .Produces<CreateProductResponse>(StatusCodes.Status201Created)
            .ProducesProblem(StatusCodes.Status400BadRequest)
            .WithSummary("Create a new product")
            .WithDescription("Create a new product");
        }
    }
}
