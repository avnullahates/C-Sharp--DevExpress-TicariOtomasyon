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
    public partial class FrmFaturalar : Form
    {
        public FrmFaturalar()
        {
            InitializeComponent();
        }

        sqlbaglantisi bgl = new sqlbaglantisi();

        void faturaListele()
        {
            SqlDataAdapter da = new SqlDataAdapter("SELECT * FROM TBL_FATURABILGI", bgl.baglanti());
            DataTable dt = new DataTable();
            da.Fill(dt);
            gridControl1.DataSource = dt;
        }

        private void FrmFaturalar_Load(object sender, EventArgs e)
        {

            faturaListele();
            temizle();
            temizleDetay();
          
        }

        private void temizle()
        {
            TxtId.Text = "";
            TxtSeri.Text = "";
            TxtSiraNo.Text = "";
            MskTarih.Text = "";
            MskSaat.Text = "";
            TxtYVergiDairesi.Text = "";
            TxtAlici.Text = "";
            TxtTeslimEden.Text = "";
            TxtTeslimAlan.Text = "";
        }

        void temizleDetay()
        {
            TxtUrunID.Text = "";
            TxtUrunAdi.Text = "";
            TxtAdet.Text = "";
            TxtFiyat.Text = "";
            TxtTutar.Text = "";
            TxtFatura_ID.Text = "";
            TxtPersonel.Text = "";
            TxtFirma.Text = "";
        }

        private void BtnTemizle_Click(object sender, EventArgs e)
        {
            temizle();
        }

        private void BtnTemizleDetay_Click(object sender, EventArgs e)
        {
            temizleDetay();
        }

        private void BtnKaydetDetay_Click(object sender, EventArgs e)
        {
            double miktar, tutar, fiyat;
            fiyat = Convert.ToDouble(TxtFiyat.Text);
            miktar = Convert.ToInt32(TxtAdet.Text);
            tutar = miktar * fiyat;
            TxtTutar.Text = tutar.ToString();
            SqlCommand komut = new SqlCommand("INSERT INTO TBL_FATURADETAY (URUNAD, MIKTAR, FIYAT, TUTAR, FATURAID) VALUES (@P1, @P2, @P3, @P4, @P5)", bgl.baglanti());
            komut.Parameters.AddWithValue("@P1", TxtUrunAdi.Text);
            komut.Parameters.AddWithValue("@P2", TxtAdet.Text);
            komut.Parameters.AddWithValue("@P3", decimal.Parse(TxtFiyat.Text));
            komut.Parameters.AddWithValue("@P4", decimal.Parse(TxtTutar.Text));
            komut.Parameters.AddWithValue("@P5", TxtFatura_ID.Text);
            komut.ExecuteNonQuery();
            bgl.baglanti().Close();
            faturaListele();


            if (comboBox1.Text == "FİRMA")
            {
                //hareket MUSTERI tablosuna veri girisi
                SqlCommand sqlCommand = new SqlCommand("insert into TBL_FIRMAHAREKETLER (URUNID, ADET, PERSONEL, FIRMA, FIYAT, TOPLAM,FATURAID, TARIH) VALUES  (@H1, @H2, @H3, @H4, @H5, @H6, @H7, @H8)", bgl.baglanti());
                sqlCommand.Parameters.AddWithValue("@H1", TxtUrunID.Text);
                sqlCommand.Parameters.AddWithValue("@H2", TxtAdet.Text);
                sqlCommand.Parameters.AddWithValue("@H3", TxtPersonel.Text);
                sqlCommand.Parameters.AddWithValue("@H4", TxtFirma.Text);
                sqlCommand.Parameters.AddWithValue("@H5", decimal.Parse(TxtFiyat.Text));
                sqlCommand.Parameters.AddWithValue("@H6", decimal.Parse(TxtTutar.Text));
                sqlCommand.Parameters.AddWithValue("@H7", TxtFatura_ID.Text);
                sqlCommand.Parameters.AddWithValue("@H8", MskTarih.Text);
               
                sqlCommand.ExecuteNonQuery();
                bgl.baglanti().Close();
            }


            if (comboBox1.Text == "MÜŞTERİ")
            {
                //hareket FIRMA tablosuna veri girisi
                SqlCommand sqlCommand2 = new SqlCommand("insert into TBL_MUSTERIHAREKETLER (URUNID, ADET, PERSONEL, MUSTERI, FIYAT, TOPLAM,FATURAID, TARIH) VALUES  (@K1, @K2, @K3, @K4, @K5, @K6, @K7, @K8)", bgl.baglanti());
                sqlCommand2.Parameters.AddWithValue("@K1", TxtUrunID.Text);
                sqlCommand2.Parameters.AddWithValue("@K2", TxtAdet.Text);
                sqlCommand2.Parameters.AddWithValue("@K3", TxtPersonel.Text);
                sqlCommand2.Parameters.AddWithValue("@K4", TxtFirma.Text);
                sqlCommand2.Parameters.AddWithValue("@K5", decimal.Parse(TxtFiyat.Text));
                sqlCommand2.Parameters.AddWithValue("@K6", decimal.Parse(TxtTutar.Text));
                sqlCommand2.Parameters.AddWithValue("@K7", TxtFatura_ID.Text);
                sqlCommand2.Parameters.AddWithValue("@K8", MskTarih.Text);
                sqlCommand2.ExecuteNonQuery();
                bgl.baglanti().Close();
            }






            //STOK SAYISINI AZALTMA
            SqlCommand komut4 = new SqlCommand("update TBL_URUNLER set adet = adet-@S1 where ID=@S2", bgl.baglanti());
            komut4.Parameters.AddWithValue("@S1", TxtAdet.Text);
            komut4.Parameters.AddWithValue("@S2", TxtUrunID.Text);
            komut4.ExecuteNonQuery();
            bgl.baglanti().Close();


            faturaListele();
            MessageBox.Show("Fatura Detay Sisteme başarılı bir şekilde eklendi", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);

        }

        private void BtnKaydet_Click(object sender, EventArgs e)
        {


            SqlCommand komut = new SqlCommand("INSERT INTO TBL_FATURABILGI (SERI, SIRANO, TARIH, SAAT, VERGIDAIRESI, ALICI, TESLIMEDEN, TESLIMALAN) VALUES (@P1, @P2, @P3, @P4, @P5, @P6, @P7, @P8)", bgl.baglanti());
            komut.Parameters.AddWithValue("@P1", TxtSeri.Text);
            komut.Parameters.AddWithValue("@P2", TxtSiraNo.Text);
            komut.Parameters.AddWithValue("@P3", MskTarih.Text);
            komut.Parameters.AddWithValue("@P4", MskSaat.Text);
            komut.Parameters.AddWithValue("@P5", TxtYVergiDairesi.Text);
            komut.Parameters.AddWithValue("@P6", TxtAlici.Text);
            komut.Parameters.AddWithValue("@P7", TxtTeslimEden.Text);
            komut.Parameters.AddWithValue("@P8", TxtTeslimAlan.Text);
            komut.ExecuteNonQuery();
            bgl.baglanti().Close();
            faturaListele();
            MessageBox.Show("Fatura Sisteme başarılı bir şekilde eklendi", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);
            temizle();
        }

        private void gridView1_FocusedRowChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs e)
        {
            DataRow dr = gridView1.GetDataRow(gridView1.FocusedRowHandle);
            if (dr != null)
            {
                TxtId.Text = dr["FATURABILGIID"].ToString();
                TxtSeri.Text = dr["SERI"].ToString();
                TxtSiraNo.Text = dr["SIRANO"].ToString();
                MskTarih.Text = dr["TARIH"].ToString();
                MskSaat.Text = dr["SAAT"].ToString();
                TxtYVergiDairesi.Text = dr["VERGIDAIRESI"].ToString();
                TxtAlici.Text = dr["ALICI"].ToString();
                TxtTeslimEden.Text = dr["TESLIMEDEN"].ToString();
                TxtTeslimAlan.Text = dr["TESLIMALAN"].ToString();
            }
        }

        private void BtnSil_Click(object sender, EventArgs e)
        {
            DialogResult secenek = MessageBox.Show("Fatura Silinecek Emin misiniz?", "Uyarı", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Error);

            if (secenek == DialogResult.Yes)
            {
                SqlCommand komut = new SqlCommand("DELETE FROM TBL_FATURABILGI WHERE FATURABILGIID=@P1", bgl.baglanti());
                komut.Parameters.AddWithValue("@P1", TxtId.Text);
                komut.ExecuteNonQuery();
                bgl.baglanti().Close();
                faturaListele();
                MessageBox.Show("Fatura Silme işlemi başarılı bir şekilde gerçekleşti!", "Uyari", MessageBoxButtons.OK, MessageBoxIcon.Information);
                temizle();
            }
        }

        private void BtnGuncelle_Click(object sender, EventArgs e)
        {
            SqlCommand komut = new SqlCommand("UPDATE TBL_FATURABILGI SET SERI=@P1, SIRANO=@P2, TARIH=@P3, SAAT=@P4, VERGIDAIRESI=@P5, ALICI=@P6, TESLIMEDEN=@P7, TESLIMALAN=@P8 WHERE FATURABILGIID=@P9 ", bgl.baglanti());
            komut.Parameters.AddWithValue("@P1", TxtSeri.Text);
            komut.Parameters.AddWithValue("@P2", TxtSiraNo.Text);
            komut.Parameters.AddWithValue("@P3", MskTarih.Text);
            komut.Parameters.AddWithValue("@P4", MskSaat.Text);
            komut.Parameters.AddWithValue("@P5", TxtYVergiDairesi.Text);
            komut.Parameters.AddWithValue("@P6", TxtAlici.Text);
            komut.Parameters.AddWithValue("@P7", TxtTeslimEden.Text);
            komut.Parameters.AddWithValue("@P8", TxtTeslimAlan.Text);
            komut.Parameters.AddWithValue("@P9", TxtId.Text);
            komut.ExecuteNonQuery();
            bgl.baglanti().Close();
            faturaListele();
            MessageBox.Show("Fatura Güncelleme başarılı bir şekilde gerçekleşmiştir", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);
            temizle();
        }

        private void gridView1_DoubleClick(object sender, EventArgs e)
        {
            FrmFaturaUrunDetay fr = new FrmFaturaUrunDetay();
            DataRow dr = gridView1.GetDataRow(gridView1.FocusedRowHandle);
            if (dr != null)
            {
                fr.id = dr["FATURABILGIID"].ToString();
            }
            fr.Show();
        }

        private void BtnBul_Click(object sender, EventArgs e)
        {
            SqlCommand komut = new SqlCommand("select URUNAD, SATISFIYAT from TBL_URUNLER WHERE ID=@P1", bgl.baglanti());

            komut.Parameters.AddWithValue("@p1", TxtUrunID.Text);
            SqlDataReader dr = komut.ExecuteReader();
            while (dr.Read())
            {
                TxtUrunAdi.Text = dr[0].ToString();
                TxtFiyat.Text = dr[1].ToString();
            }
            bgl.baglanti().Close();

        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (comboBox1.Text == "MÜŞTERİ")
            {
                labelControl17.Text = "MÜŞTERİ: ";
            }
            if (comboBox1.Text == "FİRMA")
            {
                labelControl17.Text = "FİRMA:";
            }
        }
    }
}
