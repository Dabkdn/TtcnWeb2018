using System;
using System.Security.Cryptography;

namespace Ttcn_web.Models
{
    public class Common
    {
        public static string GetMD5(string inputString)
        {
            if (inputString == null)
            {
                return null;
            }

            string stringMD5 = "";
            byte[] arrays = System.Text.Encoding
                .UTF8.GetBytes(inputString);
            MD5CryptoServiceProvider my_md5 = new MD5CryptoServiceProvider();
            arrays = my_md5.ComputeHash(arrays);
            foreach (byte array in arrays)
            {
                stringMD5 += array.ToString("x2");
            }
            return stringMD5;
        }
        public static string GetToken()
        {
            string token = "";
            Random ran = new Random();
            string tmp = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789_-";
            for (int i = 0; i < 30; i++)
            {
                token += tmp.Substring(ran.Next(0, 63), 1);
            }
            return token;
        }
    }
}