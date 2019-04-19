using System;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;

using Rxn.EntityFramework.UnitOfWork;

namespace Rxn.EntityFramework.Repositories
{
    public class EfRepository<T> : IRepository<T> where T : class
    {
        public IUnitOfWork UnitOfWork { get; set; }

        private IDbSet<T> ObjectSet => UnitOfWork.Context.Set<T>();

        public EfRepository(IUnitOfWork unitOfWork)
        {
            UnitOfWork = unitOfWork;
        }

        public virtual IQueryable<T> All()
        {
            return ObjectSet.AsQueryable();
        }

        public IQueryable<T> Where(Expression<Func<T, bool>> expression)
        {
            return ObjectSet.Where(expression);
        }

        public void Add(T entity)
        {
            ObjectSet.Add(entity);
        }

        public T Get(int id)
        {
            var idProp = ObjectSet.ElementType.GetProperty(string.Format("{0}Id", ObjectSet.ElementType.Name)) ??
                         ObjectSet.ElementType.GetProperty("Id");
            if (idProp != null)
            {
                ParameterExpression idParam = Expression.Parameter(typeof(T));
                ConstantExpression idValue = Expression.Constant(id, typeof(int));
                MemberExpression idMbr = Expression.Property(idParam, idProp);
                BinaryExpression idExpression = Expression.Equal(idMbr, idValue);

                var lambda1 =
                    Expression.Lambda<Func<T, bool>>(
                        idExpression,
                        idParam);

                var entity = ObjectSet.FirstOrDefault(lambda1);

                return entity;
            }

            return null;
        }

        public void Delete(int id)
        {
            var idProp = ObjectSet.ElementType.GetProperty(string.Format("{0}Id", ObjectSet.ElementType.Name)) ??
                         ObjectSet.ElementType.GetProperty("Id");
            if (idProp != null)
            {
                ParameterExpression idParam = Expression.Parameter(typeof(T));
                ConstantExpression idValue = Expression.Constant(id, typeof(int));
                MemberExpression idMbr = Expression.Property(idParam, idProp);
                BinaryExpression idExpression = Expression.Equal(idMbr, idValue);

                Expression<Func<T, bool>> lambda1 =
                    Expression.Lambda<Func<T, bool>>(
                        idExpression,
                        idParam);

                var entity = ObjectSet.FirstOrDefault(lambda1);

                if (entity != null)
                {
                    ObjectSet.Remove(entity);
                }
            }
        }

        public void Delete(T entity)
        {
            ObjectSet.Remove(entity);
        }
    }
}