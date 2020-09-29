using SharpHash.Base;
using System.Text;

namespace ProjectGuard.Services.Security
{
    public static class StreebogProvider
    {
        public static string GetHashCode(string input)
        {
            var provider = HashFactory.Crypto.CreateGOST3411_2012_512();
            var hash = provider.ComputeString(input, Encoding.UTF8);
            return hash.ToString();
        }

        public static string GetHashCode(byte[] input)
        {
            var provider = HashFactory.Crypto.CreateGOST3411_2012_512();
            var hash = provider.ComputeBytes(input);
            return hash.ToString();
        }
    }
}
