using AutoMapper;
using System.Collections.Generic;

namespace TMS.Application.Mappings
{
    public static class AutomapperExtensions
    {
        public static TDestination MapObject<TSource, TDestination>(this TSource value)
        {
            var config = new MapperConfiguration(cfg => { cfg.CreateMap<TSource, TDestination>(); });
            IMapper mapper = config.CreateMapper();
            return mapper.Map<TSource, TDestination>(value);
        }

        public static IEnumerable<TDestination> MapList<TSource, TDestination>(this IEnumerable<TSource> source)
        {
            var config = new MapperConfiguration(cfg => { cfg.CreateMap<TSource, TDestination>(); });
            IMapper mapper = config.CreateMapper();
            return mapper.Map<IEnumerable<TDestination>>(source);
        }
    }
}
