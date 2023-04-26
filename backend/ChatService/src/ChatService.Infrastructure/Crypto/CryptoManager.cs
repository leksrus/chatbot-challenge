using System.Security.Cryptography;
using System.Text;
using ChatService.Domain.Crypto;

namespace ChatService.Infrastructure.Crypto;

public class CryptoManager : ICryptoManager
{
    private const int KeySize = 64;
    private const int Iterations = 350000;
    private HashAlgorithmName _hashAlgorithmName =>  HashAlgorithmName.SHA512;
    public string GetHashString(string value, byte[] salt)
    {
        
        return Convert.ToBase64String(Rfc2898DeriveBytes.Pbkdf2(
            Encoding.UTF8.GetBytes(value),
            salt,
            Iterations,
            _hashAlgorithmName,
            KeySize));
    }

    public byte[] GetSalt()
    {
        return new byte[KeySize];
    }

    public bool VerifyHash(string value, string hashValue, byte[] salt)
    {
        var hashToCompare = Rfc2898DeriveBytes.Pbkdf2(value, salt, Iterations, _hashAlgorithmName, KeySize);
        
        return hashToCompare.SequenceEqual(Convert.FromBase64String(hashValue));
    }
}