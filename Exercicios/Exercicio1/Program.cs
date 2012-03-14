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

            using (SHA512Managed hashFunction = new SHA512Managed())
            {
                var hashBytes = hashFunction.ComputeHash(fileContentBytes);
                var stringResult = new UTF8Encoding().GetString(hashBytes);
            } 
        }
    }
}