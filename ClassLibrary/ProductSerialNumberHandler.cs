using Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClassLibrary
{
    public class ProductSerialNumberHandler
    {
        DataAccess da = new DataAccess();
        Serial currentSerial = new Serial();

        string path = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);

        #region Generate Product serial number.
        
        public void GenerateSerials()
        {
            using (StreamWriter outputFile = new StreamWriter(path + @"\psn.txt", false))
            {
                for (int i = 0; i < 100; i++)
                {
                    Guid PSN = Guid.NewGuid();
                    outputFile.WriteLine(PSN.ToString());
                }

                
                //foreach (var serial in serialNumbers)
                //{
                //    outputFile.WriteLine(serial);
                //}
            }
            CreateSerials();
        }
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

            using (StreamWriter sw = new StreamWriter(path + @"\Serials.csv", false))
            {
                sw.WriteLine("Product serial number" + ";" + "Uses" + ";" + "Valid");
                sw.Flush();
                foreach (string PSN in serialNumbers)
                {
                    sw.WriteLine(PSN + ";" + "0" + ";" + "True");
                    sw.Flush();
                }
            }
        }
        #endregion

        //Bliver kørt hver gang man indsender en form submission og tjekker først om PSN stadig er gyldig og
        //og ligger 1 til hvor mange gange den er blevet brugt. Hvis den er på 2 uses, gør den koden ugyldig.
        public string ValidatePSN(string PSN)
        {
            currentSerial = da.GetPSN(PSN);
            if (currentSerial.Valid == false)
            {
                return "This Product Serial Number, is invalid, try another one";
            }
            else
            {
                currentSerial.Uses++;
                if (currentSerial.Uses == 2)
                {
                    currentSerial.Valid = false;
                }
                da.SaveChanges(currentSerial);
                return "Form submitted succesfully";
            }
        }
    }
}
