using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StokTakip
{
    internal class UserDAL 
    {
        SqlConnection connection = new SqlConnection(@"server=(localdb)\MSSQLLocalDB; database=StokTakip; trusted_connection=true");
        void BaglantiyiAc()
        {
            if (connection.State == ConnectionState.Closed)
            {
                connection.Open();
            }
        }
        public DataTable KayitlariDatatableileGetir()
        {
            DataTable table = new DataTable();
            BaglantiyiAc();
            SqlCommand sqlCommand = new SqlCommand("select*from Kullanicilar", connection);
            SqlDataReader reader = sqlCommand.ExecuteReader();
            table.Load(reader);
            reader.Close();
            sqlCommand.Dispose();
            table.Dispose();
            connection.Close();
            return table;
        }
        public int Add(User user)
        {
            BaglantiyiAc();
            int islemSonucu = 0;
            SqlCommand sqlCommand = new SqlCommand("insert into Kullanicilar (Name,Surname,Username,Password,Email,Phone,UserGuid) values (@Name,@Surname,@Username,@Password,@Email,@Phone,@UserGuid)", connection);
            sqlCommand.Parameters.AddWithValue("@Name", user.Name);
            sqlCommand.Parameters.AddWithValue("@Surname", user.Surname);
            sqlCommand.Parameters.AddWithValue("@Username", user.Username);
            sqlCommand.Parameters.AddWithValue("@Password", user.Password);
            sqlCommand.Parameters.AddWithValue("@Email", user.Email);
            sqlCommand.Parameters.AddWithValue("@Phone", user.Phone);
            sqlCommand.Parameters.AddWithValue("@UserGuid", user.UserGuid);
            islemSonucu = sqlCommand.ExecuteNonQuery();
            sqlCommand.Dispose();
            connection.Close();

            return islemSonucu;
        }
        public User Get(int id)
        {
            User user = new User();
            BaglantiyiAc();
            SqlCommand sqlCommand = new SqlCommand("select*from Kullanicilar where Id=@Id", connection);
            sqlCommand.Parameters.AddWithValue("@Id", id);
            SqlDataReader reader = sqlCommand.ExecuteReader();
            if (reader.Read())
            {
                user.Id = Convert.ToInt32(reader["Id"]);
                user.Name = Convert.ToString(reader["Name"]);
                user.Surname = Convert.ToString(reader["Surname"]);
                user.Username = Convert.ToString(reader["Username"]);
                user.Password = Convert.ToString(reader["Password"]);
                user.Email = Convert.ToString(reader["Email"]);
                user.Phone = Convert.ToString(reader["Phone"]);
                user.UserGuid = Guid.Parse(reader["UserGuid"].ToString());
            }
            reader.Close();
            sqlCommand.Dispose();
            connection.Close();
            return user;

        }
        public int Update(User user)
        {
            BaglantiyiAc();
            int islemSonucu = 0;
            SqlCommand sqlCommand = new SqlCommand("Update Kullanicilar set Name=@Name,Surname=@Surname,Username=@Username,Password=@Password,Email=@Email,Phone=@Phone,UserGuid=@UserGuid where Id=@Id", connection);
            sqlCommand.Parameters.AddWithValue("@Id", user.Id);
            sqlCommand.Parameters.AddWithValue("@Name", user.Name);
            sqlCommand.Parameters.AddWithValue("@Surname", user.Surname);
            sqlCommand.Parameters.AddWithValue("@Username", user.Username);
            sqlCommand.Parameters.AddWithValue("@Password", user.Password);
            sqlCommand.Parameters.AddWithValue("@Email", user.Email);
            sqlCommand.Parameters.AddWithValue("@Phone", user.Phone);
            sqlCommand.Parameters.AddWithValue("@UserGuid", user.UserGuid);
            islemSonucu = sqlCommand.ExecuteNonQuery();
            sqlCommand.Dispose();
            connection.Close();

            return islemSonucu;
        }
        public int Delete(int id)
        {
            BaglantiyiAc();
            int islemSonucu = 0;
            SqlCommand sqlCommand = new SqlCommand("Delete from Kullanicilar where Id=@Id", connection);
            sqlCommand.Parameters.AddWithValue("@Id", id);

            islemSonucu = sqlCommand.ExecuteNonQuery();
            sqlCommand.Dispose();
            connection.Close();

            return islemSonucu;
        }
    }
}
