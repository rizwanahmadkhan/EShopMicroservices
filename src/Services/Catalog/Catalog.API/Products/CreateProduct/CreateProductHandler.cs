

namespace Catalog.API.Products.CreateProduct
{
    public record CreateProductCommand(string Name, string Description, decimal Price, string ImageFile, List<string> Categories)
        : ICommand<CreateProductResult>;
    public record CreateProductResult(Guid Id);
    internal class CreateProductCommandHandler(IDocumentSession session) : ICommandHandler<CreateProductCommand, CreateProductResult>
    {
        public async Task<CreateProductResult> Handle(CreateProductCommand command, CancellationToken cancellationToken)
        {
            //Business logic to create a product
            //create product entity from command object
            //save to database
            //return the result;

            var product = new Product
            {
                Name = command.Name,
                Description = command.Description,
                Price = command.Price,
                ImageFile = command.ImageFile,
                Categories = command.Categories,
            };

            //save to database
            session.Store(product);
            await session.SaveChangesAsync(cancellationToken);
            //return result
            return new CreateProductResult(product.Id);
        }
    }
}
