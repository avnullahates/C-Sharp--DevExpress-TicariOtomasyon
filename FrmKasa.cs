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
using DevExpress.Charts;

namespace Ticari_Otamasyon
{
    public partial class FrmKasa : Form
    {
        public FrmKasa()
        {
            InitializeComponent();
        }
        sqlbaglantisi bgl = new sqlbaglantisi();

        void musteriHareket()
        {
            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter("execute MusteriHareketler", bgl.baglanti());
            da.Fill(dt);
            gridControl1.DataSource = dt;

        }
        void firmaHareketler()
        {
            DataTable dt2 = new DataTable();
            SqlDataAdapter da2 = new SqlDataAdapter("execute FirmaHareketler", bgl.baglanti());
            da2.Fill(dt2);
            gridControl3.DataSource = dt2;
        }
        void giderler()
        {
            DataTable dt3 = new DataTable();
            SqlDataAdapter da3 = new SqlDataAdapter("select * from TBL_GIDERLER", bgl.baglanti());
            da3.Fill(dt3);
            gridControl2.DataSource = dt3;
        }

        public string ad;
        private void FrmKasa_Load(object sender, EventArgs e)
        {

            LblAktifKullanici.Text = ad;
            musteriHareket();
            firmaHareketler();
            giderler();

            //Toplam Tutari Hesaplama
            SqlCommand komut1 = new SqlCommand("select sum(tutar) from TBL_FATURAdETAY", bgl.baglanti());
            SqlDataReader dr1 = komut1.ExecuteReader();
            while (dr1.Read())
            {
                LblKasaToplam.Text = dr1[0].ToString() + " TL";
            }
            bgl.baglanti().Close();

            //SON AYIN FATURALARI
            SqlCommand komut2 = new SqlCommand("select (ELEKTRIK+SU+DOGALGAZ+INTERNET+EKSTRA) from TBL_GIDERLER order by ID asc", bgl.baglanti());
            SqlDataReader dr2 = komut2.ExecuteReader();
            while (dr2.Read())
            {
                LblOdemeler.Text = dr2[0].ToString() + " TL";
            }
            bgl.baglanti().Close();

            //son ayin personel maaslari
            SqlCommand komut3 = new SqlCommand("select MAASLAR from TBL_GIDERLER order by ID asc", bgl.baglanti());
            SqlDataReader dr3 = komut3.ExecuteReader();
            while (dr3.Read())
            {
                LblPersonelMaaslari.Text = dr3[0].ToString() + " TL";
            }
            bgl.baglanti().Close();

            //toplam musteri sayisi
            SqlCommand komut4 = new SqlCommand("select count(*) from TBL_MUSTERILER", bgl.baglanti());
            SqlDataReader dr4 = komut4.ExecuteReader();
            while (dr4.Read())
            {
                LblMusteriSayisi.Text = dr4[0].ToString();
            }
            bgl.baglanti().Close();

            //toplam firma sayisi
            SqlCommand komut5 = new SqlCommand("select count(*) from TBL_FIRMALAR", bgl.baglanti());
            SqlDataReader dr5 = komut5.ExecuteReader();
            while (dr5.Read())
            {
                LblFirmaSayisi.Text = dr5[0].ToString();
            }
            bgl.baglanti().Close();

            //toplam firma sehir sayisi
            SqlCommand komut6 = new SqlCommand("select count(distinct(IL)) from TBL_FIRMALAR", bgl.baglanti());
            SqlDataReader dr6 = komut6.ExecuteReader();
            while (dr6.Read())
            {
                LblFirmaSehirSayisi.Text = dr6[0].ToString();
            }
            bgl.baglanti().Close();

            //toplam MUSTERI sehir sayisi
            SqlCommand komut7 = new SqlCommand("select count(distinct(IL)) from TBL_MUSTERILER", bgl.baglanti());
            SqlDataReader dr7 = komut7.ExecuteReader();
            while (dr7.Read())
            {
                LblMusteriSehirSayisi.Text = dr7[0].ToString();
            }
            bgl.baglanti().Close();

            //toplam personel sayisi
            SqlCommand komut8 = new SqlCommand("select count(*) from TBL_PERSONELLER", bgl.baglanti());
            SqlDataReader dr8 = komut8.ExecuteReader();
            while (dr8.Read())
            {
                LblPersonelSayisi.Text = dr8[0].ToString();
            }
            bgl.baglanti().Close();

            //toplam URUN sayisi
            SqlCommand komut9 = new SqlCommand("select sum(ADET) from TBL_URUNLER", bgl.baglanti());
            SqlDataReader dr9 = komut9.ExecuteReader();
            while (dr9.Read())
            {
                LblStokSayisi.Text = dr9[0].ToString();
            }
            bgl.baglanti().Close();

            //1. chart control e Elektrik Faturasi son 4 ay listeleme
            SqlCommand komut10 = new SqlCommand("select top 4 AY, ELEKTRIK from TBL_GIDERLER order by ID desc", bgl.baglanti());
            SqlDataReader dr10 = komut10.ExecuteReader();
            while (dr10.Read())
            {
                chartControl1.Series["AYLAR"].Points.Add(new DevExpress.XtraCharts.SeriesPoint(dr10[0], dr10[1]));
            }
            bgl.baglanti().Close();

            //2. chart control e su Faturasi son 4 ay listeleme
            SqlCommand komut11 = new SqlCommand("select top 4 AY, SU from TBL_GIDERLER order by ID desc", bgl.baglanti());
            SqlDataReader dr11 = komut11.ExecuteReader();
            while (dr11.Read())
            {
                chartControl2.Series["AYLAR"].Points.Add(new DevExpress.XtraCharts.SeriesPoint(dr11[0], dr11[1]));
            }
            bgl.baglanti().Close();
        }
        int sayac = 0;
        private void timer1_Tick(object sender, EventArgs e)
        {

            sayac++;
            if (sayac > 0 && sayac <= 5)
            {
                //1. chart control e Elektrik Faturasi son 4 ay listeleme
                groupControl2.Text = "Elektrik";
                chartControl1.Series["AYLAR"].Points.Clear();

                SqlCommand komut10 = new SqlCommand("select top 4 AY, ELEKTRIK from TBL_GIDERLER order by ID desc", bgl.baglanti());
                SqlDataReader dr10 = komut10.ExecuteReader();
                while (dr10.Read())
                {
                    chartControl1.Series["AYLAR"].Points.Add(new DevExpress.XtraCharts.SeriesPoint(dr10[0], dr10[1]));
                }
                bgl.baglanti().Close();


            }
            if (sayac > 5 && sayac <= 10)
            {
                //1. chart control e su Faturasi son 4 ay listeleme
                groupControl2.Text = "Su";
                chartControl1.Series["AYLAR"].Points.Clear();

                SqlCommand komut11 = new SqlCommand("select top 4 AY, SU from TBL_GIDERLER order by ID desc", bgl.baglanti());
                SqlDataReader dr11 = komut11.ExecuteReader();
                while (dr11.Read())
                {
                    chartControl1.Series["AYLAR"].Points.Add(new DevExpress.XtraCharts.SeriesPoint(dr11[0], dr11[1]));
                }
                bgl.baglanti().Close();
            }
            if (sayac > 10 && sayac <= 15)
            {
                //1. chart control e dogalgaz Faturasi son 4 ay listeleme
                groupControl2.Text = "Dogalgaz";
                chartControl1.Series["AYLAR"].Points.Clear();

                SqlCommand komut12 = new SqlCommand("select top 4 AY, DOGALGAZ from TBL_GIDERLER order by ID desc", bgl.baglanti());
                SqlDataReader dr12 = komut12.ExecuteReader();
                while (dr12.Read())
                {
                    chartControl1.Series["AYLAR"].Points.Add(new DevExpress.XtraCharts.SeriesPoint(dr12[0], dr12[1]));
                }
                bgl.baglanti().Close();
            }
            if (sayac > 15 && sayac <= 20)
            {
                //1. chart control e internet Faturasi son 4 ay listeleme
                groupControl2.Text = "Internet";
                chartControl1.Series["AYLAR"].Points.Clear();

                SqlCommand komut13 = new SqlCommand("select top 4 AY, INTERNET from TBL_GIDERLER order by ID desc", bgl.baglanti());
                SqlDataReader dr13 = komut13.ExecuteReader();
                while (dr13.Read())
                {
                    chartControl1.Series["AYLAR"].Points.Add(new DevExpress.XtraCharts.SeriesPoint(dr13[0], dr13[1]));
                }
                bgl.baglanti().Close();
            }
            if (sayac > 20 && sayac <= 25)
            {
                //1. chart control e Ekstra Faturasi son 4 ay listeleme
                groupControl2.Text = "Ekstra";
                chartControl1.Series["AYLAR"].Points.Clear();

                SqlCommand komut14 = new SqlCommand("select top 4 AY, EKSTRA from TBL_GIDERLER order by ID desc", bgl.baglanti());
                SqlDataReader dr14 = komut14.ExecuteReader();
                while (dr14.Read())
                {
                    chartControl1.Series["AYLAR"].Points.Add(new DevExpress.XtraCharts.SeriesPoint(dr14[0], dr14[1]));
                }
                bgl.baglanti().Close();
            }
            if (sayac == 26)
            {
                sayac = 0;
            }
        }
        int sayac2 = 0;
        private void timer2_Tick(object sender, EventArgs e)
        {
            sayac2++;
            if (sayac2 > 0 && sayac2 <= 5)
            {
                //1. chart control e Elektrik Faturasi son 4 ay listeleme
                groupControl3.Text = "Elektrik";
                chartControl2.Series["AYLAR"].Points.Clear();

                SqlCommand komut10 = new SqlCommand("select top 4 AY, ELEKTRIK from TBL_GIDERLER order by ID desc", bgl.baglanti());
                SqlDataReader dr10 = komut10.ExecuteReader();
                while (dr10.Read())
                {
                    chartControl2.Series["AYLAR"].Points.Add(new DevExpress.XtraCharts.SeriesPoint(dr10[0], dr10[1]));
                }
                bgl.baglanti().Close();


            }
            if (sayac2 > 5 && sayac2 <= 10)
            {
                //1. chart control e su Faturasi son 4 ay listeleme
                groupControl3.Text = "Su";
                chartControl2.Series["AYLAR"].Points.Clear();

                SqlCommand komut11 = new SqlCommand("select top 4 AY, SU from TBL_GIDERLER order by ID desc", bgl.baglanti());
                SqlDataReader dr11 = komut11.ExecuteReader();
                while (dr11.Read())
                {
                    chartControl2.Series["AYLAR"].Points.Add(new DevExpress.XtraCharts.SeriesPoint(dr11[0], dr11[1]));
                }
                bgl.baglanti().Close();
            }
            if (sayac2 > 10 && sayac2 <= 15)
            {
                //1. chart control e dogalgaz Faturasi son 4 ay listeleme
                groupControl3.Text = "Dogalgaz";
                chartControl2.Series["AYLAR"].Points.Clear();

                SqlCommand komut12 = new SqlCommand("select top 4 AY, DOGALGAZ from TBL_GIDERLER order by ID desc", bgl.baglanti());
                SqlDataReader dr12 = komut12.ExecuteReader();
                while (dr12.Read())
                {
                    chartControl2.Series["AYLAR"].Points.Add(new DevExpress.XtraCharts.SeriesPoint(dr12[0], dr12[1]));
                }
                bgl.baglanti().Close();
            }
            if (sayac2 > 15 && sayac2 <= 20)
            {
                //1. chart control e internet Faturasi son 4 ay listeleme
                groupControl3.Text = "Internet";
                chartControl2.Series["AYLAR"].Points.Clear();

                SqlCommand komut13 = new SqlCommand("select top 4 AY, INTERNET from TBL_GIDERLER order by ID desc", bgl.baglanti());
                SqlDataReader dr13 = komut13.ExecuteReader();
                while (dr13.Read())
                {
                    chartControl2.Series["AYLAR"].Points.Add(new DevExpress.XtraCharts.SeriesPoint(dr13[0], dr13[1]));
                }
                bgl.baglanti().Close();
            }
            if (sayac2 > 20 && sayac2 <= 25)
            {
                //1. chart control e Ekstra Faturasi son 4 ay listeleme
                groupControl3.Text = "Ekstra";
                chartControl2.Series["AYLAR"].Points.Clear();

                SqlCommand komut14 = new SqlCommand("select top 4 AY, EKSTRA from TBL_GIDERLER order by ID desc", bgl.baglanti());
                SqlDataReader dr14 = komut14.ExecuteReader();
                while (dr14.Read())
                {
                    chartControl2.Series["AYLAR"].Points.Add(new DevExpress.XtraCharts.SeriesPoint(dr14[0], dr14[1]));
                }
                bgl.baglanti().Close();
            }
            if (sayac2 == 26)
            {
                sayac2 = 0;
            }
        }
    }
}
