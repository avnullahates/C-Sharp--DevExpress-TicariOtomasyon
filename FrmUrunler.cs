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
    public partial class FrmUrunler : Form
    {
        public FrmUrunler()
        {
            InitializeComponent();
        }
        sqlbaglantisi bgl = new sqlbaglantisi();

        void listele()
        {
            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter("SELECT * FROM TBL_URUNLER", bgl.baglanti());
            da.Fill(dt);
            gridControl1.DataSource = dt;
        }
        void Temizle()
        {
            Txtid.Text = "";
            TxtAd.Text = "";
            TxtMarka.Text = "";
            TxtModel.Text = "";
            MskYil.Text = "";
            NudAdet.Text = "";
            TxtAlis.Text = "";
            TxtSatis.Text = "";
            RchDetay.Text = "";

        }

        private void FrmUrunler_Load(object sender, EventArgs e)
        {
            listele();
            Temizle();
        }

        private void BtnKaydet_Click(object sender, EventArgs e)
        {
            //Verileri kaydetme
            SqlCommand komut = new SqlCommand("INSERT INTO TBL_URUNLER (URUNAD, MARKA, MODEL, YIL, ADET, ALISFIYAT, SATISFIYAT, DETAY) VALUES (@p1, @p2, @p3, @p4, @p5, @p6, @p7, @p8)", bgl.baglanti());
            komut.Parameters.AddWithValue("@p1", TxtAd.Text);
            komut.Parameters.AddWithValue("@p2", TxtMarka.Text);
            komut.Parameters.AddWithValue("@p3", TxtModel.Text);
            komut.Parameters.AddWithValue("@p4", MskYil.Text);
            komut.Parameters.AddWithValue("@p5", int.Parse((NudAdet.Text).ToString()));
            komut.Parameters.AddWithValue("@p6", decimal.Parse(TxtAlis.Text));
            komut.Parameters.AddWithValue("@p7", decimal.Parse(TxtSatis.Text));
            komut.Parameters.AddWithValue("@p8", RchDetay.Text);
            komut.ExecuteNonQuery();
            bgl.baglanti().Close();
            listele();
            MessageBox.Show("Ürün Sisteme başarılı bir şekilde eklendi", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);
            Temizle();

        }

        private void BtnSil_Click(object sender, EventArgs e)
        {

            DialogResult secenek = MessageBox.Show("Ürün Silinecek Emin misiniz?", "Uyarı", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Error);

            if (secenek == DialogResult.Yes)
            {
                SqlCommand komutSil = new SqlCommand("DELETE FROM TBL_URUNLER WHERE ID=@p1", bgl.baglanti());
                komutSil.Parameters.AddWithValue("@p1", Txtid.Text);
                komutSil.ExecuteNonQuery();
                bgl.baglanti().Close();
                listele();
                MessageBox.Show("Ürün Silme işlemi başarılı bir şekilde gerçekleşti!", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);
                Temizle();

            }
        }

        private void gridView1_FocusedRowChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs e)
        {
            DataRow dr = gridView1.GetDataRow(gridView1.FocusedRowHandle);
            Txtid.Text = dr["ID"].ToString();
            TxtAd.Text = dr["URUNAD"].ToString();
            TxtMarka.Text = dr["MARKA"].ToString();
            TxtModel.Text = dr["MODEL"].ToString();
            MskYil.Text = dr["YIL"].ToString();
            NudAdet.Text = dr["ADET"].ToString();
            TxtAlis.Text = dr["ALISFIYAT"].ToString();
            TxtSatis.Text = dr["SATISFIYAT"].ToString();
            RchDetay.Text = dr["DETAY"].ToString();
        }

        private void BtnGüncelle_Click(object sender, EventArgs e)
        {
            SqlCommand komut = new SqlCommand("UPDATE TBL_URUNLER SET URUNAD=@P1, MARKA=@P2, MODEL=@P3, YIL=@P4, ADET=@P5, ALISFIYAT=@P6, SATISFIYAT=@P7, DETAY=@P8 WHERE ID=@P9", bgl.baglanti());
            komut.Parameters.AddWithValue("@P1", TxtAd.Text);
            komut.Parameters.AddWithValue("@P2", TxtMarka.Text);
            komut.Parameters.AddWithValue("@P3", TxtModel.Text);
            komut.Parameters.AddWithValue("@P4", MskYil.Text);
            komut.Parameters.AddWithValue("@P5", int.Parse((NudAdet.Text).ToString()));
            komut.Parameters.AddWithValue("@P6", decimal.Parse(TxtAlis.Text));
            komut.Parameters.AddWithValue("@P7", decimal.Parse(TxtSatis.Text));
            komut.Parameters.AddWithValue("@P8", RchDetay.Text);
            komut.Parameters.Add("@P9", Txtid.Text);
            komut.ExecuteNonQuery();
            bgl.baglanti().Close();
            listele();
            MessageBox.Show("Ürün Güncelleme başarılı bir şekilde gerçekleşmiştir", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);
            Temizle();
        }

        private void BtnTemizle_Click(object sender, EventArgs e)
        {
            Temizle();
        }
    }
}
