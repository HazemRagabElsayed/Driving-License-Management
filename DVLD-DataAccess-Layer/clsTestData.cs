using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DVLDDataAccessLayer
{
    public class clsTestData
    {
        public static DataTable GetAll()
        {
            string Query = $"Select * from Tests";

            SqlConnection Connection = new SqlConnection(DataAccessSettings.ConnectionString);

            SqlCommand command = new SqlCommand();

            command.CommandText = Query;
            command.Connection = Connection;

            DataTable dt = new DataTable();

            try
            {
                Connection.Open();

                SqlDataReader reader = command.ExecuteReader();

                if (reader.HasRows)
                {
                    dt.Load(reader);
                }
            }
            catch
            {
                return null;
            }
            finally
            {
                Connection.Close();
            }

            return dt;
        }
        public static bool FindByID(int TestID, ref int TestAppointmentID,
            ref bool TestResult, ref string Notes, ref int CreatedByUserID)
        {

            string Query = $"SELECT * FROM Tests " +
                $"where TestID = @TestID";



            SqlConnection Connection = new SqlConnection(DataAccessSettings.ConnectionString);

            SqlCommand command = new SqlCommand(Query, Connection);

            command.Parameters.AddWithValue("@TestID", TestID);

            Connection.Open();

            SqlDataReader reader = command.ExecuteReader();

            try
            {
                if (reader.Read())
                {
                    TestAppointmentID = Convert.ToInt32(reader["TestAppointmentID"]);
                    TestResult = Convert.ToBoolean(reader["TestResult"]);
                    if (reader["Notes"] != DBNull.Value)
                    {
                        Notes = Convert.ToString(reader["Notes"]);
                    }
                    else
                    {
                        Notes = "";
                    }

                    CreatedByUserID = Convert.ToInt32(reader["CreatedByUserID"]);


                    return true;
                }
                else
                {
                    reader.Close();
                    Connection.Close();
                    return false;
                }
            }
            catch
            {
                return false;
            }
            finally
            {
                reader.Close();
                Connection.Close();
            }

        }
        public static int AddNew(
            int TestAppointmentID,
             bool TestResult,
             string Notes,
             int CreatedByUserID)
        {

            int TestID = -1;

            string NonQuery = $"INSERT INTO Tests" +
                $"           (TestAppointmentID" +
                $"           ,TestResult" +
                $"           ,Notes" +
                $"           ,CreatedByUserID)" +
                $"      VALUES" +
                $"           (@TestAppointmentID" +
                $"           ,@TestResult" +
                $"           ,@Notes" +
                $"           ,@CreatedByUserID) ;" +
                $"" +
                $" Update TestAppointments " +
                $" set IsLocked = 1 where TestAppointmentID = @TestAppointmentID; " +
                $" " +
                $" SELECT SCOPE_IDENTITY();";

            SqlConnection Connection = new SqlConnection(DataAccessSettings.ConnectionString);

            SqlCommand command = new SqlCommand(NonQuery, Connection);

            command.Parameters.AddWithValue("@TestAppointmentID", TestAppointmentID);
            command.Parameters.AddWithValue("@TestResult", TestResult);
            if (Notes == "")
            {
                command.Parameters.AddWithValue("@Notes", DBNull.Value);
            }
            else
            {
                command.Parameters.AddWithValue("@Notes", Notes);

            }
            command.Parameters.AddWithValue("@CreatedByUserID", CreatedByUserID);

            try
            {
                Connection.Open();

                object Result = command.ExecuteScalar();

                if (int.TryParse(Result?.ToString(), out int ID))
                {
                    TestID = ID;

                }
            }
            catch
            {
            }
            finally
            {
                Connection.Close();
            }

            return TestID;
        }
        public static bool Update(int TestID, int TestAppointmentID,
             bool TestResult, string Notes, int CreatedByUserID)
        {
            int RowsAffected = 0;

            string NonQuery = $"UPDATE Tests" +
                $" SET   TestAppointmentID = @TestAppointmentID" +
                $"      ,TestResult = @TestResult" +
                $"      ,Notes = @Notes" +
                $"      ,CreatedByUserID = @CreatedByUserID" +
                $" WHERE TestID = @TestID;";

            SqlConnection Connection = new SqlConnection(DataAccessSettings.ConnectionString);

            SqlCommand command = new SqlCommand(NonQuery, Connection);


            command.Parameters.AddWithValue("@TestAppointmentID", TestAppointmentID);
            command.Parameters.AddWithValue("@TestResult", TestResult);
            if (Notes == "")
            {
                command.Parameters.AddWithValue("@Notes", DBNull.Value);
            }
            else
            {
                command.Parameters.AddWithValue("@Notes", Notes);

            }
            command.Parameters.AddWithValue("@CreatedByUserID", CreatedByUserID);
            command.Parameters.AddWithValue("@TestID", TestID);

            try
            {
                Connection.Open();

                RowsAffected = command.ExecuteNonQuery();
            }
            catch
            {

            }
            finally
            {
                Connection.Close();
            }

            return (RowsAffected > 0);

        }


        public static bool Delete(int TestID)
        {
            int RowsAffected = 0;

            string NonQuery = $"Delete From Tests where TestID = @TestID";

            SqlConnection Connection = new SqlConnection(DataAccessSettings.ConnectionString);

            SqlCommand command = new SqlCommand(NonQuery, Connection);

            command.Parameters.AddWithValue("@TestID", TestID);


            try
            {
                Connection.Open();

                RowsAffected = command.ExecuteNonQuery();
            }
            catch
            {

            }
            finally
            {
                Connection.Close();
            }

            return (RowsAffected > 0);
        }


        public static bool GetLastTestPerTestType
                (int LocalDrivingLicenseApplicationID,
                int TestTypeID,
                ref int TestID,
                ref int TestAppointmentID
                , ref bool TestResult, ref string Notes, ref int CreatedByUserID)

        {
            string Query = $"SELECT top 1 * FROM Tests t inner join TestAppointments ta" +
                $" On t.TestAppointmentID = ta.TestAppointmentID" +
                  $" where LocalDrivingLicenseApplicationID = @LocalDrivingLicenseApplicationID" +
                  $" and TestTypeID = @TestTypeID Order by  t.TestID desc";



            SqlConnection Connection = new SqlConnection(DataAccessSettings.ConnectionString);

            SqlCommand command = new SqlCommand(Query, Connection);

            command.Parameters.AddWithValue("@TestTypeID", TestTypeID);
            command.Parameters.AddWithValue("@LocalDrivingLicenseApplicationID", LocalDrivingLicenseApplicationID);

            Connection.Open();

            SqlDataReader reader = command.ExecuteReader();

            try
            {
                if (reader.Read())
                {
                    TestAppointmentID = Convert.ToInt32(reader["TestAppointmentID"]);
                    TestResult = Convert.ToBoolean(reader["TestResult"]);
                    if (reader["Notes"] != DBNull.Value)
                    {
                        Notes = Convert.ToString(reader["Notes"]);
                    }
                    else
                    {
                        Notes = "";
                    }

                    CreatedByUserID = Convert.ToInt32(reader["CreatedByUserID"]);


                    return true;
                }
                else
                {
                    reader.Close();
                    Connection.Close();
                    return false;
                }
            }
            catch
            {
                return false;
            }
            finally
            {
                reader.Close();
                Connection.Close();
            }
        }

        public static byte CountPassedTests(int LocalDrivingLicenseApplicationID)
        {

            byte NumberOfPassedTests = 0;

            string NonQuery = $"Select Count(TestTypeID) as PassedTestCount from " +
                $" Tests t inner join TestAppointments ta " +
                $" on t.TestAppointmentID = ta.TestAppointmentID " +
                $" where ta.LocalDrivingLicenseApplicationID = @LocalDrivingLicenseApplicationID and " +
                $" t.TestResult = 1";

            SqlConnection Connection = new SqlConnection(DataAccessSettings.ConnectionString);

            SqlCommand command = new SqlCommand(NonQuery, Connection);

            command.Parameters.AddWithValue("@LocalDrivingLicenseApplicationID", LocalDrivingLicenseApplicationID);

            try
            {
                Connection.Open();

                object Result = command.ExecuteScalar();

                if (byte.TryParse(Result?.ToString(), out byte PassedTests))
                {
                    NumberOfPassedTests = PassedTests;

                }
            }
            catch
            {
            }
            finally
            {
                Connection.Close();
            }

            return NumberOfPassedTests;
        }

        public static short TotalTestTrialsPerTestType(int LocalDrivingLicenseApplicationID, int TestTypeID)
        {
            short NumberOfTries = 0;

            string NonQuery = @"select Count(*) from  LocalDrivingLicenseApplications ld 
inner join TestAppointments ta on ld.LocalDrivingLicenseApplicationID = ta.LocalDrivingLicenseApplicationID
Inner join Tests t on t.TestAppointmentID = ta.TestAppointmentID
where ld.LocalDrivingLicenseApplicationID = @LocalDrivingLicenseApplicationID and TestTypeID = @TestTypeID";

            SqlConnection Connection = new SqlConnection(DataAccessSettings.ConnectionString);

            SqlCommand command = new SqlCommand(NonQuery, Connection);

            command.Parameters.AddWithValue("@LocalDrivingLicenseApplicationID", LocalDrivingLicenseApplicationID);
            command.Parameters.AddWithValue("@TestTypeID", TestTypeID);

            try
            {
                Connection.Open();

                object Result = command.ExecuteScalar();

                if (short.TryParse(Result?.ToString(), out short NTries))
                {
                    NumberOfTries = NTries;

                }
            }
            catch
            {
            }
            finally
            {
                Connection.Close();
            }

            return NumberOfTries;
        }


    }
}
