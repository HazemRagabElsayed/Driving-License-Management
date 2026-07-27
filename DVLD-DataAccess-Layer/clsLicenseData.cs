using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DVLDDataAccessLayer
{
    public class clsLicenseData
    {
        public static bool FindByID(int LicenseID,
             ref int ApplicationID,
             ref int DriverID,
             ref int LicenseClass,
             ref DateTime IssueDate,
             ref DateTime ExpirationDate,
             ref string Notes,
             ref float PaidFees,
             ref bool IsActive,
             ref short IssueReason,
             ref int CreatedByUserID)
        {

            string Query = $"SELECT * FROM Licenses where LicenseID = @LicenseID";



            SqlConnection Connection = new SqlConnection(DataAccessSettings.ConnectionString);

            SqlCommand command = new SqlCommand(Query, Connection);

            command.Parameters.AddWithValue("@LicenseID", LicenseID);

            Connection.Open();

            SqlDataReader reader = command.ExecuteReader();

            try
            {
                if (reader.Read())
                {
                    ApplicationID = Convert.ToInt32(reader["ApplicationID"]);
                    DriverID = Convert.ToInt32(reader["DriverID"]);
                    LicenseClass = Convert.ToInt32(reader["LicenseClass"]);
                    IssueDate = Convert.ToDateTime(reader["IssueDate"]);
                    ExpirationDate = Convert.ToDateTime(reader["ExpirationDate"]);

                    if (reader["Notes"] == DBNull.Value)
                        Notes = "";
                    else
                        Notes = Convert.ToString(reader["Notes"]);

                    PaidFees = Convert.ToSingle(reader["PaidFees"]);
                    IsActive = Convert.ToBoolean(reader["IsActive"]);
                    IssueReason = Convert.ToInt16(reader["IssueReason"]);
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

        public static bool FindByDriverID(int DriverID, ref int LicenseID,
             ref int ApplicationID,
             ref int LicenseClass,
             ref DateTime IssueDate,
             ref DateTime ExpirationDate,
             ref string Notes,
             ref float PaidFees,
             ref bool IsActive,
             ref short IssueReason,
             ref int CreatedByUserID)
        {
            string Query = $"SELECT * FROM Licenses where DriverID = @DriverID";



            SqlConnection Connection = new SqlConnection(DataAccessSettings.ConnectionString);

            SqlCommand command = new SqlCommand(Query, Connection);

            command.Parameters.AddWithValue("@DriverID", DriverID);

            Connection.Open();

            SqlDataReader reader = command.ExecuteReader();

            try
            {
                if (reader.Read())
                {
                    ApplicationID = Convert.ToInt32(reader["ApplicationID"]);
                    LicenseID = Convert.ToInt32(reader["LicenseID"]);
                    LicenseClass = Convert.ToInt32(reader["LicenseClass"]);
                    IssueDate = Convert.ToDateTime(reader["IssueDate"]);
                    ExpirationDate = Convert.ToDateTime(reader["ExpirationDate"]);

                    if (reader["Notes"] == DBNull.Value)
                        Notes = "";
                    else
                        Notes = Convert.ToString(reader["Notes"]);

                    PaidFees = Convert.ToSingle(reader["PaidFees"]);
                    IsActive = Convert.ToBoolean(reader["IsActive"]);
                    IssueReason = Convert.ToInt16(reader["IssueReason"]);
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
             int LicenseClass,
             DateTime IssueDate,
             DateTime ExpirationDate,
             string Notes, float PaidFees, bool IsActive, short IssueReason,
             int CreatedByUserID)
        {

            int LicenseID = -1;

            string NonQuery = $"INSERT INTO Licenses" +
                $"           (ApplicationID" +
                $"           ,DriverID" +
                $"           ,LicenseClass" +
                $"           ,IssueDate" +
                $"           ,ExpirationDate" +
                $"           ,Notes" +
                $"           ,PaidFees" +
                $"           ,IsActive" +
                $"           ,IssueReason" +
                $"           ,CreatedByUserID)" +
                $"      VALUES" +
                $"           (@ApplicationID" +
                $"           ,@DriverID" +
                $"           ,@LicenseClass" +
                $"           ,@IssueDate" +
                $"           ,@ExpirationDate" +
                $"           ,@Notes" +
                $"           ,@PaidFees" +
                $"           ,@IsActive" +
                $"           ,@IssueReason" +
                $"           ,@CreatedByUserID);" +
                $"SELECT SCOPE_IDENTITY();";

            SqlConnection Connection = new SqlConnection(DataAccessSettings.ConnectionString);

            SqlCommand command = new SqlCommand(NonQuery, Connection);

            command.Parameters.AddWithValue("@ApplicationID", ApplicationID);
            command.Parameters.AddWithValue("@DriverID", DriverID);
            command.Parameters.AddWithValue("@LicenseClass", LicenseClass);

            command.Parameters.AddWithValue("@IssueDate", IssueDate);

            command.Parameters.AddWithValue("@ExpirationDate", ExpirationDate);

            if(string.IsNullOrEmpty(Notes))
            command.Parameters.AddWithValue("@Notes", DBNull.Value);
            else
                command.Parameters.AddWithValue("@Notes", Notes);

            command.Parameters.AddWithValue("@PaidFees", PaidFees);
            command.Parameters.AddWithValue("@IsActive", true);
            command.Parameters.AddWithValue("@IssueReason", IssueReason);

            command.Parameters.AddWithValue("@CreatedByUserID", CreatedByUserID);


            try
            {
                Connection.Open();

                object Result = command.ExecuteScalar();

                if (int.TryParse(Result?.ToString(), out int ID))
                {
                    LicenseID = ID;

                }
            }
            catch
            {
            }
            finally
            {
                Connection.Close();
            }

            return LicenseID;
        }

        public static bool Update(int LicenseID, int ApplicationID, int DriverID,
            int LicenseClass, DateTime IssueDate, DateTime ExpirationDate, string Notes, float PaidFees, bool IsActive, short IssueReason, int CreatedByUserID)
        {
            int RowsAffected = 0;

            string NonQuery = $"UPDATE Licenses" +
                $" SET   ApplicationID = @ApplicationID" +
                $"      ,DriverID = @DriverID" +
                $"      ,LicenseClass = @LicenseClass" +
                $"      ,IssueDate = @IssueDate" +
                $"      ,ExpirationDate = @ExpirationDate" +
                $"      ,Notes = @Notes" +
                $"      ,PaidFees = @PaidFees" +
                $"      ,IsActive = @IsActive" +
                $"      ,IssueReason = @IssueReason" +
                $"      ,CreatedByUserID = @CreatedByUserID" +
                $" WHERE LicenseID = @LicenseID;";

            SqlConnection Connection = new SqlConnection(DataAccessSettings.ConnectionString);

            SqlCommand command = new SqlCommand(NonQuery, Connection);


            command.Parameters.AddWithValue("@ApplicationID", ApplicationID);
            command.Parameters.AddWithValue("@DriverID", DriverID);
            command.Parameters.AddWithValue("@LicenseClass", LicenseClass);

            command.Parameters.AddWithValue("@IssueDate", IssueDate);

            command.Parameters.AddWithValue("@ExpirationDate", ExpirationDate);

            if (string.IsNullOrEmpty(Notes))
                command.Parameters.AddWithValue("@Notes", DBNull.Value);
            else
                command.Parameters.AddWithValue("@Notes", Notes);

            command.Parameters.AddWithValue("@PaidFees", PaidFees);
            command.Parameters.AddWithValue("@IsActive", IsActive);
            command.Parameters.AddWithValue("@IssueReason", IssueReason);

            command.Parameters.AddWithValue("@CreatedByUserID", CreatedByUserID);

            command.Parameters.AddWithValue("@LicenseID", LicenseID);




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
            string Query = $"Select * from Licenses";

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

        public static bool IsExist(int LicenseID)
        {
            string Query = $"Select Exist=1 From Licenses where LicenseID = @LicenseID";

            SqlConnection Connection = new SqlConnection(DataAccessSettings.ConnectionString);

            SqlCommand command = new SqlCommand(Query, Connection);

            command.Parameters.AddWithValue("@LicenseID", LicenseID);

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

        public static bool DoesApplicationhaveLicense(int ApplicationID)
        {
            string Query = $"Select Exist=1 From Licenses where ApplicationID = @ApplicationID";

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

        public static bool Delete(int LicenseID)
        {
            int RowsAffected = 0;

            string NonQuery = $"Delete From Licenses where LicenseID = @LicenseID";

            SqlConnection Connection = new SqlConnection(DataAccessSettings.ConnectionString);

            SqlCommand command = new SqlCommand(NonQuery, Connection);

            command.Parameters.AddWithValue("@LicenseID", LicenseID);


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

        public static DataTable GetAllByPersonID(int PersonID)
        {
            string Query = @"SELECT        Licenses.LicenseID,
                            Licenses.ApplicationID, LicenseClasses.ClassName,
                            Licenses.IssueDate, Licenses.ExpirationDate, Licenses.IsActive
                            FROM            Drivers INNER JOIN
                            Licenses ON Drivers.DriverID = Licenses.DriverID INNER JOIN
                            LicenseClasses ON Licenses.LicenseClass = LicenseClasses.LicenseClassID
                            WHERE        (Drivers.PersonID = @PersonID)";

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
            string Query = @"SELECT        Licenses.LicenseID,
                            Licenses.ApplicationID, LicenseClasses.ClassName,
                            Licenses.IssueDate, Licenses.ExpirationDate, Licenses.IsActive
                            FROM            Drivers INNER JOIN
                            Licenses ON Drivers.DriverID = Licenses.DriverID INNER JOIN
                            LicenseClasses ON Licenses.LicenseClass = LicenseClasses.LicenseClassID
                            WHERE        Drivers.DriverID = @DriverID";

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
        public static  bool DeactivateLicense(int LicenseID)
        {
            int RowsAffected = 0;

            string NonQuery = $"UPDATE Licenses" +
                $" SET   IsActive = 0 " +
                $" WHERE LicenseID = @LicenseID;";

            SqlConnection Connection = new SqlConnection(DataAccessSettings.ConnectionString);

            SqlCommand command = new SqlCommand(NonQuery, Connection);

            command.Parameters.AddWithValue("@LicenseID", LicenseID);

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


        public static int GetLicenseID(int ApplicationID)
        {

            int LicenseID = -1;

            string Query = $"SELECT LicenseID FROM Licenses where " +
                $" ApplicationID = @ApplicationID ";



            SqlConnection Connection = new SqlConnection(DataAccessSettings.ConnectionString);

            SqlCommand command = new SqlCommand(Query, Connection);

            command.Parameters.AddWithValue("@ApplicationID", ApplicationID);

            Connection.Open();

            SqlDataReader reader = command.ExecuteReader();

            try
            {
                if (reader.Read())
                {
                    return LicenseID = Convert.ToInt32(reader["LicenseID"]);
                }
                else
                {
                    reader.Close();
                    Connection.Close();
                    return -1;
                }
            }
            catch
            {
                return -1;
            }
            finally
            {
                reader.Close();
                Connection.Close();
            }
        }

    }
}
