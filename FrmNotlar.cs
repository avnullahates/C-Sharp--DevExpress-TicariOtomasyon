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
    public partial class FrmNotlar : Form
    {
        public FrmNotlar()
        {
            InitializeComponent();
        }
        sqlbaglantisi bgl = new sqlbaglantisi();


        void notlarListele()
        {
            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter("SELECT * FROM TBL_NOTLAR", bgl.baglanti());
            da.Fill(dt);
            gridControl1.DataSource = dt;
        }
        private void FrmNotlar_Load(object sender, EventArgs e)
        {
            notlarListele();
            temizle();
        }

        void temizle()
        {
            Txtid.Text = "";
            MskTarih.Text = "";
            MskSaat.Text = "";
            TxtBaslik.Text = "";
            TxtOlusturan.Text = "";
            TxtHitap.Text = "";
            RchDetay.Text = "";
        }
        private void BtnTemizle_Click(object sender, EventArgs e)
        {
            temizle();
        }
        private void BtnKaydet_Click(object sender, EventArgs e)
        {
            SqlCommand komut = new SqlCommand("INSERT INTO TBL_NOTLAR (TARIH, SAAT, BASLIK, OLUSTURAN, HITAP, DETAY) VALUES (@p1, @p2, @p3, @p4, @p5, @p6)", bgl.baglanti());
            komut.Parameters.AddWithValue("@P1", MskTarih.Text);
            komut.Parameters.AddWithValue("@P2", MskSaat.Text);
            komut.Parameters.AddWithValue("@P3", TxtBaslik.Text);
            komut.Parameters.AddWithValue("@P4", TxtOlusturan.Text);
            komut.Parameters.AddWithValue("@P5", TxtHitap.Text);
            komut.Parameters.AddWithValue("@P6", RchDetay.Text);
            komut.ExecuteNonQuery();
            bgl.baglanti().Close();
            notlarListele();
            MessageBox.Show("Not Sisteme başarılı bir şekilde eklendi", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);
            temizle();
        }

        private void BtnSil_Click(object sender, EventArgs e)
        {
            DialogResult secenek = MessageBox.Show("Kullanıcı Silinecek Emin misiniz?", "Uyarı", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Error);

            if (secenek == DialogResult.Yes)
            {
                SqlCommand komutSil = new SqlCommand("DELETE FROM TBL_NOTLAR WHERE ID=@P1", bgl.baglanti());
                komutSil.Parameters.AddWithValue("@P1", Txtid.Text);
                komutSil.ExecuteNonQuery();
                bgl.baglanti().Close();
                notlarListele();
                MessageBox.Show("Not Silme işlemi başarılı bir şekilde gerçekleşti!", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);
                temizle();
            }
        }

        private void gridView1_FocusedRowChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs e)
        {
            DataRow dr = gridView1.GetDataRow(gridView1.FocusedRowHandle);
            if (dr != null)
            {
                Txtid.Text = dr["ID"].ToString();
                MskTarih.Text = dr["TARIH"].ToString();
                MskSaat.Text = dr["SAAT"].ToString();
                TxtBaslik.Text = dr["BASLIK"].ToString();
                TxtOlusturan.Text = dr["OLUSTURAN"].ToString();
                TxtHitap.Text = dr["HITAP"].ToString();
                RchDetay.Text = dr["DETAY"].ToString();

            }
        }

        private void BtnGüncelle_Click(object sender, EventArgs e)
        {
            SqlCommand komut = new SqlCommand("UPDATE TBL_NOTLAR SET TARIH=@P1, SAAT=@P2, BASLIK=@P3, OLUSTURAN=@P4, HITAP=@P5, DETAY=@P6 WHERE ID=@P7", bgl.baglanti());
            komut.Parameters.AddWithValue("@P1", MskTarih.Text);
            komut.Parameters.AddWithValue("@P2", MskSaat.Text);
            komut.Parameters.AddWithValue("@P3", TxtBaslik.Text);
            komut.Parameters.AddWithValue("@P4", TxtOlusturan.Text);
            komut.Parameters.AddWithValue("@P5", TxtHitap.Text);
            komut.Parameters.AddWithValue("@P6", RchDetay.Text);
            komut.Parameters.AddWithValue("@P7", Txtid.Text);
            komut.ExecuteNonQuery();
            bgl.baglanti().Close();
            notlarListele();
            MessageBox.Show("Not Güncelleme başarılı bir şekilde gerçekleşmiştir", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);
            temizle();
        }

        private void gridView1_DoubleClick(object sender, EventArgs e)
        {
            FrmNotDetay fr = new FrmNotDetay();
            DataRow dr = gridView1.GetDataRow(gridView1.FocusedRowHandle);

            if (dr!=null)
            {
                fr.metin = dr["DETAY"].ToString();
            }
            fr.Show();
        }
    }
}
