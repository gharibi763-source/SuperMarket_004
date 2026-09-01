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
using static System.Windows.Forms.VisualStyles.VisualStyleElement.Window;


namespace SuperMarket_004
{
    public partial class FLogin : Form
    {
        public static string server = "Data Source=.; Initial Catalog= SuperMarket; Integrated Security = True";
        SqlConnection conn = new SqlConnection(server);

        public FLogin()
        {
            InitializeComponent();
        }
        public  bool dialog;
        private void btnInput_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtUser.Text))
            {
                txtUser.Focus();
            }
            else if (string.IsNullOrWhiteSpace(txtPassword.Text))
            {
                txtPassword.Focus();
            }
            else 
            {
                try
                {
                    string query = "SELECT Password FROM [User] WHERE UserName_key = @user";

                    conn.Open();

                    SqlCommand cmd = new SqlCommand(query, conn);
                    cmd.Parameters.AddWithValue("@user", txtUser.Text);


                    SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                    DataTable dt = new DataTable();
                    adapter.Fill(dt);

                    string password1 = dt.Rows[0].ItemArray[0].ToString();
                    if (txtPassword.Text == password1)
                    {
                        // رمز عبور درست است
                        Memory.username = txtUser.Text;
                        dialog = true;
                        this.Close();

                    }
                    else
                    {
                        // اشتباه بودن رمز 
                        txtPassword.Focus();
                        MessageBox.Show("رمز عبور اشتباه است");
                        dialog = false;
                    }


                }
                catch (Exception) 
                {
                    MessageBox.Show("نام کاربری اشتباه است");
                }

                conn.Close();


            }
        }
    }
}
