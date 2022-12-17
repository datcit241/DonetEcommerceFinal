using Application.Core;
using Domain;
using MediatR;

namespace Application.Products;

public class ListLabels
{
    public class Query : IRequest<List<ProductLabel>>
    {
    }

    public class Handler : IRequestHandler<Query, List<ProductLabel>>
    {
        public async Task<List<ProductLabel>> Handle(Query request, CancellationToken cancellationToken)
        {
            // return Enum.GetValues(typeof(ProductLabel)).Cast<ProductLabel>().ToList();
            return EnumsUtils.EnumToList<ProductLabel>();
        }
    }
}