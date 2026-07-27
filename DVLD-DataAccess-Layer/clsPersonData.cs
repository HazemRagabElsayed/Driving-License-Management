using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Reflection;
using System.Runtime.Remoting.Messaging;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.ListView;

namespace DVLDDataAccessLayer
{
    public class clsPersonData
    {
        public static bool FindByID(int PersonID, ref string NationalNo, ref string FirstName,
            ref string SecondName, ref string ThirdName, ref string LastName,
            ref DateTime DateOfBirth, ref short Gender, ref string Address
            , ref string Phone, ref string Email, ref int NationalityCountryID
            , ref string ImagePath)
        {

            string Query = $"SELECT * FROM People where PersonID = @PersonID";



            SqlConnection Connection = new SqlConnection(DataAccessSettings.ConnectionString);

            SqlCommand command = new SqlCommand(Query, Connection);

            command.Parameters.AddWithValue("@PersonID", PersonID);

            Connection.Open();

            SqlDataReader reader = command.ExecuteReader();

            try
            {
                if (reader.Read())
                {
                    NationalNo = Convert.ToString(reader["NationalNo"]);
                    FirstName = reader["FirstName"].ToString();
                    SecondName = reader["SecondName"].ToString();

                    if (reader["ThirdName"] != DBNull.Value)
                    {
                        ThirdName = reader["ThirdName"].ToString();
                    }
                    else
                    {
                        ThirdName = "";
                    }

                    LastName = reader["LastName"].ToString();
                    DateOfBirth = Convert.ToDateTime(reader["DateOfBirth"]);

                    //if (Convert.ToBoolean(reader["Gender"]))
                    //{
                    //    Gender = "Female";
                    //}
                    //else
                    //{
                    //    Gender = "Male";
                    //}

                    Gender = Convert.ToInt16(reader["Gender"]);


                    Address = reader["Address"].ToString();
                    Phone = reader["Phone"].ToString();


                    if (reader["Email"] != DBNull.Value)
                    {
                        Email = reader["Email"].ToString();
                    }
                    else
                    {
                        Email = "";
                    }

                    NationalityCountryID = Convert.ToInt32(reader["NationalityCountryID"]);

                    if (reader["ImagePath"] != DBNull.Value)
                    {
                        ImagePath = reader["ImagePath"].ToString();
                    }
                    else
                    {
                        ImagePath = "";
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

        public static bool FindByNationalNo(string NationalNo, ref int PersonID, ref string FirstName,
            ref string SecondName, ref string ThirdName, ref string LastName,
            ref DateTime DateOfBirth, ref short Gender, ref string Address
            , ref string Phone, ref string Email, ref int NationalityCountryID
            , ref string ImagePath)
        {

            string Query = $"SELECT * FROM People where NationalNo = @NationalNo";

            SqlConnection Connection = new SqlConnection(DataAccessSettings.ConnectionString);

            SqlCommand command = new SqlCommand(Query, Connection);

            command.Parameters.AddWithValue("@NationalNo", NationalNo);

            try
            {
                Connection.Open();

                SqlDataReader reader = command.ExecuteReader();

                if (reader.Read())
                {
                    PersonID = Convert.ToInt32(reader["PersonID"]);
                    FirstName = reader["FirstName"].ToString();
                    SecondName = reader["SecondName"].ToString();

                    if (reader["ThirdName"] != DBNull.Value)
                    {
                        ThirdName = reader["ThirdName"].ToString();
                    }
                    else
                    {
                        ThirdName = "";
                    }

                    LastName = reader["LastName"].ToString();
                    DateOfBirth = Convert.ToDateTime(reader["DateOfBirth"]);

                    //if (Convert.ToBoolean(reader["Gender"]))
                    //{
                    //    Gender = "Female";
                    //}
                    //else
                    //{
                    //    Gender = "Male";
                    //}

                    Gender = Convert.ToInt16(reader["Gender"]);

                    Address = reader["Address"].ToString();
                    Phone = reader["Phone"].ToString();


                    if (reader["Email"] != DBNull.Value)
                    {
                        Email = reader["Email"].ToString();
                    }
                    else
                    {
                        Email = "";
                    }

                    NationalityCountryID = Convert.ToInt32(reader["NationalityCountryID"]);

                    if (reader["ImagePath"] != DBNull.Value)
                    {
                        ImagePath = reader["ImagePath"].ToString();
                    }
                    else
                    {
                        ImagePath = "";
                    }
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

        public static int AddNew(string NationalNo,  string FirstName,
             string SecondName,  string ThirdName,  string LastName,
             DateTime DateOfBirth,  short Gender,  string Address
            ,  string Phone,  string Email, int NationalityCountryID
            ,  string ImagePath)
        {

            int PersonID = -1;

            string NonQuery = $"INSERT INTO People" +
                $"           (NationalNo" +
                $"           ,FirstName" +
                $"           ,SecondName" +
                $"           ,ThirdName" +
                $"           ,LastName" +
                $"           ,DateOfBirth" +
                $"           ,Gender" +
                $"           ,Address" +
                $"           ,Phone" +
                $"           ,Email" +
                $"           ,NationalityCountryID" +
                $"           ,ImagePath)" +
                $"     VALUES" +
                $"           (@NationalNo" +
                $"           ,@FirstName" +
                $"           ,@SecondName" +
                $"           ,@ThirdName" +
                $"           ,@LastName " +
                $"           ,@DateOfBirth" +
                $"           ,@Gender" +
                $"           ,@Address" +
                $"           ,@Phone" +
                $"           ,@Email" +
                $"           ,@NationalityCountryID" +
                $"           ,@ImagePath);" +
                $"SELECT SCOPE_IDENTITY();";

            SqlConnection Connection = new SqlConnection(DataAccessSettings.ConnectionString);

            SqlCommand command = new SqlCommand(NonQuery, Connection);

            command.Parameters.AddWithValue("@NationalNo", NationalNo);
            command.Parameters.AddWithValue("@FirstName", FirstName);
            command.Parameters.AddWithValue("@SecondName", SecondName);

            if (ThirdName == "")
            {
                command.Parameters.AddWithValue("@ThirdName", DBNull.Value);

            }
            else
            {
                command.Parameters.AddWithValue("@ThirdName", ThirdName);
            }

            command.Parameters.AddWithValue("@LastName", LastName);

            command.Parameters.AddWithValue("@DateOfBirth", DateOfBirth);


            command.Parameters.AddWithValue("@Gender", Gender);
           

            command.Parameters.AddWithValue("@Address", Address);
            command.Parameters.AddWithValue("@Phone", Phone);
            command.Parameters.AddWithValue("@Email", Email);
            command.Parameters.AddWithValue("@NationalityCountryID", NationalityCountryID);

            if (ImagePath == "")
            {
                command.Parameters.AddWithValue("@ImagePath", DBNull.Value);

            }
            else
            {
                command.Parameters.AddWithValue("@ImagePath", ImagePath);
            }

            try
            {
                Connection.Open();

                object Result = command.ExecuteScalar();

                if (int.TryParse(Result?.ToString(), out int ID))
                {
                    PersonID = ID;

                }
            }
            catch
            {
            }
            finally
            {
                Connection.Close();
            }

            return PersonID;
        }

        public static bool Update(int PersonID, string NationalNo, string FirstName,
            string SecondName, string ThirdName, string LastName,
            DateTime DateOfBirth, short Gender, string Address
            , string Phone, string Email, int NationalityCountryID
            , string ImagePath)
        {
            int RowsAffected = 0;

            string NonQuery = $"UPDATE People" +
                $" SET NationalNo = @NationalNo" +
                $"      ,FirstName = @FirstName" +
                $"      ,SecondName = @SecondName" +
                $"      ,ThirdName = @ThirdName" +
                $"      ,LastName = @LastName" +
                $"      ,DateOfBirth = @DateOfBirth" +
                $"      ,Gender = @Gender" +
                $"      ,Address = @Address" +
                $"      ,Phone = @Phone" +
                $"      ,Email = @Email" +
                $"      ,NationalityCountryID = @NationalityCountryID" +
                $"      ,ImagePath = @ImagePath" +
                $" WHERE PersonID = @PersonID;";

            SqlConnection Connection = new SqlConnection(DataAccessSettings.ConnectionString);

            SqlCommand command = new SqlCommand(NonQuery, Connection);

            command.Parameters.AddWithValue("@PersonID", PersonID);
            command.Parameters.AddWithValue("@NationalNo", NationalNo);
            command.Parameters.AddWithValue("@FirstName", FirstName);
            command.Parameters.AddWithValue("@SecondName", SecondName);

            if (ThirdName == "")
            {
                command.Parameters.AddWithValue("@ThirdName", DBNull.Value);

            }
            else
            {
                command.Parameters.AddWithValue("@ThirdName", ThirdName);
            }

            command.Parameters.AddWithValue("@LastName", LastName);

            command.Parameters.AddWithValue("@DateOfBirth", DateOfBirth);

  
            command.Parameters.AddWithValue("@Gender", Gender);
     

            command.Parameters.AddWithValue("@Address", Address);
            command.Parameters.AddWithValue("@Phone", Phone);
            command.Parameters.AddWithValue("@Email", Email);
            command.Parameters.AddWithValue("@NationalityCountryID", NationalityCountryID);

            if(string.IsNullOrEmpty(ImagePath))
            {
                command.Parameters.AddWithValue("@ImagePath", DBNull.Value);

            }
            else
            {
                command.Parameters.AddWithValue("@ImagePath", ImagePath);
            }

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
            string Query = $"SELECT PersonID, NationalNo, FirstName, SecondName, " +
                $"ThirdName, LastName, CASE  WHEN Gender = 0 THEN 'Male' " +
                $"WHEN Gender = 1 THEN 'Female' ELSE 'Unknown' END AS GenderCaption, DateOfBirth, " +
                $"CountryName, Phone, Email FROM  People p INNER JOIN " +
                $"Countries c ON c.CountryID = p.NationalityCountryID";

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

        //public static DataTable GetAll(string Filter = "", string Value = "")
        //{
        //    string Query = $"SELECT PersonID as 'Person ID', NationalNo as 'National No.'" +
        //        $", FirstName as 'First Name', SecondName as 'Second Name'," +
        //        $"ThirdName as 'Third Name', LastName as 'Last Name'," +
        //        $" CASE  WHEN Gender = 0 THEN 'Male' WHEN Gender = 1 THEN 'Female'" +
        //        $" ELSE 'Unknown' END AS Gender, DateOfBirth as 'Date Of Birth', " +
        //        $"CountryName as Nationality, Phone, Email FROM  People p INNER JOIN " +
        //        $"Countries c ON c.CountryID = p.NationalityCountryID";

        //    SqlConnection Connection = new SqlConnection(DataAccessSettings.ConnectionString);

        //    SqlCommand command = new SqlCommand();


        //    if(Filter != "" && Value != "")
        //    {
        //        Filter = Filter.Replace(" ","");
        //        Filter = Filter.Replace(".", "");

        //        if (Filter == "Nationality")
        //        {
        //            Filter = "CountryName";
        //        }

        //        if (Filter == "PersonID")
        //        {
        //            Query += $" Where {Filter} = @{Filter}";
        //            command.Parameters.AddWithValue("@" + Filter, Convert.ToInt32(Value));
        //        }
        //        else
        //        {
        //            Query += $" Where {Filter} LIKE @{Filter}";
        //            command.Parameters.AddWithValue("@" + Filter, Value + "%");
        //        }

        //    }


        //    command.CommandText = Query;
        //    command.Connection = Connection;


        //    DataTable dt = new DataTable();



        //    try
        //    {
        //        Connection.Open();

        //        SqlDataReader reader = command.ExecuteReader();

        //        dt.Load(reader);
        //    }
        //    catch
        //    {
        //        return null;
        //    }
        //    finally
        //    {
        //        Connection.Close();
        //    }

        //    return dt;
        //}

        public static bool IsExist(int PersonID)
        {
            string Query = $"Select Exist=1 From People where PersonID = @PersonID";

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

        public static bool IsExist(string NationalNo)
        {
            string Query = $"Select Exist=1 From People where NationalNo = @NationalNo";

            SqlConnection Connection = new SqlConnection(DataAccessSettings.ConnectionString);

            SqlCommand command = new SqlCommand(Query, Connection);

            command.Parameters.AddWithValue("@NationalNo", NationalNo);

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

        public static bool Delete(int PersonID)
        {
            int RowsAffected = 0;

            string NonQuery = $"Delete From People where PersonID = @PersonID";

            SqlConnection Connection = new SqlConnection(DataAccessSettings.ConnectionString);

            SqlCommand command = new SqlCommand(NonQuery, Connection);

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

        public static short GetAge(int PersonID)
        {
            short Age = 0;

            string Query = @"select DATEDIFF(YEAR,p.DateOfBirth,@ApplicationDate) As Age from People p
                      Where PersonID = @PersonID ";



            SqlConnection Connection = new SqlConnection(DataAccessSettings.ConnectionString);

            SqlCommand command = new SqlCommand(Query, Connection);

            command.Parameters.AddWithValue("@PersonID", PersonID);
            command.Parameters.AddWithValue("@ApplicationDate", DateTime.Now);

            Connection.Open();

            SqlDataReader reader = command.ExecuteReader();

            try
            {
                if (reader.Read())
                {
                    return Age = Convert.ToInt16(reader["Age"]);
  
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

        //public static bool IsLinkedToOtherData(int PersonID)
        //{
        //    string Query = $"SELECT top 1 LinkExists = 1 FROM People  " +
        //        $"INNER JOIN Applications ON Applications.ApplicantPersonID = People.PersonID INNER JOIN " +
        //        $"Drivers ON People.PersonID = Drivers.PersonID " +
        //        $"INNER JOIN  Users ON People.PersonID = Users.PersonID " +
        //        $"Where People.PersonID = @PersonID";



        //    SqlConnection Connection = new SqlConnection(DataAccessSettings.ConnectionString);

        //    SqlCommand command = new SqlCommand(Query, Connection);

        //    command.Parameters.AddWithValue("@PersonID", PersonID);

        //    Connection.Open();

        //    SqlDataReader reader = command.ExecuteReader();

        //    try
        //    {
        //        if (reader.Read())
        //        {
        //            return true;
        //        }
        //        else
        //        {
        //            reader.Close();
        //            Connection.Close();
        //            return false;
        //        }
        //    }
        //    catch
        //    {
        //        return false;
        //    }
        //    finally
        //    {
        //        reader.Close();
        //        Connection.Close();
        //    }
        //}
    }
}
