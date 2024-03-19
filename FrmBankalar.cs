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
    public partial class FrmBankalar : Form
    {
        public FrmBankalar()
        {
            InitializeComponent();
        }
        sqlbaglantisi bgl = new sqlbaglantisi();

        void listele()
        {
            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter("EXECUTE BankaBilgileri", bgl.baglanti());
            da.Fill(dt);
            gridControl1.DataSource = dt;
            
        }
        void temizle()
        {
            Txtid.Text = "";
            TxtBankaAdi.Text = "";
            CmbIl.Text = "";
            CmbIlce.Text = "";
            TxtSube.Text = "";
            TxtIban.Text = "";
            TxtHesapNo.Text = "";
            TxtYetkili.Text = "";
            MskTelefon.Text = "";
            MskTarih.Text = "";
            TxtHesapTuru.Text = "";
            LueFirma.Text = "";

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
        void firmaListesi()
        {
            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter("SELECT ID,AD FROM TBL_FIRMALAR", bgl.baglanti());
            da.Fill(dt);
            LueFirma.Properties.ValueMember = "ID";
            LueFirma.Properties.DisplayMember = "AD";
            LueFirma.Properties.DataSource = dt;

        }
        private void FrmBankalar_Load(object sender, EventArgs e)
        {            
            listele();
            sehirListesi();
            firmaListesi();
            temizle();
        }

        private void BtnKaydet_Click(object sender, EventArgs e)
        {
            SqlCommand komut = new SqlCommand("INSERT INTO TBL_BANKALAR (BANKAADI, IL, ILCE, SUBE, IBAN, HESAPNO, YETKILI, TELEFON, TARIH, HESAPTURU, FIRMAID) VALUES (@p1, @p2, @p3, @p4, @p5, @p6, @p7, @p8, @p9, @p10, @p11)", bgl.baglanti());
            komut.Parameters.AddWithValue("@p1", TxtBankaAdi.Text);
            komut.Parameters.AddWithValue("@p2", CmbIl.Text);
            komut.Parameters.AddWithValue("@p3", CmbIlce.Text);
            komut.Parameters.AddWithValue("@p4", TxtSube.Text);
            komut.Parameters.AddWithValue("@p5", TxtIban.Text);
            komut.Parameters.AddWithValue("@p6", TxtHesapNo.Text);
            komut.Parameters.AddWithValue("@p7", TxtYetkili.Text);
            komut.Parameters.AddWithValue("@p8", MskTelefon.Text);
            komut.Parameters.AddWithValue("@p9", MskTarih.Text);
            komut.Parameters.AddWithValue("@p10", TxtHesapTuru.Text);
            komut.Parameters.AddWithValue("@p11", LueFirma.EditValue);
            komut.ExecuteNonQuery();
            bgl.baglanti().Close();
            listele();
            MessageBox.Show("Banka Sisteme başarılı bir şekilde eklendi", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);
            temizle();
        }
        private void BtnSil_Click(object sender, EventArgs e)
        {
            DialogResult secenek = MessageBox.Show("Banka Silinecek Emin misiniz?", "Uyarı", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Error);

            if (secenek == DialogResult.Yes)
            {
                SqlCommand komuSil = new SqlCommand("DELETE FROM TBL_BANKALAR WHERE ID=@P1", bgl.baglanti());
                komuSil.Parameters.AddWithValue("@P1", Txtid.Text);
                komuSil.ExecuteNonQuery();
                bgl.baglanti().Close();
                listele();
                MessageBox.Show("Banka Silme işlemi başarılı bir şekilde gerçekleşti!", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);
                temizle();
            }
        }

        private void BtnGüncelle_Click(object sender, EventArgs e)
        {
            SqlCommand komutGuncelle = new SqlCommand("UPDATE TBL_BANKALAR SET BANKAADI=@P1, IL=@P2, ILCE=@P3, SUBE=@P4, IBAN=@P5, HESAPNO=@P6, YETKILI=@P7, TELEFON=@P8, TARIH=@P9, HESAPTURU=@P10, FIRMAID=@P11 WHERE ID=@P12", bgl.baglanti());
            komutGuncelle.Parameters.AddWithValue("@p1", TxtBankaAdi.Text);
            komutGuncelle.Parameters.AddWithValue("@p2", CmbIl.Text);
            komutGuncelle.Parameters.AddWithValue("@p3", CmbIlce.Text);
            komutGuncelle.Parameters.AddWithValue("@p4", TxtSube.Text);
            komutGuncelle.Parameters.AddWithValue("@p5", TxtIban.Text);
            komutGuncelle.Parameters.AddWithValue("@p6", TxtHesapNo.Text);
            komutGuncelle.Parameters.AddWithValue("@p7", TxtYetkili.Text);
            komutGuncelle.Parameters.AddWithValue("@p8", MskTelefon.Text);
            komutGuncelle.Parameters.AddWithValue("@p9", MskTarih.Text);
            komutGuncelle.Parameters.AddWithValue("@p10", TxtHesapTuru.Text);
            komutGuncelle.Parameters.AddWithValue("@p11", LueFirma.EditValue);
            komutGuncelle.Parameters.AddWithValue("@p12", Txtid.Text);
            komutGuncelle.ExecuteNonQuery();
            bgl.baglanti().Close();
            listele();
            MessageBox.Show("Banka Güncelleme başarılı bir şekilde gerçekleşmiştir", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);
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

        private void BtnTemizle_Click(object sender, EventArgs e)
        {
            temizle();
        }

        private void gridView1_FocusedRowChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs e)
        {
            firmaListesi();
            DataRow dr = gridView1.GetDataRow(gridView1.FocusedRowHandle);
            if (dr != null)
            {
                Txtid.Text = dr["ID"].ToString();
                TxtBankaAdi.Text = dr["BANKAADI"].ToString();
                CmbIl.Text = dr["IL"].ToString();
                CmbIlce.Text = dr["ILCE"].ToString();
                TxtSube.Text = dr["SUBE"].ToString();
                TxtIban.Text = dr["IBAN"].ToString();
                TxtHesapNo.Text = dr["HESAPNO"].ToString();
                TxtYetkili.Text = dr["YETKILI"].ToString();
                MskTelefon.Text = dr["TELEFON"].ToString();
                MskTarih.Text = dr["TARIH"].ToString();
                TxtHesapTuru.Text = dr["HESAPTURU"].ToString();
                LueFirma.Text = dr["FIRMA_ADI"].ToString();
            }
        }
    }
}
