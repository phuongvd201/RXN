using System;

using Autofac;
using Autofac.Integration.WebApi;

using Rxn.EntityFramework.Repositories;
using Rxn.EntityFramework.UnitOfWork;
using Rxn.Services;
using Rxn.WebApi.Controllers;

namespace Rxn.WebApi
{
    public static class AutofacConfig
    {
        public static IContainer Container { get; private set; }

        public static void Register()
        {
            Container = CreateContainer();
        }

        internal static IContainer CreateContainer()
        {
            var builder = new ContainerBuilder();
            builder.RegisterApiControllers(typeof(ApiControllerBase).Assembly);
            RegisterComponents(builder);

            return builder.Build();
        }

        private static void RegisterComponents(ContainerBuilder builder)
        {
            var servicesAssembly = typeof(IProductService).Assembly;

            builder.RegisterAssemblyTypes(servicesAssembly)
                .Where(x => x.Name.EndsWith("Service", StringComparison.InvariantCulture))
                .AsImplementedInterfaces()
                .InstancePerDependency();

            builder.RegisterGeneric(typeof(EfRepository<>))
                .As(typeof(IRepository<>))
                .InstancePerDependency();

            builder.RegisterType(typeof(EfUnitOfWork))
                .As(typeof(IUnitOfWork))
                .InstancePerDependency();
        }
    }
}