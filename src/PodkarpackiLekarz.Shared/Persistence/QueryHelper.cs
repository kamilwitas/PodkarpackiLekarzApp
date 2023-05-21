using System.Text;

namespace PodkarpackiLekarz.Shared.Persistence;
public static class QueryHelper
{
    public static string BuildPaginationSql(int pageNumber, int pageSize)
    {
        return $" OFFSET {(pageNumber - 1) * pageSize} ROWS " +
            $"FETCH NEXT {pageSize} ROWS ONLY ";
    }

}
