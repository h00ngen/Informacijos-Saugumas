using System;
using System.Collections.Generic;
using System.Text;
using System.Security.Cryptography;

namespace AES
{
    class AES
    {
        // 16 raktas - @McQfTjWnZr4u7x!

        private static string IV = "1337133713371337";
        AesCryptoServiceProvider crypt;

        public AES()
        {
        }
        public string Encrypt (string text,string key, CipherMode mode)
        {
            crypt = new AesCryptoServiceProvider();
            crypt.BlockSize = 128;
            crypt.KeySize = 128;                                                                    // rakto dydis
            
            byte[] textBytes = ASCIIEncoding.ASCII.GetBytes(text);                                  // tekstą paverčia į baitų masyvą
            crypt.IV = ASCIIEncoding.ASCII.GetBytes(IV);
            crypt.Key = ASCIIEncoding.ASCII.GetBytes(key);                                          // raktą paverčia į baitus
            
            crypt.Padding = PaddingMode.PKCS7;                                                      // jeigu blokas nepilnas, užpildomi paskutiniai baitai
            crypt.Mode = mode;
            
            ICryptoTransform transform = crypt.CreateEncryptor(crypt.Key,crypt.IV);
            byte[] encrypted_bytes = transform.TransformFinalBlock(textBytes, 0,textBytes.Length);  // atliekamas paskutinis blokų perstūmimas
            
            transform.Dispose();
            
            return Convert.ToBase64String(encrypted_bytes);                                         // grąžina reikšmę iš baitų į string
        }
        public string Decrypt(string text,string key, CipherMode mode)
        {
            crypt = new AesCryptoServiceProvider();
            crypt.BlockSize = 128;
            crypt.KeySize = 128;
            
            byte[] textBytes = Convert.FromBase64String(text);
            crypt.IV = ASCIIEncoding.ASCII.GetBytes(IV);
            crypt.Key = ASCIIEncoding.ASCII.GetBytes(key);
            
            crypt.Padding = PaddingMode.PKCS7;
            crypt.Mode = mode;
            
            ICryptoTransform transform = crypt.CreateDecryptor(crypt.Key, crypt.IV);
            byte[] decrypted_bytes = transform.TransformFinalBlock(textBytes, 0, textBytes.Length);
            
            transform.Dispose();
            
            return ASCIIEncoding.ASCII.GetString(decrypted_bytes);
        }
    }
}
