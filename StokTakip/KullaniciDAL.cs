using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StokTakip
{
    internal class KullaniciDAL : OrtakDAL
    {
        internal bool KullaniciGiris(string kullaniciAdi, string sifre)
        {
            BaglantiyiAc();
            SqlCommand sqlCommand = new SqlCommand("select * from Kullanicilar where Username=@Username and Password=@Password", connection);
            sqlCommand.Parameters.AddWithValue("@Username", kullaniciAdi);
            sqlCommand.Parameters.AddWithValue("@Password", sifre);
            SqlDataReader reader = sqlCommand.ExecuteReader();
            // reader.Read();
            try
            {
                if (reader.HasRows)
                {
                    return true;
                }
            }
            catch (Exception)
            {
                throw;
            }
            finally // hata oluşsa da oluşmasa da çalışır
            {
                reader.Close();
                sqlCommand.Dispose();
                connection.Close();
            }
            return false;
        }
    }
}
