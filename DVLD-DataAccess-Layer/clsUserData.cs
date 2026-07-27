using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DVLDDataAccessLayer
{
    public class clsUserData
    {
        public static bool FindByID(int UserID, ref string UserName, ref string Password,
           ref bool IsActive, ref int PersonID)
        {

            string Query = $"SELECT * FROM Users where UserID = @UserID";



            SqlConnection Connection = new SqlConnection(DataAccessSettings.ConnectionString);

            SqlCommand command = new SqlCommand(Query, Connection);

            command.Parameters.AddWithValue("@UserID", UserID);

            Connection.Open();

            SqlDataReader reader = command.ExecuteReader();

            try
            {
                if (reader.Read())
                {
                    UserName = Convert.ToString(reader["UserName"]);
                    Password = reader["Password"].ToString();
                    IsActive = Convert.ToBoolean(reader["IsActive"]);
                    PersonID = Convert.ToInt32(reader["PersonID"]);


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

        public static bool FindByUserNameAndPassword(string UserName, string Password, ref int UserID,
            ref bool IsActive, ref int PersonID)
        {

            string Query = $"SELECT * FROM Users where UserName = @UserName and" +
                $" Password = @Password";

            SqlConnection Connection = new SqlConnection(DataAccessSettings.ConnectionString);

            SqlCommand command = new SqlCommand(Query, Connection);

            command.Parameters.AddWithValue("@UserName", UserName);
            command.Parameters.AddWithValue("@Password", Password);

            try
            {
                Connection.Open();

                SqlDataReader reader = command.ExecuteReader();

                if (reader.Read())
                {
                    UserID = Convert.ToInt32(reader["UserID"]);
                    IsActive = Convert.ToBoolean(reader["IsActive"]);
                    PersonID = Convert.ToInt32(reader["PersonID"]);
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
                Connection.Close();
            }



        }


        public static bool FindByUserName(string UserName, ref string Password, ref int UserID,
           ref bool IsActive, ref int PersonID)
        {

            string Query = $"SELECT * FROM Users where UserName = @UserName ";

            SqlConnection Connection = new SqlConnection(DataAccessSettings.ConnectionString);

            SqlCommand command = new SqlCommand(Query, Connection);

            command.Parameters.AddWithValue("@UserName", UserName);

            try
            {
                Connection.Open();

                SqlDataReader reader = command.ExecuteReader();

                if (reader.Read())
                {
                    UserID = Convert.ToInt32(reader["UserID"]);
                    Password = reader["Password"].ToString();
                    IsActive = Convert.ToBoolean(reader["IsActive"]);
                    PersonID = Convert.ToInt32(reader["PersonID"]);
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
                Connection.Close();
            }



        }

        public static int AddNew(string UserName, string Password,
             bool IsActive, int PersonID)
        {

            int UserID = -1;

            string NonQuery = $"INSERT INTO Users" +
                $"           (UserName" +
                $"           ,Password" +
                $"           ,IsActive" +
                $"           ,PersonID)" +
                $"      VALUES"         +
                $"           (@UserName" +
                $"           ,@Password" +
                $"           ,@IsActive" +
                $"           ,@PersonID);" +
                $"SELECT SCOPE_IDENTITY();";

            SqlConnection Connection = new SqlConnection(DataAccessSettings.ConnectionString);

            SqlCommand command = new SqlCommand(NonQuery, Connection);

            command.Parameters.AddWithValue("@UserName", UserName);
            command.Parameters.AddWithValue("@Password", Password);
            command.Parameters.AddWithValue("@IsActive", IsActive);
            command.Parameters.AddWithValue("@PersonID", PersonID);


            try
            {
                Connection.Open();

                object Result = command.ExecuteScalar();

                if (int.TryParse(Result?.ToString(), out int ID))
                {
                    UserID = ID;

                }
            }
            catch
            {
            }
            finally
            {
                Connection.Close();
            }

            return UserID;
        }

        public static bool Update(int UserID, string UserName, string Password,
            bool IsActive, int PersonID)
        {
            int RowsAffected = 0;

            string NonQuery = $"UPDATE Users" +
                $" SET   UserName = @UserName" +
                $"      ,Password = @Password" +
                $"      ,IsActive = @IsActive" +
                $"      ,PersonID = @PersonID" +
                $" WHERE UserID = @UserID;";

            SqlConnection Connection = new SqlConnection(DataAccessSettings.ConnectionString);

            SqlCommand command = new SqlCommand(NonQuery, Connection);

            command.Parameters.AddWithValue("@UserID", UserID);
            command.Parameters.AddWithValue("@UserName", UserName);
            command.Parameters.AddWithValue("@Password", Password);
            command.Parameters.AddWithValue("@IsActive", IsActive);
            command.Parameters.AddWithValue("@PersonID", PersonID);



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
            string Query = $"SELECT UserID, u.PersonID," +
                $"(p.FirstName + ' ' + p.SecondName +" +
                $" (Case When p.ThirdName = NULL then '' else ' ' end) + p.ThirdName + ' ' + p.LastName) as FullName,  UserName, IsActive" +
                $" FROM  Users u INNER JOIN People p ON p.PersonID = u.PersonID";

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


        public static bool IsExist(int UserID)
        {
            string Query = $"Select Exist=1 From Users where UserID = @UserID";

            SqlConnection Connection = new SqlConnection(DataAccessSettings.ConnectionString);

            SqlCommand command = new SqlCommand(Query, Connection);

            command.Parameters.AddWithValue("@UserID", UserID);

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

        public static bool IsExist(string UserName)
        {
            string Query = $"Select Exist=1 From Users where UserName = @UserName";

            SqlConnection Connection = new SqlConnection(DataAccessSettings.ConnectionString);

            SqlCommand command = new SqlCommand(Query, Connection);

            command.Parameters.AddWithValue("@UserName", UserName);

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

        public static bool Delete(int UserID)
        {
            int RowsAffected = 0;

            string NonQuery = $"Delete From Users where UserID = @UserID";

            SqlConnection Connection = new SqlConnection(DataAccessSettings.ConnectionString);

            SqlCommand command = new SqlCommand(NonQuery, Connection);

            command.Parameters.AddWithValue("@UserID", UserID);


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


        public static bool IsPersonAUser(int PersonID)
        {
            string Query = $"Select Exist=1 From Users where PersonID = @PersonID";

            SqlConnection Connection = new SqlConnection(DataAccessSettings.ConnectionString);

            SqlCommand command = new SqlCommand(Query, Connection);

            command.Parameters.AddWithValue("@PersonID", PersonID);

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

    }
}
