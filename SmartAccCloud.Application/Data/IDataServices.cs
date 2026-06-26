namespace SmartAccCloud.Application.Data;

public interface IDataServices
{
    Task<List<T>> GetListObject<T>(string strQuery, string strConnect);
    Task<List<T>> GetListObject<T>(string strStoreName, string strConnect, object values);
    Task<T> GetSingleObject<T>(string strQuery, string strConnect);
    Task<T> GetSingleObject<T>(string strQuery, string strConnect, object values);

    Task<bool> ExcuteNonQueryAsync(string str_Query, string str_Connec);
    Task<bool> ExcuteNonQueryAsync(string strStoreName, string str_Connec, object values);
    Task<int> ExcuteNonQuery(string strStoreName, string strConnec, object values);
}