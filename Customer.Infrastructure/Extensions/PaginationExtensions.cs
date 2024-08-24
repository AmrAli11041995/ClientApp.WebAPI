using Customer.DTOs.Common;

namespace Customer.Infrastructure.Extensions
{
    public static class PaginationExtensions
    {
        public static IQueryable<T> Paginate<T>(this IQueryable<T> query, PaginatedFiltration paginatedFiltration)
            where T : class
        {
            if (paginatedFiltration.CurrentPage != null && paginatedFiltration.PageSize != null)
            {
                return query.Skip(((int)paginatedFiltration.CurrentPage - 1) * (int)paginatedFiltration.PageSize).Take((int)paginatedFiltration.PageSize);
            }
            return query;
        }

        public static IList<T> Paginate<T>(this IList<T> query, PaginatedFiltration paginatedFiltration)
            where T : class
        {
            if (paginatedFiltration.CurrentPage != null && paginatedFiltration.PageSize != null)
            {
                return query.Skip(((int)paginatedFiltration.CurrentPage - 1) * (int)paginatedFiltration.PageSize).Take((int)paginatedFiltration.PageSize).ToList();
            }
            return query;
        }
    }
}