using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Net;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;

namespace DVLDDataAccessLayer
{
    public class clsCountryData
    {
        static public bool FindByID(int CountryID, ref string CountryName)
        {
            string Query = $"SELECT * From Countries" +
                $" Where CountryID = @CountryID";

            SqlConnection Connection = new SqlConnection(DataAccessSettings.ConnectionString);

            SqlCommand command = new SqlCommand(Query, Connection);

            command.Parameters.AddWithValue("@CountryID", CountryID);

            Connection.Open();

            SqlDataReader reader = command.ExecuteReader();

            try
            {
                if (reader.Read())
                {
                    CountryName = reader["CountryName"].ToString();
     
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

        static public bool FindByCountryName(string CountryName , ref int CountryID)
        {
            string Query = $"SELECT * From Countries" +
                $" Where CountryName = @CountryName";

            SqlConnection Connection = new SqlConnection(DataAccessSettings.ConnectionString);

            SqlCommand command = new SqlCommand(Query, Connection);

            command.Parameters.AddWithValue("@CountryName", CountryName);

            Connection.Open();

            SqlDataReader reader = command.ExecuteReader();

            try
            {
                if (reader.Read())
                {
                    CountryID = Convert.ToInt16(reader["CountryID"]);

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

        static public DataTable GetAll()
        {
            string Query = $"SELECT CountryName FROM  Countries";

            SqlConnection Connection = new SqlConnection(DataAccessSettings.ConnectionString);

            SqlCommand command = new SqlCommand();

            command.CommandText = Query;
            command.Connection = Connection;


            DataTable dt = new DataTable();



            try
            {
                Connection.Open();

                SqlDataReader reader = command.ExecuteReader();

                dt.Load(reader);
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
    }
}
