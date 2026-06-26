namespace SmartAccCloud.Application.Interfaces.Identities;

public interface IPasswordHasher
{
    string EncryptMd5(string sOriginal, bool isNew = true);
    string DecryptMd5(string sToOriginal, bool isNew = true);
}