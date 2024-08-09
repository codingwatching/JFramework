// *********************************************************************************
// # Project: Forest
// # Unity: 2022.3.5f1c1
// # Author: jinyijie
// # Version: 1.0.0
// # History: 2024-08-09  12:08
// # Copyright: 2024, jinyijie
// # Description: This is an automatically generated comment.
// *********************************************************************************

using System.IO;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace JFramework
{
    internal static class Obfuscator
    {
        public static byte[] Encrypt(byte[] data, string key)
        {
            using var aes = Aes.Create();
            aes.Key = Encoding.UTF8.GetBytes(key);

            var iv = new byte[16];
            RandomNumberGenerator.Fill(iv);
            aes.IV = iv;

            using var ms = new MemoryStream();
            ms.Write(iv);

            using var cs = new CryptoStream(ms, aes.CreateEncryptor(), CryptoStreamMode.Write);
            cs.Write(data);
            cs.FlushFinalBlock();

            return ms.ToArray();
        }

        public static byte[] Decrypt(byte[] data, string key)
        {
            using var aes = Aes.Create();
            aes.Key = Encoding.UTF8.GetBytes(key);

            using var ms = new MemoryStream(data);

            var iv = new byte[16];
            ms.Read(iv);
            aes.IV = iv;

            using var cs = new CryptoStream(ms, aes.CreateDecryptor(), CryptoStreamMode.Read);
            using var rs = new MemoryStream();
            cs.CopyTo(rs);

            return rs.ToArray();
        }

        public static async Task<byte[]> EncryptAsync(byte[] data, string key)
        {
            using var aes = Aes.Create();
            aes.Key = Encoding.UTF8.GetBytes(key);

            var iv = new byte[16];
            RandomNumberGenerator.Fill(iv);
            aes.IV = iv;

            await using var ms = new MemoryStream();
            await ms.WriteAsync(iv);

            await using var cs = new CryptoStream(ms, aes.CreateEncryptor(), CryptoStreamMode.Write);
            await cs.WriteAsync(data);
            cs.FlushFinalBlock();

            return ms.ToArray();
        }

        public static async Task<byte[]> DecryptAsync(byte[] data, string key)
        {
            using var aes = Aes.Create();
            aes.Key = Encoding.UTF8.GetBytes(key);

            await using var ms = new MemoryStream(data);

            var iv = new byte[16];
            await ms.ReadAsync(iv);
            aes.IV = iv;

            await using var cs = new CryptoStream(ms, aes.CreateDecryptor(), CryptoStreamMode.Read);
            await using var rs = new MemoryStream();
            await cs.CopyToAsync(rs);

            return rs.ToArray();
        }
    }
}