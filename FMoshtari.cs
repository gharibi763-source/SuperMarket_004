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
using System.Security.Cryptography.X509Certificates;


namespace SuperMarket_004
{
    public partial class FMoshtari : Form
    {
        public static string server = "Data Source=.; Initial Catalog= SuperMarket; Integrated Security = True";
        SqlConnection conn = new SqlConnection(server);
        public FMoshtari()
        {
            InitializeComponent();
        }

        public void NewCode() 
        {
            try
            {
                string maxcode = "SELECT MAX(Customer_Id) FROM Customers";
                SqlDataAdapter DA = new SqlDataAdapter(maxcode, conn);
                DataTable DT = new DataTable();
                DA.Fill(DT);
                txtboxEshterak.Text = (Convert.ToInt32(DT.Rows[0].ItemArray[0]) + 1).ToString();

            }
            catch (Exception) 
            {
                txtboxEshterak.Text = "1000";
            }

        }

        private void btnSaveCustomer_Click(object sender, EventArgs e)
        {
             

            if (string.IsNullOrWhiteSpace(txtboxEshterak.Text))
            {
                txtboxEshterak.Focus();
            }

            else if (string.IsNullOrWhiteSpace(txtboxCustomer.Text))
            {
                txtboxCustomer.Focus();
            }

            else if (string.IsNullOrWhiteSpace(txtboxAddress.Text))
            {
                txtboxAddress.Focus();
            }

            else if (string.IsNullOrWhiteSpace(txtboxPhone.Text))
            {
                txtboxPhone.Focus();
            }
            else 
            {
                try
                {
                    
                    string query = "INSERT INTO Customers(Customer_Id, Name,  Address, Phone)  VALUES(@id, @name, @address, @phone)";

                   
                    conn.Open();
                    SqlCommand comm = new SqlCommand(query, conn);

                    comm.Parameters.AddWithValue("@id",Convert.ToInt32(txtboxEshterak.Text));
                    comm.Parameters.AddWithValue("@name", txtboxCustomer.Text);
                    comm.Parameters.AddWithValue("@address", txtboxAddress.Text);
                    comm.Parameters.AddWithValue("@phone", Convert.ToInt64(txtboxPhone.Text));

                    comm.ExecuteNonQuery();
                    conn.Close();

                    txtboxAddress.Text = txtboxCustomer.Text = txtboxEshterak.Text = txtboxPhone.Text = " ";
                    txtboxCustomer.Focus();


                    MessageBox.Show("اطلاعات به درستی ثبت شد");
                    FMoshtari_Load(sender, e);
                }
                catch (Exception) 
                {
                    MessageBox.Show("Error");
                }            
            }
        }

        private void FMoshtari_Load(object sender, EventArgs e)
        {            
            NewCode();

            string selectAll = "SELECT * FROM Customers";
            SqlDataAdapter dataAD = new SqlDataAdapter(selectAll, conn);
            DataTable table = new DataTable();

            dataAD.Fill(table);
            dataGridView1.DataSource = table;
 
        }
    }
}
