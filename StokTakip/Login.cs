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
    public partial class Login : Form
    {
        public Login()
        {
            InitializeComponent();
        }
        KullaniciDAL dAL = new KullaniciDAL();

        private void btnGiris_Click(object sender, EventArgs e)
        {
            var sonuc = dAL.KullaniciGiris(txtKullaniciAdi.Text, txtSifre.Text);
            if (sonuc) // eğer kullanıcı girişi başarılıysa
            {
                 //MessageBox.Show("Giriş Başarılı!");
                 //this.Hide();
                groupBox1.Visible = false; // formu gizle
                menuStrip1.Visible = true; // üst menüyü göster
            }
            else
                MessageBox.Show("Giriş Başarısız!");
        }

        private void kategoriYönetimiToolStripMenuItem_Click(object sender, EventArgs e)
        {

            KategoriYonetimi kategoriYonetimi = new KategoriYonetimi();
            kategoriYonetimi.ShowDialog();
        }

        private void ürünYönetimiToolStripMenuItem_Click(object sender, EventArgs e)
        {
            UrunYonetimi urunYonetimi = new UrunYonetimi();
            urunYonetimi.ShowDialog();
        }

        private void kullanıcıYönetimiToolStripMenuItem_Click(object sender, EventArgs e)
        {
            KullaniciYonetimi kullaniciYonetimi = new KullaniciYonetimi();
            kullaniciYonetimi.ShowDialog();
        }
    }
}
