using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Nakliye
{
    public partial class Cari : Form
    {

        string imgLocation = "";

        public Cari()
        {
            InitializeComponent();
        }

        private void Cari_Load(object sender, EventArgs e)
        {
            // TODO: Bu kod satırı 'cariDatabase1DataSet1.Cari' tablosuna veri yükler. Bunu gerektiği şekilde taşıyabilir, veya kaldırabilirsiniz.
            this.cariTableAdapter.Fill(this.cariDatabase1DataSet1.Cari);
            
            
            int cariSayisi = CariDataGridView.Rows.Count;
            int cariSayisi2 = (cariSayisi - 1);
            string stringCari = cariSayisi2.ToString();

            cariSayisilb.Text = "Mevcut Cari Girdisi: " + stringCari.ToString();

        }


        private void kaydetButton_Click(object sender, EventArgs e)
        {
            if(cariPicture.Image != null)
            {
                byte[] images = null;
                FileStream Stream = new FileStream(imgLocation, FileMode.Open, FileAccess.Read);
                BinaryReader brs = new BinaryReader(Stream);
                images = brs.ReadBytes((int)Stream.Length);

                string sql = "INSERT INTO Cari(Carikodu, Cariunvan, Babaadi, Caritipi, Vatandasno, Gorevi, Adrestipi, Isim, Soyisim, Telefon, Postakodu, Ulke, Il,Ilce, Adres, Image) VALUES('" + carikodutb.Text + "', " +
                    "'" + cariunvantb.Text + "'," +
                    "'" + babaaditb.Text + "'," +
                    "'" + caritipitb.Text + "'," +
                    "'" + vatandasnotb.Text + "'," +
                    "'" + gorevitb.Text + "'," +
                    "'" + adrestipicb.Text + "'," +
                    "'" + isimtb.Text + "'," +
                    "'" + soyisimtb.Text + "'," +
                    "'" + telefontb.Text + "'," +
                    "'" + postakodutb.Text + "'," +
                    "'" + ulketb.Text + "'," +
                    "'" + iltb.Text + "'," +
                    "'" + ilcetb.Text + "'," +
                    "'" + adrestb.Text + "'," +
                    "'" + @images + "')";

                SqlConnection conn = new SqlConnection("Data Source=(LocalDB)\\MSSQLLocalDB;AttachDbFilename=\"C:\\Users\\Cemalettin\\Desktop\\Nakliye Proje\\Nakliye\\Nakliye\\Database1.mdf\";Integrated Security=True");

                conn.Open();
                SqlCommand sqlCommand = new SqlCommand(sql, conn);

                sqlCommand.Parameters.Add(new SqlParameter("@images", images));

                sqlCommand.ExecuteNonQuery();
                MessageBox.Show("KAYIT BAŞARILI");

                conn.Close();

                Cari_Load(null, null);
            }
            if(cariPicture.Image == null){
                string sql = "INSERT INTO Cari(Carikodu, Cariunvan, Babaadi, Caritipi, Vatandasno, Gorevi, Adrestipi, Isim, Soyisim, Telefon, Postakodu, Ulke, Il,Ilce, Adres) VALUES('" + carikodutb.Text + "', " +
                    "'" + cariunvantb.Text + "'," +
                    "'" + babaaditb.Text + "'," +
                    "'" + caritipitb.Text + "'," +
                    "'" + vatandasnotb.Text + "'," +
                    "'" + gorevitb.Text + "'," +
                    "'" + adrestipicb.Text + "'," +
                    "'" + isimtb.Text + "'," +
                    "'" + soyisimtb.Text + "'," +
                    "'" + telefontb.Text + "'," +
                    "'" + postakodutb.Text + "'," +
                    "'" + ulketb.Text + "'," +
                    "'" + iltb.Text + "'," +
                    "'" + ilcetb.Text + "'," +
                    "'" + adrestb.Text + "')";

                SqlConnection conn = new SqlConnection("Data Source=(LocalDB)\\MSSQLLocalDB;AttachDbFilename=\"C:\\Users\\Cemalettin\\Desktop\\Nakliye Proje\\Nakliye\\Nakliye\\Database1.mdf\";Integrated Security=True");

                conn.Open();
                SqlCommand sqlCommand = new SqlCommand(sql, conn);

                sqlCommand.ExecuteNonQuery();
                MessageBox.Show("KAYIT BAŞARILI");

                conn.Close();

                Cari_Load(null, null);
            }
     
        }

        private void FotografSecButton_Click(object sender, EventArgs e)
        {
            try
            {
                // open file dialog   
                OpenFileDialog dialog = new OpenFileDialog();
                // image filters  
                dialog.Filter = "Image Files(*.jpg; *.jpeg; *.gif; *.bmp)|*.jpg; *.jpeg; *.gif; *.bmp";
                if (dialog.ShowDialog() == DialogResult.OK)
                {
                    imgLocation = dialog.FileName.ToString();
                    cariPicture.ImageLocation = imgLocation;
                    // göreseli PictureBox üzerinde göster
                    cariPicture.Image = new Bitmap(dialog.FileName);
                }
            }
            catch (Exception)
            {
                MessageBox.Show("Göresel yüklenirken hata oluştu!", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void CariDataGridView_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            int index = e.RowIndex;
            DataGridViewRow selectedRows = CariDataGridView.Rows[index];
            tbReportNo.Text = selectedRows.Cells[0].Value.ToString();
        }

        public void displayDatas()
        {
            AraclarDatabase cariDatabase = new AraclarDatabase();
            CariDataGridView.DataSource = cariDatabase.selectCmd("Select * From Cari");
        }

        private void silButton_Click(object sender, EventArgs e)
        {

            DialogResult ask = MessageBox.Show("Girişi Silmek İstediğinizden Emin Misiniz?", "UYARI!", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            if (ask == DialogResult.Yes)
            {

                string sql = "Delete from Cari Where Id='" + tbReportNo.Text + "'";
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
            AraclarDatabase ad = new AraclarDatabase();

            //List All Data
            if (searchcb.SelectedItem.Equals("Tüm Veri"))
            {
                string sqlSearch = "Select * From Cari";
                CariDataGridView.DataSource = ad.selectCmd(sqlSearch);
                searchtb.Clear();
            }

            //select by Plaka
            if (searchcb.SelectedItem.Equals("Cari Kodu"))
            {
                displayDatas();
                string sqlSearch = "Select * From Cari Where Carikodu='" +
                searchtb.Text + "'";
                CariDataGridView.DataSource = ad.selectCmd(sqlSearch);
                searchtb.Clear();
            }

            //select by Marka
            if (searchcb.SelectedItem.Equals("Cari Ünvan"))
            {
                displayDatas();
                string sqlSearch = "Select * From Cari Where Cariunvan='" +
                searchtb.Text + "'";
                CariDataGridView.DataSource = ad.selectCmd(sqlSearch);
                searchtb.Clear();
            }

            //select by Model
            if (searchcb.SelectedItem.Equals("Cari Tipi"))
            {
                displayDatas();
                string sqlSearch = "Select * From Cari Where Caritipi='" +
                searchtb.Text + "'";
                CariDataGridView.DataSource = ad.selectCmd(sqlSearch);
                searchtb.Clear();
            }

            //select by Adres Tipi
            if (searchcb.SelectedItem.Equals("Adres Tipi"))
            {
                displayDatas();
                string sqlSearch = "Select * From Cari Where Aractipi='" +
                searchtb.Text + "'";
                CariDataGridView.DataSource = ad.selectCmd(sqlSearch);
                searchtb.Clear();
            }

            //select by İsim
            if (searchcb.SelectedItem.Equals("İsim"))
            {
                displayDatas();
                string sqlSearch = "Select * From Cari Where Isim='" +
                searchtb.Text + "'";
                CariDataGridView.DataSource = ad.selectCmd(sqlSearch);
                searchtb.Clear();
            }

            //select by Soyisim
            if (searchcb.SelectedItem.Equals("Soyisim"))
            {
                displayDatas();
                string sqlSearch = "Select * From Cari Where Soyisim='" +
                searchtb.Text + "'";
                CariDataGridView.DataSource = ad.selectCmd(sqlSearch);
                searchtb.Clear();
            }

            //select by Telefon
            if (searchcb.SelectedItem.Equals("Telefon"))
            {
                displayDatas();
                string sqlSearch = "Select * From Cari Where Telefon='" +
                searchtb.Text + "'";
                CariDataGridView.DataSource = ad.selectCmd(sqlSearch);
                searchtb.Clear();
            }

            //select by Posta Kodu
            if (searchcb.SelectedItem.Equals("Posta Kodu"))
            {
                displayDatas();
                string sqlSearch = "Select * From Cari Where Postakodu='" +
                searchtb.Text + "'";
                CariDataGridView.DataSource = ad.selectCmd(sqlSearch);
                searchtb.Clear();
            }

            //select by Ülke
            if (searchcb.SelectedItem.Equals("Ülke"))
            {
                displayDatas();
                string sqlSearch = "Select * From Cari Where Ulke='" +
                searchtb.Text + "'";
                CariDataGridView.DataSource = ad.selectCmd(sqlSearch);
                searchtb.Clear();
            }

            //select by İl
            if (searchcb.SelectedItem.Equals("İl"))
            {
                displayDatas();
                string sqlSearch = "Select * From Cari Where Il='" +
                searchtb.Text + "'";
                CariDataGridView.DataSource = ad.selectCmd(sqlSearch);
                searchtb.Clear();
            }

            //select by İlce
            if (searchcb.SelectedItem.Equals("İlçe"))
            {
                displayDatas();
                string sqlSearch = "Select * From Cari Where Ilce='" +
                searchtb.Text + "'";
                CariDataGridView.DataSource = ad.selectCmd(sqlSearch);
                searchtb.Clear();
            }
        }
    }
}
