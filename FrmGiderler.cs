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
    public partial class FrmGiderler : Form
    {
        public FrmGiderler()
        {
            InitializeComponent();
        }
        sqlbaglantisi bgl = new sqlbaglantisi();

        void giderListesi()
        {
            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter("SELECT * FROM TBL_GIDERLER", bgl.baglanti());
            da.Fill(dt);
            gridControl1.DataSource = dt;
        }

        private void FrmGiderler_Load(object sender, EventArgs e)
        {
            Temizle();
            giderListesi();
        }

        void Temizle()
        {
            CmbAy.Text = "";
            CmbYil.Text = "";
            TxtElektrik.Text = "";
            TxtSu.Text = "";
            TxtDogalgaz.Text = "";
            TxtInternet.Text = "";
            TxtMaaslar.Text = "";
            TxtEkstra.Text = "";
            RchNotlar.Text = "";

        }
        private void BtnKaydet_Click(object sender, EventArgs e)
        {
            SqlCommand komut = new SqlCommand("INSERT INTO TBL_GIDERLER (AY, YIL, ELEKTRIK, SU, DOGALGAZ, INTERNET, MAASLAR, EKSTRA, NOTLAR) values (@P1, @P2, @P3, @P4, @P5, @P6, @P7, @P8, @P9)", bgl.baglanti());
            komut.Parameters.AddWithValue("@P1", CmbAy.Text);
            komut.Parameters.AddWithValue("@P2", CmbYil.Text);
            komut.Parameters.AddWithValue("@P3", decimal.Parse(TxtElektrik.Text));
            komut.Parameters.AddWithValue("@P4", decimal.Parse(TxtSu.Text));
            komut.Parameters.AddWithValue("@P5", decimal.Parse(TxtDogalgaz.Text));
            komut.Parameters.AddWithValue("@P6", decimal.Parse(TxtInternet.Text));
            komut.Parameters.AddWithValue("@P7", decimal.Parse(TxtMaaslar.Text));
            komut.Parameters.AddWithValue("@P8", decimal.Parse(TxtEkstra.Text));
            komut.Parameters.AddWithValue("@P9", RchNotlar.Text);
            komut.ExecuteNonQuery();
            bgl.baglanti().Close();
            giderListesi();
            MessageBox.Show("Gider Sisteme başarılı bir şekilde eklendi", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);
            Temizle();
        }

        private void BtnSil_Click(object sender, EventArgs e)
        {
            DialogResult secenek = MessageBox.Show("Kullanıcı Silinecek Emin misiniz?", "Uyarı", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Error);

            if (secenek == DialogResult.Yes)
            {
                SqlCommand komutSil = new SqlCommand("DELETE FROM TBL_GIDERLER WHERE ID=@P1", bgl.baglanti());
                komutSil.Parameters.AddWithValue("@P1", Txtid.Text);
                komutSil.ExecuteNonQuery();
                bgl.baglanti().Close();
                giderListesi();
                MessageBox.Show("Gider Silme işlemi başarılı bir şekilde gerçekleşti!", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);
                Temizle();
            }
        }
        private void BtnGüncelle_Click(object sender, EventArgs e)
        {
            SqlCommand komutGuncelle = new SqlCommand("UPDATE TBL_GIDERLER SET AY=@P1, YIL=@P2, ELEKTRIK=@P3, SU=@P4, DOGALGAZ=@P5, INTERNET=@P6, MAASLAR=@P7, EKSTRA=@P8, NOTLAR=@P9 WHERE ID=@P10", bgl.baglanti());
            komutGuncelle.Parameters.AddWithValue("@P1", CmbAy.Text);
            komutGuncelle.Parameters.AddWithValue("@P2", CmbYil.Text);
            komutGuncelle.Parameters.AddWithValue("@P3", decimal.Parse(TxtElektrik.Text));
            komutGuncelle.Parameters.AddWithValue("@P4", decimal.Parse(TxtSu.Text));
            komutGuncelle.Parameters.AddWithValue("@P5", decimal.Parse(TxtDogalgaz.Text));
            komutGuncelle.Parameters.AddWithValue("@P6", decimal.Parse(TxtInternet.Text));
            komutGuncelle.Parameters.AddWithValue("@P7", decimal.Parse(TxtMaaslar.Text));
            komutGuncelle.Parameters.AddWithValue("@P8", decimal.Parse(TxtEkstra.Text));
            komutGuncelle.Parameters.AddWithValue("@P9", RchNotlar.Text);
            komutGuncelle.Parameters.AddWithValue("@P10", Txtid.Text);
            komutGuncelle.ExecuteNonQuery();
            bgl.baglanti().Close();
            giderListesi();
            MessageBox.Show("Gider Guncelleme işlemi başarılı bir şekilde gerçekleşti!", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);
            Temizle();
        }


        private void gridView1_FocusedRowChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs e)
        {
            DataRow dr = gridView1.GetDataRow(gridView1.FocusedRowHandle);
            if (dr != null)
            {
                Txtid.Text = dr["ID"].ToString();
                CmbAy.Text = dr["AY"].ToString();
                CmbYil.Text = dr["YIL"].ToString();
                TxtElektrik.Text = dr["ELEKTRIK"].ToString();
                TxtSu.Text = dr["SU"].ToString();
                TxtDogalgaz.Text = dr["DOGALGAZ"].ToString();
                TxtInternet.Text = dr["INTERNET"].ToString();
                TxtMaaslar.Text = dr["MAASLAR"].ToString();
                TxtEkstra.Text = dr["EKSTRA"].ToString();
                RchNotlar.Text = dr["NOTLAR"].ToString();
                TxtElektrik.Focus();
            }
        }


        private void BtnTemizle_Click(object sender, EventArgs e)
        {
            Temizle();
        }


    }
}
