using AutoMapper;

namespace Utility
{
    public class MappingUtility
    {
        public static TDest Map<TSource, TDest>(TSource viewModel)
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<TSource, TDest>();
            });
            IMapper mapper = config.CreateMapper();
            TDest result = mapper.Map<TSource, TDest>(viewModel);
            return result;
        }
    }
}
