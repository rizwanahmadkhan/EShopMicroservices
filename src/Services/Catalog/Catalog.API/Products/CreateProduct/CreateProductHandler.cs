

namespace Catalog.API.Products.CreateProduct
{
    public record CreateProductCommand(string Name, string Description, decimal Price, string ImageFile, List<string> Categories)
        : ICommand<CreateProductResult>;
    public record CreateProductResult(Guid Id);

    public class CreateProductCommandValidator : AbstractValidator<CreateProductCommand>
    {
        public CreateProductCommandValidator()
        {
            RuleFor(x => x.Name).NotEmpty().WithMessage("Product name is required.");
            RuleFor(x => x.ImageFile).NotEmpty().WithMessage("Product image is required.");
            RuleFor(x => x.Categories).NotEmpty().WithMessage("Product categories are required.");
            RuleFor(x => x.Price).GreaterThan(0).WithMessage("Product price must be greater than 0.");
        }
    }
    internal class CreateProductCommandHandler(IDocumentSession session, ILogger<CreateProductCommandHandler> logger) : ICommandHandler<CreateProductCommand, CreateProductResult>
    {
        public async Task<CreateProductResult> Handle(CreateProductCommand command, CancellationToken cancellationToken)
        {
            //Business logic to create a product
            //create product entity from command object
            //save to database
            //return the result;

            logger.LogInformation("CreateProductCommandHandler.Handle called with {@Command}", command);

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
