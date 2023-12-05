using System;
using System.IO;

namespace Document
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Document\n");

            do
            {
                try
                {
                    Console.Write("Enter the name of the document: ");
                    string documentName = Console.ReadLine();

                    Console.Write("Enter the content of the document: ");
                    string content = Console.ReadLine();

                    if (!documentName.EndsWith(".txt"))
                    {
                        documentName += ".txt";
                    }

                    string filePath = Path.Combine(Environment.CurrentDirectory, documentName);

                    File.WriteAllText(filePath, content);

                    int characterCount = content.Length;

                    Console.WriteLine($"{documentName} was successfully saved. The document contains {characterCount} characters.");
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"An error occurred: {ex.Message}");
                }

                Console.Write("Do you want to save another document? (yes/no): ");
            } while (Console.ReadLine().ToLower() == "yes");
        }
    }
}

