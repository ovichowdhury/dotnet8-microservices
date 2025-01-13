using Catalog.API.Exceptions;
using Catalog.API.Models;
using ClassLib.CQRS;
using System.Diagnostics;

namespace Catalog.API.Products.UpdateProduct
{
    public record UpdateProductCommand(Guid Id, string Name, List<string> Category, string Description, string ImageFile, decimal? Price)
        : ICommand<UpdateProductResult>;
    public record UpdateProductResult(bool IsSuccess);

    public class UpdateProductValidator : AbstractValidator<UpdateProductCommand>
    {
        public UpdateProductValidator()
        {
            RuleFor(x => x.Id).NotEmpty().WithMessage("Id is required");

            When(x => x.Name != null, () =>
            {
                RuleFor(x => x.Name)
                  .MinimumLength(2)
                  .WithMessage("Name must be minimum 2 character long");
            });

            When(x => x.Description != null, () =>
            {
                RuleFor(x => x.Description)
                  .MinimumLength(5)
                  .WithMessage("Desc must be minimum 5 character long");
            });

            When(x => x.Category != null, () =>
            {
                RuleFor(x => x.Category)
                  .NotEmpty().WithMessage("Category is required");
            });

            When(x => x.ImageFile != null, () =>
            {
                RuleFor(x => x.ImageFile)
                  .NotEmpty().WithMessage("Image is required");
            });

            When(x => x.Price > 0, () =>
            {
                RuleFor(x => x.Price)
                  .GreaterThan(1).WithMessage("Price shoud be greater than 1");
            });
            

        }
    }

    internal class UpdateProductHandler(IDocumentSession session) : ICommandHandler<UpdateProductCommand, UpdateProductResult>
    {
        public async Task<UpdateProductResult> Handle(UpdateProductCommand command, CancellationToken cancellationToken)
        {
            var product = await session.LoadAsync<Product>(command.Id, cancellationToken);

            if (product is null)
            {
                throw new ProductNotFoundException("Product not Found");
            }

            product.Name = command.Name;
            product.Category = command.Category;
            product.Description = command.Description;
            product.ImageFile = command.ImageFile;
            product.Price = command.Price ?? 0;

            session.Update(product);
            await session.SaveChangesAsync(cancellationToken);

            return new UpdateProductResult(true);
        }
    }
}
