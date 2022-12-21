using Application.Core;
using Domain;
using MediatR;
using Persistence;

namespace Application.Products;

public class Create
{
    public class Command : IRequest<Result<Unit>>
    {
        public Product Product { get; set; }
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
            _context.Products.Add(request.Product);

            var result = await _context.SaveChangesAsync() > 0;

            if (!result)
            {
                return Result<Unit>.Failure("Failed to save changes");
            }

            return Result<Unit>.Success(Unit.Value);
        }
    }
}