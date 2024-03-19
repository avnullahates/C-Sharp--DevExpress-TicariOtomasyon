using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;

namespace Ticari_Otamasyon
{
    public partial class FrmPersonel : Form
    {
        public FrmPersonel()
        {
            InitializeComponent();
        }
        sqlbaglantisi bgl = new sqlbaglantisi();

        void personelListe()
        {
            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter("SELECT * FROM TBL_PERSONELLER", bgl.baglanti());
            da.Fill(dt);
            gridControl1.DataSource = dt;
        }
        void Temizle()
        {
            Txtid.Text = "";
            TxtAd.Text = "";
            TxtSoyad.Text = "";
            MskTelefon.Text = "";
            MskTC.Text = "";
            TxtMail.Text = "";
            CmbIl.Text = "";
            CmbIlce.Text = "";
            RchAdres.Text = "";
            TxtGorev.Text = "";
        }

        void sehirListesi()
        {
            SqlCommand komu = new SqlCommand("SELECT SEHIR FROM TBL_ILLER", bgl.baglanti());
            SqlDataReader dr = komu.ExecuteReader();
            while (dr.Read())
            {
                CmbIl.Properties.Items.Add(dr[0]);
            }
            bgl.baglanti().Close();
        }
        private void FrmPersonel_Load(object sender, EventArgs e)
        {
            personelListe();
            sehirListesi();
            Temizle();
        }

        private void BtnKaydet_Click(object sender, EventArgs e)
        {
            SqlCommand komutEkle = new SqlCommand("INSERT INTO TBL_PERSONELLER  (AD, SOYAD, TELEFON, TC, MAIL, IL, ILCE, ADRES, GOREV) VALUES (@P1, @P2, @P3, @P4, @P5, @P6, @P7, @P8, @P9)", bgl.baglanti());

            komutEkle.Parameters.AddWithValue("@P1", TxtAd.Text);
            komutEkle.Parameters.AddWithValue("@P2", TxtSoyad.Text);
            komutEkle.Parameters.AddWithValue("@P3", MskTelefon.Text);
            komutEkle.Parameters.AddWithValue("@P4", MskTC.Text);
            komutEkle.Parameters.AddWithValue("@P5", TxtMail.Text);
            komutEkle.Parameters.AddWithValue("@P6", CmbIl.Text);
            komutEkle.Parameters.AddWithValue("@P7", CmbIlce.Text);
            komutEkle.Parameters.AddWithValue("@P8", RchAdres.Text);
            komutEkle.Parameters.AddWithValue("@P9", TxtGorev.Text);

            komutEkle.ExecuteNonQuery();
            bgl.baglanti().Close();
            personelListe();
            MessageBox.Show("Personel Sisteme başarılı bir şekilde eklendi", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);
            Temizle();
        }

        private void BtnSil_Click(object sender, EventArgs e)
        {
            DialogResult secenek = MessageBox.Show("Kullanıcı Silinecek Emin misiniz?", "Uyarı", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Error);

            if (secenek == DialogResult.Yes)
            {
                SqlCommand komuSil = new SqlCommand("DELETE FROM TBL_PERSONELLER WHERE ID=@P1", bgl.baglanti());
                komuSil.Parameters.AddWithValue("@P1", Txtid.Text);
                komuSil.ExecuteNonQuery();
                bgl.baglanti().Close();
                personelListe();
                MessageBox.Show("Personel Silme işlemi başarılı bir şekilde gerçekleşti!", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);
                Temizle();
            }
        }

        private void BtnGüncelle_Click(object sender, EventArgs e)
        {
            SqlCommand komutGuncelle = new SqlCommand("UPDATE TBL_PERSONELLER SET AD=@P1, SOYAD=@P2, TELEFON=@P3, TC=@P4, MAIL=@P5, IL=@P6, ILCE=@P7, ADRES=@P8, GOREV=@P9 WHERE  ID=@P10", bgl.baglanti());

            komutGuncelle.Parameters.AddWithValue("@P1", TxtAd.Text);
            komutGuncelle.Parameters.AddWithValue("@P2", TxtSoyad.Text);
            komutGuncelle.Parameters.AddWithValue("@P3", MskTelefon.Text);
            komutGuncelle.Parameters.AddWithValue("@P4", MskTC.Text);
            komutGuncelle.Parameters.AddWithValue("@P5", TxtMail.Text);
            komutGuncelle.Parameters.AddWithValue("@P6", CmbIl.Text);
            komutGuncelle.Parameters.AddWithValue("@P7", CmbIlce.Text);
            komutGuncelle.Parameters.AddWithValue("@P8", RchAdres.Text);
            komutGuncelle.Parameters.AddWithValue("@P9", TxtGorev.Text);
            komutGuncelle.Parameters.AddWithValue("@P10", Txtid.Text);

            komutGuncelle.ExecuteNonQuery();
            bgl.baglanti().Close();
            personelListe();

            MessageBox.Show("Personel Güncelleme başarılı bir şekilde gerçekleşmiştir", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);
            Temizle();


        }

        private void gridView1_FocusedRowChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs e)
        {
            DataRow dr = gridView1.GetDataRow(gridView1.FocusedRowHandle);
            if (dr != null)
            {
                Txtid.Text = dr["ID"].ToString();
                TxtAd.Text = dr["AD"].ToString();
                TxtSoyad.Text = dr["SOYAD"].ToString();
                MskTelefon.Text = dr["TELEFON"].ToString();
                MskTC.Text = dr["TC"].ToString();
                TxtMail.Text = dr["MAIL"].ToString();
                CmbIl.Text = dr["IL"].ToString();
                CmbIlce.Text = dr["ILCE"].ToString();
                RchAdres.Text = dr["ADRES"].ToString();
                TxtGorev.Text = dr["GOREV"].ToString();
                TxtAd.Focus();
            }
        }

        private void BtnTemizle_Click(object sender, EventArgs e)
        {
            Temizle();
        }

        private void CmbIl_SelectedIndexChanged(object sender, EventArgs e)
        {
            CmbIlce.Properties.Items.Clear();
            SqlCommand komut = new SqlCommand("SELECT ILCE FROM TBL_ILCELER WHERE SEHIR=@P1 ", bgl.baglanti());
            komut.Parameters.AddWithValue("@P1", CmbIl.SelectedIndex + 1);
            SqlDataReader dr = komut.ExecuteReader();
            while (dr.Read())
            {
                CmbIlce.Properties.Items.Add(dr[0]);
            }
            bgl.baglanti().Close();
        }
    }
}
