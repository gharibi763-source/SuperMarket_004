using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;


namespace SuperMarket_004
{
    public partial class FUser : Form
    {
        public static string Address = "Data Source = .; Initial Catalog = SuperMarket; Integrated Security = True";
        SqlConnection connect = new SqlConnection(Address);
        public FUser()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
           

            if (textBox1.Text == "")
            {
                textBox1.Focus(); 
            }
            else if (textBox3.Text == "")
            {
                textBox3.Focus();
            }
            else 
            {
                int usertype;
                if (radioButton1.Checked == true)
                {
                    usertype = 1;
                }
                else 
                {
                    usertype = 0;
                }
                try
                {

                    string query = "INSERT INTO    [User](UserName_key, Password, UserType)   VALUES(@username_key, @password, @usertype)";


                    connect.Open();

                    SqlCommand command = new SqlCommand(query, connect);

                    command.Parameters.AddWithValue("@username_key", textBox1.Text);
                    command.Parameters.AddWithValue("@password", textBox3.Text);
                    command.Parameters.AddWithValue("@usertype", usertype);
                    command.ExecuteNonQuery();

                    connect.Close();
                    MessageBox.Show("اطلاعات کاربر جدید به درستی ثبت شد");
                    FUser_Load( sender,  e);

                }
                catch (Exception) 
                {
                    MessageBox.Show("اطلاعات تکراری است");
                }                
            }
        }


        private void FUser_Load(object sender, EventArgs e)
        {
            string selectAll = "SELECT * FROM [User]";
            SqlDataAdapter adapter = new SqlDataAdapter(selectAll, Address);

            DataTable DT = new DataTable();
            adapter.Fill(DT);
            dataGridView1.DataSource = DT;
        }
    }
}
