using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;

namespace SuperMarket_004
{
    public partial class FProductEdit : Form
    {
        
        public static string server = "Data Source = .; Initial Catalog = SuperMarket; Integrated Security = True";
        SqlConnection connect = new SqlConnection(server);
        Boolean check;
        public FProductEdit()
        {
            InitializeComponent();
        }

        private void txtCodeProduct_TextChanged(object sender, EventArgs e)
        {
            try
            {
                string select = "SELECT * FROM Products WHERE Product_Id = @id";
                connect.Open();
                SqlCommand command = new SqlCommand(select, connect);
                command.Parameters.AddWithValue("@id", txtCodeProduct.Text);



                SqlDataAdapter adapter = new SqlDataAdapter(command);


                DataTable datatable = new DataTable();
                adapter.Fill(datatable);
               
                txtNameProduct.Text = datatable.Rows[0].ItemArray[1].ToString();
                txtPriceProduct.Text = datatable.Rows[0].ItemArray[2].ToString();
                txtMojodiProduct.Text = datatable.Rows[0].ItemArray[3].ToString();
                check = true;

            }
            catch (Exception) 
            {
                check = false;
            }
            connect.Close();

        }

        private void btnEditProduct_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtCodeProduct.Text) || !int.TryParse(txtCodeProduct.Text, out int id))
            {
                txtCodeProduct.Focus();
            }
            else if (string.IsNullOrWhiteSpace(txtNameProduct.Text))
            {
                txtNameProduct.Focus();

            }
            else if (string.IsNullOrWhiteSpace(txtPriceProduct.Text) || !int.TryParse(txtPriceProduct.Text, out int price))
            {
                txtPriceProduct.Focus();

            }
            else if (string.IsNullOrWhiteSpace(txtMojodiProduct.Text) || !int.TryParse(txtMojodiProduct.Text, out int mojodi))
            {
                txtMojodiProduct.Focus();
            }
            else if (check == false)
            {
                MessageBox.Show("همچین داده ای وجود ندارد");
            }
            else 
            {
                try
                {
                    string update = "UPDATE Products SET Product_Name = @name, Price = @price, Stock = @stock WHERE Product_id = @id";
                    SqlCommand comm = new SqlCommand(update, connect);
                    connect.Open();

                    comm.Parameters.AddWithValue("@id", txtCodeProduct.Text);
                    comm.Parameters.AddWithValue("@name", txtNameProduct.Text);
                    comm.Parameters.AddWithValue("@price", txtPriceProduct.Text);
                    comm.Parameters.AddWithValue("@stock", txtMojodiProduct.Text);

                    comm.ExecuteNonQuery();
                    
                    MessageBox.Show("ویرایش اطلاعات با موفقیت انجام شد");
                    FProductEdit_Load(sender, e);
                    txtCodeProduct.Text = txtNameProduct.Text = txtPriceProduct.Text = txtMojodiProduct.Text = " ";

                }
                catch (Exception) 
                {
                    MessageBox.Show("لطفا داده ها رو برسی کنید");
                }
                connect.Close();

            }            
        }

        private void btnDeleteProduct_Click(object sender, EventArgs e)
        {
            string delete = "DELETE FROM Products WHERE Product_Id = @id";
            connect.Open();
            SqlCommand comm1 = new SqlCommand(delete, connect);

            comm1.Parameters.AddWithValue("@id", txtCodeProduct.Text);
            comm1.ExecuteNonQuery();

            connect.Close();
            MessageBox.Show("کالا با موفقیت حذف شد");
            FProductEdit_Load(sender, e);
        }

        private void FProductEdit_Load(object sender, EventArgs e)
        {

            string select = "SELECT * FROM Products";
            SqlDataAdapter adapterload = new SqlDataAdapter(select, connect);
            DataTable DT = new DataTable();
            adapterload.Fill(DT);
            dataGridView1.DataSource = DT;
             
        }
    }
}
