using System;
using System.IO;
using static System.Net.Mime.MediaTypeNames;


namespace Standard_in_out
{
    internal class Program
    {
        const string FILE_NAME = "TEST.txt";
        static void Main(string[] args)
        {
            using (FileStream fs = new FileStream(FILE_NAME, FileMode.Create))
            {
                using (StreamWriter w = new StreamWriter(fs))
                {
                    w.WriteLine("Hello  WORDLs");
                }
            }

            using (FileStream fs = new FileStream(FILE_NAME, FileMode.Open, FileAccess.Read))
            {
                using (StreamReader r = new StreamReader(fs))
                {
                    Console.WriteLine(r.ReadToEnd());
                }
            }
        }
    }
}