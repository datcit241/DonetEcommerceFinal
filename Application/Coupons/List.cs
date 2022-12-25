using Application.Core;
using Application.Queries;
using Domain;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Persistence;

namespace Application.Coupons;

public class List
{
    public class Query : IRequest<Result<PagedList<Product>>>
    {
        public PagingParams QueryParams { get; set; }
    }

    public class Handler : IRequestHandler<Query, Result<PagedList<Product>>>
    {
        private readonly DataContext _context;

        public Handler(DataContext context)
        {
            _context = context;
        }

        public async Task<Result<PagedList<Product>>> Handle(Query request, CancellationToken cancellationToken)
        {
            var query = _context
                .Products
                .Include(entity => entity
                    .Prices
                    .OrderByDescending(price => price.DateSet)
                    .Take(1))
                .Include(entity => entity.Images.Take(1))
                .Include(entity =>
                    entity.Discounts.Where(discountProduct =>
                        discountProduct.Discount.DiscountStatus == DiscountStatus.OnGoing))
                .AsQueryable();

            if (string.IsNullOrEmpty(request.QueryParams.SearchString))
                query = query.Where(entity => entity.Name.Contains(request.QueryParams.SearchString));

            query = ProductOrderQuery.ApplyOrder(query, request.QueryParams.Order, request.QueryParams.OrderBy);

            var products =
                await PagedList<Product>.CreateAsync(
                    query,
                    request.QueryParams.PageNumber,
                    request.QueryParams.PageSize
                );

            return Result<PagedList<Product>>.Success(products);
        }
    }
}