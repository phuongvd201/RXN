using System.Linq;

namespace Rxn.Services.Helpers
{
    public static class QueryableExtension
    {
        // For readable
        public static T[] MakeQueryToDatabase<T>(this IQueryable<T> source)
        {
            return source.ToArray();
        }
    }
}