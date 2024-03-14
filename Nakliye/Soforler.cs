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
    
    public partial class Soforler : Form
    { 
        public Soforler()
        {
            InitializeComponent();

        }

        string imgLocation = "";

        private void Soforler_Load(object sender, EventArgs e)
        {
            // TODO: Bu kod satırı 'database1DataSet.Soforler' tablosuna veri yükler. Bunu gerektiği şekilde taşıyabilir, veya kaldırabilirsiniz.
            this.soforlerTableAdapter.Fill(this.database1DataSet.Soforler);

            int soforSayisi = SoforlerDataGridView.Rows.Count;
            int soforSayisi2 = (soforSayisi - 1);
            string stringSofor = soforSayisi2.ToString();

            soforSayisilb.Text = "Mevcut Şoför Girdisi: "+stringSofor.ToString();
        }

       

        private void kaydetButton_Click(object sender, EventArgs e)
        {
            if(soforPicture.Image != null)
            {
                byte[] images = null;
                FileStream Stream = new FileStream(imgLocation, FileMode.Open, FileAccess.Read);
                BinaryReader brs = new BinaryReader(Stream);
                images = brs.ReadBytes((int)Stream.Length);

                string sql = "INSERT INTO Soforler(Isim, Soyisim, Dogumtarihi, Kangrubu, Baglioldugucari, Il,Ilce,Image) VALUES('" + isimtb.Text + "', " +
                    "'" + soyisimtb.Text + "'," +
                    "'" + tarih.Value.ToString() + "'," +
                    "'" + kangrubucb.Text + "'," +
                    "'" + caritb.Text + "'," +
                    "'" + iltb.Text + "'," +
                    "'" + ilcetb.Text + "'," +
                    "'" + @images + "')";

                SqlConnection conn = new SqlConnection("Data Source=(LocalDB)\\MSSQLLocalDB;AttachDbFilename=\"C:\\Users\\Cemalettin\\Desktop\\Nakliye Proje\\Nakliye\\Nakliye\\Database1.mdf\";Integrated Security=True");

                conn.Open();
                SqlCommand sqlCommand = new SqlCommand(sql, conn);

                sqlCommand.Parameters.Add(new SqlParameter("@images", images));

                sqlCommand.ExecuteNonQuery();
                MessageBox.Show("KAYIT BAŞARILI");
                conn.Close();

                Soforler_Load(null, null);
            }
            if(soforPicture.Image == null)
            {
                string sql = "INSERT INTO Soforler(Isim, Soyisim, Dogumtarihi, Kangrubu, Baglioldugucari, Il,Ilce) VALUES('" + isimtb.Text + "', " +
                    "'" + soyisimtb.Text + "'," +
                    "'" + tarih.Value.ToString() + "'," +
                    "'" + kangrubucb.Text + "'," +
                    "'" + caritb.Text + "'," +
                    "'" + iltb.Text + "'," +
                    "'" + ilcetb.Text + "')";

                SqlConnection conn = new SqlConnection("Data Source=(LocalDB)\\MSSQLLocalDB;AttachDbFilename=\"C:\\Users\\Cemalettin\\Desktop\\Nakliye Proje\\Nakliye\\Nakliye\\Database1.mdf\";Integrated Security=True");

                conn.Open();
                SqlCommand sqlCommand = new SqlCommand(sql, conn);

                sqlCommand.ExecuteNonQuery();
                MessageBox.Show("KAYIT BAŞARILI");
                conn.Close();

                Soforler_Load(null, null);
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
                    soforPicture.ImageLocation = imgLocation;
                    // göreseli PictureBox üzerinde göster
                    soforPicture.Image = new Bitmap(dialog.FileName);
                }
            }
            catch (Exception)
            {
                MessageBox.Show("Göresel yüklenirken hata oluştu!", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void SoforDataGridView_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            int index = e.RowIndex;
            DataGridViewRow selectedRows = SoforlerDataGridView.Rows[index];
            tbReportNo.Text = selectedRows.Cells[0].Value.ToString();
        }

        public void displayDatas()
        {
            AraclarDatabase aracDatabase = new AraclarDatabase();
            SoforlerDataGridView.DataSource = aracDatabase.selectCmd("Select * From Soforler");
        }

        private void silButton_Click(object sender, EventArgs e)
        {
            DialogResult ask = MessageBox.Show("Girişi Silmek İstediğinizden Emin Misiniz?", "UYARI!", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            if (ask == DialogResult.Yes)
            {

                string sql = "Delete from Soforler Where Id='" + tbReportNo.Text + "'";
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
                string sqlSearch = "Select * From Soforler";
                SoforlerDataGridView.DataSource = ad.selectCmd(sqlSearch);
                searchtb.Clear();
            }

            //select by İsim
            if (searchcb.SelectedItem.Equals("İsim"))
            {
                displayDatas();
                string sqlSearch = "Select * From Soforler Where Isim='" +
                searchtb.Text + "'";
                SoforlerDataGridView.DataSource = ad.selectCmd(sqlSearch);
                searchtb.Clear();
            }

            //select by Soyisim
            if (searchcb.SelectedItem.Equals("Soyisim"))
            {
                displayDatas();
                string sqlSearch = "Select * From Soforler Where Soyisim='" +
                searchtb.Text + "'";
                SoforlerDataGridView.DataSource = ad.selectCmd(sqlSearch);
                searchtb.Clear();
            }

            //select by Kan Grubu
            if (searchcb.SelectedItem.Equals("Kan Grubu"))
            {
                displayDatas();
                string sqlSearch = "Select * From Soforler Where Kangrubu='" +
                searchtb.Text + "'";
                SoforlerDataGridView.DataSource = ad.selectCmd(sqlSearch);
                searchtb.Clear();
            }

            //select by Baglioldugucari
            if (searchcb.SelectedItem.Equals("Bağlı Olduğu cari"))
            {
                displayDatas();
                string sqlSearch = "Select * From Soforler Where Baglioldugucari='" +
                searchtb.Text + "'";
                SoforlerDataGridView.DataSource = ad.selectCmd(sqlSearch);
                searchtb.Clear();
            }

            //select by Il
            if (searchcb.SelectedItem.Equals("Doğum İl"))
            {
                displayDatas();
                string sqlSearch = "Select * From Soforler Where Il='" +
                searchtb.Text + "'";
                SoforlerDataGridView.DataSource = ad.selectCmd(sqlSearch);
                searchtb.Clear();
            }

            //select by Ilce
            if (searchcb.SelectedItem.Equals("Doğum İlçe"))
            {
                displayDatas();
                string sqlSearch = "Select * From Soforler Where Ilce='" +
                searchtb.Text + "'";
                SoforlerDataGridView.DataSource = ad.selectCmd(sqlSearch);
                searchtb.Clear();
            }
        }
    }
}
