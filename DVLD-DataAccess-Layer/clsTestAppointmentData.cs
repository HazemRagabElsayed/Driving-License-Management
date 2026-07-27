using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DVLDDataAccessLayer
{
    public class clsTestAppointmentData
    {
        public static bool FindByID(int TestAppointmentID,
            ref int TestTypeID,
            ref int LocalDrivingLicenseApplicationID,
            ref DateTime AppointmentDate,
            ref float PaidFees,
            ref int CreatedByUserID,
            ref bool IsLocked, ref int RetakeTestApplicationID)
        {

            string Query = $"SELECT * FROM TestAppointments where TestAppointmentID = @TestAppointmentID";



            SqlConnection Connection = new SqlConnection(DataAccessSettings.ConnectionString);

            SqlCommand command = new SqlCommand(Query, Connection);

            command.Parameters.AddWithValue("@TestAppointmentID", TestAppointmentID);

            Connection.Open();

            SqlDataReader reader = command.ExecuteReader();

            try
            {
                if (reader.Read())
                {

  

                    TestTypeID = Convert.ToInt32(reader["TestTypeID"]);
                    LocalDrivingLicenseApplicationID = Convert.ToInt32(reader["LocalDrivingLicenseApplicationID"]);
                    AppointmentDate = Convert.ToDateTime(reader["AppointmentDate"]);
                    PaidFees = Convert.ToSingle(reader["PaidFees"]);
                    CreatedByUserID = Convert.ToInt32(reader["CreatedByUserID"]);
                    IsLocked = Convert.ToBoolean(reader["IsLocked"]);

                    if(reader["RetakeTestApplicationID"] != DBNull.Value)
                    {
                        RetakeTestApplicationID = Convert.ToInt32(reader["RetakeTestApplicationID"]);
                    }  

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

        public static int AddNew(int TestTypeID,
             int LocalDrivingLicenseApplicationID,
             DateTime AppointmentDate,
             float PaidFees,
             int CreatedByUserID, int RetakeTestApplicationID)
        {

            int TestAppointmentID = -1;

            string NonQuery = $"INSERT INTO TestAppointments" +
                $"           (TestTypeID" +
                $"           ,LocalDrivingLicenseApplicationID" +
                $"           ,AppointmentDate" +
                $"           ,PaidFees" +
                $"           ,CreatedByUserID" +
                $"           ,IsLocked" +
                $"           ,RetakeTestApplicationID)" +
                $"      VALUES" +
                $"           (@TestTypeID" +
                $"           ,@LocalDrivingLicenseApplicationID" +
                $"           ,@AppointmentDate" +
                $"           ,@PaidFees" +
                $"           ,@CreatedByUserID" +
                $"           ,@IsLocked" +
                $"           ,@RetakeTestApplicationID);" +
                $"SELECT SCOPE_IDENTITY();";

            SqlConnection Connection = new SqlConnection(DataAccessSettings.ConnectionString);

            SqlCommand command = new SqlCommand(NonQuery, Connection);

            command.Parameters.AddWithValue("@TestTypeID", TestTypeID);
            command.Parameters.AddWithValue("@LocalDrivingLicenseApplicationID", LocalDrivingLicenseApplicationID);
            command.Parameters.AddWithValue("@AppointmentDate", AppointmentDate);
            command.Parameters.AddWithValue("@PaidFees", PaidFees);
            command.Parameters.AddWithValue("@CreatedByUserID", CreatedByUserID);
            command.Parameters.AddWithValue("@IsLocked", false);

            if(RetakeTestApplicationID != -1)
            {
                command.Parameters.AddWithValue("@RetakeTestApplicationID", RetakeTestApplicationID);
            }
            else
            {
                command.Parameters.AddWithValue("@RetakeTestApplicationID", DBNull.Value);
            }

                try
                {
                    Connection.Open();

                    object Result = command.ExecuteScalar();

                    if (int.TryParse(Result?.ToString(), out int ID))
                    {
                        TestAppointmentID = ID;

                    }
                }
                catch
                {
                }
                finally
                {
                    Connection.Close();
                }

            return TestAppointmentID;
        }

        public static bool Update(int TestAppointmentID, int TestTypeID, int LocalDrivingLicenseApplicationID,
            DateTime AppointmentDate, float PaidFees, int CreatedByUserID, bool IsLocked, int RetakeTestApplicationID)
        {
            int RowsAffected = 0;

            string NonQuery = $"UPDATE TestAppointments" +
                $" SET   TestTypeID = @TestTypeID" +
                $"      ,LocalDrivingLicenseApplicationID = @LocalDrivingLicenseApplicationID" +
                $"      ,AppointmentDate = @AppointmentDate" +
                $"      ,PaidFees = @PaidFees" +
                $"      ,CreatedByUserID = @CreatedByUserID" +
                $"      ,IsLocked = @IsLocked" +
                $"      ,RetakeTestApplicationID = @RetakeTestApplicationID" +
                $" WHERE TestAppointmentID = @TestAppointmentID;";

            SqlConnection Connection = new SqlConnection(DataAccessSettings.ConnectionString);

            SqlCommand command = new SqlCommand(NonQuery, Connection);

            command.Parameters.AddWithValue("@TestTypeID", TestTypeID);
            command.Parameters.AddWithValue("@LocalDrivingLicenseApplicationID", LocalDrivingLicenseApplicationID);
            command.Parameters.AddWithValue("@AppointmentDate", AppointmentDate);
            command.Parameters.AddWithValue("@PaidFees", PaidFees);
            command.Parameters.AddWithValue("@CreatedByUserID", CreatedByUserID);
            command.Parameters.AddWithValue("@IsLocked", IsLocked);

            if (RetakeTestApplicationID != -1)
            {
                command.Parameters.AddWithValue("@RetakeTestApplicationID", RetakeTestApplicationID);
            }
            else
            {
                command.Parameters.AddWithValue("@RetakeTestApplicationID", DBNull.Value);
            }

            command.Parameters.AddWithValue("@TestAppointmentID", TestAppointmentID);



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

        public static DataTable GetAll()
        {
            string Query = $"Select * from TestAppointments";

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

        public static DataTable GetAllTestAppointmentsPerTestType
                (int LDLApp, int TestTypeID)
        {
            string Query = $"Select TestAppointmentID, AppointmentDate, PaidFees, IsLocked from TestAppointments" +
                $" where LocalDrivingLicenseApplicationID = @LocalDrivingLicenseApplicationID" +
                $" and TestTypeID = @TestTypeID";


            SqlConnection Connection = new SqlConnection(DataAccessSettings.ConnectionString);

            SqlCommand command = new SqlCommand();

            command.Parameters.AddWithValue("@LocalDrivingLicenseApplicationID", LDLApp);
            command.Parameters.AddWithValue("@TestTypeID", TestTypeID);


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

        public static bool IsExist(int TestAppointmentID)
        {
            string Query = $"Select Exist=1 From TestAppointments where TestAppointmentID = @TestAppointmentID";

            SqlConnection Connection = new SqlConnection(DataAccessSettings.ConnectionString);

            SqlCommand command = new SqlCommand(Query, Connection);

            command.Parameters.AddWithValue("@TestAppointmentID", TestAppointmentID);

            Connection.Open();

            SqlDataReader reader = command.ExecuteReader();

            try
            {
                if (reader.HasRows)
                    return true;
                else
                    return false;

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


        public static bool Delete(int TestAppointmentID)
        {
            int RowsAffected = 0;

            string NonQuery = $"Delete From TestAppointments where TestAppointmentID = @TestAppointmentID";

            SqlConnection Connection = new SqlConnection(DataAccessSettings.ConnectionString);

            SqlCommand command = new SqlCommand(NonQuery, Connection);

            command.Parameters.AddWithValue("@TestAppointmentID", TestAppointmentID);


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

        public static bool GetLastTestAppointmentByLDAppIDAndTestType(
            int TestTypeID,
            int LocalDrivingLicenseApplicationID,
            ref int TestAppointmentID,
            ref DateTime AppointmentDate,
            ref float PaidFees,
            ref int CreatedByUserID,
            ref bool IsLocked, ref int RetakeTestApplicationID)
        {

            string Query = $"SELECT top 1 * FROM TestAppointments ta Inner Join" +
                $" Tests t On t.TestAppointmentID = ta.TestAppointmentID" +
                $" where ta.TestTypeID = @TestTypeID and" +
                $" ta.LocalDrivingLicenseApplicationID = @LocalDrivingLicenseApplicationID" +
                $" Order By ta.TestAppointmentID Desc";



            SqlConnection Connection = new SqlConnection(DataAccessSettings.ConnectionString);

            SqlCommand command = new SqlCommand(Query, Connection);

            command.Parameters.AddWithValue("@TestAppointmentID", TestAppointmentID);

            Connection.Open();

            SqlDataReader reader = command.ExecuteReader();

            try
            {
                if (reader.Read())
                {


                    TestAppointmentID = Convert.ToInt32(reader["TestAppointmentID"]);
                    AppointmentDate = Convert.ToDateTime(reader["AppointmentDate"]);
                    PaidFees = Convert.ToSingle(reader["PaidFees"]);
                    CreatedByUserID = Convert.ToInt32(reader["CreatedByUserID"]);
                    IsLocked = Convert.ToBoolean(reader["IsLocked"]);

                    if (reader["RetakeTestApplicationID"] != DBNull.Value)
                    {
                        RetakeTestApplicationID = Convert.ToInt32(reader["RetakeTestApplicationID"]);
                    }
                    else
                    {
                        RetakeTestApplicationID = -1;
                    }

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

        public static DateTime GetLastTestAppointment(int LocalDrivingLicenseApplicationID)
        {
            DateTime LastTestAppointmentDate = DateTime.Now;

            string NonQuery = @"SELECT top 1 ta.AppointmentDate
                                FROM LocalDrivingLicenseApplications ld INNER JOIN
                                TestAppointments ta ON
                                ld.LocalDrivingLicenseApplicationID = ta.LocalDrivingLicenseApplicationID
						        where and ld.LocalDrivingLicenseApplicationID = @LocalDrivingLicenseApplicationID 
                                desc ta.TestAppointmentID";

            SqlConnection Connection = new SqlConnection(DataAccessSettings.ConnectionString);

            SqlCommand command = new SqlCommand(NonQuery, Connection);

            command.Parameters.AddWithValue
                ("@LocalDrivingLicenseApplicationID", LocalDrivingLicenseApplicationID);

            try
            {
                Connection.Open();

                object Result = command.ExecuteScalar();

                if (DateTime.TryParse(Result?.ToString(), out DateTime LastTestAppointment))
                {
                    LastTestAppointmentDate = LastTestAppointment;

                }
            }
            catch
            {
            }
            finally
            {
                Connection.Close();
            }

            return LastTestAppointmentDate;
        }

        public static bool DoesPersonHaveActiveTestAppointmentPerTestType(int LocalDrivingLicenseApplicationID, int TestTypeID)
        {
            string Query = @"select Found=1 from TestAppointments  ta
inner join LocalDrivingLicenseApplications ld on ta.LocalDrivingLicenseApplicationID = ld.LocalDrivingLicenseApplicationID
where 
ta.LocalDrivingLicenseApplicationID = @LocalDrivingLicenseApplicationID
and ta.IsLocked = 0
and ta.TestTypeID = @TestTypeID";



            SqlConnection Connection = new SqlConnection(DataAccessSettings.ConnectionString);

            SqlCommand command = new SqlCommand(Query, Connection);

            command.Parameters.AddWithValue("@LocalDrivingLicenseApplicationID", LocalDrivingLicenseApplicationID);
            command.Parameters.AddWithValue("@TestTypeID", TestTypeID);

            Connection.Open();

            SqlDataReader reader = command.ExecuteReader();

            try
            {
                if (reader.HasRows)
                {

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

        public static bool DoesPersonPassedPerTestType(int LocalDrivingLicenseApplicationID,int TestTypeID)
        {
            string Query = @"SELECT Passed=1
                                FROM LocalDrivingLicenseApplications ld INNER JOIN
                                TestAppointments ta ON
                                ld.LocalDrivingLicenseApplicationID = ta.LocalDrivingLicenseApplicationID
                                inner join Tests t on t.TestAppointmentID = ta.TestAppointmentID
						        where ta.TestTypeID = @TestTypeID and 
                                ld.LocalDrivingLicenseApplicationID = @LocalDrivingLicenseApplicationID 
                                and TestResult = 1 ";

            SqlConnection Connection = new SqlConnection(DataAccessSettings.ConnectionString);

            SqlCommand command = new SqlCommand(Query, Connection);

            command.Parameters.AddWithValue
                ("@LocalDrivingLicenseApplicationID", LocalDrivingLicenseApplicationID);
            command.Parameters.AddWithValue("@TestTypeID", TestTypeID);

            try
            {
                Connection.Open();

                SqlDataReader reader = command.ExecuteReader();

                if (reader.Read())
                {
                    return true;
                }
            }
            catch
            {
            }
            finally
            {
                Connection.Close();
            }

            return false;
        }

        public static bool DoesPersonAttendedTestType(int LocalDrivingLicenseApplicationID, int TestTypeID)
        {
            string Query = @"select Found=1 from TestAppointments ta inner join
                            LocalDrivingLicenseApplications ld on
                           ta.LocalDrivingLicenseApplicationID = ld.LocalDrivingLicenseApplicationID
                           where ta.TestTypeID = @TestTypeID and 
                           ld.LocalDrivingLicenseApplicationID = @LocalDrivingLicenseApplicationID
 and IsLocked = 1";



            SqlConnection Connection = new SqlConnection(DataAccessSettings.ConnectionString);

            SqlCommand command = new SqlCommand(Query, Connection);

            command.Parameters.AddWithValue("@LocalDrivingLicenseApplicationID", LocalDrivingLicenseApplicationID);
            command.Parameters.AddWithValue("@TestTypeID", TestTypeID);

            Connection.Open();

            SqlDataReader reader = command.ExecuteReader();

            try
            {
                if (reader.HasRows)
                {

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

        public static int GetTestID(int TestAppointmentID)
        {

            int TestID = -1;

            string Query = $"SELECT TestID FROM Tests t Inner Join " +
                $" TestAppointments ta on ta.TestAppointmentID = t.TestAppointmentID " +
                $" where t.TestAppointmentID = @TestAppointmentID ";



            SqlConnection Connection = new SqlConnection(DataAccessSettings.ConnectionString);

            SqlCommand command = new SqlCommand(Query, Connection);

            command.Parameters.AddWithValue("@TestAppointmentID", TestAppointmentID);

            Connection.Open();

            SqlDataReader reader = command.ExecuteReader();

            try
            {
                if (reader.Read())
                {
                     TestID = Convert.ToInt32(reader["TestID"]);
                    
                }
                else
                {
                    reader.Close();
                    Connection.Close();
                }
            }
            catch
            {

            }
            finally
            {
                reader.Close();
                Connection.Close();
            }
            return TestID;
        }

    }
}
