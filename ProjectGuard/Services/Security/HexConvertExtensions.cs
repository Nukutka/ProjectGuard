using System;
using System.Linq;
using System.Text;

namespace ProjectGuard.Services.Security
{
    public static class HexConvertExtensions
    {
        public static string ToHexString(this string str)
        {
            var sb = new StringBuilder();
            //var bytes = Encoding.GetEncoding(1251).GetBytes(str); // GOST
            var bytes = Encoding.UTF8.GetBytes(str);
            foreach (var i in bytes)
            {
                sb.Append(i.ToString("X2"));
            }
            return sb.ToString();
        }

        public static string ToHexString(this byte[] input)
        {
            return BitConverter.ToString(input.Reverse().ToArray()).Replace("-", "").ToLower();
        }

        public static byte[] ToByteArray(this string input)
        {
            byte[] bytes = new byte[input.Length / 2];
            for (int i = 0; i < bytes.Length; i++)
            {
                bytes[i] = Convert.ToByte(input.Substring((bytes.Length - i - 1) * 2, 2), 16);
            }
            return bytes;
        }
    }
}
