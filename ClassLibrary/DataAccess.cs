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

        //Check if the ProductSerialNumber for the .txt in MyDocuments are in the database table "SERIAL"
        public void CheckForExistingPSN()
        {
            string statement = "SELECT COUNT(*) FROM dbo.SERIALS";
            int count = 0;
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                using (SqlCommand cmdCount = new SqlCommand(statement, connection))
                {
                    connection.Open();
                    count = (int)cmdCount.ExecuteScalar();
                    if (count != 100)
                    {
                        SerialGenerator sgen = new SerialGenerator();
                        sgen.CreateSerials();
                    }
                }
            }
        }

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
        //Update an existing ProductSerialNumber in the database
        public void UpdatePSN(Serial psn)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                try
                {
                    connection.Open();

                    SqlCommand cmd = new SqlCommand("spUpdatePSN", connection);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add(new SqlParameter("@ProductSerialNumber", psn.ProductSerialNumber));
                    cmd.Parameters.Add(new SqlParameter("@Uses", psn.Uses));
                    cmd.Parameters.Add(new SqlParameter("@Valid", psn.Valid));

                    cmd.ExecuteNonQuery();
                }
                catch (SqlException e)
                {
                   
                }
            }
        }
        //Inserts new ProductSerialNumber into the data table "SERIALS"
        public void InsertSerial(List<string> psnList)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                foreach (var psn in psnList)
                {
                    try
                    {
                        if (connection.State == ConnectionState.Closed)
                        {
                            connection.Open();
                        }

                        SqlCommand cmd = new SqlCommand("spInsertPSN", connection);
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.Add(new SqlParameter("@ProductSerialNumber", psn));

                        cmd.ExecuteNonQuery();
                        
                    }
                    catch (SqlException e)
                    {

                    }
                }
            }
        }
        //Find a specific ProductSerialNumber in the database, and returns it
        public Serial GetPSNDB(string psn)
        {
            bool run = true;
            Serial PSN = new Serial();
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                try
                {
                    connection.Open();
                    
                    SqlCommand cmd = new SqlCommand("spGetPSN", connection);
                    cmd.CommandType = CommandType.StoredProcedure;
                    SqlDataReader reader = cmd.ExecuteReader();

                    if (reader.HasRows && run)
                    {
                        while (run)
                        {
                            reader.Read();
                            if (reader["ProductSerialNumber"].ToString() == psn)
                            {
                                PSN = new Serial()
                                {
                                    ProductSerialNumber = psn,
                                    Uses = (int)(reader["Uses"]),
                                    Valid = (bool)reader["Valid"]
                                };
                                run = false;
                                reader.Close();
                            }
                        }
                    }
                    cmd.ExecuteNonQuery();
                }
                catch (SqlException e)
                {
                    Serial notFound = new Serial();
                    notFound.Valid = false;
                    return notFound;
                }
                return PSN;
            }
        }

        #region ProductSerialNumber in .csv file
        //Saves changes to the ProductSerialNumbers in the .csv file.
        public void SavePSNChangesCSV(Serial serial)
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
        public Serial GetPSNCSV(string PSN)
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
        #endregion
    }
}
