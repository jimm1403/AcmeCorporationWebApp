using System;
using System.Collections.Generic;
using System.IO;

namespace Models
{
    public class SerialGenerator
    {
        string path = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);

        //Checks if the Product Serial Number files exist.
        public void LocalFileCheck()
        {
            if (!File.Exists(path + @"\psn.txt"))
            {
                GenerateSerials();
            }
        }

        //Generates 100 unique GUIDs and creates a .txt file in the Documents folder, and write the GUIDs in that file.
        public void GenerateSerials()
        {
            using (StreamWriter outputFile = new StreamWriter(path + @"\psn.txt", false))
            {
                for (int i = 0; i < 100; i++)
                {
                    Guid PSN = Guid.NewGuid();
                    outputFile.WriteLine(PSN.ToString());
                }
            }
        }

        //Reads all the GUIDs from a .txt file in Documents folder, and convert the strings to Serial objects, 
        //and then writes them in to a .csv file, with the appropriate coloums.
        public void CreateSerials()
        {
            List<string> serialNumbers = new List<string>();

            using (var sr = new StreamReader(path + @"\psn.txt"))
            {
                for (int i = 0; i < 100; i++)
                {
                    string line = sr.ReadLine();
                    if (line != null)
                    {
                        serialNumbers.Add(line);
                    }
                }
            }
            DataAccess DA = new DataAccess();
            DA.InsertSerial(serialNumbers);

            //using (StreamWriter sw = new StreamWriter(path + @"\Serials.csv", false))
            //{
            //    sw.WriteLine("Product serial number" + ";" + "Uses" + ";" + "Valid");
            //    sw.Flush();
            //    foreach (string PSN in serialNumbers)
            //    {
            //        sw.WriteLine(PSN + ";" + "0" + ";" + "True");
            //        sw.Flush();
            //    }
            //}
        }
    }
}
