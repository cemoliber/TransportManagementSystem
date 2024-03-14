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
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void exitButton_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void enterButton_Click(object sender, EventArgs e)
        {

            string username = "admin";
            string password = "123";


            String getUsername = userNametb.Text;
            String getPassword = passwordtb.Text;

            if (username != getUsername.TrimEnd() || password != getPassword.TrimEnd())
            {
                MessageBox.Show("Kullanıcı adı veya şifre hatalı", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
            {
                this.Hide();
                Main main = new Main();
                main.Show();
            }


        }
    }
}
