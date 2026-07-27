using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DVLDDataAccessLayer
{
    public class clsLocalDrivingLicenseApplicationData
    {
        public static DataTable GetAll()
        {
            string Query = $"Select * from LocalDrivingLicenseApplications_View";

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
        public static bool FindByID(int LocalDrivingLicenseApplicationID , ref int ApplicationID, ref int LicenseClassID)
        {

            string Query = $"SELECT * FROM LocalDrivingLicenseApplications" +
                $" where LocalDrivingLicenseApplicationID = @LocalDrivingLicenseApplicationID";



            SqlConnection Connection = new SqlConnection(DataAccessSettings.ConnectionString);

            SqlCommand command = new SqlCommand(Query, Connection);

            command.Parameters.AddWithValue("@LocalDrivingLicenseApplicationID", LocalDrivingLicenseApplicationID);

            Connection.Open();

            SqlDataReader reader = command.ExecuteReader();

            try
            {
                if (reader.Read())
                {
                    ApplicationID = Convert.ToInt32(reader["ApplicationID"]);
                    LicenseClassID = Convert.ToInt32(reader["LicenseClassID"]);


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
        public static int AddNew(int ApplicationID,int LicenseClassID)
        {

            int LocalDrivingLicenseApplicationID = -1;

            string NonQuery = $"INSERT INTO LocalDrivingLicenseApplications" +
                $"           (ApplicationID" +
                $"           ,LicenseClassID)" +
                $"      VALUES" +
                $"           (@ApplicationID" +
                $"           ,@LicenseClassID) " +
                $"SELECT SCOPE_IDENTITY();";

            SqlConnection Connection = new SqlConnection(DataAccessSettings.ConnectionString);

            SqlCommand command = new SqlCommand(NonQuery, Connection);

            command.Parameters.AddWithValue("@ApplicationID", ApplicationID);
            command.Parameters.AddWithValue("@LicenseClassID", LicenseClassID);

            try
            {
                Connection.Open();

                object Result = command.ExecuteScalar();

                if (int.TryParse(Result?.ToString(), out int ID))
                {
                    LocalDrivingLicenseApplicationID = ID;

                }
            }
            catch
            {
            }
            finally
            {
                Connection.Close();
            }

            return LocalDrivingLicenseApplicationID;
        }
        public static bool Update(int LocalDrivingLicenseApplicationID,  int ApplicationID,  int LicenseClassID)
        {
            int RowsAffected = 0;

            string NonQuery = $"UPDATE LocalDrivingLicenseApplications" +
                $" SET   ApplicationID = @ApplicationID" +
                $"      ,LicenseClassID = @LicenseClassID"  +
                $" WHERE LocalDrivingLicenseApplicationID = @LocalDrivingLicenseApplicationID;";

            SqlConnection Connection = new SqlConnection(DataAccessSettings.ConnectionString);

            SqlCommand command = new SqlCommand(NonQuery, Connection);

            command.Parameters.AddWithValue("@ApplicationID", ApplicationID);
            command.Parameters.AddWithValue("@LicenseClassID", LicenseClassID);
            command.Parameters.AddWithValue("@LocalDrivingLicenseApplicationID", LocalDrivingLicenseApplicationID);

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


        public static bool Delete(int LocalDrivingLicenseApplicationID)
        {
            int RowsAffected = 0;

            string NonQuery = $"Delete From LocalDrivingLicenseApplications where LocalDrivingLicenseApplicationID = @LocalDrivingLicenseApplicationID";

            SqlConnection Connection = new SqlConnection(DataAccessSettings.ConnectionString);

            SqlCommand command = new SqlCommand(NonQuery, Connection);

            command.Parameters.AddWithValue("@LocalDrivingLicenseApplicationID", LocalDrivingLicenseApplicationID);


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

        public static bool IsExist(int LocalDrivingLicenseApplicationID)
        {
            string Query = $"Select Exist=1 From LocalDrivingLicenseApplications where" +
                $"  LocalDrivingLicenseApplicationID = @LocalDrivingLicenseApplicationID";

            SqlConnection Connection = new SqlConnection(DataAccessSettings.ConnectionString);

            SqlCommand command = new SqlCommand(Query, Connection);

            command.Parameters.AddWithValue("@LocalDrivingLicenseApplicationID"
                , LocalDrivingLicenseApplicationID);

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

        public static bool PersonHasNewLicenseClassApplication(ref int ApplicationID, int ApplicantPersonID, int LicenseClassID)
        {

            string Query = $"SELECT  a.ApplicationID FROM Applications a " +
                $"INNER JOIN LocalDrivingLicenseApplications ld ON a.ApplicationID = ld.ApplicationID " +
                $"where a.ApplicantPersonID = @ApplicantPersonID and LicenseClassID  = @LicenseClassID and a.ApplicationStatus = 1";



            SqlConnection Connection = new SqlConnection(DataAccessSettings.ConnectionString);

            SqlCommand command = new SqlCommand(Query, Connection);

            command.Parameters.AddWithValue("@ApplicantPersonID", ApplicantPersonID);
            command.Parameters.AddWithValue("@LicenseClassID", LicenseClassID);

            Connection.Open();

            SqlDataReader reader = command.ExecuteReader();

            try
            {
                if (reader.Read())
                {
                    ApplicationID = Convert.ToInt32(reader["ApplicationID"]);
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

        public static bool DoesLDAppApplicantHaveLicense(int LocalDrivingLicenseApplicationID)
        {
            string Query = @"select Exist=1 from LocalDrivingLicenseApplications ld
Inner join Applications a on a.ApplicationID = ld.ApplicationID 
inner join Licenses l on a.ApplicationID = l.ApplicationID
where ld.LocalDrivingLicenseApplicationID = @LocalDrivingLicenseApplicationID ";



            SqlConnection Connection = new SqlConnection(DataAccessSettings.ConnectionString);

            SqlCommand command = new SqlCommand(Query, Connection);

            command.Parameters.AddWithValue("@LocalDrivingLicenseApplicationID", LocalDrivingLicenseApplicationID);

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
                    reader.Close();
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

        public static bool DoesLDAppApplicantActiveHaveLicense(int LocalDrivingLicenseApplicationID)
        {
            string Query = @"select Exist=1 from LocalDrivingLicenseApplications ld
Inner join Applications a on a.ApplicationID = ld.ApplicationID 
inner join Licenses l on a.ApplicationID = l.ApplicationID
where ld.LocalDrivingLicenseApplicationID = @LocalDrivingLicenseApplicationID and IsActive = 1";



            SqlConnection Connection = new SqlConnection(DataAccessSettings.ConnectionString);

            SqlCommand command = new SqlCommand(Query, Connection);

            command.Parameters.AddWithValue("@LocalDrivingLicenseApplicationID", LocalDrivingLicenseApplicationID);

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
                    reader.Close();
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

        public static bool DoesPersonHaveLicense
                (int ApplicantPersonID, int LicenseClassID)
        {
            string Query = @"select * from LocalDrivingLicenseApplications ld
                             Inner join Applications a on a.ApplicationID = ld.ApplicationID 
                             inner join Licenses l on a.ApplicationID = l.ApplicationID
                             where a.ApplicantPersonID = @ApplicantPersonID and ld.LicenseClassID = @LicenseClassID";



            SqlConnection Connection = new SqlConnection(DataAccessSettings.ConnectionString);

            SqlCommand command = new SqlCommand(Query, Connection);

            command.Parameters.AddWithValue("@ApplicantPersonID", ApplicantPersonID);
            command.Parameters.AddWithValue("@LicenseClassID", LicenseClassID);

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
                    reader.Close();
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

    }
}
