using System;
using System.Collections.Generic;
using System.IO;

namespace Models
{
    public class SerialGenerator
    {
        List<string> serialNumbers = new List<string>();
        string path = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);

        public void GenerateSerials()
        {
            
            for (int i = 0; i < 100; i++)
            {
                Guid serialNr = Guid.NewGuid();
                serialNumbers.Add(serialNr.ToString());
            }

            using (StreamWriter outputFile = new StreamWriter(path + @"\psn.txt"))
            {
                foreach (var serial in serialNumbers)
                {
                    outputFile.WriteLine(serial);
                }
            }
        }

        public void CreateSerials()
        {
            using (var sr = new StreamReader(path + @"\psn.txt"))
            {
                for (int i = 0; i < 100; i++)
                {
                    if (sr.ReadLine() != null)
                    {
                        serialNumbers.Add(sr.ReadLine());
                    }
                }
            }

            using (var sw = new StreamWriter(path + @"\Serials.csv"))
            {
                sw.WriteLine("Product serial numer" + ";" + "Uses" + ";" + "Valid");
                sw.Flush();
                foreach (string PSN in serialNumbers)
                {
                    sw.WriteLine(PSN + ";" + "0" + ";" + "1");
                    sw.Flush();
                }
            }
        }
    }
}
