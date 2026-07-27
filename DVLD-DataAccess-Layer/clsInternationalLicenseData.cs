using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DVLDDataAccessLayer
{
    public class clsInternationalLicenseData
    {
        public static bool FindByID(int InternationalLicenseID,
             ref int ApplicationID,
             ref int DriverID,
             ref int IssuedUsingLocalLicenseID,
             ref DateTime IssueDate,
             ref DateTime ExpirationDate,
             ref bool IsActive,
             ref int CreatedByUserID)
        {

            string Query = $"SELECT * FROM InternationalLicenses where InternationalLicenseID = @InternationalLicenseID";



            SqlConnection Connection = new SqlConnection(DataAccessSettings.ConnectionString);

            SqlCommand command = new SqlCommand(Query, Connection);

            command.Parameters.AddWithValue("@InternationalLicenseID", InternationalLicenseID);

            Connection.Open();

            SqlDataReader reader = command.ExecuteReader();

            try
            {
                if (reader.Read())
                {
                    ApplicationID = Convert.ToInt32(reader["ApplicationID"]);
                    DriverID = Convert.ToInt32(reader["DriverID"]);
                    IssuedUsingLocalLicenseID = Convert.ToInt32(reader["IssuedUsingLocalLicenseID"]);
                    IssueDate = Convert.ToDateTime(reader["IssueDate"]);
                    ExpirationDate = Convert.ToDateTime(reader["ExpirationDate"]);
                    IsActive = Convert.ToBoolean(reader["IsActive"]);
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

        public static int AddNew(int ApplicationID,
             int DriverID,
             int IssuedUsingLocalLicenseID,
             int CreatedByUserID)
        {

            int InternationalLicenseID = -1;

            string NonQuery = $" Update InternationalLicenses " +
                $" set IsActive = 0 Where DriverID = @DriverID; " +
                $"INSERT INTO InternationalLicenses" +
                $"           (ApplicationID" +
                $"           ,DriverID" +
                $"           ,IssuedUsingLocalLicenseID" +
                $"           ,IssueDate" +
                $"           ,ExpirationDate" +
                $"           ,IsActive" +
                $"           ,CreatedByUserID)" +
                $"      VALUES" +
                $"           (@ApplicationID" +
                $"           ,@DriverID" +
                $"           ,@IssuedUsingLocalLicenseID" +
                $"           ,@IssueDate" +
                $"           ,@ExpirationDate" +
                $"           ,@IsActive" +
                $"           ,@CreatedByUserID);" +
                $"SELECT SCOPE_IDENTITY();";

            SqlConnection Connection = new SqlConnection(DataAccessSettings.ConnectionString);

            SqlCommand command = new SqlCommand(NonQuery, Connection);

            command.Parameters.AddWithValue("@ApplicationID", ApplicationID);
            command.Parameters.AddWithValue("@DriverID", DriverID);
            command.Parameters.AddWithValue("@IssuedUsingLocalLicenseID", IssuedUsingLocalLicenseID);
            command.Parameters.AddWithValue("@IssueDate", DateTime.Now);
            command.Parameters.AddWithValue("@ExpirationDate", DateTime.Now.AddYears(1));
            command.Parameters.AddWithValue("@IsActive", true);
            command.Parameters.AddWithValue("@CreatedByUserID", CreatedByUserID);


            try
            {
                Connection.Open();

                object Result = command.ExecuteScalar();

                if (int.TryParse(Result?.ToString(), out int ID))
                {
                    InternationalLicenseID = ID;

                }
            }
            catch
            {
            }
            finally
            {
                Connection.Close();
            }

            return InternationalLicenseID;
        }

        public static bool Update(int InternationalLicenseID, int ApplicationID, int DriverID,
            int IssuedUsingLocalLicenseID, DateTime IssueDate, DateTime ExpirationDate, bool IsActive, int CreatedByUserID)
        {
            int RowsAffected = 0;

            string NonQuery = $"UPDATE InternationalLicenses" +
                $" SET   ApplicationID = @ApplicationID" +
                $"      ,DriverID = @DriverID" +
                $"      ,IssuedUsingLocalLicenseID = @IssuedUsingLocalLicenseID" +
                $"      ,IssueDate = @IssueDate" +
                $"      ,ExpirationDate = @ExpirationDate" +
                $"      ,Notes = @Notes" +
                $"      ,CreatedByUserID = @CreatedByUserID" +
                $" WHERE InternationalLicenseID = @InternationalLicenseID;";

            SqlConnection Connection = new SqlConnection(DataAccessSettings.ConnectionString);

            SqlCommand command = new SqlCommand(NonQuery, Connection);


            command.Parameters.AddWithValue("@ApplicationID", ApplicationID);
            command.Parameters.AddWithValue("@DriverID", DriverID);
            command.Parameters.AddWithValue("@IssuedUsingLocalLicenseID", IssuedUsingLocalLicenseID);

            command.Parameters.AddWithValue("@IssueDate", IssueDate);

            command.Parameters.AddWithValue("@ExpirationDate", ExpirationDate);

            command.Parameters.AddWithValue("@IsActive", IsActive);

            command.Parameters.AddWithValue("@CreatedByUserID", CreatedByUserID);

            command.Parameters.AddWithValue("@InternationalLicenseID", InternationalLicenseID);




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
            string Query = $"Select * from InternationalLicenses " +
                $"Desc ExpirationDate and IsActive Desc";

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

        public static DataTable GetAllByPersonID(int PersonID)
        {
            string Query = @"SELECT 
                             il.InternationalLicenseID,
                             il.ApplicationID,
                             il.IssuedUsingLocalLicenseID,
                             il.IssueDate,
                             il.ExpirationDate,
                             il.IsActive
                             FROM Drivers d INNER JOIN InternationalLicenses il ON il.DriverID = d.DriverID
                             WHERE d.PersonID = @PersonID";

            SqlConnection Connection = new SqlConnection(DataAccessSettings.ConnectionString);

            SqlCommand command = new SqlCommand();

            command.CommandText = Query;
            command.Connection = Connection;

            command.Parameters.AddWithValue("@PersonID", PersonID);

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


        public static DataTable GetDriverLicenses(int DriverID)
        {
            string Query = @"SELECT 
                             InternationalLicenseID,
                             ApplicationID,
                             IssuedUsingLocalLicenseID,
                             IssueDate,
                             ExpirationDate,
                             IsActive
                             FROM InternationalLicenses 
                             WHERE DriverID = @DriverID Order by ExpirationDate and IsActive Desc";

            SqlConnection Connection = new SqlConnection(DataAccessSettings.ConnectionString);

            SqlCommand command = new SqlCommand();

            command.CommandText = Query;
            command.Connection = Connection;

            command.Parameters.AddWithValue("@DriverID", DriverID);

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

        public static bool IsExist(int InternationalLicenseID)
        {
            string Query = $"Select Exist=1 From InternationalLicenses where InternationalLicenseID = @InternationalLicenseID";

            SqlConnection Connection = new SqlConnection(DataAccessSettings.ConnectionString);

            SqlCommand command = new SqlCommand(Query, Connection);

            command.Parameters.AddWithValue("@InternationalLicenseID", InternationalLicenseID);

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

        public static bool DoesApplicationhaveInternationalLicense(int ApplicationID)
        {
            string Query = $"Select Exist=1 From InternationalLicenses where ApplicationID = @ApplicationID";

            SqlConnection Connection = new SqlConnection(DataAccessSettings.ConnectionString);

            SqlCommand command = new SqlCommand(Query, Connection);

            command.Parameters.AddWithValue("@ApplicationID", ApplicationID);

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

        public static bool Delete(int InternationalLicenseID)
        {
            int RowsAffected = 0;

            string NonQuery = $"Delete From InternationalLicenses where InternationalLicenseID = @InternationalLicenseID";

            SqlConnection Connection = new SqlConnection(DataAccessSettings.ConnectionString);

            SqlCommand command = new SqlCommand(NonQuery, Connection);

            command.Parameters.AddWithValue("@InternationalLicenseID", InternationalLicenseID);


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

        public static int GetActiveInternationalLicenseID(int DriverID)
        {

            int InternationalLicenseID = -1;

            string Query = $"Select top 1 InternationalLicenseID From InternationalLicenses " +
                $" where DriverID = @DriverID and GetDate() between IssueDate and ExpirationDate " +
                $" Order by ExpirationDate Desc";

            SqlConnection Connection = new SqlConnection(DataAccessSettings.ConnectionString);

            SqlCommand command = new SqlCommand(Query, Connection);

            command.Parameters.AddWithValue("@DriverID", DriverID);

            Connection.Open();

            SqlDataReader reader = command.ExecuteReader();

            try
            {
                if (reader.Read())
                {
                    InternationalLicenseID = (int)reader["InternationalLicenseID"];
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

            return InternationalLicenseID;
        }
    }
}
