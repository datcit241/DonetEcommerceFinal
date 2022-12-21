using Application.Core;
using Domain;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Persistence;

namespace Application.Products;

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
                .Include(entity => entity.Prices)
                .Include(entity => entity.Images)
                .AsQueryable();

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