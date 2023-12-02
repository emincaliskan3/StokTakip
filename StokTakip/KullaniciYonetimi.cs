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
    public partial class KullaniciYonetimi : Form
    {
        public KullaniciYonetimi()
        {
            InitializeComponent();
        }
        UserDAL userDAL = new UserDAL();
        void Temizle()
        {
            txtAd.Text = string.Empty;
            txtSoyad.Text = string.Empty;
            txtEmail.Text = string.Empty;
            txtKullaniciAdi.Text = string.Empty;
            txtSifre.Text = string.Empty;
            txtTelefon.Text = string.Empty;
            

            btnEkle.Enabled = true;
            btnGuncelle.Enabled = false;
            btnSil.Enabled = false;
        }

        private void dgvKullanicilar_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                int id = Convert.ToInt32(dgvKullanicilar.CurrentRow.Cells[0].Value);
                User kayit = userDAL.Get(id);
                txtAd.Text = kayit.Name;
                txtSoyad.Text = kayit.Surname;
                txtEmail.Text = kayit.Email;
                txtKullaniciAdi.Text = kayit.Username;
                txtSifre.Text = kayit.Password;
                txtTelefon.Text = kayit.Phone;
                btnEkle.Enabled = false;
                btnGuncelle.Enabled = true;
                btnSil.Enabled = true;
            }
            catch (Exception)
            {

                MessageBox.Show("Hata Oluştu!");
            }

        }

        private void KullaniciYonetimi_Load(object sender, EventArgs e)
        {
            dgvKullanicilar.DataSource = userDAL.KayitlariDatatableileGetir();
        }

        private void btnEkle_Click(object sender, EventArgs e)
        {
            try
            {
                var sonuc = userDAL.Add(new User()
                {
                    Name = txtAd.Text,
                    Surname = txtSoyad.Text,
                    Email = txtEmail.Text,
                    Username = txtKullaniciAdi.Text,
                    Password = txtSifre.Text,
                    Phone = txtTelefon.Text,

                });
                if (sonuc > 0)
                {
                    Temizle();
                    dgvKullanicilar.DataSource =
                    userDAL.KayitlariDatatableileGetir();
                    MessageBox.Show("Kayıt Başarılı!");
                }
                else
                    MessageBox.Show("Hata Oluştu!");

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
                var sonuc = userDAL.Update(new User()
                {
                    Id = Convert.ToInt32(dgvKullanicilar.CurrentRow.Cells[0].Value),
                    Name = txtAd.Text,
                    Surname = txtSoyad.Text,
                    Email = txtEmail.Text,
                    Username = txtKullaniciAdi.Text,
                    Password = txtSifre.Text,
                    Phone = txtTelefon.Text,
                });
                if (sonuc > 0)
                {
                    Temizle();
                    dgvKullanicilar.DataSource =
                        userDAL.KayitlariDatatableileGetir();
                    MessageBox.Show("Kayıt Başarılı!");
                }
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
                int id = (int)dgvKullanicilar.CurrentRow.Cells[0].Value;
                var sonuc = userDAL.Delete(id);
                if (sonuc > 0)
                {
                    Temizle();
                    dgvKullanicilar.DataSource =
                        userDAL.KayitlariDatatableileGetir();
                    MessageBox.Show("Silme Başarılı!");
                }
                else
                    MessageBox.Show("Kayıt Silinemedi!");
            }
            catch (Exception)
            {
                MessageBox.Show("Hata Oluştu!");
            }
        }
    }
}
