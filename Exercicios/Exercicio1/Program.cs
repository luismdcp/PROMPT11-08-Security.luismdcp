using System.IO;
using System.Security.Cryptography;
using System.Text;

namespace Exercicio1
{
    class Program
    {
        static void Main(string[] args)
        {
            string filePath = @"c:\1.png";
            byte[] fileContentBytes = File.ReadAllBytes(filePath);

            SHA512Managed hashFunction = new SHA512Managed();
            var hashBytes = hashFunction.ComputeHash(fileContentBytes);
            var encoding = new UTF8Encoding();
            var stringResult = encoding.GetString(hashBytes);
        }
    }
}