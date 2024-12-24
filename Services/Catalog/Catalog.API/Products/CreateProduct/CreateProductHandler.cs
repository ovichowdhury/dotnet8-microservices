using Catalog.API.Models;
using ClassLib.CQRS;

namespace Catalog.API.Products.CreateProduct
{
    public record CreateProductCommand(Guid Id, string Name, string Description, List<string> Category, string ImageFile, decimal Price) : ICommand<CreateProductResponse>;
    public record CreateProductResult(Guid Id);
    public class CreateProductHandler : ICommandHandler<CreateProductCommand, CreateProductResponse>
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

            return new CreateProductResponse(Guid.NewGuid());
        }
    }
}
