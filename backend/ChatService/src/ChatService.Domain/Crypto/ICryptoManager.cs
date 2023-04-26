namespace ChatService.Domain.Crypto;

public interface ICryptoManager
{
    string GetHashString(string value, byte[] salt);

    byte[] GetSalt();

    bool VerifyHash(string value, string hashValue, byte[] salt);
}