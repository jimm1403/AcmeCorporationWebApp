using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Linq;

namespace Models
{
    public class DataAccess
    {
        List<Serial> serialList = new List<Serial>();
        List<Submission> subList = new List<Submission>();

        string filepath = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + @"\Serials.csv";

        private static string connectionString = @"Data Source=(LocalDB)\v11.0;Integrated Security = True";

        #region Save ProductSerialNumber in Database table
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
        #endregion

        //Saves a submission in a FORMSUB table in the local database.
        public void SaveSubmission(Submission formSub)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                try
                {
                    connection.Open();

                    SqlCommand cmd = new SqlCommand("spInsertFormSub", connection);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add(new SqlParameter("@FirstName", formSub.FirstName));
                    cmd.Parameters.Add(new SqlParameter("@LastName", formSub.LastName));
                    cmd.Parameters.Add(new SqlParameter("@Email", formSub.Email));
                    cmd.Parameters.Add(new SqlParameter("@PhoneNumber", formSub.PhoneNumber));
                    cmd.Parameters.Add(new SqlParameter("@DateOfBirth", formSub.DateOfBirth));
                    cmd.Parameters.Add(new SqlParameter("@ProductSerialNumber", formSub.ProductSerialNumber));

                    cmd.ExecuteNonQuery();
                }
                catch (SqlException e)
                {

                }
            }
        }
        //Saves changes to the ProductSerialNumbers in the .csv file.
        public void SavePSNChanges(Serial serial)
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
        //Gets all the ProductSerialNumbers from a .csv file, and returns the PSN that fits the parameter Serial.
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
            if (serialList.Find(x => x.ProductSerialNumber == PSN) == null)
            {
                Serial notFound = new Serial();
                notFound.Valid = false;
                return notFound;
            }
            else
            {
                return serialList.Find(x => x.ProductSerialNumber == PSN);
            }
        }
        //Gets all the submissions from the FORMSUB database table.
        public List<Submission> GetSubmissions()
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                try
                {
                    connection.Open();

                    SqlCommand cmdGetJobs = new SqlCommand("spGetSubmissions", connection);
                    cmdGetJobs.CommandType = CommandType.StoredProcedure;
                    SqlDataReader reader = cmdGetJobs.ExecuteReader();

                    if (reader.HasRows)
                    {
                        while (reader.Read())
                        {
                            Submission submission = new Submission(reader["FirstName"].ToString(),
                                reader["LastName"].ToString(),
                                reader["Email"].ToString(),
                                reader["PhoneNumber"].ToString(),
                                reader["DateOfBirth"].ToString(),
                                reader["ProductSerialNumber"].ToString());
                            subList.Add(submission);
                        }
                    }
                }
                catch (SqlException e)
                {
                    
                }
            }

            return subList;
        }
    }
}
