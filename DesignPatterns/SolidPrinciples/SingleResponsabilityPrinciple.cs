using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace DesignPatterns
{
    public class SingleResponsabilityPrinciple
    {
        public class Journal
        {
            private readonly List<string> entries = new List<string>();
            private static int count = 0;

            public int AddEntry(string text)
            {
                entries.Add($"{++count}: {text}");
                return count;
            }

            public void RemoveEntry(int index)
            {
                entries.RemoveAt(index);
            }
            public override string ToString()
            {
                return string.Join(Environment.NewLine, entries);
            }

            /* DON'T DO THIS, violates 1st SOLID principle: Single responsability*/

            //public void Save(string filename) 
            //{
            //    File.WriteAllText(filename, ToString());
            //}

            //public static Journal Load(string fileName) { }

            //public void Load(Uri uri) 
            //{
            //}
        }



        /*Instead create a separate class which holds this different reponsability*/
        public class Persistence
        {
            public void SaveToFile(Journal j, string fileName, bool overwrite = false)
            {
                if (overwrite || !File.Exists(fileName))
                    File.WriteAllText(fileName, j.ToString());
            }
        }

        static void Mainn(string[] args)
        {
            Console.WriteLine("**********  Single Responsability Principle Example  **********");
            var j = new Journal();
            j.AddEntry("my first string");
            j.AddEntry("my second string");
            Console.WriteLine(j);


            var p = new Persistence();
            var fileName = @"C:\temp\jounral.txt";
            p.SaveToFile(j, fileName, true);
            //Process.Start(fileName);
        }
    }
}
