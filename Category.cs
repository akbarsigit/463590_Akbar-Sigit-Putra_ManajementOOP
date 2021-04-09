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
    public partial class Category : Form, Interface1
    {
        public Category()
        {
            InitializeComponent();
        }

        SqlConnection Connection = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename='C: \Users\Akbar\source\repos\Project Menejement\database\manajementdb.mdf';Integrated Security=True;Connect Timeout=30");
        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                Connection.Open();
                string query = "INSERT INTO CategoryTable VALUES(" + CatgryIdTb.Text + ",'" +CatgryNameTb.Text+ "','" +CatgryDescTb.Text+"')";
                SqlCommand cmd = new SqlCommand(query, Connection);
                cmd.ExecuteNonQuery();
                MessageBox.Show("Berhasil ditambahkan");
                Connection.Close();
                dataLoad();
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        public void dataLoad()
        {
            Connection.Open();
            string query = "SELECT * FROM CategoryTable ";
            SqlDataAdapter sda = new SqlDataAdapter(query,Connection);
            SqlCommandBuilder build = new SqlCommandBuilder(sda);
            var dataSet = new DataSet();
            sda.Fill(dataSet);
            CatgryData.DataSource = dataSet.Tables[0];

            Connection.Close();
        }
        private void Category_Load(object sender, EventArgs e)
        {
            dataLoad();
            LoadComboBox();
        }

        private void ComboBox_SelectionChangeCommitted(object sender, EventArgs e)
        {
            Connection.Open();
            string query = "SELECT * FROM ProductTable WHERE catgry = '" + ComboBox.SelectedValue.ToString() + "' ";
            SqlDataAdapter sda = new SqlDataAdapter(query, Connection);
            SqlCommandBuilder build = new SqlCommandBuilder(sda);
            var ds = new DataSet();
            sda.Fill(ds);
            CatgryData.DataSource = ds.Tables[0];
            Connection.Close();
        }

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
            Connection.Close();
        }

        private void CatgryData_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            CatgryIdTb.Text = CatgryData.SelectedRows[0].Cells[0].Value.ToString();
            CatgryNameTb.Text = CatgryData.SelectedRows[0].Cells[1].Value.ToString();
            CatgryDescTb.Text = CatgryData.SelectedRows[0].Cells[2].Value.ToString();
        }

        private void button6_Click(object sender, EventArgs e)
        {
            try
            {
                if(CatgryIdTb.Text == "")
                {
                    MessageBox.Show("Silahkan pilih kategori untuk dihapus!");
                }
                else
                {
                    Connection.Open();
                    string query = "DELETE FROM CategoryTable WHERE id = " + CatgryIdTb.Text + "";
                    SqlCommand command = new SqlCommand(query, Connection);
                    command.ExecuteNonQuery();
                    MessageBox.Show("Kategori berhasil dihapus");
                    Connection.Close();
                    dataLoad();
                }
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            try
            {
                if(CatgryIdTb.Text == "" || CatgryNameTb.Text == "" || CatgryDescTb.Text == "")
                {
                    MessageBox.Show("Missing Information");
                }
                else
                {
                    Connection.Open();
                    string query = "UPDATE CategoryTable SET name= '" + CatgryNameTb.Text + "', desc='" + CatgryDescTb.Text + "' WHERE id = " + CatgryIdTb.Text + "";
                    SqlCommand command = new SqlCommand(query, Connection);
                    command.ExecuteNonQuery();
                    MessageBox.Show("Edit Berhasil");
                    Connection.Close();
                    dataLoad();
                }
                
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
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
