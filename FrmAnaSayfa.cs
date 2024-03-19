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
using System.Xml;

namespace Ticari_Otamasyon
{
    public partial class FrmAnaSayfa : Form
    {
        public FrmAnaSayfa()
        {
            InitializeComponent();
        }
        sqlbaglantisi bgl = new sqlbaglantisi();

        void StoklListele()
        {
            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter("select URUNAD, sum(adet) as 'Adet' from TBL_URUNLER group by URUNAD having sum(adet)<=20", bgl.baglanti());
            da.Fill(dt);
            gridControlStoklar.DataSource = dt;
        }
        void AjandaListele()
        {
            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter("select top 8 TARIH,SAAT ,BASLIK from TBL_NOTLAR order by ID desc", bgl.baglanti());
            da.Fill(dt);
            gridControlAjanda.DataSource = dt;
        }
        void Son10Hareket()
        {//FirmaHareket2 
            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter("execute FirmaHareket2 ", bgl.baglanti());
            da.Fill(dt);
            gridControlSonOnHareket.DataSource = dt;
        }

        void fihrist()
        {
            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter("select AD, TELEFON1 from TBL_FIRMALAR", bgl.baglanti());
            da.Fill(dt);
            gridControlFihrist.DataSource = dt;
        }
        void HaberListele(string a)
        {
            listBox1.Items.Clear();
            XmlTextReader xmlOku = new XmlTextReader(a);
            while (xmlOku.Read())
            {
                if (xmlOku.Name == "title")
                {
                    listBox1.Items.Add(xmlOku.ReadString());
                }
            }
        }
        private void FrmAnaSayfa_Load(object sender, EventArgs e)
        {
            StoklListele();
            AjandaListele();
            Son10Hareket();
            fihrist();
            webBrowser1.Navigate("https://www.tcmb.gov.tr/kurlar/today.xml");
            string adres = "https://www.hurriyet.com.tr/rss/anasayfa";
            HaberListele(adres);
        }

        private void pbHurriyet_Click(object sender, EventArgs e)
        {
            string adres = "https://www.hurriyet.com.tr/rss/anasayfa";
            HaberListele(adres);
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            string adres = "https://www.sabah.com.tr/rss/gununicinden.xml";
            HaberListele(adres);
        }

        private void pbMilliGazate_Click(object sender, EventArgs e)
        {
            string adres = "https://www.milligazete.com.tr/rss.xml";
            HaberListele(adres);
        }

        private void pcMilliyet_Click(object sender, EventArgs e)
        {
            string adres = "https://www.milliyet.com.tr/rss/rssNew/dunyaRss.xml";
            HaberListele(adres);
        }
    }
}
