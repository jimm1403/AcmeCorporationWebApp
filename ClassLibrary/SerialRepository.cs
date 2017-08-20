using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models
{
    class SerialRepository
    {
        //DataAccessFacade DAF = new DataAccessFacade();

        List<Serial> serialList = new List<Serial>();

        #region Singleton
        private static volatile SerialRepository instance;
        private static object synchronizationRoot = new Object();

        public static SerialRepository Instance
        {
            get
            {
                if (instance == null)
                {
                    lock (synchronizationRoot)
                    {
                        if (instance == null)
                        {
                            instance = new SerialRepository();
                        }
                    }
                }
                return instance;
            }
        }
        #endregion

        //public int FindSerial(string serial)
        //{
        //    Serial foundSerial = serialList.Find(x => x.ProductSerialNumber == serial);
        //    return foundSerial.Used;
        //}
        //public void SendToDatabase()
        //{
        //    foreach (var serial in serialList)
        //    {
        //        DAF.SerialToDatabase(serial);
        //    }
        //}

        public void AddToSerialList(Serial PSN)
        {
            serialList.Add(PSN);
        }

    }
    public class Serial
    {
        string productSerialNumber;
        int uses;
        bool valid;

        public string ProductSerialNumber { get { return productSerialNumber; } set { productSerialNumber = value; } }
        public int Uses { get { return uses; } set { uses = value; } }
        public bool Valid { get { return valid; } set { valid = value; } }

        //public Serial(string serial, int uses, bool valid)
        //{
        //    productSerialNumber = serial;
        //    this.uses = uses;
        //    this.valid = valid;
        //}
    }
}
