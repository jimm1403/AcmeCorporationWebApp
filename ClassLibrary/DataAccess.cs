using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models
{
    //public class DataAccessFacade
    //{
    //    DataAccess dataAccess = new DataAccess();
    //    public void SerialToDatabase(Serial PSN)
    //    {
    //        dataAccess.SaveSerial(PSN);
    //    }
    //    public void FormSubToDatabase(FormSub formSub)
    //    {
    //        dataAccess.SaveFormSub(formSub);
    //    }
    //}
    public class DataAccess
    {
        List<Serial> serialList = new List<Serial>();
        string filepath = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + @"\Serials.csv";
        

        private static string connectionString = @"Data Source =.\SQLEXPRESS;Initial Catalog = master; Integrated Security = True; MultipleActiveResultSets=True";
        //public void SaveSerial(Serial PSN)
        //{
        //    using (SqlConnection connection = new SqlConnection(connectionString))
        //    {
        //        try
        //        {
        //            connection.Open();

        //            SqlCommand cmd = new SqlCommand("spInsertSerial", connection);
        //            cmd.CommandType = CommandType.StoredProcedure;
        //            cmd.Parameters.Add(new SqlParameter("@ProductSerialNumber", PSN.ProductSerialNumber));
        //            cmd.Parameters.Add(new SqlParameter("@Used", PSN.Used));

        //            cmd.ExecuteNonQuery();
        //        }
        //        catch (SqlException e)
        //        {
                    
        //        }
        //    }
        //}

        //public void SaveFormSub(FormSub formSub)
        //{
        //    using (SqlConnection connection = new SqlConnection(connectionString))
        //    {
        //        try
        //        {
        //            connection.Open();

        //            SqlCommand cmd = new SqlCommand("spInsertFormSub", connection);
        //            cmd.CommandType = CommandType.StoredProcedure;
        //            cmd.Parameters.Add(new SqlParameter("@FirstName", formSub.FirstName));
        //            cmd.Parameters.Add(new SqlParameter("@LastName", formSub.LastName));
        //            cmd.Parameters.Add(new SqlParameter("@Email", formSub.Email));
        //            cmd.Parameters.Add(new SqlParameter("@PhoneNumber", formSub.PhoneNumber));
        //            cmd.Parameters.Add(new SqlParameter("@DateOfBirth", formSub.DateOfBirth));
        //            cmd.Parameters.Add(new SqlParameter("@ProductSerialNumber", formSub.ProductSerialNumber));

        //            cmd.ExecuteNonQuery();
        //        }
        //        catch (SqlException e)
        //        {

        //        }
        //    }
        //}

        public Serial GetPSN(string PSN)
        {
            var data = File.ReadAllLines(filepath)
            .Skip(1)
            .Select(x => x.Split(';'))
            .Select(x => new Serial
            {
                ProductSerialNumber = x[0],
                Uses = int.Parse(x[1]),
                Valid = (x[2].Equals("True") ? true : false)
            }
            );
            
            serialList.AddRange(data);
            
            return serialList.Find(x => x.ProductSerialNumber == PSN);
        }
        public void SaveChanges(Serial serial)
        {
            serialList.Remove(serialList.Find(x => x.ProductSerialNumber == serial.ProductSerialNumber));
            serialList.Add(serial);

            using (StreamWriter sw = new StreamWriter(filepath, false))
            {
                sw.WriteLine("Product serial numer" + ";" + "Uses" + ";" + "Valid");
                sw.Flush();
                foreach (Serial PSN in serialList)
                {
                    sw.WriteLine(PSN.ProductSerialNumber + ";" + PSN.Uses.ToString() + ";" + PSN.Valid.ToString());
                    sw.Flush();
                }
            }
        }
    }
}
