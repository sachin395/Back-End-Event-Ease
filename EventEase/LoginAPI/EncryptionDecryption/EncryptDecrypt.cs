using System.Security.Cryptography;
using System.Text;

namespace LoginAPI.EncryptionDecryption
{
    public class EncryptDecrypt
    {
        public string DecryptString(string cipherText)
        {
            Aes aes = GetEncryptionAlgorithm();
            byte[] buffer = Convert.FromBase64String(cipherText);
            MemoryStream memoryStream = new MemoryStream(buffer);
            ICryptoTransform decryptor = aes.CreateDecryptor(aes.Key, aes.IV);
            CryptoStream cryptoStream = new CryptoStream(memoryStream, decryptor, CryptoStreamMode.Read);
            StreamReader streamReader = new StreamReader(cryptoStream);
            return streamReader.ReadToEnd();
        }
        public Aes GetEncryptionAlgorithm()
        {
            Aes aes = Aes.Create();

            var secret_key = Encoding.UTF8.GetBytes("dfddtftfkffttkftkdddfddt");
            var initialization_vector = Encoding.UTF8.GetBytes("dfddtftfkffttkfh");

            aes.Key = secret_key;
            aes.IV = initialization_vector;
            return aes;
        }
        public string EncryptUsingAES256(string plainText)
        {
            using (Aes aesAlg = Aes.Create())
            {
                aesAlg.Key = Encoding.UTF8.GetBytes("dfddtftfkffttkftkdddfddt"); ;
                aesAlg.IV = Encoding.UTF8.GetBytes("dfddtftfkffttkfh");
                ICryptoTransform encryptor = aesAlg.CreateEncryptor(aesAlg.Key, aesAlg.IV);
                using (MemoryStream msEncrypt = new MemoryStream())
                {
                    using (CryptoStream csEncrypt = new CryptoStream(msEncrypt, encryptor, CryptoStreamMode.Write))
                    {
                        using (StreamWriter swEncrypt = new StreamWriter(csEncrypt))
                        {
                            swEncrypt.Write(plainText);
                        }
                    }
                    return Convert.ToBase64String(msEncrypt.ToArray());
                }
            }
        }
    }
}
