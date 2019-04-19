using System.Data.Entity;

namespace Rxn.EntityFramework.UnitOfWork
{
    public class EfUnitOfWork : IUnitOfWork
    {
        public DbContext Context { get; set; }

        public EfUnitOfWork()
        {
            Context = ContextCreator.CreateContext();
        }

        public void Commit()
        {
            Context.SaveChanges();
        }

        public bool LazyLoadingEnabled
        {
            get { return Context.Configuration.LazyLoadingEnabled; }
            set { Context.Configuration.LazyLoadingEnabled = value; }
        }

        public void Dispose()
        {
            Context.Dispose();
        }
    }
}