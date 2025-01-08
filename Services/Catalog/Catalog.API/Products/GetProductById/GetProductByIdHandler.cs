using Catalog.API.Exceptions;
using Catalog.API.Models;
using ClassLib.CQRS;

namespace Catalog.API.Products.GetProductById
{
    public record GetProductByIdQuery(Guid Id) : IQuery<GetProductByIdResult>;
    public record GetProductByIdResult(Product Product);
    internal class GetProductByIdHandler(IDocumentSession session, ILogger<GetProductByIdHandler> logger)
        : IQueryHandler<GetProductByIdQuery, GetProductByIdResult>
    {
        public async Task<GetProductByIdResult> Handle(GetProductByIdQuery query, CancellationToken cancellationToken)
        {
            logger.LogInformation($"GetProductsByIdHandler.Handle Called with {query}");

            var product = await session.LoadAsync<Product>(query.Id, cancellationToken);

            if (product is null)
            {
                throw new ProductNotFoundException("Product not Found");
            }
            return new GetProductByIdResult(product);
        }
    }
}
