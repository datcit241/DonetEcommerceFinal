using Application.Core;
using Domain;
using MediatR;

namespace Application.Products;

public class ListStatus
{
    public class Query : IRequest<List<ProductStatus>>
    {
    }

    public class Handler : IRequestHandler<Query, List<ProductStatus>>
    {
        public async Task<List<ProductStatus>> Handle(Query request, CancellationToken cancellationToken)
        {
            return EnumsUtils.EnumToList<ProductStatus>();
        }
    }
}