using Domain;

namespace Application.Queries;

public class ProductOrderQuery
{
    public static IQueryable<Product> ApplyOrder(IQueryable<Product> query, string? order, string? orderBy)
    {
        if (!string.IsNullOrEmpty(order))
            switch (orderBy)
            {
                case "price":
                    if (order == "asc")
                        query = query.OrderBy(
                            entity => entity.Prices
                                .OrderByDescending(price => price.DateSet).First()
                        );
                    else
                        query = query.OrderByDescending(
                            entity => entity.Prices
                                .OrderByDescending(price => price.DateSet).First()
                        );

                    break;
                case "quantity":
                    if (order == "asc")
                        query = query.OrderBy(entity => entity.Quantity);
                    else
                        query = query.OrderByDescending(entity => entity.Quantity);
                    break;
                case "description":
                    if (order == "asc")
                        query = query.OrderBy(entity => entity.Description);
                    else
                        query = query.OrderByDescending(entity => entity.Description);
                    break;
                case "revenue":
                    if (order == "asc")
                        query = query.OrderBy(entity => entity.Revenue);
                    else
                        query = query.OrderByDescending(entity => entity.Revenue);
                    break;
                case "status":
                    if (order == "asc")
                        query = query.OrderBy(entity => entity.Status.ToString());
                    else
                        query = query.OrderByDescending(entity => entity.Status.ToString());
                    break;
                default:
                    if (order == "asc")
                        query = query.OrderBy(entity => entity.Name);
                    else
                        query = query.OrderByDescending(entity => entity.Name);
                    break;
            }

        return query;
    }
}