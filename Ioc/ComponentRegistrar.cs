using Authentication.Authentication.Commands;
using Authentication.Authentication.Commands.Factory;
using Authentication.Model;
using Autofac;
using Common.Images;
using Common.Shared;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Repository;
using Services.Interfaces;
using System.Linq;
using System.Reflection;
using Utility;

namespace Ioc
{
    public class ComponentRegistrar : Autofac.Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            base.Load(builder);
            var assembly = Assembly.GetExecutingAssembly();
            builder.RegisterAssemblyTypes(new[] { Assembly.GetAssembly(typeof(IDateTimeService)) })
                .Where(x => x.IsClass && x.Name.EndsWith("Service"))
                .As(t => t.GetInterfaces().Any() ? t.GetInterfaces()[0] : t);

            builder.RegisterAssemblyTypes(new[] { Assembly.GetAssembly(typeof(IModelUtility)) })
                .Where(x => x.IsClass && x.Name.EndsWith("Utility"))
                .As(t => t.GetInterfaces().Any() ? t.GetInterfaces()[0] : t);

            builder.RegisterAssemblyTypes(new[] { Assembly.GetAssembly(typeof(IDapperRepository)) })
            .Where(x => x.IsClass && x.Name.EndsWith("Repository"))
            .As(t => t.GetInterfaces().Any() ? t.GetInterfaces()[0] : t);

            builder.RegisterAssemblyTypes(new[] { Assembly.GetAssembly(typeof(ILoginCommand)) })
            .Where(x => x.IsClass && x.Name.EndsWith("Command"))
            .As(t => t.GetInterfaces().Any() ? t.GetInterfaces()[0] : t);

            builder.RegisterAssemblyTypes(new[] { Assembly.GetAssembly(typeof(JwtIssuerOptions)) })
            .Where(x => x.IsClass && x.Name.EndsWith("Options"))
            .As(t => t.GetInterfaces().Any() ? t.GetInterfaces()[0] : t);

            builder.RegisterAssemblyTypes(new[] { Assembly.GetAssembly(typeof(IJwtFactory)) })
              .Where(x => x.IsClass && x.Name.EndsWith("Factory"))
              .As(t => t.GetInterfaces().Any() ? t.GetInterfaces()[0] : t);

            builder.RegisterAssemblyTypes(new[] { Assembly.GetAssembly(typeof(IAccountServices)) })
                .Where(x => x.IsClass && x.Name.EndsWith("Services"))
                .As(t => t.GetInterfaces().Any() ? t.GetInterfaces()[0] : t);

            builder.RegisterAssemblyTypes(new[] { Assembly.GetAssembly(typeof(IAuthenticatedUserService)) })
                .Where(x => x.IsClass && x.Name.EndsWith("Service"))
                .As(t => t.GetInterfaces().Any() ? t.GetInterfaces()[0] : t);

            //----------------------Common-----------------------------------------------
            builder.RegisterType<UnitOfWork>().As<IUnitOfWork>();
            builder.RegisterType<HttpContextAccessor>().As<IHttpContextAccessor>();
            builder.RegisterType<ImageHandler>().As<IImageHandler>();
            builder.RegisterType<ImageWriter>().As<IImageWriter>();
            builder.RegisterType<DbContext>().As<DbContext>();
        }
    }
}
