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
    public partial class FrmMusteriler : Form
    {
        public FrmMusteriler()
        {
            InitializeComponent();
        }

        sqlbaglantisi bgl = new sqlbaglantisi();
        void listele()
        {
            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter("SELECT * FROM TBL_MUSTERILER", bgl.baglanti());
            da.Fill(dt);
            gridControl1.DataSource = dt;
        }
        void sehirListesi()
        {
            SqlCommand komut = new SqlCommand("SELECT SEHIR FROM TBL_ILLER", bgl.baglanti());
            SqlDataReader dr = komut.ExecuteReader();
            while (dr.Read())
            {
                CmbIl.Properties.Items.Add(dr[0]);
            }
            bgl.baglanti().Close();
        }
        void temizle()
        {

            Txtid.Text = "";
            TxtAd.Text = "";
            TxtSoyad.Text = "";
            MskTelefon1.Text = "";
            MskTelefon2.Text = "";
            MskTC.Text = "";
            TxtMail.Text = "";
            CmbIl.Text = "";
            CmbIlce.Text = "";
            TxtVergi.Text = "";
            RchAdres.Text = "";

        }
        private void FrmMusteriler_Load(object sender, EventArgs e)
        {
            listele();
            sehirListesi();
            temizle();
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

        private void BtnKaydet_Click(object sender, EventArgs e)
        {
            SqlCommand komut = new SqlCommand("INSERT INTO TBL_MUSTERILER (AD, SOYAD, TELEFON, TELEFON2, TC, MAIL, IL,ILCE, ADRES, VERGIDAIRE) VALUES (@p1, @p2, @p3, @p4, @p5, @p6, @p7, @p8, @p9, @p10)", bgl.baglanti());

            komut.Parameters.AddWithValue("@p1", TxtAd.Text);
            komut.Parameters.AddWithValue("@p2", TxtSoyad.Text);
            komut.Parameters.AddWithValue("@p3", MskTelefon1.Text);
            komut.Parameters.AddWithValue("@p4", MskTelefon2.Text);
            komut.Parameters.AddWithValue("@p5", MskTC.Text);
            komut.Parameters.AddWithValue("@p6", TxtMail.Text);
            komut.Parameters.AddWithValue("@p7", CmbIl.Text);
            komut.Parameters.AddWithValue("@p8", CmbIlce.Text);
            komut.Parameters.AddWithValue("@p9", RchAdres.Text);
            komut.Parameters.AddWithValue("@p10", TxtVergi.Text);
            komut.ExecuteNonQuery();
            bgl.baglanti().Close();
            listele();
            MessageBox.Show("Müşteri Sisteme başarılı bir şekilde eklendi", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);
            temizle();


        }

        private void BtnSil_Click(object sender, EventArgs e)
        {

            DialogResult secenek = MessageBox.Show("Kullanıcı Silinecek Emin misiniz?", "Uyarı", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Error);

            if (secenek == DialogResult.Yes)
            {
                SqlCommand komuSil = new SqlCommand("DELETE FROM TBL_MUSTERILER WHERE ID=@P1", bgl.baglanti());
                komuSil.Parameters.AddWithValue("@p1", Txtid.Text);
                komuSil.ExecuteNonQuery();
                bgl.baglanti().Close();
                listele();
                MessageBox.Show("Ürün Silme işlemi başarılı bir şekilde gerçekleşti!", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);
                temizle();
            }


        }

        private void BtnGüncelle_Click(object sender, EventArgs e)
        {
            SqlCommand komutGuncelle = new SqlCommand("UPDATE TBL_MUSTERILER SET AD=@P1, SOYAD=@P2, TELEFON=@P3, TELEFON2=@P4, TC=@P5, MAIL=@P6, IL=@P7, ILCE=@P8, ADRES=@P9, VERGIDAIRE=@P10 WHERE ID=@P11", bgl.baglanti());
            komutGuncelle.Parameters.AddWithValue("@P1", TxtAd.Text);
            komutGuncelle.Parameters.AddWithValue("@P2", TxtSoyad.Text);
            komutGuncelle.Parameters.AddWithValue("@P3", MskTelefon1.Text);
            komutGuncelle.Parameters.AddWithValue("@P4", MskTelefon2.Text);
            komutGuncelle.Parameters.AddWithValue("@P5", MskTC.Text);
            komutGuncelle.Parameters.AddWithValue("@P6", TxtMail.Text);
            komutGuncelle.Parameters.AddWithValue("@P7", CmbIl.Text);
            komutGuncelle.Parameters.AddWithValue("@P8", CmbIlce.Text);
            komutGuncelle.Parameters.AddWithValue("@P9", RchAdres.Text);
            komutGuncelle.Parameters.AddWithValue("@P10", TxtVergi.Text);
            komutGuncelle.Parameters.Add("@P11", Txtid.Text);
            komutGuncelle.ExecuteNonQuery();
            bgl.baglanti().Close();
            listele();
            MessageBox.Show("Müşteri Güncelleme başarılı bir şekilde gerçekleşmiştir", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);
            temizle();


        }

        private void gridView1_FocusedRowChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs e)
        {
            DataRow dr = gridView1.GetDataRow(gridView1.FocusedRowHandle);
            if (dr != null)
            {
                Txtid.Text = dr["ID"].ToString();
                TxtAd.Text = dr["AD"].ToString();
                TxtSoyad.Text = dr["SOYAD"].ToString();
                MskTelefon1.Text = dr["TELEFON"].ToString();
                MskTelefon2.Text = dr["TELEFON2"].ToString();
                MskTC.Text = dr["TC"].ToString();
                TxtMail.Text = dr["MAIL"].ToString();
                CmbIl.Text = dr["IL"].ToString();
                CmbIlce.Text = dr["ILCE"].ToString();
                TxtVergi.Text = dr["VERGIDAIRE"].ToString();
                RchAdres.Text = dr["ADRES"].ToString();
            }
        }

        private void BtnTemizle_Click(object sender, EventArgs e)
        {
            temizle();
        }
    }
}
