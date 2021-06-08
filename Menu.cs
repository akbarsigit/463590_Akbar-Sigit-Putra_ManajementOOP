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
    public partial class Menu : Form
    {
        public Menu()
        {
            InitializeComponent();
        }

        private void Menu_Load(object sender, EventArgs e)
        {
            LabelDate.Text = DateTime.Today.Day.ToString() + "/" + DateTime.Today.Month.ToString() + "/" + DateTime.Today.Year.ToString();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Category cat = new Category();
            cat.Show();
            this.Hide();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Product prod = new Product();
            prod.Show();
            this.Hide();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            Selling prod = new Selling();
            prod.Show();
            this.Hide();
        }
    }
}
