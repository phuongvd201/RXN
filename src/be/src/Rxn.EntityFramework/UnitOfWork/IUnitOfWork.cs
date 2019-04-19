using System;
using System.Data.Entity;

namespace Rxn.EntityFramework.UnitOfWork
{
    public interface IUnitOfWork : IDisposable
    {
        DbContext Context { get; set; }

        void Commit();

        bool LazyLoadingEnabled { get; set; }
    }
}