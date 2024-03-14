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
using static System.Net.Mime.MediaTypeNames;

namespace Nakliye
{
    public partial class Araclar : Form
    {

        string imgLocation = "";

        public Araclar()
        {
            InitializeComponent();
        }

        private void Araclar_Load(object sender, EventArgs e)
        {
            // TODO: Bu kod satırı 'araclarDataSet.Araclar' tablosuna veri yükler. Bunu gerektiği şekilde taşıyabilir, veya kaldırabilirsiniz.
            this.araclarTableAdapter.Fill(this.araclarDataSet.Araclar);

            int aracSayisi = AraclarDataGridView.Rows.Count;
            int aracSayisi2 = (aracSayisi - 1);
            string stringArac = aracSayisi2.ToString();

            aracSayisilb.Text = "Mevcut Arac Girdisi: " + stringArac.ToString();
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
                    aracPicture.ImageLocation = imgLocation;
                    // göreseli PictureBox üzerinde göster
                    aracPicture.Image = new Bitmap(dialog.FileName);
                }
            }
            catch (Exception)
            {
                MessageBox.Show("Göresel yüklenirken hata oluştu!", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void kaydetButton_Click(object sender, EventArgs e)
        {
            if(aracPicture.Image != null)
            {
                byte[] images = null;
                FileStream Stream = new FileStream(imgLocation, FileMode.Open, FileAccess.Read);
                BinaryReader brs = new BinaryReader(Stream);
                images = brs.ReadBytes((int)Stream.Length);


                string sql = "INSERT INTO Araclar(Plaka, Marka, Model, Dorsedurum, Yakit, Sahiplik,Sasino,Motorno,Motorhacmi,Motorgucu,Motoryagi,Vitesturu,Silindirsayisi,Frensayisi,Lastiksayisi,Lastikebadi,Aracagirligi,Aracyuksekligi,Aracgenisligi,Aracuzunlugu,Adblue,Image) VALUES('" + plakaTb.Text + "', " +
                    "'" + markaTb.Text + "'," +
                    "'" + modelTb.Text + "'," +
                    "'" + dorseCb.Text + "'," +
                    "'" + yakitTuruTb.Text + "'," +
                    "'" + SahiplikDurumCb.Text + "'," +
                    "'" + SasiNo.Text + "'," +
                    "'" + MotorNo.Text + "'," +
                    "'" + MotorHacmiTb.Text + "'," +
                    "'" + MotorGucu.Text + "'," +
                    "'" + MotorYagi.Text + "'," +
                    "'" + VitesTuruCb.Text + "'," +
                    "'" + Silindirsayisitb.Text + "'," +
                    "'" + FrenSayisiTb.Text + "'," +
                    "'" + LastikSayisiTb.Text + "'," +
                    "'" + LastikEbadiTb.Text + "'," +
                    "'" + AracAgirligiTb.Text + "'," +
                    "'" + AracYuksekligiTb.Text + "'," +
                    "'" + AracGenisligiTb.Text + "'," +
                    "'" + AracUzunluguTb.Text + "'," +
                    "'" + adbluecb.Text + "'," +
                    "'" + @images + "')";

                SqlConnection conn = new SqlConnection("Data Source=(LocalDB)\\MSSQLLocalDB;AttachDbFilename=\"C:\\Users\\Cemalettin\\Desktop\\Nakliye Proje\\Nakliye\\Nakliye\\Database1.mdf\";Integrated Security=True");

                conn.Open();
                SqlCommand sqlCommand = new SqlCommand(sql, conn);

                sqlCommand.Parameters.Add(new SqlParameter("@images", images));

                sqlCommand.ExecuteNonQuery();
                MessageBox.Show("KAYIT BAŞARILI");

                Araclar_Load(null, null);
            }
            if(aracPicture.Image == null)
            {
                string sql = "INSERT INTO Araclar(Plaka, Marka, Model, Dorsedurum, Yakit, Sahiplik,Sasino,Motorno,Motorhacmi,Motorgucu,Motoryagi,Vitesturu,Silindirsayisi,Frensayisi,Lastiksayisi,Lastikebadi,Aracagirligi,Aracyuksekligi,Aracgenisligi,Aracuzunlugu,Adblue) VALUES('" + plakaTb.Text + "', " +
                    "'" + markaTb.Text + "'," +
                    "'" + modelTb.Text + "'," +
                    "'" + dorseCb.Text + "'," +
                    "'" + yakitTuruTb.Text + "'," +
                    "'" + SahiplikDurumCb.Text + "'," +
                    "'" + SasiNo.Text + "'," +
                    "'" + MotorNo.Text + "'," +
                    "'" + MotorHacmiTb.Text + "'," +
                    "'" + MotorGucu.Text + "'," +
                    "'" + MotorYagi.Text + "'," +
                    "'" + VitesTuruCb.Text + "'," +
                    "'" + Silindirsayisitb.Text + "'," +
                    "'" + FrenSayisiTb.Text + "'," +
                    "'" + LastikSayisiTb.Text + "'," +
                    "'" + LastikEbadiTb.Text + "'," +
                    "'" + AracAgirligiTb.Text + "'," +
                    "'" + AracYuksekligiTb.Text + "'," +
                    "'" + AracGenisligiTb.Text + "'," +
                    "'" + AracUzunluguTb.Text + "'," +
                    "'" + adbluecb.Text + "')";

                SqlConnection conn = new SqlConnection("Data Source=(LocalDB)\\MSSQLLocalDB;AttachDbFilename=\"C:\\Users\\Cemalettin\\Desktop\\Nakliye Proje\\Nakliye\\Nakliye\\Database1.mdf\";Integrated Security=True");

                conn.Open();
                SqlCommand sqlCommand = new SqlCommand(sql, conn);

                sqlCommand.ExecuteNonQuery();
                MessageBox.Show("KAYIT BAŞARILI");

                Araclar_Load(null, null);
            }
            
                

        }

        private void AraclarDataGridView_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            int index = e.RowIndex;
            DataGridViewRow selectedRows = AraclarDataGridView.Rows[index];
            tbReportNo.Text = selectedRows.Cells[0].Value.ToString();
        }

        //to display datas
        public void displayDatas()
        {
            AraclarDatabase aracDatabase = new AraclarDatabase();
            AraclarDataGridView.DataSource = aracDatabase.selectCmd("Select * From Araclar");
        }

        private void silButton_Click(object sender, EventArgs e)
        {
            
            DialogResult ask = MessageBox.Show("Girişi Silmek İstediğinizden Emin Misiniz?", "UYARI!", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            if (ask == DialogResult.Yes)
            {

                string sql = "Delete from Araclar Where Id='" + tbReportNo.Text + "'";
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
                string sqlSearch = "Select * From Araclar";
                AraclarDataGridView.DataSource = ad.selectCmd(sqlSearch);
                searchtb.Clear();
            }

            //select by Plaka
            if (searchcb.SelectedItem.Equals("Plaka"))
            {
                displayDatas();
                string sqlSearch = "Select * From Araclar Where Plaka='" +
                searchtb.Text + "'";
                AraclarDataGridView.DataSource = ad.selectCmd(sqlSearch);
                searchtb.Clear();
            }

            //select by Marka
            if (searchcb.SelectedItem.Equals("Marka"))
            {
                displayDatas();
                string sqlSearch = "Select * From Araclar Where Marka='" +
                searchtb.Text + "'";
                AraclarDataGridView.DataSource = ad.selectCmd(sqlSearch);
                searchtb.Clear();
            }

            //select by Model
            if (searchcb.SelectedItem.Equals("Model"))
            {
                displayDatas();
                string sqlSearch = "Select * From Araclar Where Model='" +
                searchtb.Text + "'";
                AraclarDataGridView.DataSource = ad.selectCmd(sqlSearch);
                searchtb.Clear();
            }

            //select by Sasino
            if (searchcb.SelectedItem.Equals("Şasi No"))
            {
                displayDatas();
                string sqlSearch = "Select * From Araclar Where Sasino='" +
                searchtb.Text + "'";
                AraclarDataGridView.DataSource = ad.selectCmd(sqlSearch);
                searchtb.Clear();
            }

            //select by Motorno
            if (searchcb.SelectedItem.Equals("Motor No"))
            {
                displayDatas();
                string sqlSearch = "Select * From Araclar Where Motorno='" +
                searchtb.Text + "'";
                AraclarDataGridView.DataSource = ad.selectCmd(sqlSearch);
                searchtb.Clear();
            }

        }
    }
}
