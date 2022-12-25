using Application.Core;
using Application.Interfaces;
using Domain;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Persistence;

namespace Application.Carts;

public class List
{
    public class Query : IRequest<Result<List<CartDetails>>>
    {
    }

    public class Handler : IRequestHandler<Query, Result<List<CartDetails>>>
    {
        private readonly DataContext _context;
        private readonly IUserAccessor _userAccessor;

        public Handler(DataContext context, IUserAccessor userAccessor)
        {
            _context = context;
            _userAccessor = userAccessor;
        }

        public async Task<Result<List<CartDetails>>> Handle(Query request, CancellationToken cancellationToken)
        {
            var user = await _context.Users.FirstOrDefaultAsync(user => user.UserName == _userAccessor.GetUsername());

            var cartDetailsList = await _context.CartDetails
                .Include(entity => entity.Product)
                .Include(entity => entity.VariationOption)
                .ThenInclude(entity => entity.Variation)
                .Where(entity => entity.CartId == user.Id)
                .ToListAsync();

            return Result<List<CartDetails>>.Success(cartDetailsList);
        }
    }
}