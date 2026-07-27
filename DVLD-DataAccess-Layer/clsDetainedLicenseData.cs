using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace DVLDDataAccessLayer
{
    public class clsDetainedLicenseData
    {
        public static bool FindByID(
            int DetainID,
            ref int LicenseID,
            ref DateTime DetainDate,
            ref float FineFees,
            ref int CreatedByUserID,
            ref bool IsReleased,
            ref DateTime? ReleaseDate,
            ref int ReleasedByUserID,
            ref int ReleaseApplicationID)
        {

            string Query = $"SELECT * FROM DetainedLicenses where DetainID = @DetainID Order by DetainID Desc";



            SqlConnection Connection = new SqlConnection(DataAccessSettings.ConnectionString);

            SqlCommand command = new SqlCommand(Query, Connection);

            command.Parameters.AddWithValue("@DetainID", DetainID);

            Connection.Open();

            SqlDataReader reader = command.ExecuteReader();

            try
            {
                if (reader.Read())
                {
                    LicenseID = Convert.ToInt32(reader["LicenseID"]);
                    DetainDate = Convert.ToDateTime(reader["DetainDate"]);
                    FineFees = Convert.ToSingle(reader["FineFees"]);
                    CreatedByUserID = Convert.ToInt32(reader["CreatedByUserID"]);
                    IsReleased = Convert.ToBoolean(reader["IsReleased"]);

                    if (reader["ReleaseDate"] == DBNull.Value)
                        ReleaseDate = null;
                    else
                        ReleaseDate = Convert.ToDateTime(reader["ReleaseDate"]);

                    if (reader["ReleasedByUserID"] == DBNull.Value)
                        ReleasedByUserID = -1;
                    else
                        ReleasedByUserID = Convert.ToInt32(reader["ReleasedByUserID"]);

                    if (reader["ReleaseApplicationID"] == DBNull.Value)
                        ReleaseApplicationID = -1;
                    else
                        ReleaseApplicationID = Convert.ToInt32(reader["ReleaseApplicationID"]);


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

        public static bool FindByLicenseID(
     int LicenseID,
    ref int DetainID,
    ref DateTime DetainDate,
    ref float FineFees,
    ref int CreatedByUserID,
    ref bool IsReleased,
    ref DateTime? ReleaseDate,
    ref int ReleasedByUserID,
    ref int ReleaseApplicationID)
        {

            string Query = $"SELECT * FROM DetainedLicenses where LicenseID = @LicenseID  Order by DetainID Desc";



            SqlConnection Connection = new SqlConnection(DataAccessSettings.ConnectionString);

            SqlCommand command = new SqlCommand(Query, Connection);

            command.Parameters.AddWithValue("@LicenseID", LicenseID);

            Connection.Open();

            SqlDataReader reader = command.ExecuteReader();

            try
            {
                if (reader.Read())
                {
                    DetainID = Convert.ToInt32(reader["DetainID"]);
                    DetainDate = Convert.ToDateTime(reader["DetainDate"]);
                    FineFees = Convert.ToSingle(reader["FineFees"]);
                    CreatedByUserID = Convert.ToInt32(reader["CreatedByUserID"]);
                    IsReleased = Convert.ToBoolean(reader["IsReleased"]);


                    if (reader["ReleaseDate"] == DBNull.Value)
                        ReleaseDate = null;
                    else
                        ReleaseDate = Convert.ToDateTime(reader["ReleaseDate"]);

                    if (reader["ReleasedByUserID"] == DBNull.Value)
                        ReleasedByUserID = -1;
                    else
                        ReleasedByUserID = Convert.ToInt32(reader["ReleasedByUserID"]);

                    if (reader["ReleaseApplicationID"] == DBNull.Value)
                        ReleaseApplicationID = -1;
                    else
                        ReleaseApplicationID = Convert.ToInt32(reader["ReleaseApplicationID"]);

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
            int LicenseID,
            float FineFees,
            int CreatedByUserID)
        {

            int DetainID = -1;

            string NonQuery = $"INSERT INTO DetainedLicenses" +
                $"           (LicenseID" +
                $"           ,DetainDate" +
                $"           ,FineFees" +
                $"           ,CreatedByUserID" +
                $"           ,IsReleased" +
                $"           ,ReleaseDate" +
                $"           ,ReleasedByUserID" +
                $"           ,ReleaseApplicationID)" +
                $"      VALUES" +
                $"           (@LicenseID" +
                $"           ,@DetainDate" +
                $"           ,@FineFees" +
                $"           ,@CreatedByUserID" +
                $"           ,@IsReleased" +
                $"           ,@ReleaseDate" +
                $"           ,@ReleasedByUserID" +
                $"           ,@ReleaseApplicationID);" +
                $"SELECT SCOPE_IDENTITY();";

            SqlConnection Connection = new SqlConnection(DataAccessSettings.ConnectionString);

            SqlCommand command = new SqlCommand(NonQuery, Connection);

            command.Parameters.AddWithValue("@LicenseID", LicenseID);
            command.Parameters.AddWithValue("@DetainDate", DateTime.Now);
            command.Parameters.AddWithValue("@FineFees", FineFees);
            command.Parameters.AddWithValue("@CreatedByUserID", CreatedByUserID);
            command.Parameters.AddWithValue("@IsReleased", false);
            command.Parameters.AddWithValue("@ReleaseDate", DBNull.Value);
            command.Parameters.AddWithValue("@ReleasedByUserID", DBNull.Value);
            command.Parameters.AddWithValue("@ReleaseApplicationID", DBNull.Value);



            try
            {
                Connection.Open();

                object Result = command.ExecuteScalar();

                if (int.TryParse(Result?.ToString(), out int ID))
                {
                    DetainID = ID;

                }
            }
            catch
            {
            }
            finally
            {
                Connection.Close();
            }

            return DetainID;
        }

        public static bool Update(int DetainID,int LicenseID,
            DateTime DetainDate,
            float FineFees,
            int CreatedByUserID,
            bool IsReleased,
            DateTime? ReleaseDate,
            int ReleasedByUserID,
            int ReleaseApplicationID)
        {
            int RowsAffected = 0;

            string NonQuery = $"UPDATE DetainedLicenses" +
                $" SET   LicenseID = @LicenseID" +
                $"      ,DetainDate = @DetainDate" +
                $"      ,FineFees = @FineFees" +
                $"      ,CreatedByUserID = @CreatedByUserID" +
                $"      ,IsReleased = @IsReleased" +
                $"      ,ReleaseDate = @ReleaseDate" +
                $"      ,ReleasedByUserID = @ReleasedByUserID" +
                $"      ,ReleaseApplicationID = @ReleaseApplicationID " +
                $" WHERE DetainID = @DetainID;";

            SqlConnection Connection = new SqlConnection(DataAccessSettings.ConnectionString);

            SqlCommand command = new SqlCommand(NonQuery, Connection);


            command.Parameters.AddWithValue("@LicenseID", LicenseID);
            command.Parameters.AddWithValue("@DetainDate", DetainDate);
            command.Parameters.AddWithValue("@FineFees", FineFees);
            command.Parameters.AddWithValue("@CreatedByUserID", CreatedByUserID);
            command.Parameters.AddWithValue("@IsReleased", IsReleased);

            if(ReleaseDate != null)
                command.Parameters.AddWithValue("@ReleaseDate", ReleaseDate);
            else
                command.Parameters.AddWithValue("@ReleaseDate", DBNull.Value);

            if (ReleasedByUserID != -1)
                command.Parameters.AddWithValue("@ReleasedByUserID", ReleasedByUserID);
            else
                command.Parameters.AddWithValue("@ReleasedByUserID", DBNull.Value);

            if (ReleaseApplicationID != -1)
                command.Parameters.AddWithValue("@ReleaseApplicationID", ReleaseApplicationID);
            else
                command.Parameters.AddWithValue("@ReleaseApplicationID", DBNull.Value);

            command.Parameters.AddWithValue("@DetainID", DetainID);




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
            string Query = $"Select * from MyDetainedLicenses_View Order by IsReleased , DetainID Asc";

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

        public static bool IsExist(int DetainID)
        {
            string Query = $"Select Exist=1 From DetainedLicenses where DetainID = @DetainID";

            SqlConnection Connection = new SqlConnection(DataAccessSettings.ConnectionString);

            SqlCommand command = new SqlCommand(Query, Connection);

            command.Parameters.AddWithValue("@DetainID", DetainID);

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

        public static bool IsLicenseDetained(int LicenseID)
        {
            string Query = $"Select Exist=1 From DetainedLicenses where LicenseID = @LicenseID and IsReleased = 0";

            SqlConnection Connection = new SqlConnection(DataAccessSettings.ConnectionString);

            SqlCommand command = new SqlCommand(Query, Connection);

            command.Parameters.AddWithValue("@LicenseID", LicenseID);

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

        public static bool Delete(int DetainID)
        {
            int RowsAffected = 0;

            string NonQuery = $"Delete From DetainedLicenses where DetainID = @DetainID";

            SqlConnection Connection = new SqlConnection(DataAccessSettings.ConnectionString);

            SqlCommand command = new SqlCommand(NonQuery, Connection);

            command.Parameters.AddWithValue("@DetainID", DetainID);


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

        public static bool ReleaseByLicenseID(
            int LicenseID,
            int ReleasedByUserID,
            int ReleaseApplicationID)
        {
            int RowsAffected = 0;

            string NonQuery = $"UPDATE DetainedLicenses" +
                $" SET   IsReleased = @IsReleased" +
                $"      ,ReleaseDate = @ReleaseDate" +
                $"      ,ReleasedByUserID = @ReleasedByUserID" +
                $"      ,ReleaseApplicationID = @ReleaseApplicationID " +
                $" WHERE LicenseID = @LicenseID;";

            SqlConnection Connection = new SqlConnection(DataAccessSettings.ConnectionString);

            SqlCommand command = new SqlCommand(NonQuery, Connection);


            command.Parameters.AddWithValue("@IsReleased", true);
            command.Parameters.AddWithValue("@ReleaseDate", DateTime.Now);
            command.Parameters.AddWithValue("@ReleasedByUserID", ReleasedByUserID);
            command.Parameters.AddWithValue("@ReleaseApplicationID", ReleaseApplicationID);
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

        public static bool Release(
    int DetainID,
    int ReleasedByUserID,
    int ReleaseApplicationID)
        {
            int RowsAffected = 0;

            string NonQuery = $"UPDATE DetainedLicenses" +
                $" SET   IsReleased = @IsReleased" +
                $"      ,ReleaseDate = @ReleaseDate" +
                $"      ,ReleasedByUserID = @ReleasedByUserID" +
                $"      ,ReleaseApplicationID = @ReleaseApplicationID " +
                $" WHERE DetainID = @DetainID;";

            SqlConnection Connection = new SqlConnection(DataAccessSettings.ConnectionString);

            SqlCommand command = new SqlCommand(NonQuery, Connection);


            command.Parameters.AddWithValue("@IsReleased", true);
            command.Parameters.AddWithValue("@ReleaseDate", DateTime.Now);
            command.Parameters.AddWithValue("@ReleasedByUserID", ReleasedByUserID);
            command.Parameters.AddWithValue("@ReleaseApplicationID", ReleaseApplicationID);
            command.Parameters.AddWithValue("@DetainID", DetainID);




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
