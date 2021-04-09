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

    public partial class Selling : Form, Interface1
    {
        private int GrandTotal { get; set; } = 0;
        private int total { get; set; }
        private int i { get; set; } = 0;
        public Selling()
        {
            InitializeComponent();
        }

        SqlConnection Connection = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename='C: \Users\Akbar\source\repos\Project Menejement\database\manajementdb.mdf';Integrated Security=True;Connect Timeout=30");
        public void dataLoad()
        {
            Connection.Open();
            string query = "SELECT name, qnty FROM ProductTable";
            SqlDataAdapter sda = new SqlDataAdapter(query, Connection);
            SqlCommandBuilder build = new SqlCommandBuilder(sda);
            var dataSet = new DataSet();
            sda.Fill(dataSet);
            ProdDGV1.DataSource = dataSet.Tables[0];

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

        private void LoadBills()
        {
            Connection.Open();
            string query = "SELECT * FROM BillTable";
            SqlDataAdapter sda = new SqlDataAdapter(query, Connection);
            SqlCommandBuilder build = new SqlCommandBuilder(sda);
            var dataSet = new DataSet();
            sda.Fill(dataSet);
            BillDGV.DataSource = dataSet.Tables[0];

            Connection.Close();
        }

        private void Selling_Load(object sender, EventArgs e)
        {
            dataLoad();
            LoadBills();
            LoadComboBox();
            LabelDate.Text = DateTime.Today.Day.ToString() + "/" + DateTime.Today.Month.ToString() + "/" + DateTime.Today.Year.ToString();
        }

        private void ProdDGV1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            ProdName.Text = ProdDGV1.SelectedRows[0].Cells[0].Value.ToString();
            ProdPrice.Text = ProdDGV1.SelectedRows[0].Cells[1].Value.ToString();
        }

        private void AddProduct_Click(object sender, EventArgs e)
        {
            if (ProdName.Text == "" || ProdQty.Text == "")
            {
                MessageBox.Show("Pilih Produk dan Jumlahnya");
            }

            else
            {
                total = Convert.ToInt32(ProdPrice.Text) * Convert.ToInt32(ProdQty.Text);
                DataGridViewRow newRow = new DataGridViewRow();
                newRow.CreateCells(PesananDGV);
                newRow.Cells[0].Value = i + 1;
                newRow.Cells[1].Value = ProdName.Text;
                newRow.Cells[2].Value = ProdPrice.Text;
                newRow.Cells[3].Value = ProdQty.Text;
                newRow.Cells[4].Value = total;
                PesananDGV.Rows.Add(newRow);
                GrandTotal += total;
                Totallbl.Text = ""+GrandTotal;
                i++;
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (Billid.Text== "")
            {
                MessageBox.Show("Tidak Ada Produk");
            }
            else
            {
                try
                {
                    Connection.Open();
                    string query = "INSERT INTO BillTable VALUES('" + Billid.Text + "','" + SellerName.Text + "','" + LabelDate.Text + "'," + Totallbl.Text + ")";
                    SqlCommand cmd = new SqlCommand(query, Connection);
                    cmd.ExecuteNonQuery();
                    MessageBox.Show("Order Ditambahkan");
                    Connection.Close();
                    LoadBills();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
        }

        private void ComboBox_SelectionChangeCommitted(object sender, EventArgs e)
        {
            Connection.Open();
            string query = "SELECT name, qnty FROM ProductTable WHERE catgry = '" + ComboBox.SelectedValue.ToString() +"' ";
            SqlDataAdapter sda = new SqlDataAdapter(query, Connection);
            SqlCommandBuilder build = new SqlCommandBuilder(sda);
            var ds = new DataSet();
            sda.Fill(ds);
            ProdDGV1.DataSource = ds.Tables[0];
            Connection.Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Category cat = new Category();
            cat.Show();
            this.Hide();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            Product prod = new Product();
            prod.Show();
            this.Hide();
        }
    }
}
