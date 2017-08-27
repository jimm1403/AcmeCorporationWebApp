using Models;
using System.Collections.Generic;

namespace ClassLibrary
{
    public class ProductSerialNumberHandler
    {
        DataAccess dataAccess = new DataAccess();
        Serial currentSerial = new Serial();
        
        //Validates the serials send in with the form submissions, to see if it is still valid
        public string ValidatePSN(string PSN)
        {
            if (PSN.Length == 36)
            {
                currentSerial = dataAccess.GetPSNDB(PSN);
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
                    dataAccess.UpdatePSN(currentSerial);
                    return "valid";
                }
            }
            else
            {
                return "invalid";
            }
        }

        public string ValidatePsnTEST(string psn, List<Serial> psnList)
        {
            
            if (psn.Length == 36)
            {
                Serial psnToValidate = psnList.Find(x => x.ProductSerialNumber == psn);
                if (psnToValidate.Valid == false)
                {
                    return "invalid";
                }
                else
                {
                    psnToValidate.Uses++;
                    if (psnToValidate.Uses == 2)
                    {
                        psnToValidate.Valid = false;
                    }

                    psnList.Remove(psnToValidate);
                    psnList.Add(psnToValidate);
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
