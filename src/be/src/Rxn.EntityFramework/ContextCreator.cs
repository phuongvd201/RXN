using System;
using System.Configuration;
using System.Data.Entity;
using System.Linq;

namespace Rxn.EntityFramework
{
    public class ContextCreator
    {
        private static readonly string ConnectionString = ConfigurationManager.ConnectionStrings
            .Cast<ConnectionStringSettings>()
            .Select(x => x.Name)
            .Where(x => x.StartsWith("DefaultConnection", StringComparison.InvariantCultureIgnoreCase))
            .OrderBy(x => x)
            .FirstOrDefault();

        public static DbContext CreateContext()
        {
            var context = new RxnDbContext(ConnectionString);
            context.Database.CommandTimeout = 600;
            return context;
        }
    }
}