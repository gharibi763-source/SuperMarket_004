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
    public partial class FMoshtariEdit : Form
    {
        public static string server = "Data Source=.; Initial Catalog= SuperMarket; Integrated Security = True";
        SqlConnection conn = new SqlConnection(server);

        Boolean checkcode = false;
        public FMoshtariEdit()
        {
            InitializeComponent();
        }

        private void btnEdit_Click(object sender, EventArgs e)
        {
            if (txtboxCustomer.Text == " ")
            {
                txtboxCustomer.Focus();
            }

            else if (txtboxPhone.Text == " ")
            {
                txtboxPhone.Focus();
            }

            else if (txtboxAddress.Text == " ")
            {
                txtboxAddress.Focus();
            }

            else if (txtboxEshterak.Text == " ")
            {
                txtboxEshterak.Focus();
            }
            else if (checkcode == false)
            {
                MessageBox.Show("مشتری با این کد ثیت نشده است");

            }
            else 
            {
                string updatequery = "UPDATE  Customers  SET   Name = @name, Address = @address, Phone = @phone  WHERE  Customer_Id = @cusid ";
                conn.Open();
                SqlCommand comm = new SqlCommand(updatequery, conn);
                comm.Parameters.AddWithValue("@name", txtboxCustomer.Text);
                comm.Parameters.AddWithValue("@address", txtboxAddress.Text);
                comm.Parameters.AddWithValue("@phone", Convert.ToInt64(txtboxPhone.Text));
                comm.Parameters.AddWithValue("@cusid", Convert.ToInt32(txtboxEshterak.Text));

                comm.ExecuteNonQuery();
                conn.Close();
                MessageBox.Show("اطلاعات با موفقیت ویرایش شد");
                FMoshtariEdit_Load(sender, e);
            }
        }

        private void txtboxEshterak_TextChanged(object sender, EventArgs e)
        {
            try
            {
                string selectwhere = "SELECT * FROM Customers WHERE Customer_Id = " + Convert.ToInt32(txtboxEshterak.Text);
                SqlDataAdapter adapter = new SqlDataAdapter(selectwhere, conn);
                DataTable DT = new DataTable();
                adapter.Fill(DT);

                txtboxCustomer.Text = DT.Rows[0].ItemArray[1].ToString();
                txtboxPhone.Text = DT.Rows[0].ItemArray[3].ToString();
                txtboxAddress.Text = DT.Rows[0].ItemArray[2].ToString();

                checkcode = true;

            }
            catch (Exception) 
            {
                txtboxAddress.Text = txtboxCustomer.Text = txtboxPhone.Text = " ";

                checkcode = false;
            }          
        }


        private void btnDelete_Click(object sender, EventArgs e)
        {
            try
            {
                string delete = "DELETE FROM Customers WHERE Customer_id = @id";

                conn.Open();
                SqlCommand command = new SqlCommand(delete, conn);
                command.Parameters.AddWithValue("@id", Convert.ToInt32(txtboxEshterak.Text));
                command.ExecuteNonQuery();
                conn.Close();
                MessageBox.Show("اطلاعات حذف شدند");
                FMoshtariEdit_Load(sender, e);
            }
            catch (Exception) 
            {
                MessageBox.Show("لطفا کد را وارد کنید");
            }            
        }


        private void FMoshtariEdit_Load(object sender, EventArgs e)
        {
            string select = "SELECT * FROM Customers";
            SqlDataAdapter adapter = new SqlDataAdapter(select, conn);
            DataTable DTable = new DataTable();
            adapter.Fill(DTable);
            dataGridView1.DataSource = DTable; 
        }
    }
}
