using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StokTakip
{
    internal class UrunDAL
    {
        SqlConnection connection = new SqlConnection(@"server=(localdb)\MSSQLLocalDB; database=StokTakip; trusted_connection=true;"); // sql server a bağlanmamızı sağlayacak nesne
        void BaglantiyiAc()
        {
            if (connection.State == ConnectionState.Closed) // eğer yukardaki bağlantı kapalıysa aç
            {
                connection.Open(); // bağlantıyı aç
            }
        }
        public List<Urun> UrunleriGetir()
        {
            List<Urun> urunler = new List<Urun>(); 
            BaglantiyiAc(); 
            SqlCommand sqlCommand = new SqlCommand("select * from Urunler", connection); // sql komutu yazabilmemizi sağlayan araç
            SqlDataReader reader = sqlCommand.ExecuteReader(); // sqlCommand ile gelecek olan veriyi SqlDataReader nesnesi ile okuyoruz. ExecuteReader metodu ise sorguyu sql de çalıştırabilmemizi sağlar. 
            while (reader.Read()) 
            {
                // veritabanından gelen her ürün için aşağıda bir nesne oluşturup gelen veriyi bu nesneye yüklüyoruz
                Urun urun = new Urun()
                {
                    Id = Convert.ToInt32(reader["ID"]),
                    Name = Convert.ToString(reader["Name"]),
                    Description = Convert.ToString(reader["Description"]),
                    Price = Convert.ToDecimal(reader["Price"])
                };
                urunler.Add(urun); // üstte db den okuduğumuz ürünü listeye ekliyoruz
            }
            reader.Close(); // liste hazırlandıktan sonra okuyucuyu kapat
            sqlCommand.Dispose(); // sql komutunu kapat
            connection.Close(); // sql bağlantısını kapat
            return urunler; // listeyi metodun çağrılacağı yere gönder
        }

        public DataTable UrunleriDatatableileGetir()
        {
            DataTable table = new DataTable();
            BaglantiyiAc();
            SqlCommand sqlCommand = new SqlCommand("select * from Urunler", connection); // sql komutumuzu çalıştırdık
            SqlDataReader reader = sqlCommand.ExecuteReader(); // sql komutundan gelen datayı okuduk
            table.Load(reader); // yukardaki boş tabloya okunan datayı yükledik
            reader.Close();
            sqlCommand.Dispose();
            connection.Close();
            return table;
        }
        public DataTable UrunleriDatatableileGetir(string kelime)
        {
            DataTable table = new DataTable();
            BaglantiyiAc();
            SqlCommand sqlCommand = new SqlCommand("select * from Urunler where Name like @kelime", connection); // sql komutumuzu çalıştırdık
            sqlCommand.Parameters.AddWithValue("@kelime", "%" + kelime + "%");
            SqlDataReader reader = sqlCommand.ExecuteReader(); // sql komutundan gelen datayı okuduk
            table.Load(reader); // yukardaki boş tabloya okunan datayı yükledik
            reader.Close();
            sqlCommand.Dispose();
            connection.Close();
            return table;
        }

        public int Add(Urun urun) // bu metot dışarıdan bir urun nesnesi alacak
        {
            BaglantiyiAc();
            int islemSonucu = 0; 
            SqlCommand sqlCommand = new SqlCommand("insert into Urunler(Name, Description, Price, Stock) values(@Name, @Description, @Price, @Stock) ", connection); // kayıt ekleme komutlarımız
            sqlCommand.Parameters.AddWithValue("@Name", urun.Name);
            sqlCommand.Parameters.AddWithValue("@Description", urun.Description);
            sqlCommand.Parameters.AddWithValue("@Price", urun.Price);
            sqlCommand.Parameters.AddWithValue("@Stock", urun.Stock);
            islemSonucu = sqlCommand.ExecuteNonQuery(); 
            sqlCommand.Dispose();
            connection.Close();
            return islemSonucu;
        }
        public int Update(Urun urun) // bu metot dışarıdan bir urun nesnesi alacak
        {
            BaglantiyiAc();
            int islemSonucu = 0;
            SqlCommand sqlCommand = new SqlCommand("update Urunler set Name =@Name, Description=@Description, Price= @Price, Stock=@Stock where Id=@UrunId ", connection); 
            sqlCommand.Parameters.AddWithValue("@UrunId", urun.Id); 
            sqlCommand.Parameters.AddWithValue("@Name", urun.Name);
            sqlCommand.Parameters.AddWithValue("@Description", urun.Description);
            sqlCommand.Parameters.AddWithValue("@Price", urun.Price);
            sqlCommand.Parameters.AddWithValue("@Stock", urun.Stock);
            islemSonucu = sqlCommand.ExecuteNonQuery(); 
            sqlCommand.Dispose();
            connection.Close();
            return islemSonucu;
        }
        public int Delete(int id) 
        {
            BaglantiyiAc();
            int islemSonucu = 0; 
            SqlCommand sqlCommand = new SqlCommand("delete from Urunler where Id=@UrunId ", connection);
            sqlCommand.Parameters.AddWithValue("@UrunId", id); 
            islemSonucu = sqlCommand.ExecuteNonQuery(); 
            sqlCommand.Dispose();
            connection.Close();
            return islemSonucu;
        }
        public Urun UrunGetir(int id)
        {
            Urun urun = new Urun();
            BaglantiyiAc();
            SqlCommand sqlCommand = new SqlCommand("select * from Urunler where Id=@UrunId ", connection);
            sqlCommand.Parameters.AddWithValue("@UrunId", id);
            SqlDataReader reader = sqlCommand.ExecuteReader();
            if (reader.Read())
            {
                urun.Id = Convert.ToInt32(reader["ID"]);
                urun.Name = Convert.ToString(reader["Name"]);
                urun.Description = Convert.ToString(reader["Description"]);
                urun.Price = Convert.ToDecimal(reader["Price"]);
                urun.Stock = Convert.ToInt32(reader["Stock"]);


            }
            reader.Close();
            sqlCommand.Dispose();
            connection.Close();
            return urun;
        }
    }
}
