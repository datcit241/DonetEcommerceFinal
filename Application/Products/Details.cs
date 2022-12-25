using Application.Core;
using Domain;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Persistence;

namespace Application.Products;

public class Details
{
    public class Query : IRequest<Result<Product>>
    {
        public Guid Id { get; set; }
    }

    public class Handler : IRequestHandler<Query, Result<Product>>
    {
        private readonly DataContext _context;

        public Handler(DataContext context)
        {
            _context = context;
        }

        public async Task<Result<Product>> Handle(Query request, CancellationToken cancellationToken)
        {
            var product =
                await _context
                    .Products
                    .Include(entity => entity.Prices)
                    .Include(entity => entity.Images)
                    .Include(entity => entity.Ratings)
                    .FirstOrDefaultAsync(entity => entity.Id == request.Id);
            product.Variations = await _context
                .ProductVariations
                .Where(entity => entity.ProductId == request.Id)
                .Include(entity => entity.Variation)
                .ThenInclude(entity => entity.VariationOptions)
                .ToListAsync();
            product.Discounts = await _context
                .DiscountProducts
                .Where(entity =>
                    entity.ProductId == request.Id && entity.Discount.DiscountStatus == DiscountStatus.OnGoing)
                .Include(entity => entity.Discount)
                .ToListAsync();

            return Result<Product>.Success(
                product
            );
        }
    }
}