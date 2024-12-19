using MediatR;

namespace Catalog.API.Products
{
    public record CreateProductCommand(Guid Id, string Name, string Description, List<string> Category, string ImageFile, decimal Price) : IRequest<CreateProductResponse>;
    public record CreateProductResponse(Guid Id);
    public class CreateProductHandler : IRequestHandler<CreateProductCommand, CreateProductResponse>
    {
        public Task<CreateProductResponse> Handle(CreateProductCommand request, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}
