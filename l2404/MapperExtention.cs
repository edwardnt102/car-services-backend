using AutoMapper;
using Microsoft.Extensions.DependencyInjection;
using Services.Mapping;
using System.Reflection;

namespace l2404
{
    public static class MapperExtention
    {
        public static void InitMapper(this IServiceCollection service)
        {
            service.AddAutoMapper(Assembly.GetExecutingAssembly());
            var mappingConfig = new MapperConfiguration(mc =>
            {
                mc.AddProfile(new MappingProfile());
            });

            IMapper mapper = mappingConfig.CreateMapper();
            service.AddSingleton(mapper);
        }
    }
}
