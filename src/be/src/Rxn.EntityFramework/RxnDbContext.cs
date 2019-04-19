using System.Data.Entity;
using System.Data.Entity.Validation;
using System.Data.SqlClient;
using System.Reflection;

using Rxn.EntityFramework.DbEntities;

using Log4net = log4net;

namespace Rxn.EntityFramework
{
    internal class RxnDbContext : DbContext
    {
        private static readonly Log4net.ILog Log = Log4net.LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);

        internal RxnDbContext(string connectionString) : base(connectionString)
        {
        }

        public override int SaveChanges()
        {
            try
            {
                return base.SaveChanges();
            }
            catch (SqlException ex)
            {
                Log.Error(ex);
                throw;
            }
            catch (DbEntityValidationException ex)
            {
                foreach (var item in ex.EntityValidationErrors)
                {
                    if (!item.IsValid)
                    {
                        foreach (var e in item.ValidationErrors)
                        {
                            Log.WarnFormat("Entity: {2}; Property: {0}; Message: {1}", e.PropertyName, e.ErrorMessage, item.Entry.Entity);
                        }
                    }
                }

                throw;
            }
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<ItemMaster>();
            modelBuilder.Entity<ItemMasterInventory>();
        }
    }
}