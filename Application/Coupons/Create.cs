using Application.Core;
using Domain;
using MediatR;
using Persistence;

namespace Application.Coupons;

public class Create
{
    public class Command : IRequest<Result<Unit>>
    {
        public Coupon Coupon { get; set; }
    }

    public class Handler : IRequestHandler<Command, Result<Unit>>
    {
        private readonly DataContext _context;

        public Handler(DataContext context)
        {
            _context = context;
        }

        public async Task<Result<Unit>> Handle(Command request, CancellationToken cancellationToken)
        {
            _context.Coupons.Add(request.Coupon);

            var result = await _context.SaveChangesAsync() > 0;

            if (!result) return Result<Unit>.Failure("Failed to save changes");

            return Result<Unit>.Success(Unit.Value);
        }
    }
}