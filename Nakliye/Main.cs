using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Nakliye
{
    public partial class Main : Form
    {
        public Main()
        {
            InitializeComponent();
        }

        public void AddControls(Form f)
        {
            mainPanel.Controls.Clear();
            f.Dock = DockStyle.Fill;
            f.TopLevel = false;
            mainPanel.Controls.Add(f);
            f.Show();
        }

        private void araclarButton_Click(object sender, EventArgs e)
        {
            AddControls(new Araclar());
        }

        private void soforlerButton_Click(object sender, EventArgs e)
        {
            AddControls(new Soforler());
        }

        private void cariButton_Click(object sender, EventArgs e)
        {
            AddControls(new Cari());
        }

        private void nakliyeButton_Click(object sender, EventArgs e)
        {
            AddControls(new NakliyePage());
        }

        private void anasayfaButton_Click(object sender, EventArgs e)
        {
            AddControls(new Anasayfa());

        }

        private void dorselerButton_Click(object sender, EventArgs e)
        {
            //AddControls(new Trailer());
        }
    }
}
