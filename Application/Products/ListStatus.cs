using Application.Core;
using Domain;
using MediatR;

namespace Application.Products;

public class ListStatus
{
    public class Query : IRequest<Result<List<ProductStatus>>>
    {
    }

    public class Handler : IRequestHandler<Query, Result<List<ProductStatus>>>
    {
        public async Task<Result<List<ProductStatus>>> Handle(Query request, CancellationToken cancellationToken)
        {
            return Result<List<ProductStatus>>.Success(EnumsUtils.EnumToList<ProductStatus>());
        }
    }
}