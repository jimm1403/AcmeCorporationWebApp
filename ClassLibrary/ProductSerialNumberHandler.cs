using Models;

namespace ClassLibrary
{
    public class ProductSerialNumberHandler
    {
        DataAccess dataAccess = new DataAccess();
        Serial currentSerial = new Serial();
        
        //Bliver kørt hver gang man indsender en form submission og tjekker først om PSN stadig er gyldig og
        //og ligger 1 til hvor mange gange den er blevet brugt. Hvis den er på 2 uses, gør den koden ugyldig.
        public string ValidatePSN(string PSN)
        {
            if (PSN.Length == 36)
            {
                currentSerial = dataAccess.GetPSN(PSN);
                if (currentSerial.Valid == false)
                {
                    return "invalid";
                }
                else
                {
                    currentSerial.Uses++;
                    if (currentSerial.Uses == 2)
                    {
                        currentSerial.Valid = false;
                    }
                    dataAccess.SavePSNChanges(currentSerial);
                    return "valid";
                }
            }
            else
            {
                return "invalid";
            }
        }
    }
}
