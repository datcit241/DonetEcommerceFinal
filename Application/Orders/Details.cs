using Application.Core;
using Domain;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Persistence;

namespace Application.Orders;

public class Details
{
    public class Query : IRequest<Result<CustomerOrder>>
    {
        public Guid Id { get; set; }
    }

    public class Handler : IRequestHandler<Query, Result<CustomerOrder>>
    {
        private readonly DataContext _context;

        public Handler(DataContext context)
        {
            _context = context;
        }

        public async Task<Result<CustomerOrder>> Handle(Query request, CancellationToken cancellationToken)
        {
            var order = await _context.CustomerOrders
                    .Include(entity => entity.OrderDetails).ThenInclude(entity => entity.Product)
                    .Include(entity => entity.OrderDetails).ThenInclude(entity => entity.Price)
                    .Include(entity => entity.OrderDetails).ThenInclude(entity => entity.VariationOption)
                    .ThenInclude(entity => entity.Variation)
                    .FirstOrDefaultAsync(entity => entity.Id == request.Id)
                ;

            return Result<CustomerOrder>.Success(
                order
            );
        }
    }
}