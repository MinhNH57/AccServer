using System.ComponentModel.DataAnnotations.Schema;
using System.Security.Cryptography;
using System.Text;
using Finbuckle.MultiTenant.Abstractions;
using Microsoft.Extensions.Configuration;

namespace SmartAccCloud.Application.Models.Users;

public class TenantInfoCustomize : ITenantInfo
{
    private const string SecretKey = "Smart+_$#@!";

    public string? Id
    {
        get => CompanyId;
        set => CompanyId = value;
    }

    public string Identifier { get; set; }

    [NotMapped]
    public string? Name
    {
        get => ShortName;
        set => ShortName = value;
    }

    public string ShortName { get; set; }

    [NotMapped]
    public string CompanyId
    {
        get => Identifier;
        set => Identifier = value;
    }

    public string? StringConnection { get; set; }
    public string CompanyKey { get; set; } = string.Empty;
    public string? ServerName { get; set; }
    public string? DatabaseName { get; set; }
    public string? UserID { get; set; }
    public string? PasswordSql { get; set; }
    public string? YearWork { get; set; }
    public bool IsActive { get; set; }
    public string? CompanyName { get; set; }
    public string? CompanyAddress { get; set; }
    public string? CompanyPhone { get; set; }
    public DateTime CreateDate { get; set; }
    public string? Notes { get; set; }

    public string ConnectionString()
    {
        if (!string.IsNullOrEmpty(ServerName)
            && !string.IsNullOrEmpty(DatabaseName)
            && !string.IsNullOrEmpty(YearWork)
            && !string.IsNullOrEmpty(UserID)
            && !string.IsNullOrEmpty(PasswordSql))
            return string.Format(
                "Server={0};Database={1}{2};User ID={3};Password={4};TrustServerCertificate=True;MultipleActiveResultSets=true",
                SmartDecrypt(ServerName, SecretKey),
                SmartDecrypt(DatabaseName, SecretKey), YearWork,
                SmartDecrypt(UserID, SecretKey),
                SmartDecrypt(PasswordSql, SecretKey));

        throw new InvalidOperationException("Cannot get Connection string");
    }

    public string ConnectionDefault()
    {
        IConfiguration config = new ConfigurationManager();
        return config.GetConnectionString("MultitenantConnection")!;
    }

    private static string SmartDecrypt(string sToOriginal, string sPassKey)
    {
        if (string.IsNullOrEmpty(sToOriginal))
            return "SmartSoftware";

        byte[] sPassKeyArray;
        byte[] sOriginalArray = Convert.FromBase64String(sToOriginal);
        MD5CryptoServiceProvider MD5Hash = new MD5CryptoServiceProvider();
        sPassKeyArray = MD5Hash.ComputeHash(UTF8Encoding.UTF8.GetBytes(sPassKey));
        MD5Hash.Clear();
        TripleDESCryptoServiceProvider tripleDesCsp =
            new TripleDESCryptoServiceProvider()
            {
                Key = sPassKeyArray, Mode = CipherMode.ECB,
                Padding = PaddingMode.PKCS7
            };
        ICryptoTransform cTransform = tripleDesCsp.CreateDecryptor();
        byte[] resultArray =
            cTransform.TransformFinalBlock(sOriginalArray, 0,
                sOriginalArray.Length);
        tripleDesCsp.Clear();
        return UTF8Encoding.UTF8.GetString(resultArray);
    }
}

public class SmartProvider
{
    public static string SmartEncrypt(string sOriginal, string sPassKey)
    {
        if (string.IsNullOrEmpty(sOriginal))
            sOriginal = "SmartSoftware";
        byte[] sPassKeyArray;
        byte[] sOriginalArray = UTF8Encoding.UTF8.GetBytes(sOriginal);
        MD5CryptoServiceProvider MD5Hash = new MD5CryptoServiceProvider();
        sPassKeyArray =
            MD5Hash.ComputeHash(UTF8Encoding.UTF8.GetBytes(sPassKey));
        MD5Hash.Clear();
        TripleDESCryptoServiceProvider tripleDesCsp =
            new TripleDESCryptoServiceProvider()
            {
                Key = sPassKeyArray, Mode = CipherMode.ECB,
                Padding = PaddingMode.PKCS7
            };
        ICryptoTransform cTransform = tripleDesCsp.CreateEncryptor();
        byte[] resultArray =
            cTransform.TransformFinalBlock(sOriginalArray, 0,
                sOriginalArray.Length);
        tripleDesCsp.Clear();
        return Convert.ToBase64String(resultArray, 0, resultArray.Length);
    }
}