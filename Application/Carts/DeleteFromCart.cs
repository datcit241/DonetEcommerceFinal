using Application.Core;
using Domain;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Persistence;

namespace Application.Carts;

public class DeleteFromCarts
{
    public class Command : IRequest<Result<Unit>>
    {
        public CartDetails CartDetails { get; set; }
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
            var cartDetails = await _context.CartDetails.FirstOrDefaultAsync(
                entity => entity.CartId == request.CartDetails.CartId
                          && entity.ProductId == request.CartDetails.ProductId
                          && entity.VariationOptionId == request.CartDetails.VariationOptionId
            );

            if (cartDetails == null) return null;

            _context.Remove(cartDetails);
            var result = await _context.SaveChangesAsync() > 0;

            if (!result) return Result<Unit>.Failure("Failed to save changes");

            return Result<Unit>.Success(Unit.Value);
        }
    }
}