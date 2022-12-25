using Application.Core;
using Application.Interfaces;
using Domain;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Persistence;

namespace Application.Carts;

public class Details
{
    public class Query : IRequest<Result<CartDetails>>
    {
        public CartDetails CartDetails { get; set; }
    }

    public class Handler : IRequestHandler<Query, Result<CartDetails>>
    {
        private readonly DataContext _context;
        private readonly IUserAccessor _userAccessor;

        public Handler(DataContext context, IUserAccessor userAccessor)
        {
            _context = context;
            _userAccessor = userAccessor;
        }

        public async Task<Result<CartDetails>> Handle(Query request, CancellationToken cancellationToken)
        {
            var user = await _context.Users.FirstOrDefaultAsync(
                entity => entity.UserName == _userAccessor.GetUsername());
            var cartDetails = await _context.CartDetails
                .Include(entity => entity.Product)
                .Include(entity => entity.VariationOption)
                .ThenInclude(entity => entity.Variation)
                .FirstOrDefaultAsync(
                    entity => entity.ProductId == request.CartDetails.ProductId
                              && entity.VariationOptionId == request.CartDetails.VariationOptionId
                              && entity.CartId == user.Id
                );

            return Result<CartDetails>.Success(
                cartDetails
            );
        }
    }
}