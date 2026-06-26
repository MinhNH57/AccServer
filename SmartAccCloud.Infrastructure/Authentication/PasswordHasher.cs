using System.Security.Cryptography;
using System.Text;
using Microsoft.Extensions.Configuration;
using SmartAccCloud.Application.Interfaces.Identities;

namespace SmartAccCloud.Infrastructure.Authentication;

public class PasswordHasher : IPasswordHasher
{
    private readonly IConfiguration _configuration;
    private string sPassKey = string.Empty;

    public PasswordHasher(IConfiguration configuration)
    {
        _configuration = configuration;
        sPassKey = _configuration.GetSection("Cryptography")["Secret"]!;
    }

    public string EncryptMd5(string sOriginal, bool isNew = false)
    {
        try
        {
            if (string.IsNullOrEmpty(sOriginal))
                sOriginal = "SmartSoftware";
            byte[] sPassKeyArray;
            byte[] sOriginalArray = UTF8Encoding.UTF8.GetBytes(sOriginal);
            MD5CryptoServiceProvider MD5Hash = new MD5CryptoServiceProvider();

            if (!isNew)
            {
                sPassKey = _configuration.GetSection("Cryptography")["SecretOld"]!;
            }
            sPassKeyArray = MD5Hash.ComputeHash(UTF8Encoding.UTF8.GetBytes(sPassKey));
            MD5Hash.Clear();
            TripleDESCryptoServiceProvider tripleDesCsp = new TripleDESCryptoServiceProvider()
                { Key = sPassKeyArray, Mode = CipherMode.ECB, Padding = PaddingMode.PKCS7 };
            ICryptoTransform cTransform = tripleDesCsp.CreateEncryptor();
            byte[] resultArray = cTransform.TransformFinalBlock(sOriginalArray, 0, sOriginalArray.Length);
            tripleDesCsp.Clear();
            return Convert.ToBase64String(resultArray, 0, resultArray.Length);
        }
        catch (System.Exception e)
        {
            throw e;
        }
    }
    public string DecryptMd5(string sToOriginal, bool isNew = true)
    {
        try
        {
            if (string.IsNullOrEmpty(sToOriginal))
                return "SmartSoftware";
            byte[] sPassKeyArray;
            byte[] sOriginalArray = Convert.FromBase64String(sToOriginal);
            MD5CryptoServiceProvider MD5Hash = new MD5CryptoServiceProvider();
            if (!isNew)
            {
                sPassKey = _configuration.GetSection("Cryptography")["SecretOld"]!;
            }
            sPassKeyArray = MD5Hash.ComputeHash(UTF8Encoding.UTF8.GetBytes(sPassKey));
            MD5Hash.Clear();
            TripleDESCryptoServiceProvider tripleDesCsp = new TripleDESCryptoServiceProvider()
                { Key = sPassKeyArray, Mode = CipherMode.ECB, Padding = PaddingMode.PKCS7 };
            ICryptoTransform cTransform = tripleDesCsp.CreateDecryptor();
            byte[] resultArray = cTransform.TransformFinalBlock(sOriginalArray, 0, sOriginalArray.Length);
            tripleDesCsp.Clear();
            return UTF8Encoding.UTF8.GetString(resultArray);
        }
        catch (System.Exception e)
        {
            throw e;
        }
    }
}