namespace BuildingBlocks.SmartMapper;
public interface IMapConfiguration<TSource, TDestination>
{
    List<PropertyMap> GetCustomMappings();
}
