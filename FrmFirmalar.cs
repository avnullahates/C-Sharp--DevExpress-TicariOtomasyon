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
    public partial class FrmFirmalar : Form
    {
        public FrmFirmalar()
        {
            InitializeComponent();
        }
        sqlbaglantisi bgl = new sqlbaglantisi();
        void firmaListesi()
        {
            SqlDataAdapter da = new SqlDataAdapter("SELECT * FROM TBL_FIRMALAR", bgl.baglanti());
            DataTable dt = new DataTable();
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

            TxtId.Text = "";
            TxtAd.Text = "";
            TxtYetkiliGorev.Text = "";
            TxtYetkili.Text = "";
            MskYetkiliTC.Text = "";
            TxtSektor.Text = "";
            MskTelefon1.Text = "";
            MskTelefon2.Text = "";
            MskTelefon3.Text = "";
            TxtMail.Text = "";
            MskTFax.Text = "";
            CmbIl.Text = "";
            CmbIlce.Text = "";
            TxtVergi.Text = "";
            RchAdres.Text = "";
            TxtKod1.Text = "";
            TxtKod2.Text = "";
            TxtKod3.Text = "";

        }
        void cariKodAciklamalar()
        {
            SqlCommand komut = new SqlCommand("SELECT FIRMAKOD1 FROM TBL_KODLAR", bgl.baglanti());
            SqlDataReader dr = komut.ExecuteReader();
            while (dr.Read())
            {
                RchKod1.Text = dr[0].ToString();
            }
            bgl.baglanti().Close();
        }
        private void FrmFirmalar_Load(object sender, EventArgs e)
        {
            firmaListesi();
            sehirListesi();
            temizle();
            cariKodAciklamalar();
        }

        private void gridView1_FocusedRowChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs e)
        {
            DataRow dr = gridView1.GetDataRow(gridView1.FocusedRowHandle);
            if (dr != null)
            {
                TxtId.Text = dr["ID"].ToString();
                TxtAd.Text = dr["AD"].ToString();
                TxtYetkiliGorev.Text = dr["YETKILISTATU"].ToString();
                TxtYetkili.Text = dr["YETKILIADSOYAD"].ToString();
                MskYetkiliTC.Text = dr["YETKILITC"].ToString();
                TxtSektor.Text = dr["SEKTOR"].ToString();
                MskTelefon1.Text = dr["TELEFON1"].ToString();
                MskTelefon2.Text = dr["TELEFON2"].ToString();
                MskTelefon3.Text = dr["TELEFON3"].ToString();
                TxtMail.Text = dr["MAIL"].ToString();
                MskTFax.Text = dr["FAX"].ToString();
                CmbIl.Text = dr["IL"].ToString();
                CmbIlce.Text = dr["ILCE"].ToString();
                TxtVergi.Text = dr["VERGIDAIRE"].ToString();
                RchAdres.Text = dr["ADRES"].ToString();
                TxtKod1.Text = dr["OZELKOD1"].ToString();
                TxtKod2.Text = dr["OZELKOD2"].ToString();
                TxtKod3.Text = dr["OZELKOD3"].ToString();
                TxtAd.Focus();
            }
        }

        private void BtnKaydet_Click(object sender, EventArgs e)
        {
            SqlCommand komut = new SqlCommand("INSERT INTO TBL_FIRMALAR (AD, YETKILISTATU, YETKILIADSOYAD, YETKILITC, SEKTOR, TELEFON1, TELEFON2, TELEFON3, MAIL, FAX, IL, ILCE, VERGIDAIRE, ADRES, OZELKOD1, OZELKOD2, OZELKOD3) VALUES (@P1, @P2, @P3, @P4, @P5, @P6, @P7, @P8, @P9, @P10, @P11, @P12, @P13, @P14, @P15, @P16, @P17)", bgl.baglanti());
            komut.Parameters.AddWithValue("@P1", TxtAd.Text);
            komut.Parameters.AddWithValue("@P2", TxtYetkiliGorev.Text);
            komut.Parameters.AddWithValue("@P3", TxtYetkili.Text);
            komut.Parameters.AddWithValue("@P4", MskYetkiliTC.Text);
            komut.Parameters.AddWithValue("@P5", TxtSektor.Text);
            komut.Parameters.AddWithValue("@P6", MskTelefon1.Text);
            komut.Parameters.AddWithValue("@P7", MskTelefon2.Text);
            komut.Parameters.AddWithValue("@P8", MskTelefon3.Text);
            komut.Parameters.AddWithValue("@P9", TxtMail.Text);
            komut.Parameters.AddWithValue("@P10", MskTFax.Text);
            komut.Parameters.AddWithValue("@P11", CmbIl.Text);
            komut.Parameters.AddWithValue("@P12", CmbIlce.Text);
            komut.Parameters.AddWithValue("@P13", TxtVergi.Text);
            komut.Parameters.AddWithValue("@P14", RchAdres.Text);
            komut.Parameters.AddWithValue("@P15", TxtKod1.Text);
            komut.Parameters.AddWithValue("@P16", TxtKod2.Text);
            komut.Parameters.AddWithValue("@P17", TxtKod3.Text);
            komut.ExecuteNonQuery();
            bgl.baglanti().Close();
            firmaListesi();
            MessageBox.Show("Firma Sisteme başarılı bir şekilde eklendi", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);

            temizle();
        }

        private void BtnSil_Click(object sender, EventArgs e)
        {
            DialogResult secenek = MessageBox.Show("Kullanıcı Silinecek Emin misiniz?", "Uyarı", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Error);

            if (secenek == DialogResult.Yes)
            {
                SqlCommand komutSil = new SqlCommand("DELETE FROM TBL_FIRMALAR WHERE ID=@P1", bgl.baglanti());
                komutSil.Parameters.AddWithValue("@P1", TxtId.Text);
                komutSil.ExecuteNonQuery();
                bgl.baglanti().Close();
                firmaListesi();
                MessageBox.Show("Firma Silme işlemi başarılı bir şekilde gerçekleşti!", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);
                temizle();
            }
        }

        private void BtnTemizle_Click(object sender, EventArgs e)
        {
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

        private void BtnGüncelle_Click(object sender, EventArgs e)
        {
            SqlCommand komutGuncelle = new SqlCommand("UPDATE TBL_FIRMALAR SET AD=@P1, YETKILISTATU=@P2, YETKILIADSOYAD=@P3, YETKILITC=@P4, SEKTOR=@P5, TELEFON1=@P6, TELEFON2=@P7, TELEFON3=@P8, MAIL=@P9, FAX=@P10, IL=@P11, ILCE=@P12, VERGIDAIRE=@P13, ADRES=@P14, OZELKOD1=@P15, OZELKOD2=@P16, OZELKOD3=@P17 WHERE ID=@P18", bgl.baglanti());

            komutGuncelle.Parameters.AddWithValue("@P1", TxtAd.Text);
            komutGuncelle.Parameters.AddWithValue("@P2", TxtYetkiliGorev.Text);
            komutGuncelle.Parameters.AddWithValue("@P3", TxtYetkili.Text);
            komutGuncelle.Parameters.AddWithValue("@P4", MskYetkiliTC.Text);
            komutGuncelle.Parameters.AddWithValue("@P5", TxtSektor.Text);
            komutGuncelle.Parameters.AddWithValue("@P6", MskTelefon1.Text);
            komutGuncelle.Parameters.AddWithValue("@P7", MskTelefon2.Text);
            komutGuncelle.Parameters.AddWithValue("@P8", MskTelefon3.Text);
            komutGuncelle.Parameters.AddWithValue("@P9", TxtMail.Text);
            komutGuncelle.Parameters.AddWithValue("@P10", MskTFax.Text);
            komutGuncelle.Parameters.AddWithValue("@P11", CmbIl.Text);
            komutGuncelle.Parameters.AddWithValue("@P12", CmbIlce.Text);
            komutGuncelle.Parameters.AddWithValue("@P13", TxtVergi.Text);
            komutGuncelle.Parameters.AddWithValue("@P14", RchAdres.Text);
            komutGuncelle.Parameters.AddWithValue("@P15", TxtKod1.Text);
            komutGuncelle.Parameters.AddWithValue("@P16", TxtKod2.Text);
            komutGuncelle.Parameters.AddWithValue("@P17", TxtKod3.Text);
            komutGuncelle.Parameters.AddWithValue("@P18", TxtId.Text);
            komutGuncelle.ExecuteNonQuery();
            bgl.baglanti().Close();
            firmaListesi();
            MessageBox.Show("Firma Güncelleme başarılı bir şekilde gerçekleşmiştir", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);          
            temizle();

        }
    }
}
