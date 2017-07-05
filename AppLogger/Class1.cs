using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Security.Cryptography;
using System.IO;

namespace AppLogger

{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Input Password to Encrypt:"); // Prompt
            string pass = Console.ReadLine(); // Get string from user;
            using (MD5 md5Hash = MD5.Create())
            {
                string hash = GetMd5Hash(md5Hash, pass); // Method to compute input into hash

                Console.WriteLine("The MD5 hash of " + pass + " is: " + hash + ".");

                Console.WriteLine("Verifying the hash...");

                if (VerifyMd5Hash(md5Hash, pass, hash))
                {
                    Console.WriteLine("The hashes are the same.");
                }
                else
                {
                    Console.WriteLine("The hashes are not same.");
                }
                Console.WriteLine("Press any key to exit."); // Keeps the debug window open until pressing a key
                Console.ReadKey();
                {
                    string path = @"c:\temp\password.txt";  // Creating password.txt in path
                    if (!File.Exists(path))
                    {
                        // Writing the content of pass + hash into the .txt
                        string contents = "Klartext passwort: " + pass + Environment.NewLine + "Verschlüsselter hash des passworts: " + hash + Environment.NewLine;
                        File.WriteAllText(path, contents);
                    }
                }
            }



        }
        static string GetMd5Hash(MD5 md5Hash, string input)
        {

            // Convert the input string to a byte array and compute the hash
            byte[] data = md5Hash.ComputeHash(Encoding.UTF8.GetBytes(input));

            // Create a new Stringbuilder to collect the bytes and create a string.
            StringBuilder sBuilder = new StringBuilder();

            // Loop through each byte of the hashed data and format each one as a hexadecimal string.
            for (int i = 0; i < data.Length; i++)
            {
                sBuilder.Append(data[i].ToString("x2"));
            }

            // Return the hexadecimal string.
            return sBuilder.ToString();
        }

        // Verify a hash against a string.
        static bool VerifyMd5Hash(MD5 md5Hash, string input, string hash)
        {
            // Hash the input.
            string hashOfInput = GetMd5Hash(md5Hash, input);

            // Create a StringComparer an compare the hashes.
            StringComparer comparer = StringComparer.OrdinalIgnoreCase;

            if (0 == comparer.Compare(hashOfInput, hash))
            {
                return true;
            }
            else
            {
                return false;
            }
        }

    }
}