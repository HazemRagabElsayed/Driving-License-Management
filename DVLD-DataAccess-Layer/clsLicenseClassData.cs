using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DVLDDataAccessLayer
{
    public class clsLicenseClassData
    {
        public static DataTable GetAll()
        {
            string Query = $"Select * from LicenseClasses";

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
        public static bool FindByID(int LicenseClassID, ref string ClassName, ref string ClassDescription,
            ref short MinimumAllowedAge,ref short DefaultValidityLength, ref float ClassFees)
        {

            string Query = $"SELECT * FROM LicenseClasses where LicenseClassID = @LicenseClassID";



            SqlConnection Connection = new SqlConnection(DataAccessSettings.ConnectionString);

            SqlCommand command = new SqlCommand(Query, Connection);

            command.Parameters.AddWithValue("@LicenseClassID", LicenseClassID);

            Connection.Open();

            SqlDataReader reader = command.ExecuteReader();

            try
            {
                if (reader.Read())
                {
                    ClassName = Convert.ToString(reader["ClassName"]);
                    ClassDescription = Convert.ToString(reader["ClassDescription"]);
                    MinimumAllowedAge = Convert.ToInt16(reader["MinimumAllowedAge"]);
                    DefaultValidityLength = Convert.ToInt16(reader["DefaultValidityLength"]);
                    ClassFees = Convert.ToSingle(reader["ClassFees"]);


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
        public static bool FindByClassName( string ClassName,ref int LicenseClassID, ref string ClassDescription,
            ref short MinimumAllowedAge, ref short DefaultValidityLength, ref float ClassFees)
        {

            string Query = $"SELECT * FROM LicenseClasses where ClassName = @ClassName";



            SqlConnection Connection = new SqlConnection(DataAccessSettings.ConnectionString);

            SqlCommand command = new SqlCommand(Query, Connection);

            command.Parameters.AddWithValue("@ClassName", ClassName);

            Connection.Open();

            SqlDataReader reader = command.ExecuteReader();

            try
            {
                if (reader.Read())
                {
                    LicenseClassID = Convert.ToInt32(reader["LicenseClassID"]);
                    ClassDescription = Convert.ToString(reader["ClassDescription"]);
                    MinimumAllowedAge = Convert.ToInt16(reader["MinimumAllowedAge"]);
                    DefaultValidityLength = Convert.ToInt16(reader["DefaultValidityLength"]);
                    ClassFees = Convert.ToSingle(reader["ClassFees"]);


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

        public static bool Update(int LicenseClassID, string ClassName, string ClassDescription,
             short MinimumAllowedAge, short DefaultValidityLength, float ClassFees)
        {
            int RowsAffected = 0;

            string NonQuery = $"UPDATE LicenseClasses" +
                $" SET   ClassName = @ClassName" +
                $"      ,ClassDescription = @ClassDescription" +
                $"      ,MinimumAllowedAge = @MinimumAllowedAge" +
                $"      ,DefaultValidityLength = @DefaultValidityLength" +
                $"      ,ClassFees = @ClassFees" +
                $" WHERE LicenseClassID = @LicenseClassID;";

            SqlConnection Connection = new SqlConnection(DataAccessSettings.ConnectionString);

            SqlCommand command = new SqlCommand(NonQuery, Connection);

            command.Parameters.AddWithValue("@ClassName", ClassName);
            command.Parameters.AddWithValue("@ClassDescription", ClassDescription);
            command.Parameters.AddWithValue("@MinimumAllowedAge", MinimumAllowedAge);
            command.Parameters.AddWithValue("@DefaultValidityLength", DefaultValidityLength);
            command.Parameters.AddWithValue("@ClassFees", ClassFees);
            command.Parameters.AddWithValue("@LicenseClassID", LicenseClassID);

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
