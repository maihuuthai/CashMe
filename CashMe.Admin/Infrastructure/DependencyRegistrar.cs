using Autofac;
using Autofac.Integration.Mvc;
using CashMe.Service;
using CashMe.Admin;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using CashMe.Service.Role;
using CashMe.Service.DisplayMenu;

namespace CashMe.Infrastructure
{
    public static class DependencyRegistrar
    {
        public static void Register()
        {
            var builder = new ContainerBuilder();
            builder.RegisterControllers(typeof(MvcApplication).Assembly);
            // MVC - OPTIONAL: Register model binders that require DI.
            builder.RegisterModelBinders(typeof(MvcApplication).Assembly);
            builder.RegisterModelBinderProvider();

            // MVC - OPTIONAL: Register web abstractions like HttpContextBase.
            builder.RegisterModule<AutofacWebTypesModule>();

            // MVC - OPTIONAL: Enable property injection in view pages.
            builder.RegisterSource(new ViewRegistrationSource());

            // MVC - OPTIONAL: Enable property injection into action filters.
            builder.RegisterFilterProvider();

            //installation localization service
            builder.RegisterType<CategoriesService>().As<ICategoriesService>().InstancePerLifetimeScope();
            builder.RegisterType<PercentService>().As<IPercentService>().InstancePerLifetimeScope();
            builder.RegisterType<Linked_SiteService>().As<ILinked_SiteService>().InstancePerLifetimeScope();
            builder.RegisterType<VoucherService>().As<IVoucherService>().InstancePerLifetimeScope();
            builder.RegisterType<CashbackService>().As<ICashbackService>().InstancePerLifetimeScope();
            builder.RegisterType<GroupSiteService>().As<IGroupSiteService>().InstancePerLifetimeScope();
            builder.RegisterType<CashoutService>().As<ICashoutService>().InstancePerLifetimeScope();
            builder.RegisterType<MessageService>().As<IMessageService>().InstancePerLifetimeScope();
            builder.RegisterType<AccountServices>().As<IAccountServices>().InstancePerLifetimeScope();
            builder.RegisterType<RoleServices>().As<IRoleServices>().InstancePerLifetimeScope();
            builder.RegisterType<DisplayMenuServices>().As<IDisplayMenuServices>().InstancePerLifetimeScope();

            // Set the dependency resolver to be Autofac.
            var container = builder.Build();
            DependencyResolver.SetResolver(new AutofacDependencyResolver(container));
        }
    }

}
