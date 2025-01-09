using Catalog.API.Models;
using ClassLib.CQRS;


namespace Catalog.API.Products.CreateProduct
{
    public record CreateProductCommand(Guid Id, string Name, string Description, List<string> Category, string ImageFile, decimal Price) : ICommand<CreateProductResponse>;
    public record CreateProductResult(Guid Id);

    public class CreateProductValidator : AbstractValidator<CreateProductCommand>
    {
        public CreateProductValidator() 
        {
            RuleFor(x => x.Name).NotEmpty().WithMessage("Name is required");
            RuleFor(x => x.Description).NotEmpty().WithMessage("Desc is required");
            RuleFor(x => x.Category).NotEmpty().WithMessage("Category is required");
            RuleFor(x => x.ImageFile).NotEmpty().WithMessage("Image is required");
            RuleFor(x => x.Price).GreaterThan(0).WithMessage("Price shoud be at least 1");

        }
    }

    internal class CreateProductHandler(IDocumentSession session) : ICommandHandler<CreateProductCommand, CreateProductResponse>
    {
        public async Task<CreateProductResponse> Handle(CreateProductCommand command, CancellationToken cancellationToken)
        {
            var product = new Product()
            {
                Name = command.Name,
                Description = command.Description,
                Category = command.Category,
                ImageFile = command.ImageFile,
                Price = command.Price,
            };

            session.Store(product);
            await session.SaveChangesAsync(cancellationToken);

            return new CreateProductResponse(product.Id);
        }
    }
}
