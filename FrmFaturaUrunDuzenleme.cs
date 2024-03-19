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
    public partial class FrmFaturaUrunDuzenleme : Form
    {
        public FrmFaturaUrunDuzenleme()
        {
            InitializeComponent();
        }

        public string urunId;
        sqlbaglantisi bgl = new sqlbaglantisi();

        void listeleFatura()
        {
            TxtUrunID.Text = urunId;
            SqlCommand komut = new SqlCommand("SELECT * FROM TBL_FATURADETAY WHERE FATURAURUNID=@P1", bgl.baglanti());
            komut.Parameters.AddWithValue("@P1", urunId);
            SqlDataReader dr = komut.ExecuteReader();


        }
        private void FrmFaturaUrunDuzenleme_Load(object sender, EventArgs e)
        {
            TxtUrunID.Text = urunId;
            SqlCommand komut = new SqlCommand("SELECT * FROM TBL_FATURADETAY WHERE FATURAURUNID=@P1", bgl.baglanti());
            komut.Parameters.AddWithValue("@P1", urunId);
            SqlDataReader dr = komut.ExecuteReader();

            while (dr.Read())
            {

                TxtFiyat.Text = dr[2].ToString();
                TxtAdet.Text = dr[3].ToString();
                TxtUrunAdi.Text = dr[1].ToString();
                bgl.baglanti().Close();
            }
        }

        private void BtnGuncelle_Click(object sender, EventArgs e)
        {
            double miktar, tutar, fiyat;
            fiyat = Convert.ToDouble(TxtFiyat.Text);
            miktar = Convert.ToDouble(TxtAdet.Text);
            tutar = miktar * fiyat;

            TxtTutar.Text = tutar.ToString();
            SqlCommand komut = new SqlCommand("UPDATE TBL_FATURADETAY SET URUNAD=@P1, MIKTAR=@P2, FIYAT=@P3, TUTAR=@P4 WHERE FATURAURUNID=@P5", bgl.baglanti());
            komut.Parameters.AddWithValue("@P1", TxtUrunAdi.Text);
            komut.Parameters.AddWithValue("@P2", TxtAdet.Text);
            komut.Parameters.AddWithValue("@P3", TxtFiyat.Text);
            komut.Parameters.AddWithValue("@P4", TxtTutar.Text);
            komut.Parameters.AddWithValue("@P5", TxtUrunID.Text);
            komut.ExecuteNonQuery();
            bgl.baglanti().Close();

            MessageBox.Show("Fatura Detay Güncelleme başarılı bir şekilde gerçekleşmiştir", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void BtnSil_Click(object sender, EventArgs e)
        {
            DialogResult secenek = MessageBox.Show("Kullanıcı Silinecek Emin misiniz?", "Uyarı", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Error);

            if (secenek == DialogResult.Yes)
            {
                SqlCommand komutSil = new SqlCommand("DELETE FROM TBL_FATURADETAY WHERE FATURAURUNID=@P1", bgl.baglanti());
                komutSil.Parameters.AddWithValue("@P1", TxtUrunID.Text);
                komutSil.ExecuteNonQuery();
                bgl.baglanti().Close();

                MessageBox.Show("Fatura Detay Silme işlemi başarılı bir şekilde gerçekleşti!", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }
    }
}
