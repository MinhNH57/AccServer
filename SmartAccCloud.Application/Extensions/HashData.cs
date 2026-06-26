using System.Security.Cryptography;
using System.Text;

namespace SmartAccCloud.Application.Extensions;

public static class HashData
{
    /// <summary>
    /// Mã hoá sha256 + base64
    /// </summary>
    /// <param name="input">Chuỗi cần mã hóa</param>
    /// <returns></returns>
    public static string ComputeSha256(string input)
    {
        using SHA256 sha256 = SHA256.Create();

        byte[] bytes = Encoding.UTF8.GetBytes(input);
        byte[] hashBytes = sha256.ComputeHash(bytes);

        return Convert.ToBase64String(hashBytes);
    }
    public static DateTime ToDateTime(this long timestamp)
    {
        var datatimeOffset = DateTimeOffset.FromUnixTimeMilliseconds(timestamp);
        return datatimeOffset.ToOffset(TimeSpan.FromHours(7)).DateTime;
    }
}