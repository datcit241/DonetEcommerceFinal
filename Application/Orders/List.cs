using Application.Core;
using Application.Interfaces;
using Domain;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Persistence;

namespace Application.Orders;

public class List
{
    public class Query : IRequest<Result<PagedList<CustomerOrder>>>
    {
        public PagingParams QueryParams { get; set; }
    }

    public class Handler : IRequestHandler<Query, Result<PagedList<CustomerOrder>>>
    {
        private readonly DataContext _context;
        private readonly IUserAccessor _userAccessor;

        public Handler(DataContext context)
        {
            _context = context;
        }

        public async Task<Result<PagedList<CustomerOrder>>> Handle(Query request, CancellationToken cancellationToken)
        {
            var query = _context
                .CustomerOrders
                .Include(entity => entity.OrderDetails).ThenInclude(entity => entity.Product)
                .Include(entity => entity.OrderDetails).ThenInclude(entity => entity.Price)
                .Include(entity => entity.OrderDetails).ThenInclude(entity => entity.VariationOption)
                .ThenInclude(entity => entity.Variation)
                .OrderByDescending(entity => entity.OrderDate);

            var orderList =
                await PagedList<CustomerOrder>.CreateAsync(
                    query,
                    request.QueryParams.PageNumber,
                    request.QueryParams.PageSize
                );

            return Result<PagedList<CustomerOrder>>.Success(orderList);
        }
    }
}