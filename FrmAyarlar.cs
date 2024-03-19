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
    public partial class FrmAyarlar : Form
    {
        public FrmAyarlar()
        {
            InitializeComponent();
        }
        sqlbaglantisi bgl = new sqlbaglantisi();

        void listele()
        {
            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter("select * from TBL_ADMIN", bgl.baglanti());
            da.Fill(dt);
            gridControl1.DataSource = dt;
        }
        private void FrmAyarlar_Load(object sender, EventArgs e)
        {
            listele();
            temizle();
        }

        void temizle()
        {
            TxtKullaniciAdi.Text = "";
            TxtSifre.Text = "";
        }
        private void BtnIslem_Click(object sender, EventArgs e)
        {

            if (BtnIslem.Text == "KAYDET")
            {
                SqlCommand komu = new SqlCommand("insert into TBL_ADMIN values (@p1, @p2)", bgl.baglanti());

                komu.Parameters.AddWithValue("@p1", TxtKullaniciAdi.Text);
                komu.Parameters.AddWithValue("@p2", TxtSifre.Text);
                komu.ExecuteNonQuery();
                bgl.baglanti();
                MessageBox.Show("Yeni Admin Sisteme başarılı bir şekilde eklendi", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);
                listele();
                temizle();
            }
            if (BtnIslem.Text == "GÜNCELLE")
            {
                SqlCommand komut = new SqlCommand("update TBL_ADMIN set Sifre=@P2 where KullaniciAd=@P1", bgl.baglanti());
                komut.Parameters.AddWithValue("@p1", TxtKullaniciAdi.Text);
                komut.Parameters.AddWithValue("@p2", TxtSifre.Text);
                komut.ExecuteNonQuery();
                bgl.baglanti();
                MessageBox.Show("Admin Sifre Güncelleme başarılı bir şekilde gerçekleşmiştir", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);
                listele();
                temizle();
            }

        }

        private void gridView1_FocusedRowChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs e)
        {
            DataRow dr = gridView1.GetDataRow(gridView1.FocusedRowHandle);
            if (dr != null)
            {
                TxtKullaniciAdi.Text = dr["KullaniciAd"].ToString();
                TxtSifre.Text = dr["Sifre"].ToString();
            }
        }

        private void TxtKullaniciAdi_TextChanged(object sender, EventArgs e)
        {
            if (TxtKullaniciAdi.Text != "")
            {
                BtnIslem.Text = "GÜNCELLE";
                BtnIslem.BackColor = Color.Green;
                TxtKullaniciAdi.Enabled = false;


            }
            else
            {
                BtnIslem.Text = "KAYDET";
                BtnIslem.BackColor = Color.YellowGreen;
                TxtKullaniciAdi.Enabled = true;
            }
        }

        private void TxtTemizle_Click(object sender, EventArgs e)
        {
            temizle();
        }
    }
}
