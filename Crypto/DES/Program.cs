#nullable enable
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Text;
using DES.Classes;
using DES.Utils;

namespace DES
{
    class Program
    {
        private static void Main(string[] args)
        {
            byte[] initializationVector = { 1, 3, 3, 7, 1, 3, 3, 7 };
            const ulong key = 1337133713371337;
            
            var crypto = new CypherContext(BitConverter.GetBytes(key), 
                EncryptionMode.OFB, initializationVector);
            crypto.Encrypter = new Classes.DES();
            crypto.Encrypter.GenerateRoundKeys(BitConverter.GetBytes(key));
            
            const string origin = "testing.txt";
            const string encrypted = "encrypted.txt";
            const string decrypted = "decrypted.txt";
            var workingDir = Directory.GetParent(Environment.CurrentDirectory)?.Parent?.Parent?.FullName;
            if (workingDir == null)
                return;
            var originPath = Path.Combine(workingDir, origin);
            var encryptedPath = Path.Combine(workingDir, encrypted);
            var decryptedPath = Path.Combine(workingDir, decrypted);
            
            crypto.Encrypt(originPath, encryptedPath);
            crypto.Decrypt(encryptedPath, decryptedPath);
        }
    }
}