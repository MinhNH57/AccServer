using Microsoft.Data.SqlClient;
using System.Data;
using Dapper;
using SmartAccCloud.Application.Data;


namespace SmartAccCloud.Infrastructure.Persistence.Dapper;

[Serializable]
public class SmartDataServices : IDataServices
{
    /// <summary>
    /// Hàm rả về danh sách bản ghi sử dụng TQuery
    /// </summary>
    /// <typeparam name="T">Kiểu dữ liệu trả vê</typeparam>
    /// <param name="strQuery">Query</param>
    /// <param name="strConnect">Chuỗi kết nối</param> 
    /// <returns>T</returns>
    public async Task<List<T>> GetListObject<T>(string strQuery, string strConnect)
    {
        IEnumerable<T> arr;
        await using (var conn = new SqlConnection(strConnect))
        {
            if (conn.State == ConnectionState.Closed) { conn.Open(); }
            try
            {

                arr = await conn.QueryAsync<T>(strQuery, commandType: CommandType.Text);
            }
            catch { throw; }
            finally { conn.Close(); }
        }
        return arr.ToList();
    }
    /// <summary>
    /// Hàm rả về danh sách bản ghi sử dụng Store Procedure 
    /// </summary>
    /// <typeparam name="T">Kiểu dữ liệu trả vê</typeparam>
    /// <param name="strStoreName">Tên Store Procedure </param>
    /// <param name="strConnect">Chuỗi kết nối</param>
    /// <param name="values">Danh sách tham số</param>
    /// <returns>T</returns>
    public async Task<List<T>> GetListObject<T>(string strStoreName, string strConnect, object values)
    {

        IEnumerable<T> objList;
        await using (var conn = new SqlConnection(strConnect))
        {
            if (conn.State == ConnectionState.Closed) { conn.Open(); }
            try
            {
                objList = await conn.QueryAsync<T>(strStoreName, values, commandType: CommandType.StoredProcedure);
            }
            catch { throw; }
            finally { conn.Close(); }
        }
        return objList.ToList();
    }
    /// <summary>
    /// Hàm thực hiện đọc một bản ghi sử dụng TQuery
    /// </summary>
    /// <typeparam name="T">Kiểu dữ liệu trả về</typeparam>
    /// <param name="strQuery">Query</param>
    /// <param name="strConnect">Chuỗi kết nối</param> 
    /// <returns>T</returns>
    public async Task<T> GetSingleObject<T>(string strQuery, string strConnect)
    {
        IEnumerable<T> arr;
        T? a = default;
        await using (var conn = new SqlConnection(strConnect))
        {
            if (conn.State == ConnectionState.Closed)
            { conn.Open(); }
            try
            {
                arr = await conn.QueryAsync<T>(strQuery, commandType: CommandType.Text);
            }
            catch
            { throw; }
            finally { conn.Close(); }
        }
        if (arr.Any())
        { return arr.First(); }
        else return a;
    }
    /// <summary>
    /// Hàm thực hiện đọc một bản ghi sử dụng Store Procedure
    /// </summary>
    /// <typeparam name="T">Kiểu dữ liệu trả về</typeparam>
    /// <param name="strStoreName">Store Procedure</param>
    /// <param name="strConnect">Chuỗi kết nối</param>
    /// <param name="values">Các tham số của Store</param>
    /// <returns>T</returns>
    public async Task<T> GetSingleObject<T>(string strStoreName, string strConnect, object values)
    {
        IEnumerable<T> arr;
        T? a = default;
        await using (var conn = new SqlConnection(strConnect))
        {
            if (conn.State == ConnectionState.Closed)
            { conn.Open(); }
            try
            {
                arr = await conn.QueryAsync<T>(strStoreName, values, commandType: CommandType.StoredProcedure);
            }
            catch
            { throw; }
            finally { conn.Close(); }
        }
        if (arr.Any())
        { return arr.First(); }
        else return a;
    }





    public async Task<bool> ExcuteNonQueryAsync(string strQuery, string strConnec)
    {
        bool result = false;
        SqlConnection cnn = new SqlConnection(strConnec);
        SqlCommand command = new SqlCommand(strQuery, cnn);
        try
        {
            if (cnn.State == ConnectionState.Closed)
                cnn.Open();
            command.CommandTimeout = 60;
            await command.ExecuteNonQueryAsync();
            result = true;
        }
        catch (System.Exception ex)
        {
            throw ex;
        }
        return result;
    }
    public async Task<bool> ExcuteNonQueryAsync(string strStoreName, string strConnec, object values)
    {
        bool rsl = false;
        using (var conn = new SqlConnection(strConnec))
        {
            if (conn.State == ConnectionState.Closed) { conn.Open(); }

            try
            {
                await conn.ExecuteAsync(strStoreName, values, commandTimeout: 120,
                    commandType: CommandType.StoredProcedure);
                rsl = true;
            }
            catch (System.Exception ex)
            {
                throw ex;
            }
            finally
            {
                conn.Close(); conn.Dispose();
            }
        }
        return rsl;
    }

    /// <summary>
    /// Hàm trả về một DataSet sau khi thực thi một store procedure
    /// </summary>
    /// <param name="strConnect">Chuỗi kết nối</param>
    /// <param name="strStoreName">Tên store procedure</param>
    /// <param name="values">Arr tham số var values = new { IdDonViOk = 100, Paramater = "TONTHEOMAY", Mid = sMid }; cach truyen tham so vao values paramater</param>
    /// <returns>DataSet</returns>
    public static async Task<DataSet> ExcuteStoreReturnDataset(string strConnect, string strStoreName, object values)
    {
        DataSet dataSet = new();
        await using var conn = new SqlConnection(strConnect);
        if (conn.State == ConnectionState.Closed)
        { conn.Open(); }
        try
        {
            IDataReader rd = await conn.ExecuteReaderAsync(strStoreName, values, commandType: CommandType.StoredProcedure);
            if (rd != null)
            {
                DataTable tb = new("SmartData");
                tb.Load(rd);
                dataSet.Tables.Add(tb);
            }
        }
        catch { throw; }
        finally { conn.Close(); }

        return dataSet;
    }
    /// <summary>
    /// Hàm trả về một DataSet sau khi thực thi một truy vấn
    /// </summary>
    /// <param name="strConnect">Chuỗi kết nối</param>
    /// <param name="strQuery">Chuỗi truy vấn</param>
    /// <returns>DataSet</returns>
    public static async Task<DataSet> ExcuteQueryReturnDataset(string strConnect, string strQuery)
    {
        DataSet dataSet = new();
        await using var conn = new SqlConnection(strConnect);
        if (conn.State == ConnectionState.Closed) { conn.Open(); }
        try
        {
            IDataReader rd = await conn.ExecuteReaderAsync(strQuery, commandType: CommandType.Text);
            if (rd != null)
            {
                DataTable tb = new("SmartData");
                tb.Load(rd);
                dataSet.Tables.Add(tb);
            }
        }
        catch { throw; }
        finally { conn.Close(); }

        return dataSet;
    }
    /// <summary>
    /// Hàm trả về một DataTable sau khi thực thi một store procedure
    /// </summary>
    /// <param name="strConnect">Chuỗi kết nối</param>
    /// <param name="strStoreName">Tên store procedure</param>
    /// <param name="values">Arr tham số var values = new { IdDonViOk = 100, Paramater = "TONTHEOMAY", Mid = sMid }; cach truyen tham so vao values paramater</param>
    /// <returns>DataTable</returns>
    public static async Task<DataTable> ExcuteStoreReturnDataTable(string strConnect, string strStoreName, object values)
    {
        DataTable tb = new DataTable { TableName = "Smart" };
        await using var conn = new SqlConnection(strConnect);
        if (conn.State == ConnectionState.Closed) { conn.Open(); }
        try
        {
            IDataReader rd = await conn.ExecuteReaderAsync(strStoreName, values, commandType: CommandType.StoredProcedure);
            if (rd != null) { tb.Load(rd); }
        }
        catch { throw; }
        finally { conn.Close(); }

        return tb;
    }
    public static DataTable ExcuteStoreReturnDataTableNotAsync(string strConnect, string strStoreName, object values)
    {
        DataTable tb = new DataTable { TableName = "Smart" }; ;
        using var conn = new SqlConnection(strConnect);
        if (conn.State == ConnectionState.Closed) { conn.Open(); }
        try
        {
            IDataReader rd = conn.ExecuteReader(strStoreName, values, commandType: CommandType.StoredProcedure);
            if (rd != null) { tb.Load(rd); }
        }
        catch { throw; }
        finally { conn.Close(); }

        return tb;
    }
    /// <summary>
    /// Hàm trả về một DataTable sau khi thực thi một truy vấn
    /// </summary>
    /// <param name="strConnect">Chuỗi kết nối</param>
    /// <param name="strQuery">Chuỗi truy vấn</param>
    /// <returns>DataTable</returns>
    public static async Task<DataTable> ExcuteQueryReturnDataTable(string strTableName, string strConnect, string strQuery)
    {
        DataTable tb = new DataTable() { TableName = strTableName };
        await using var conn = new SqlConnection(strConnect);
        if (conn.State == ConnectionState.Closed) { conn.Open(); }
        try
        {
            IDataReader rd = await conn.ExecuteReaderAsync(strQuery, commandType: CommandType.Text);
            if (rd != null) { tb.Load(rd); }
        }
        catch { throw; }
        finally { conn.Close(); }

        return tb;
    }
    /// <summary>
    /// Hàm trả về một DataTable sau khi thực thi một truy vấn
    /// </summary>
    /// <param name="strConnect">Chuỗi kết nối</param>
    /// <param name="strQuery">Chuỗi truy vấn</param>
    /// <returns>DataTable</returns>
    public static DataTable ExcuteQueryReturnDataTableNotAsync(string strTableName, string strConnect, string strQuery)
    {
        DataTable tb = new DataTable() { TableName = strTableName };
        using var conn = new SqlConnection(strConnect);
        if (conn.State == ConnectionState.Closed) { conn.Open(); }
        try
        {
            IDataReader rd = conn.ExecuteReader(strQuery, commandType: CommandType.Text);
            if (rd != null) { tb.Load(rd); }
        }
        catch { throw; }
        finally { conn.Close(); }

        return tb;
    }
    public static DataTable ExcuteQueryReturnDataTableNotAsync(string strConnect, string strQuery)
    {
        DataTable tb = new DataTable();
        using var conn = new SqlConnection(strConnect);
        if (conn.State == ConnectionState.Closed) { conn.Open(); }
        try
        {
            IDataReader rd = conn.ExecuteReader(strQuery, commandType: CommandType.Text);
            if (rd != null) { tb.Load(rd); }
        }
        catch { throw; }
        finally { conn.Close(); }

        return tb;
    }
    public async Task<int> ExcuteNonQuery(string strStoreName, string strConnec, object values)
    {
        int result = 0;
        using (var conn = new SqlConnection(strConnec))
        {
            if (conn.State == ConnectionState.Closed) { conn.Open(); }

            try
            {
                result = await conn.ExecuteAsync(strStoreName, values, commandTimeout: 120,
                    commandType: CommandType.StoredProcedure);
            }
            catch (System.Exception ex)
            {
                throw ex;
            }
            finally
            {
                conn.Close(); conn.Dispose();
            }
        }
        return result;
    }
    public static async Task<bool> Exists(string strQuery, string strConnect)
    {
        bool rsl = false;
        await using var conn = new SqlConnection(strConnect);
        if (conn.State == ConnectionState.Closed)
        { conn.Open(); }
        try
        {
            string? strCode = await conn.ExecuteScalarAsync<string>(strQuery, commandType: CommandType.Text);
            rsl = !string.IsNullOrEmpty(strCode);
        }
        catch { throw; }
        finally { conn.Close(); }

        return rsl;
    }
    public static bool Exists(string strColumName, string strValueColumName, string strTableName, string strConnect)
    {
        using var conn = new SqlConnection(strConnect);
        if (conn.State == ConnectionState.Closed)
        { conn.Open(); }
        try
        {
            string? strCode = conn.ExecuteScalar<string>(string.Format("select {0} from {1} where {0}=N'{2}'", strColumName, strTableName, strValueColumName), commandType: CommandType.Text);
            return !string.IsNullOrEmpty(strCode);
        }
        catch { throw; }
        finally { conn.Close(); }
    }
    public static async Task<bool> CheckDuplicatesInvNumber(string strInvNumber, string strTaxcode, string strId, string strConnect)
    {
        await using var conn = new SqlConnection(strConnect);
        if (conn.State == ConnectionState.Closed)
        { conn.Open(); }
        try
        {
            string? strCode = await conn.ExecuteScalarAsync<string>(string.Format("select InvoiceNumber from SmartVatTaxList where InvoiceNumber=N'{0}' and TaxCode=N'{1}' and IdContents<>'{2}'", strInvNumber, strTaxcode, strId), commandType: CommandType.Text);
            return string.IsNullOrEmpty(strCode);
        }
        catch { throw; }
        finally { conn.Close(); }
    }
    public static async Task<double> ExecuteScalaryReturnValue(string strConnect, string strQuery)
    {
        double rsl = 0;
        await using var conn = new SqlConnection(strConnect);
        if (conn.State == ConnectionState.Closed) { conn.Open(); }
        try { rsl = await conn.ExecuteScalarAsync<double>(strQuery); }
        catch { throw; }
        finally { conn.Close(); }

        return rsl;
    }
    public static double ExecuteScalarReturnValueNotAsync(string strConnect, string strQuery)
    {
        double rsl = 0;
        using var conn = new SqlConnection(strConnect);
        if (conn.State == ConnectionState.Closed) { conn.Open(); }
        try { rsl = conn.ExecuteScalar<double>(strQuery); }
        catch { throw; }
        finally { conn.Close(); }

        return rsl;
    }

    public static object ExecuteScalarReturnObjectNotAsync(string strConnect, string strQuery)
    {
        object? rsl;
        using var conn = new SqlConnection(strConnect);
        if (conn.State == ConnectionState.Closed) { conn.Open(); }
        try { rsl = conn.ExecuteScalar<object>(strQuery); }
        catch { throw; }
        finally { conn.Close(); }

        return rsl;
    }
}