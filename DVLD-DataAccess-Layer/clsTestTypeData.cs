using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DVLDDataAccessLayer
{
    public class clsTestTypeData
    {
        public static DataTable GetAll()
        {
            string Query = $"Select * from TestTypes";

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
        public static bool FindByID(int TestTypeID, ref string TestTypeTitle,
            ref string TestTypeDescription, ref float TestTypeFees)
        {

            string Query = $"SELECT * FROM TestTypes where TestTypeID = @TestTypeID";



            SqlConnection Connection = new SqlConnection(DataAccessSettings.ConnectionString);

            SqlCommand command = new SqlCommand(Query, Connection);

            command.Parameters.AddWithValue("@TestTypeID", TestTypeID);

            Connection.Open();

            SqlDataReader reader = command.ExecuteReader();

            try
            {
                if (reader.Read())
                {
                    TestTypeTitle = Convert.ToString(reader["TestTypeTitle"]);
                    TestTypeDescription = Convert.ToString(reader["TestTypeDescription"]);
                    TestTypeFees = Convert.ToSingle(reader["TestTypeFees"]);


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

        public static bool Update(int TestTypeID, string TestTypeTitle, string TestTypeDescription, float TestTypeFees)
        {
            int RowsAffected = 0;

            string NonQuery = $"UPDATE TestTypes" +
                $" SET   TestTypeTitle = @TestTypeTitle" +
                $"      ,TestTypeDescription = @TestTypeDescription" +
                $"      ,TestTypeFees = @TestTypeFees" +
                $" WHERE TestTypeID = @TestTypeID;";

            SqlConnection Connection = new SqlConnection(DataAccessSettings.ConnectionString);

            SqlCommand command = new SqlCommand(NonQuery, Connection);

            command.Parameters.AddWithValue("@TestTypeTitle", TestTypeTitle);
            command.Parameters.AddWithValue("@TestTypeDescription", TestTypeDescription);
            command.Parameters.AddWithValue("@TestTypeFees", TestTypeFees);
            command.Parameters.AddWithValue("@TestTypeID", TestTypeID);

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
