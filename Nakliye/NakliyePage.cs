using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Net.Mime.MediaTypeNames;

namespace Nakliye
{
    public partial class NakliyePage : Form
    {
        public NakliyePage()
        {
            InitializeComponent();
        }

        private void Nakliye_Load(object sender, EventArgs e)
        {
            // TODO: Bu kod satırı 'nakliyeDatabase1DataSet1.Nakliye' tablosuna veri yükler. Bunu gerektiği şekilde taşıyabilir, veya kaldırabilirsiniz.
            this.nakliyeTableAdapter.Fill(this.nakliyeDatabase1DataSet1.Nakliye);

            int nakliyeSayisi = NakliyeDataGridView.Rows.Count;
            int nakliyeSayisi2 = (nakliyeSayisi - 1);
            string stringNakliye = nakliyeSayisi2.ToString();

            nakliyeSayisilb.Text = "Mevcut Nakliye Girdisi: " + stringNakliye.ToString();
        }

        private void kaydetButton_Click(object sender, EventArgs e)
        {

            float floatKdvtutar;

            string stringkdv = kdvtb.Text;
            float k = float.Parse(stringkdv, CultureInfo.InvariantCulture);

            string stringtutar = tutartb.Text;
            float t = float.Parse(stringtutar, CultureInfo.InvariantCulture);

            floatKdvtutar = ((t * k) / 100);

            kdvTutarlbl.Text = floatKdvtutar.ToString();

            string sql = "INSERT INTO Nakliye(Tarih, Arac, Aracsahibi, Sofor, Isveren, Yuklemeyeri, Indirmeyeri, Yukcinsi, Aciklama, Toplamkg, Toplamsefer, Ortalamakg, Toplamyekun, Sefer, Prim, Kdv, Kg, Bfiyat, Kdvtutar, Tutar, Primtutar,Yekun) VALUES('" + tarih.Value.ToString() + "', " +
                "'" + aractb.Text + "'," +
                "'" + aracsahibitb.Text + "'," +
                "'" + sofortb.Text + "'," +
                "'" + isverentb.Text + "'," +
                "'" + yuklemeyeritb.Text + "'," +
                "'" + indirmeyeritb.Text + "'," +
                "'" + yukcinsitb.Text + "'," +
                "'" + aciklamatb.Text + "'," +
                "'" + toplamkgtb.Text + "'," +
                "'" + toplamsefertb.Text + "'," +
                "'" + ortalamakgtb.Text + "'," +
                "'" + toplamyekuntb.Text + "'," +
                "'" + sefertb.Text + "'," +
                "'" + primcb.Text + "'," +
                "'" + kdvtb.Text + "'," +
                "'" + kgtb.Text + "'," +
                "'" + bfiyattb.Text + "'," +
                "'" + floatKdvtutar + "'," +
                "'" + tutartb.Text + "'," +
                "'" + primtutartb.Text + "'," +
                "'" + yekuntb.Text + "')";

            SqlConnection conn = new SqlConnection("Data Source=(LocalDB)\\MSSQLLocalDB;AttachDbFilename=\"C:\\Users\\Cemalettin\\Desktop\\Nakliye Proje\\Nakliye\\Nakliye\\Database1.mdf\";Integrated Security=True");

            conn.Open();
            SqlCommand sqlCommand = new SqlCommand(sql, conn);

            sqlCommand.ExecuteNonQuery();
            MessageBox.Show("KAYIT BAŞARILI");

            Nakliye_Load(null, null);
        }

        private void NakliyeDataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            int index = e.RowIndex;
            DataGridViewRow selectedRows = NakliyeDataGridView.Rows[index];
            tbReportNo.Text = selectedRows.Cells[0].Value.ToString();
        }

        //to display datas
        public void displayDatas()
        {
            AraclarDatabase nakliyeDatabase = new AraclarDatabase();
            NakliyeDataGridView.DataSource = nakliyeDatabase.selectCmd("Select * From Nakliye");
        }

        private void silButton_Click(object sender, EventArgs e)
        {
            DialogResult ask = MessageBox.Show("Girişi Silmek İstediğinizden Emin Misiniz?", "UYARI!", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            if (ask == DialogResult.Yes)
            {

                string sql = "Delete from Nakliye Where Id='" + tbReportNo.Text + "'";
                AraclarDatabase araclarDatabase = new AraclarDatabase();

                if (araclarDatabase.cudCMD(sql) > 0)
                {
                    displayDatas();
                    MessageBox.Show("Giriş Başarıyla Silindi", "Başarılı");
                }
                else
                {
                    MessageBox.Show("İşlem Sırasında Hata Oluştu", "Hata");
                }

            }
        }

        private void searchButton_Click(object sender, EventArgs e)
        {
            AraclarDatabase nd = new AraclarDatabase();

            //List All Data
            if (searchcb.SelectedItem.Equals("Tüm Veri"))
            {
                string sqlSearch = "Select * From Nakliye";
                NakliyeDataGridView.DataSource = nd.selectCmd(sqlSearch);
                searchtb.Clear();
            }

            //select by Plaka
            if (searchcb.SelectedItem.Equals("Araç"))
            {
                displayDatas();
                string sqlSearch = "Select * From Nakliye Where Arac='" +
                searchtb.Text + "'";
                NakliyeDataGridView.DataSource = nd.selectCmd(sqlSearch);
                searchtb.Clear();
            }

            //select by Marka
            if (searchcb.SelectedItem.Equals("Araç Sahibi"))
            {
                displayDatas();
                string sqlSearch = "Select * From Nakliye Where Aracsahibi='" +
                searchtb.Text + "'";
                NakliyeDataGridView.DataSource = nd.selectCmd(sqlSearch);
                searchtb.Clear();
            }

            //select by Model
            if (searchcb.SelectedItem.Equals("Şoför"))
            {
                displayDatas();
                string sqlSearch = "Select * From Nakliye Where Sofor='" +
                searchtb.Text + "'";
                NakliyeDataGridView.DataSource = nd.selectCmd(sqlSearch);
                searchtb.Clear();
            }

            //select by Sasino
            if (searchcb.SelectedItem.Equals("İşveren"))
            {
                displayDatas();
                string sqlSearch = "Select * From Araclar Where Isveren='" +
                searchtb.Text + "'";
                NakliyeDataGridView.DataSource = nd.selectCmd(sqlSearch);
                searchtb.Clear();
            }

            //select by Motorno
            if (searchcb.SelectedItem.Equals("Yükleme Yeri"))
            {
                displayDatas();
                string sqlSearch = "Select * From Nakliye Where Yuklemeyeri='" +
                searchtb.Text + "'";
                NakliyeDataGridView.DataSource = nd.selectCmd(sqlSearch);
                searchtb.Clear();
            }

            //select by Motorno
            if (searchcb.SelectedItem.Equals("İndirme Yeri"))
            {
                displayDatas();
                string sqlSearch = "Select * From Nakliye Where Indirmeyeri='" +
                searchtb.Text + "'";
                NakliyeDataGridView.DataSource = nd.selectCmd(sqlSearch);
                searchtb.Clear();
            }

            //select by Motorno
            if (searchcb.SelectedItem.Equals("Yük Cinsi"))
            {
                displayDatas();
                string sqlSearch = "Select * From Nakliye Where Yukcinsi='" +
                searchtb.Text + "'";
                NakliyeDataGridView.DataSource = nd.selectCmd(sqlSearch);
                searchtb.Clear();
            }
        }
    }
}
