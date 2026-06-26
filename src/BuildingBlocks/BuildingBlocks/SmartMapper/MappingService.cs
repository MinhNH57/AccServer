using System.Reflection;
using System.Xml.Serialization;

namespace BuildingBlocks.SmartMapper;
public class MappingService : IMappingService
{
    // Cache để tăng tốc 30–50 lần
    private static readonly Dictionary<Type, PropertyInfo[]> Cache = new();

    private PropertyInfo[] GetProperties(Type type)
    {
        if (!Cache.TryGetValue(type, out var props))
        {
            props = type.GetProperties(BindingFlags.Public | BindingFlags.Instance);
            Cache[type] = props;
        }
        return props;
    }


    /// <summary>
    /// Áp dụng cấu hình custom mapping từ IMapConfiguration nếu có
    /// </summary>
    private void ApplyConfiguration<TSource, TDestination>(MapOptions options)
    {
        var configType = AppDomain.CurrentDomain.GetAssemblies()
            .SelectMany(a => a.GetTypes())
            .FirstOrDefault(t => typeof(IMapConfiguration<TSource, TDestination>).IsAssignableFrom(t)
                                 && t is { IsInterface: false, IsAbstract: false });
        if (configType != null)
        {
            var config = (IMapConfiguration<TSource, TDestination>)Activator.CreateInstance(configType)!;
            options.CustomPropertyMaps.AddRange(config.GetCustomMappings());
        }
    }


    /// <summary>
    /// Map giữa 2 Dto với nhau
    /// </summary>
    /// <typeparam name="TSource">Model nguồn</typeparam>
    /// <typeparam name="TDestination">Model cần map đến</typeparam>
    /// <param name="source">Dữ liệu của input</param>
    /// <param name="options">Các thuộc tính không map</param>
    /// <returns></returns>
    public TDestination Map<TSource, TDestination>(TSource source, MapOptions? options = null)
        where TDestination : new()
    {
        if (source == null) return default!;

        options ??= new MapOptions();
        ApplyConfiguration<TSource, TDestination>(options);

        var dest = new TDestination();
        var srcProps = GetProperties(typeof(TSource));
        var destProps = GetProperties(typeof(TDestination));

        foreach (var destProp in destProps)
        {
            if (!destProp.CanWrite) continue;
            if (destProp.GetCustomAttribute<SmartMapIgnoreAttribute>() != null) continue;
            if (options.IgnoreProperties.Contains(destProp.Name)) continue;

            var customMap = options.CustomPropertyMaps.FirstOrDefault(m => m.Destination == destProp.Name);
            PropertyInfo? srcProp = null;
            if (customMap != null)
            {
                srcProp = srcProps.FirstOrDefault(p => p.Name == customMap.Source);
            }
            else
            {
                srcProp = srcProps.FirstOrDefault(p => p.Name == destProp.Name);
            }

            if (srcProp == null) continue;

            var value = srcProp.GetValue(source);
            destProp.SetValue(dest, value);
        }

        return dest;
    }

    /// <summary>
    /// Map giữa 2 List Dto với nhau
    /// </summary>
    /// <typeparam name="TSource">List data nguồn</typeparam>
    /// <typeparam name="TDestination">List data cần map đến</typeparam>
    /// <param name="sourceList">Dữ liệu của input</param>
    /// <param name="options">Các thuộc tính không map</param>
    /// <returns></returns>
    public List<TDestination> MapList<TSource, TDestination>(
        IEnumerable<TSource> sourceList,
        MapOptions? options = null)
        where TDestination : new()
    {
        var result = new List<TDestination>();

        options ??= new MapOptions();
        ApplyConfiguration<TSource, TDestination>(options);

        var srcProps = GetProperties(typeof(TSource));
        var destProps = GetProperties(typeof(TDestination));

        // Cache các property map được
        var mappableProps = destProps
            .Where(dp => dp.CanWrite
                         && dp.GetCustomAttribute<SmartMapIgnoreAttribute>() == null
                         && !options.IgnoreProperties.Contains(dp.Name))
            .Select(dp =>
            {
                var customMap = options.CustomPropertyMaps.FirstOrDefault(m => m.Destination == dp.Name);
                var srcProp = customMap != null
                    ? srcProps.FirstOrDefault(p => p.Name == customMap.Source)
                    : srcProps.FirstOrDefault(p => p.Name == dp.Name);
                return new { Dest = dp, Src = srcProp };
            })
            .Where(p => p.Src != null)
            .ToList();

        foreach (var source in sourceList)
        {
            var dest = new TDestination();
            foreach (var p in mappableProps)
            {
                var value = p.Src!.GetValue(source);
                p.Dest.SetValue(dest, value);
            }
            result.Add(dest);
        }

        return result;
    }
    /// <summary>
    /// Map dữ liệu từ DTO vào entity, bỏ qua các property có [SmartMapIgnore]
    /// </summary>
    /// <typeparam name="TSource">Loại DTO</typeparam>
    /// <typeparam name="TDestination">Loại entity</typeparam>
    /// <param name="source">DTO nguồn</param>
    /// <param name="destination">Entity đích</param>
    public void MapExistingModels<TSource, TDestination>(TSource source, TDestination destination)
    {
        if (source == null || destination == null) return;

        var sourceProps = typeof(TSource).GetProperties(BindingFlags.Public | BindingFlags.Instance);
        var destProps = typeof(TDestination).GetProperties(BindingFlags.Public | BindingFlags.Instance);

        foreach (var destProp in destProps)
        {
            // Bỏ qua property có attribute SmartMapIgnore
            if (destProp.GetCustomAttribute<SmartMapIgnoreAttribute>() != null)
                continue;

            // Tìm property tương ứng cùng tên và kiểu tương thích
            var sourceProp = Array.Find(sourceProps, p => p.Name == destProp.Name && destProp.PropertyType.IsAssignableFrom(p.PropertyType));
            if (sourceProp != null)
            {
                var value = sourceProp.GetValue(source);
                destProp.SetValue(destination, value);
            }
        }
    }

    /// <summary>
    /// Map danh sách DTO vào danh sách entity đã có sẵn (update), dựa trên property key
    /// </summary>
    /// <typeparam name="TSource">DTO</typeparam>
    /// <typeparam name="TDestination">Entity</typeparam>
    /// <typeparam name="TKey"></typeparam>
    /// <param name="sources">Danh sách DTO</param>
    /// <param name="destinations">Danh sách entity đã load từ DB</param>
    /// <param name="keySelectorSource">Func chọn key từ DTO</param>
    /// <param name="keySelectorDest">Func chọn key từ entity</param>
    public void MapListExistingUpdate<TSource, TDestination, TKey>(IEnumerable<TSource> sources, IList<TDestination> destinations, Func<TSource, TKey> keySelectorSource,
        Func<TDestination, TKey> keySelectorDest) where TKey : notnull
    {
        var destDict = destinations.ToDictionary(keySelectorDest);

        foreach (var source in sources)
        {
            var key = keySelectorSource(source);
            if (destDict.TryGetValue(key, out var dest))
            {
                MapExistingModels(source, dest);
            }
        }
    }
    /// <summary>
    /// Map từ danh sách nguồn sang một đối tượng đích tổng hợp DTO
    /// </summary>
    /// <typeparam name="TSource">Nguồn</typeparam>
    /// <typeparam name="TDestination">Đích</typeparam>
    /// <param name="sourceList"></param>
    /// <param name="options"></param>
    /// <returns></returns>
    public TDestination MapFromList<TSource, TDestination>(IEnumerable<TSource> sourceList, MapOptions? options = null) where TDestination : new()
    {
        var destination = new TDestination();

        if (sourceList == null)
            return destination;

        var first = sourceList.FirstOrDefault();
        if (first == null)
            return destination;

        // tái sử dụng logic mapping của bạn
        MapExistingModels(first, destination);

        return destination;
    }
}