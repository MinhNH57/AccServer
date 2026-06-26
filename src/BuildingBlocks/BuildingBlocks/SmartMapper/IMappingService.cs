namespace BuildingBlocks.SmartMapper;

public interface IMappingService
{
    /// <summary>
    /// Map giữa 2 Dto với nhau
    /// </summary>
    /// <typeparam name="TSource">Model nguồn</typeparam>
    /// <typeparam name="TDestination">Model cần map đến</typeparam>
    /// <param name="source">Dữ liệu của input</param>
    /// <param name="options">Các thuộc tính không map</param>
    /// <returns></returns>
    TDestination Map<TSource, TDestination>(TSource source, MapOptions? options = null)
        where TDestination : new();

    /// <summary>
    /// Map list giữa 2 Dto với nhau  
    /// </summary>
    List<TDestination> MapList<TSource, TDestination>(
        IEnumerable<TSource> sourceList,
        MapOptions? options = null)
        where TDestination : new();

    /// <summary>
    /// Map dữ liệu từ DTO vào entity, bỏ qua các property có [SmartMapIgnore]
    /// </summary>
    /// <typeparam name="TSource">Loại DTO</typeparam>
    /// <typeparam name="TDestination">Loại entity</typeparam>
    /// <param name="source">DTO nguồn</param>
    /// <param name="destination">Entity đích</param>
    void MapExistingModels<TSource, TDestination>(TSource source, TDestination destination);
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
    void MapListExistingUpdate<TSource, TDestination, TKey>(
        IEnumerable<TSource> sources,
        IList<TDestination> destinations,
        Func<TSource, TKey> keySelectorSource,
        Func<TDestination, TKey> keySelectorDest);

    /// <summary>
    /// Map từ danh sách nguồn sang một đối tượng đích tổng hợp DTO
    /// </summary>
    /// <typeparam name="TSource">Nguồn</typeparam>
    /// <typeparam name="TDestination">Đích</typeparam>
    /// <param name="sourceList"></param>
    /// <param name="options"></param>
    /// <returns></returns>
    TDestination MapFromList<TSource, TDestination>(
        IEnumerable<TSource> sourceList,
        MapOptions? options = null)
        where TDestination : new();
}
