﻿namespace BitCoinsWebApp.Utilities
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using System.Security.Cryptography;
    using System.Text;
    using System.Threading.Tasks;

    public class SHA1
    {
        private static byte[] key = { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13, 14, 15, 16, 17, 18, 19, 20, 21, 22, 23, 24 };
        private static byte[] iv = { 65, 110, 68, 26, 69, 178, 200, 219 };


        //standard IV and key values for application where applicable. should only be used internally
        //if interfacing with outside applications, come up with a different IV and key
        public static string tripleDESKey
        {
            get
            {
                return "123400000000aaaaffffeeee";
            }
        }

        public static byte[] tripleDESIV
        {
            get
            {
                return (new byte[] { 8, 8, 9, 1, 1, 2, 3, 5 });
            }
        }

        public static string Encode(string value)
        {
            UTF8Encoding utf8encoder = new UTF8Encoding();
            //UnicodeEncoding ue = new UnicodeEncoding();
            byte[] inputInBytes = utf8encoder.GetBytes(value);
            //  Create a new TripleDES service provider 
            TripleDESCryptoServiceProvider tdesProvider = new TripleDESCryptoServiceProvider();
            //  The ICryptTransform interface uses the TripleDES 
            //  crypt provider along with encryption key and init vector 
            //  information 
            ICryptoTransform cryptoTransform = tdesProvider.CreateEncryptor(key, iv);
            //  All cryptographic functions need a stream to output the 
            //  encrypted information. Here we declare a memory stream 
            //  for this purpose. 
            MemoryStream encryptedStream = new MemoryStream();
            CryptoStream cryptStream = new CryptoStream(encryptedStream, cryptoTransform, CryptoStreamMode.Write);
            //  Write the encrypted information to the stream. Flush the information 
            //  when done to ensure everything is out of the buffer. 
            try
            {
                cryptStream.Write(inputInBytes, 0, inputInBytes.Length);
                cryptStream.FlushFinalBlock();
                encryptedStream.Position = 0;
                //  Read the stream back into a Byte array and return it to the calling 
                //  method. 
                byte[] result = new byte[encryptedStream.Length];
                encryptedStream.Read(result, 0, (int)encryptedStream.Length);

                return Convert.ToBase64String(result);
            }
            finally
            {
                cryptStream.Dispose();
            }
        }

        public static string Decrypt(string encText)
        {
            //  UTFEncoding is used to transform the decrypted Byte Array 
            //  information back into a string. 
            UTF8Encoding utf8encoder = new UTF8Encoding();
            //UnicodeEncoding ue = new UnicodeEncoding();
            byte[] inputInBytes = Convert.FromBase64String(encText);
            TripleDESCryptoServiceProvider tdesProvider = new TripleDESCryptoServiceProvider();
            //  As before we must provide the encryption/decryption key along with 
            //  the init vector. 
            ICryptoTransform cryptoTransform = tdesProvider.CreateDecryptor(key, iv);
            //  Provide a memory stream to decrypt information into 
            MemoryStream decryptedStream = new MemoryStream();
            CryptoStream cryptStream = new CryptoStream(decryptedStream, cryptoTransform, CryptoStreamMode.Write);
            cryptStream.Write(inputInBytes, 0, inputInBytes.Length);
            cryptStream.FlushFinalBlock();
            decryptedStream.Position = 0;
            //  Read the memory stream and convert it back into a string 
            byte[] result = new byte[decryptedStream.Length];
            decryptedStream.Read(result, 0, (int)decryptedStream.Length);
            cryptStream.Dispose();
            //UTF8Encoding myutf = new UTF8Encoding();
            //return myutf.GetString(result);
            return utf8encoder.GetString(result);
        }

       
        public static string RandomString(int length)
        {
            Random random = new Random();
            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
            return new string(Enumerable.Repeat(chars, length)
              .Select(s => s[random.Next(s.Length)]).ToArray());
        }
    }
}
