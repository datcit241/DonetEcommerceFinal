using Application.Core;
using AutoMapper;
using Domain;
using MediatR;
using Persistence;

namespace Application.Orders;

public class Edit
{
    public class Command : IRequest<Result<Unit>>
    {
        public CustomerOrder CustomerOrder { get; set; }
    }

    public class Handler : IRequestHandler<Command, Result<Unit>>
    {
        private readonly DataContext _context;
        private readonly IMapper _mapper;

        public Handler(DataContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<Result<Unit>> Handle(Command request, CancellationToken cancellationToken)
        {
            var order = await _context.CustomerOrders.FindAsync(request.CustomerOrder.Id);

            if (order == null) return null;

            _mapper.Map(request.CustomerOrder, order);

            var result = await _context.SaveChangesAsync() > 0;

            if (!result) return Result<Unit>.Failure("Failed to save changes");

            return Result<Unit>.Success(Unit.Value);
        }
    }
}