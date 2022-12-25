using Application.Core;
using Domain;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Persistence;

namespace Application.Coupons;

public class Details
{
    public class Query : IRequest<Result<Coupon>>
    {
        public Guid Id { get; set; }
    }

    public class Handler : IRequestHandler<Query, Result<Coupon>>
    {
        private readonly DataContext _context;

        public Handler(DataContext context)
        {
            _context = context;
        }

        public async Task<Result<Coupon>> Handle(Query request, CancellationToken cancellationToken)
        {
            var coupon = await _context
                    .Coupons
                    .Include(entity => entity.Users)
                    .FirstOrDefaultAsync(entity => entity.Id == request.Id)
                ;

            return Result<Coupon>.Success(
                coupon
            );
        }
    }
}