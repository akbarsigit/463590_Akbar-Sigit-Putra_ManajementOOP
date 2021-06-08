using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Project_Menejement
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void label1_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void bunifuThinButton21_Click(object sender, EventArgs e)
        {
            if(username.Text == "" || password.Text == "")
            {
                MessageBox.Show("Masukkan 'Admin' untuk username dan 'Admin' untuk Password");
            }
            else
            {
                if(username.Text == "Admin" && password.Text == "Admin")
                {
                    Menu menu = new Menu();
                    menu.Show();
                    this.Hide();
                }
                else
                {
                    MessageBox.Show("Masukkan 'Admin' untuk username dan 'Admin' untuk Password");
                }
            }
        }
    }
}
