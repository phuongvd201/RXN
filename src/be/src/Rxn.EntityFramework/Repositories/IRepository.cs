using System;
using System.Linq;
using System.Linq.Expressions;

using Rxn.EntityFramework.UnitOfWork;

namespace Rxn.EntityFramework.Repositories
{
    public interface IRepository<T>
    {
        IUnitOfWork UnitOfWork { get; set; }

        IQueryable<T> All();

        IQueryable<T> Where(Expression<Func<T, bool>> expression);

        void Add(T entity);

        void Delete(int id);

        T Get(int id);

        void Delete(T entity);
    }
}