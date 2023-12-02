using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StokTakip
{
    internal class OrtakDAL
    {
        public SqlConnection connection = new SqlConnection(@"server=(localdb)\MSSQLLocalDB; database=StokTakip; trusted_connection=true;"); // sql server a bağlanmamızı sağlayacak nesne

        public void BaglantiyiAc()
        {
            if (connection.State == ConnectionState.Closed) // eğer yukardaki bağlantı kapalıysa 
            {
                connection.Open(); // bağlantıyı aç
            }
        }

        public DataTable KayitlariDatatableileGetir(string SqlSorgu)
        {
            DataTable table = new DataTable(); // boş bir datatable oluştur
            BaglantiyiAc();
            SqlCommand sqlCommand = new SqlCommand(SqlSorgu, connection); // sql komutumuzu çalıştırdık
            SqlDataReader reader = sqlCommand.ExecuteReader(); // sql komutundan gelen datayı okuduk
            table.Load(reader); // yukardaki boş tabloya okunan datayı yükledik
            reader.Close();
            sqlCommand.Dispose();
            table.Dispose();
            connection.Close();
            return table;
        }
    }
}
