using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace StokTakip
{
    public partial class KategoriYonetimi : Form
    {
        public KategoriYonetimi()
        {
            InitializeComponent();
        }
        KategoriDAL kategoriDAL = new KategoriDAL();
        void Temizle()
        {
            txtKategoriAciklamasi.Text = string.Empty;
            txtKategoriAdi.Text = string.Empty;

            btnEkle.Enabled = true;
            btnGuncelle.Enabled = false;
            btnSil.Enabled = false;
        }

        private void KategoriYonetimi_Load(object sender, EventArgs e)
        {
            dgvKategoriler.DataSource = kategoriDAL.KayitlariDatatableileGetir();
            
            

        }

        private void btnEkle_Click(object sender, EventArgs e)
        {
            try
            {
                var sonuc = kategoriDAL.Add(new Kategori()
                {
                    Description = txtKategoriAciklamasi.Text,
                    IsActive = cbDurum.Checked,
                    Name = txtKategoriAdi.Text
                });
                if (sonuc > 0)
                {
                    Temizle();
                    dgvKategoriler.DataSource = kategoriDAL.KayitlariDatatableileGetir();
                    MessageBox.Show("Kayıt Başarılı!");
                }
                else
                    MessageBox.Show("Kayıt Başarısız!");
            }
            catch (Exception)
            {
                MessageBox.Show("Hata Oluştu!");
            }
        }

        private void dgvKategoriler_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                int id = Convert.ToInt32(dgvKategoriler.CurrentRow.Cells[0].Value);

                Kategori kayit = kategoriDAL.Get(id);

                txtKategoriAciklamasi.Text = kayit.Description;
                txtKategoriAdi.Text = kayit.Name;
                cbDurum.Checked = kayit.IsActive;

                btnEkle.Enabled = false;
                btnGuncelle.Enabled = true;
                btnSil.Enabled = true;
                btnGeri.Enabled = true;
            }
            catch (Exception)
            {
                MessageBox.Show("Hata Oluştu!");
            }
        }

        private void btnGuncelle_Click(object sender, EventArgs e)
        {
            try
            {
                var sonuc = kategoriDAL.Update(new Kategori()
                {
                    Id = Convert.ToInt32(dgvKategoriler.CurrentRow.Cells[0].Value),
                    Description = txtKategoriAciklamasi.Text,
                    IsActive = cbDurum.Checked,
                    Name = txtKategoriAdi.Text
                });
                if (sonuc > 0)
                {
                    Temizle();
                    dgvKategoriler.DataSource = kategoriDAL.KayitlariDatatableileGetir();
                    MessageBox.Show("Kayıt Başarılı!");
                }
                else
                    MessageBox.Show("Kayıt Başarısız!");
            }
            catch (Exception)
            {
                MessageBox.Show("Hata Oluştu!");
            }
        }

        private void btnSil_Click(object sender, EventArgs e)
        {
            try
            {
                int id = (int)dgvKategoriler.CurrentRow.Cells[0].Value;
                var sonuc = kategoriDAL.Delete(id);
                if (sonuc > 0)
                {
                    Temizle();
                    dgvKategoriler.DataSource = kategoriDAL.KayitlariDatatableileGetir();
                    MessageBox.Show("Kayıt Silindi!");
                }
                else
                    MessageBox.Show("Kayıt Silinemedi!");
            }
            catch (Exception)
            {
                MessageBox.Show("Hata Oluştu!");
            }
        }

        private void btnGeri_Click(object sender, EventArgs e)
        {
            Temizle();
            btnGeri.Enabled = false;
        }

      
    }
}
