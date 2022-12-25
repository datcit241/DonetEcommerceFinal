using Application.Core;
using Domain;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Persistence;

namespace Application.Carts;

public class AddToCart
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
            var thatSameCartDetails = await _context.CartDetails.FirstOrDefaultAsync(
                entity => entity.CartId == request.CartDetails.CartId
                          && entity.ProductId == request.CartDetails.ProductId
                          && entity.VariationOptionId == request.CartDetails.VariationOptionId
            );

            if (thatSameCartDetails != null)
                thatSameCartDetails.Quantity += request.CartDetails.Quantity;
            else
                _context.Add(request.CartDetails);

            var result = await _context.SaveChangesAsync() > 0;

            if (!result) return Result<Unit>.Failure("Failed to save changes");

            return Result<Unit>.Success(Unit.Value);
        }
    }
}