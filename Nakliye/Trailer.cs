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
    public partial class Trailer : Form
    {
        public Trailer()
        {
            InitializeComponent();
        }

        private void Trailer_Load(object sender, EventArgs e)
        {
            // TODO: Bu kod satırı 'trailerDatabase1DataSet2.Trailer' tablosuna veri yükler. Bunu gerektiği şekilde taşıyabilir, veya kaldırabilirsiniz.
            this.trailerTableAdapter.Fill(this.trailerDatabase1DataSet2.Trailer);

        }

        private void kaydetButton_Click(object sender, EventArgs e)
        {

            string sql = "INSERT INTO Trailer(DorseNo, DorseUzunluk, DorseTipi, Tasinan, Sofor, HareketTarihi,VarisTarihi,Model,Genislik,Uzunlu) VALUES('" + DorseNo.Text + "', " +
                "'" + DorseUzunluk.Text + "'," +
                "'" + DorseTipi.Text + "'," +
                "'" + TasinanUrun.Text + "'," +
                "'" + Sofor.Text + "'," +
                "'" + date1.Value.ToString()+ "'," +
                "'" + date2.Value.ToString() + "'," +
                "'" + Model.Text + "'," +
                "'" + Genislik.Text + "'," +
                "'" + Uzunluk.Text + "')";

            SqlConnection conn = new SqlConnection("Data Source=(LocalDB)\\MSSQLLocalDB;AttachDbFilename=\"C:\\Users\\Cemalettin\\Desktop\\Nakliye Proje\\Nakliye\\Nakliye\\Database1.mdf\";Integrated Security=True");

            conn.Open();
            SqlCommand sqlCommand = new SqlCommand(sql, conn);

            conn.Close();
            sqlCommand.ExecuteNonQuery();
            MessageBox.Show("KAYIT BAŞARILI");

        }

        private void AraclarDataGridView_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            int index = e.RowIndex;
            DataGridViewRow selectedRows = AraclarDataGridView.Rows[index];
            tbReportNo.Text = selectedRows.Cells[0].Value.ToString();
        }

        public void displayDatas()
        {
            AraclarDatabase aracDatabase = new AraclarDatabase();
            AraclarDataGridView.DataSource = aracDatabase.selectCmd("Select * From Trailer");
        }

        private void silButton_Click(object sender, EventArgs e)
        {
            DialogResult ask = MessageBox.Show("Girişi Silmek İstediğinizden Emin Misiniz?", "UYARI!", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            if (ask == DialogResult.Yes)
            {

                string sql = "Delete from Trailer Where Id='" + tbReportNo.Text + "'";
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
    }
}
