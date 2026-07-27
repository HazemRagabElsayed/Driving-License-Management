using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DVLDDataAccessLayer
{
    public class clsApplicationData
    {
        public static bool FindByID(int ApplicationID,
             ref int ApplicantPersonID,
             ref DateTime ApplicationDate,
             ref int ApplicationTypeID,
             ref byte ApplicationStatus,
             ref DateTime LastStatusDate,
             ref float PaidFees,
             ref int CreatedByUserID)
        {

            string Query = $"SELECT * FROM Applications where ApplicationID = @ApplicationID";



            SqlConnection Connection = new SqlConnection(DataAccessSettings.ConnectionString);

            SqlCommand command = new SqlCommand(Query, Connection);

            command.Parameters.AddWithValue("@ApplicationID", ApplicationID);

            Connection.Open();

            SqlDataReader reader = command.ExecuteReader();

            try
            {
                if (reader.Read())
                {
                    ApplicantPersonID = Convert.ToInt32(reader["ApplicantPersonID"]);
                    ApplicationDate = Convert.ToDateTime(reader["ApplicationDate"]);
                    ApplicationTypeID = Convert.ToInt32(reader["ApplicationTypeID"]);
                    ApplicationStatus = (byte)reader["ApplicationStatus"];
                    LastStatusDate = Convert.ToDateTime(reader["LastStatusDate"]);
                    PaidFees = Convert.ToSingle(reader["PaidFees"]);
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

        public static int AddNew(int ApplicantPersonID,
             DateTime ApplicationDate,
             int ApplicationTypeID,
             byte ApplicationStatus,
             DateTime LastStatusDate,
             float PaidFees,
             int CreatedByUserID)
        {

            int ApplicationID = -1;

            string NonQuery = $"INSERT INTO Applications" +
                $"           (ApplicantPersonID" +
                $"           ,ApplicationDate" +
                $"           ,ApplicationTypeID" +
                $"           ,ApplicationStatus" +
                $"           ,LastStatusDate" +
                $"           ,PaidFees" +
                $"           ,CreatedByUserID)" +
                $"      VALUES" +
                $"           (@ApplicantPersonID" +
                $"           ,@ApplicationDate" +
                $"           ,@ApplicationTypeID" +
                $"           ,@ApplicationStatus" +
                $"           ,@LastStatusDate" +
                $"           ,@PaidFees" +
                $"           ,@CreatedByUserID);" +
                $"SELECT SCOPE_IDENTITY();";

            SqlConnection Connection = new SqlConnection(DataAccessSettings.ConnectionString);

            SqlCommand command = new SqlCommand(NonQuery, Connection);

            command.Parameters.AddWithValue("@ApplicantPersonID", ApplicantPersonID);
            command.Parameters.AddWithValue("@ApplicationDate", ApplicationDate);
            command.Parameters.AddWithValue("@ApplicationTypeID", ApplicationTypeID);

            command.Parameters.AddWithValue("@ApplicationStatus", ApplicationStatus);

            command.Parameters.AddWithValue("@LastStatusDate", LastStatusDate);
            command.Parameters.AddWithValue("@PaidFees", PaidFees);
            command.Parameters.AddWithValue("@CreatedByUserID", CreatedByUserID);

            try
            {
                Connection.Open();

                object Result = command.ExecuteScalar();

                if (int.TryParse(Result?.ToString(), out int ID))
                {
                    ApplicationID = ID;

                }
            }
            catch
            {
            }
            finally
            {
                Connection.Close();
            }

            return ApplicationID;
        }

        public static bool Update(int ApplicationID, int ApplicantPersonID, DateTime ApplicationDate,
            int ApplicationTypeID, byte ApplicationStatus, DateTime LastStatusDate , float PaidFees , int CreatedByUserID)
        {
            int RowsAffected = 0;

            string NonQuery = $"UPDATE Applications" +
                $" SET   ApplicantPersonID = @ApplicantPersonID" +
                $"      ,ApplicationDate = @ApplicationDate" +
                $"      ,ApplicationTypeID = @ApplicationTypeID" +
                $"      ,ApplicationStatus = @ApplicationStatus" +
                $"      ,LastStatusDate = @LastStatusDate" +
                $"      ,PaidFees = @PaidFees" +
                $"      ,CreatedByUserID = @CreatedByUserID" +
                $" WHERE ApplicationID = @ApplicationID;";

            SqlConnection Connection = new SqlConnection(DataAccessSettings.ConnectionString);

            SqlCommand command = new SqlCommand(NonQuery, Connection);

            command.Parameters.AddWithValue("@ApplicantPersonID", ApplicantPersonID);
            command.Parameters.AddWithValue("@ApplicationDate", ApplicationDate);
            command.Parameters.AddWithValue("@ApplicationTypeID", ApplicationTypeID);
            command.Parameters.AddWithValue("@ApplicationStatus", ApplicationStatus);
            command.Parameters.AddWithValue("@LastStatusDate", LastStatusDate);
            command.Parameters.AddWithValue("@PaidFees", PaidFees);
            command.Parameters.AddWithValue("@CreatedByUserID", CreatedByUserID);
            command.Parameters.AddWithValue("@ApplicationID", ApplicationID);




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
            string Query = $"Select * from Applications";

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

        public static bool IsExist(int ApplicationID)
        {
            string Query = $"Select Exist=1 From Applications where ApplicationID = @ApplicationID";

            SqlConnection Connection = new SqlConnection(DataAccessSettings.ConnectionString);

            SqlCommand command = new SqlCommand(Query, Connection);

            command.Parameters.AddWithValue("@ApplicationID", ApplicationID);

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

        public static bool DoesPersonhaveApplication(int ApplicantPersonID)
        {
            string Query = $"Select Exist=1 From Applications where ApplicantPersonID = @ApplicantPersonID";

            SqlConnection Connection = new SqlConnection(DataAccessSettings.ConnectionString);

            SqlCommand command = new SqlCommand(Query, Connection);

            command.Parameters.AddWithValue("@ApplicantPersonID", ApplicantPersonID);

            Connection.Open();

            SqlDataReader reader = command.ExecuteReader();

            try
            {
                if (reader.Read())
                {
                    return true;
                }
                else
                {
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

        public static bool Delete(int ApplicationID)
        {
            int RowsAffected = 0;

            string NonQuery = $"Delete From Applications where ApplicationID = @ApplicationID";

            SqlConnection Connection = new SqlConnection(DataAccessSettings.ConnectionString);

            SqlCommand command = new SqlCommand(NonQuery, Connection);

            command.Parameters.AddWithValue("@ApplicationID", ApplicationID);


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

        public static bool UpdateStatus(int ApplicationID, byte ApplicationStatus)
        {
            int RowsAffected = 0;

            string NonQuery = $"UPDATE Applications " +
                $"SET   ApplicationStatus = @ApplicationStatus " +
                $", LastStatusDate = @LastStatusDate " +
                $"WHERE ApplicationID = @ApplicationID;";

            SqlConnection Connection = new SqlConnection(DataAccessSettings.ConnectionString);

            SqlCommand command = new SqlCommand(NonQuery, Connection);

 
            command.Parameters.AddWithValue("@ApplicationStatus", ApplicationStatus);
            command.Parameters.AddWithValue("@LastStatusDate", DateTime.Now);
            command.Parameters.AddWithValue("@ApplicationID", ApplicationID);

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


    }
}
