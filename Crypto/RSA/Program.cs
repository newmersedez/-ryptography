using System;
using System.Numerics;
using System.Security.Claims;
using RSA.Classes;

namespace RSA
{
    class Program
    {
        private static void Main(string[] args)
        {
            var crypto = new Classes.RSA(PrimalityTestMode.Fermat, 0.7, 30);
            
            var origin = new BigInteger(13371337228);
            var encrypted = crypto.Encrypt(origin);
            var decrypted = crypto.Decrypt(encrypted);
            Console.WriteLine("Origin {0}", origin);
            Console.WriteLine("Encrypted {0}", encrypted);
            Console.WriteLine("Decrypted {0}", decrypted);
            
            crypto.GenerateNewKey();
            encrypted = crypto.Encrypt(origin);
            decrypted = crypto.Decrypt(encrypted);
            Console.WriteLine("\nOrigin {0}", origin);
            Console.WriteLine("Encrypted {0}", encrypted);
            Console.WriteLine("Decrypted {0}", decrypted);
        }
    }
}