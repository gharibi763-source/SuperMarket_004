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


namespace SuperMarket_004
{
    public partial class FProducts : Form
    {
        public static string server = "Data Source =.; Initial Catalog = SuperMarket ; Integrated Security = True";
        SqlConnection connect = new SqlConnection(server);
        public FProducts()
        {
            InitializeComponent();
        }
        Boolean check;

        private void btnSaveProduct_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtCodeProduct.Text))
            {
                txtCodeProduct.Focus();
            }

            else if (check == true)             
            {
                MessageBox.Show("این داده قبلا ثبت شده");
                txtCodeProduct.Focus();
            
            }


            else if (string.IsNullOrWhiteSpace(txtNameProduct.Text))
            {
                txtNameProduct.Focus();
            }


            else if (string.IsNullOrWhiteSpace(txtPriceProduct.Text))
            {
                txtPriceProduct.Focus();
            }


            else if (string.IsNullOrWhiteSpace(txtMojodiProduct.Text))
            {
                txtMojodiProduct.Focus();
            }
            else
            {
                try
                {
                    // insert into product
                    string insert = "INSERT INTO Products(Product_Id, Product_Name, Price, Stock) VALUES(@id, @nameProduct, @price, @stock)";
                    connect.Open();
                    SqlCommand command = new SqlCommand(insert, connect);

                    command.Parameters.AddWithValue("id", Convert.ToInt64(txtCodeProduct.Text));
                    command.Parameters.AddWithValue("@nameProduct", txtNameProduct.Text);
                    command.Parameters.AddWithValue("@price", txtPriceProduct.Text);
                    command.Parameters.AddWithValue("@stock", txtMojodiProduct.Text);

                    command.ExecuteNonQuery();
                    connect.Close();
                    MessageBox.Show("اطلاعات با موفقیت ثبت شد");
                    FProducts_Load(sender, e);
                    txtCodeProduct.Text = txtMojodiProduct.Text = txtNameProduct.Text = txtPriceProduct.Text = " ";
                    txtCodeProduct.Focus();
                }
                catch (Exception)
                {
                    MessageBox.Show("لطفا داده ها رو درست وارد کنید");
                    connect.Close();
                }
            }
        }

        private void FProducts_Load(object sender, EventArgs e)
        {
            string selectall = "SELECT * FROM Products";
            SqlDataAdapter data = new SqlDataAdapter(selectall, connect);

            DataTable table = new DataTable();
            data.Fill(table);
            dataGridView1.DataSource = table;
        }

        private void txtCodeProduct_TextChanged(object sender, EventArgs e)
        {
            string where = "SELECT * FROM Products WHERE Product_Id = @id";
            connect.Open();
            SqlCommand command = new SqlCommand(where, connect);

            command.Parameters.AddWithValue("@id", txtCodeProduct.Text);
            object natije  = command.ExecuteScalar();
            check = Convert.ToBoolean(natije);
            
            connect.Close();          
            

        }
    }
}
