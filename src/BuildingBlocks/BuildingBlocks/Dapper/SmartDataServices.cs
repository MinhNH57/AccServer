using System.Data;
using AsyncDapperExtensions;
using Dapper;
using Microsoft.Data.SqlClient;

namespace BuildingBlocks.Dapper;

[Serializable]
public class SmartDataServices
{
    /// <summary>
    /// Hàm rả về danh sách bản ghi sử dụng TQuery
    /// </summary>
    /// <typeparam name="T">Kiểu dữ liệu trả vê</typeparam>
    /// <param name="strQuery">Query</param>
    /// <param name="strConnect">Chuỗi kết nối</param>
    /// <param name="clt"></param>
    /// <returns>T</returns>
    public async Task<List<T>> GetListObject<T>(string strQuery, string strConnect, CancellationToken clt = default)
    {
        await using (var conn = new SqlConnection(strConnect))
        {
            if (conn.State == ConnectionState.Closed) { await conn.OpenAsync(clt); }
            try
            {
                var arr = await conn.QueryAsyncWithToken<T>(strQuery, commandType: CommandType.Text, cancellationToken: clt)
                    .ConfigureAwait(false);
                return arr.ToList();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                throw;
            }
        }
    }
    /// <summary>
    /// Hàm rả về 1 giá trị
    /// </summary>
    /// <typeparam name="T">Kiểu dữ liệu trả vê</typeparam>
    /// <param name="strQuery">Query</param>
    /// <param name="strConnect">Chuỗi kết nối</param> 
    /// <returns>T</returns>
    public async Task<decimal> GetValue(string strQuery, string strConnect)
    {
        IEnumerable<decimal> arr;
        await using (var conn = new SqlConnection(strConnect))
        {
            if (conn.State == ConnectionState.Closed) { conn.Open(); }
            try
            {
                arr = await conn.QueryAsync<decimal>(strQuery, commandType: CommandType.Text);
            }
            finally { conn.Close(); }
        }
        return arr.First();
    }

    /// <summary>
    /// Hàm rả về danh sách bản ghi sử dụng Store Procedure 
    /// </summary>
    /// <typeparam name="T">Kiểu dữ liệu trả vê</typeparam>
    /// <param name="strStoreName">Tên Store Procedure </param>
    /// <param name="strConnect">Chuỗi kết nối</param>
    /// <param name="values">Danh sách tham số</param>
    /// <param name="clt"></param>
    /// <returns>T</returns>
    public async Task<List<T>> GetListObject<T>(string strStoreName, string strConnect, object values, CancellationToken clt = default)
    {
        ArgumentNullException.ThrowIfNull(strConnect);

        await using var conn = new SqlConnection(strConnect);
        if (conn.State == ConnectionState.Closed) { await conn.OpenAsync(clt); }
        try
        {
            var objList = await conn.QueryAsyncWithToken<T>(strStoreName, values,
                    commandType: CommandType.StoredProcedure, cancellationToken: clt)
                .ConfigureAwait(false);
            return objList.ToList();
        }

        catch (Exception ex)
        {
            Console.WriteLine(ex.Message);
            throw;
        }
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
            finally { conn.Close(); }
        }

        var enumerable = arr as T[] ?? arr.ToArray();
        if (enumerable.Any())
        { return enumerable.First(); }

        return a;
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
            finally { conn.Close(); }
        }

        var enumerable = arr as T[] ?? arr.ToArray();
        if (enumerable.Any())
        { return enumerable.First(); }

        return a;
    }

    public async Task<dynamic?> GetSingleObjectStore(string strStoreName, string strConnect, object values)
    {
        dynamic? arr;
        //dynamic? a = default;
        await using var conn = new SqlConnection(strConnect);
        if (conn.State == ConnectionState.Closed)
            conn.Open();
        try
        {
            arr = await conn.QuerySingleOrDefaultAsync(strStoreName, values, commandType: CommandType.StoredProcedure);
        }
        finally { conn.Close(); }

        return arr;
    }

    public async Task<dynamic?> GetSingleObjectQuery(string strQuery, string strConnect)
    {
        dynamic? arr;
        //dynamic? a = default;
        await using var conn = new SqlConnection(strConnect);
        if (conn.State == ConnectionState.Closed)
            conn.Open();
        try
        {
            arr = await conn.QuerySingleOrDefaultAsync(strQuery, commandType: CommandType.Text);
        }
        finally { conn.Close(); }

        return arr;
    }


    public async Task<bool> ExcuteNonQueryAsync(string strQuery, string strConnec)
    {
        bool result;
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
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message);
            throw;
        }
        return result;
    }

    public async Task<bool> ExcuteNonQueryAsync(string strStoreName, string strConnec, object values, CancellationToken clt = default)
    {
        ArgumentNullException.ThrowIfNull(strConnec);

        bool rsl = false;
        await using var conn = new SqlConnection(strConnec);
        if (conn.State == ConnectionState.Closed) { await conn.OpenAsync(clt); }

        try
        {
            await conn.ExecuteAsyncWithToken(strStoreName, values, commandTimeout: 120,
                commandType: CommandType.StoredProcedure, cancellationToken: clt)
                .ConfigureAwait(false);
            rsl = true;
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message);
            throw;
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
            {
                DataTable tb = new("SmartData");
                tb.Load(rd);
                dataSet.Tables.Add(tb);
            }
        }
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
            {
                DataTable tb = new("SmartData");
                tb.Load(rd);
                dataSet.Tables.Add(tb);
            }
        }
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
            tb.Load(rd);
        }
        finally { conn.Close(); }

        return tb;
    }
    public static DataTable ExcuteStoreReturnDataTableNotAsync(string strConnect, string strStoreName, object values)
    {
        DataTable tb = new DataTable { TableName = "Smart" };
        using var conn = new SqlConnection(strConnect);
        if (conn.State == ConnectionState.Closed) { conn.Open(); }
        try
        {
            IDataReader rd = conn.ExecuteReader(strStoreName, values, commandType: CommandType.StoredProcedure);
            tb.Load(rd);
        }
        finally { conn.Close(); }

        return tb;
    }

    /// <summary>
    /// Hàm trả về một DataTable sau khi thực thi một truy vấn
    /// </summary>
    /// <param name="strTableName"></param>
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
            tb.Load(rd);
        }
        finally { conn.Close(); }

        return tb;
    }

    /// <summary>
    /// Hàm trả về một DataTable sau khi thực thi một truy vấn
    /// </summary>
    /// <param name="strTableName"></param>
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
            tb.Load(rd);
        }
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
            tb.Load(rd);
        }
        finally { conn.Close(); }

        return tb;
    }

    public async Task<int> ExcuteNonQuery(string strStoreName, string strConnec, object values, CancellationToken clt = default)
    {
        int result = 0;
        await using var conn = new SqlConnection(strConnec);
        if (conn.State == ConnectionState.Closed) { await conn.OpenAsync(clt); }

        try
        {
            result = await conn.ExecuteAsyncWithToken(strStoreName, values, commandTimeout: 120,
                commandType: CommandType.StoredProcedure, cancellationToken: clt)
                .ConfigureAwait(false);
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message);
            throw;
        }

        return result;
    }
    public async Task<List<T>> ExecQueryListAsync<T>(string spName, string connStr, object? parameters = null, CancellationToken ct = default)
    {
        await using var conn = new SqlConnection(connStr);
        if (conn.State == ConnectionState.Closed) await conn.OpenAsync(ct);

        var cmd = new CommandDefinition(
            spName, parameters, commandType: CommandType.StoredProcedure,
            commandTimeout: 120, cancellationToken: ct);

        var rows = await conn.QueryAsync<T>(cmd);
        return rows.AsList();
    }
    public static async Task<bool> Exists(string strQuery, string strConnect)
    {
        bool rsl;
        await using var conn = new SqlConnection(strConnect);
        if (conn.State == ConnectionState.Closed)
        { conn.Open(); }
        try
        {
            string? strCode = await conn.ExecuteScalarAsync<string>(strQuery, commandType: CommandType.Text);
            rsl = !string.IsNullOrEmpty(strCode);
        }
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
        finally { conn.Close(); }
    }

    public static async Task<bool> CheckDuplicatesInvNumber(string strInvNumber, string strTaxcode, string strId, string strConnect)
    {
        await using var conn = new SqlConnection(strConnect);
        if (conn.State == ConnectionState.Closed)
        { conn.Open(); }
        try
        {
            string? strCode = await conn.ExecuteScalarAsync<string>(
                $"select InvoiceNumber from SmartVatTaxList where InvoiceNumber=N'{strInvNumber}' and TaxCode=N'{strTaxcode}' and IdContents<>'{strId}'", commandType: CommandType.Text);
            return string.IsNullOrEmpty(strCode);
        }
        finally { conn.Close(); }
    }
    public static async Task<double> ExecuteScalaryReturnValue(string strConnect, string strQuery)
    {
        double rsl;
        await using var conn = new SqlConnection(strConnect);
        if (conn.State == ConnectionState.Closed) { conn.Open(); }
        try { rsl = await conn.ExecuteScalarAsync<double>(strQuery); }
        finally { conn.Close(); }

        return rsl;
    }
    public static double ExecuteScalarReturnValueNotAsync(string strConnect, string strQuery)
    {
        double rsl;
        using var conn = new SqlConnection(strConnect);
        if (conn.State == ConnectionState.Closed) { conn.Open(); }
        try { rsl = conn.ExecuteScalar<double>(strQuery); }
        finally { conn.Close(); }

        return rsl;
    }

    public static object? ExecuteScalarReturnObjectNotAsync(string strConnect, string strQuery)
    {
        object? rsl;
        using var conn = new SqlConnection(strConnect);
        if (conn.State == ConnectionState.Closed) { conn.Open(); }
        try { rsl = conn.ExecuteScalar<object>(strQuery); }
        finally { conn.Close(); }

        return rsl;
    }
}