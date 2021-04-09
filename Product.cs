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

namespace Project_Menejement
{
    public partial class Product : Form, Interface1
    {
        public Product()
        {
            InitializeComponent();
        }

        SqlConnection Connection = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename='C: \Users\Akbar\source\repos\Project Menejement\database\manajementdb.mdf';Integrated Security=True;Connect Timeout=30");

        public void LoadComboBox()
        {
            Connection.Open();
            SqlCommand command = new SqlCommand("SELECT name from CategoryTable", Connection);
            SqlDataReader rdr;
            rdr = command.ExecuteReader();
            DataTable dataTable = new DataTable();
            dataTable.Columns.Add("name", typeof(string));
            dataTable.Load(rdr);
            ComboBox.ValueMember = "name";
            ComboBox.DataSource = dataTable;
            SearchCB.ValueMember = "name";
            SearchCB.DataSource = dataTable;
            Connection.Close();
        }

        private void SearchCB_SelectionChangeCommitted(object sender, EventArgs e)
        {
            Connection.Open();
            string query = "SELECT * FROM ProductTable WHERE catgry = '" + SearchCB.SelectedValue.ToString() + "' ";
            SqlDataAdapter sda = new SqlDataAdapter(query, Connection);
            SqlCommandBuilder build = new SqlCommandBuilder(sda);
            var ds = new DataSet();
            sda.Fill(ds);
            ProdDGV.DataSource = ds.Tables[0];
            Connection.Close();
        }
        private void Product_Load(object sender, EventArgs e)
        {
            LoadComboBox();
            dataLoad();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Category cat = new Category();
            cat.Show();
            this.Hide();
        }

        public void dataLoad()
        {
            Connection.Open();
            string query = "SELECT * FROM ProductTable";
            SqlDataAdapter sda = new SqlDataAdapter(query, Connection);
            SqlCommandBuilder build = new SqlCommandBuilder(sda);
            var dataSet = new DataSet();
            sda.Fill(dataSet);
            ProdDGV.DataSource = dataSet.Tables[0];

            Connection.Close();
        }


        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                Connection.Open();
                string query = "INSERT INTO ProductTable VALUES('"+ Prodid.Text + "','" + ProdName.Text + "','" + ProdQty.Text + "','" + ProdPrice.Text + "', '"+ComboBox.SelectedValue.ToString()+"')";
                SqlCommand cmd = new SqlCommand(query, Connection);
                cmd.ExecuteNonQuery();
                MessageBox.Show("Berhasil ditambahkan");
                Connection.Close();
                dataLoad();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void guna2DataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            Prodid.Text = ProdDGV.SelectedRows[0].Cells[0].Value.ToString();
            ProdName.Text = ProdDGV.SelectedRows[0].Cells[1].Value.ToString();
            ProdQty.Text = ProdDGV.SelectedRows[0].Cells[2].Value.ToString();
            ProdPrice.Text = ProdDGV.SelectedRows[0].Cells[3].Value.ToString();
            ComboBox.Text = ProdDGV.SelectedRows[0].Cells[4].Value.ToString();
        }

        private void button6_Click(object sender, EventArgs e)
        {
            try
            {
                if (Prodid.Text == "")
                {
                    MessageBox.Show("Silahkan pilih kategori untuk dihapus!");
                }
                else
                {
                    Connection.Open();
                    string query = "DELETE FROM ProductTable WHERE id = " + Prodid.Text + "";
                    SqlCommand command = new SqlCommand(query, Connection);
                    command.ExecuteNonQuery();
                    MessageBox.Show("Produk berhasil dihapus");
                    Connection.Close();
                    dataLoad();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            try
            {
                if (Prodid.Text == "" || ProdName.Text == "" || ProdQty.Text == "")
                {
                    MessageBox.Show("Missing Information");
                }
                else
                {
                    Connection.Open();
                    string query = "UPDATE ProductTable SET name='" + ProdName.Text + "', qnty='" + ProdQty.Text + "', price='" + ProdPrice.Text + "' , catgry='" + ProdPrice.Text + "' WHERE id = '" + ComboBox.SelectedValue.ToString() + "'";
                    SqlCommand command = new SqlCommand(query, Connection);
                    command.ExecuteNonQuery();
                    MessageBox.Show("Edit Berhasil");
                    Connection.Close();
                    dataLoad();
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            Selling prod = new Selling();
            prod.Show();
            this.Hide();
        }
    }
}
