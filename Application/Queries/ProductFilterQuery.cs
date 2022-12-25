using Domain;

namespace Application.Queries;

public class ProductFilterQuery
{
    public static IQueryable<Product> ApplyFilter(IQueryable<Product> query, string filter, string filterBy)
    {
        if (!string.IsNullOrEmpty(filter))
            switch (filterBy)
            {
                case "status":
                    query = query.Where(p => p.Status.ToString() == filter);
                    break;
                default:
                    break;
            }

        return query;
    }
}