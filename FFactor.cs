using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;


using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Input;


namespace SuperMarket_004
{
    public partial class FFactor : Form
    {
        public static string server = "Data Source=.; Initial Catalog= SuperMarket; Integrated Security = True";
        SqlConnection conn = new SqlConnection(server);

        Boolean checkcustomer, checkkala, checkfactor;
        public FFactor()
        {
            InitializeComponent();
        }


        public void NewCode()
        {
            try
            {
                // کد فاکتور
                string max = "SELECT MAX(Factor_Id) FROM Factors";
                SqlDataAdapter adapter = new SqlDataAdapter(max, conn);
                DataTable DT = new DataTable();
                adapter.Fill(DT);

                lblFactor.Text = (Convert.ToInt32(DT.Rows[0].ItemArray[0]) + 1).ToString();

            }
            catch (Exception)
            {
                lblFactor.Text = "5000";
            }
        }

        public void ShowData() 
        {
            string showdata = "SELECT  Products.Product_Id AS [کد کالا] , Products.Product_Name AS [نام کالا],\r\n    Aghlam.Tedad AS [تعداد],   \r\n\t\tProducts.Price AS [قیمت واحد],    Products.Stock ,  Aghlam.Tedad *  Products.Price AS  [قیمت کل]     FROM  Products\r\nJOIN  Aghlam\r\nON Products.Product_Id  = Aghlam.Product_Id  WHERE Aghlam.Factor_Id = @factor_id  ";

            conn.Open();
            SqlCommand comman5 = new SqlCommand(showdata, conn);
            comman5.Parameters.AddWithValue("@factor_id", Convert.ToInt32(lblFactor.Text)); 

            SqlDataAdapter adapter3 = new SqlDataAdapter(comman5);
            DataTable DT = new DataTable();
            adapter3.Fill(DT);

            dataGridView1.DataSource = DT;
            conn.Close();
        }

        public void ShowFactor() 
        {           
            //  برگردادند جمع کل فاکتور فروش
        
            string sumquery = "SELECT SUM(Products.Price * Aghlam.Tedad) FROM Products JOIN Aghlam ON Aghlam.Product_Id = Products.Product_Id WHERE Aghlam.Factor_Id = @facid";
            conn.Open();
            SqlCommand command = new SqlCommand(sumquery, conn);

            command.Parameters.AddWithValue("@facid", Convert.ToInt32(lblFactor.Text));
            
            object result = command.ExecuteScalar();
            lblSumPrice.Text = result.ToString();            
        }


        private void FFactor_Load(object sender, EventArgs e)
        {
            //کد فاکتور اشتراک
            NewCode();

           
            // تاریخ خرید

            lblDate.Text =ShamsiDate.miladitoshamsi(DateTime.Now);

        }



        private void txtEshterak_TextChanged(object sender, EventArgs e)
        {
            try
            {
                string selectwhere = "SELECT * FROM Customers WHERE Customer_Id = @customId";
                conn.Open();
                SqlCommand command = new SqlCommand(selectwhere, conn);
                command.Parameters.AddWithValue("@customId", txtEshterak.Text);

                SqlDataAdapter adapter = new SqlDataAdapter(command);
                DataTable datatable = new DataTable();
                adapter.Fill(datatable);

                lblMoshtari.Text = datatable.Rows[0].ItemArray[1].ToString();
                lblAddress.Text  = datatable.Rows[0].ItemArray[2].ToString();
                lblPhone.Text    = datatable.Rows[0].ItemArray[3].ToString();
                checkcustomer = true;

            }
            catch (Exception) 
            {
               lblMoshtari.Text = lblAddress.Text = lblPhone.Text = " ";
                checkcustomer = false;
                
            }
            conn.Close();
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtEshterak.Text) && !int.TryParse(txtEshterak.Text, out int eshterak) || checkcustomer == false)
            {
                MessageBox.Show("کد معتبر نمیباشد یا همچین کدی وجود ندارد");
                txtEshterak.Focus();
            }
            else if (string.IsNullOrWhiteSpace(txtProductCode.Text) && !int.TryParse(txtProductCode.Text, out int procode) || checkkala == false)
            {
                MessageBox.Show("کد معتبر نمیباشد یا همچین کدی وجود ندارد");
                txtProductCode.Focus();
            }
            else if (string.IsNullOrWhiteSpace(txtNumber.Text) && !int.TryParse(txtNumber.Text, out int number))
            {
                MessageBox.Show("مقدار خالی یا غیر عدد است");
                txtNumber.Focus();
            }
            else if (Convert.ToInt32(txtNumber.Text) > Convert.ToInt32(lblStock.Text))
            {
                MessageBox.Show("تعداد بیشتر از موجودی است");
                txtNumber.Focus();
            }
            else
            {
                if (checkfactor == false) // برسی ثبت شدن اطلاعات فاکتور
                {
                    // INSERT INTO Factor
                    string insert = " INSERT INTO Factors(Factor_Id, UserName_key, Customer_Id, Date, Time) VALUES (@facid, @userkey, @customid, @date, @time)";
                    conn.Open();

                    SqlCommand comm3 = new SqlCommand(insert, conn);
                    comm3.Parameters.AddWithValue("@facid", Convert.ToInt32(lblFactor.Text));
                    comm3.Parameters.AddWithValue("@userkey", "سمانه");
                    comm3.Parameters.AddWithValue("@customid", Convert.ToInt32(txtEshterak.Text));
                    comm3.Parameters.AddWithValue("@date", lblDate.Text);
                    comm3.Parameters.AddWithValue("@time", DateTime.Now.ToLongTimeString());

                    comm3.ExecuteNonQuery();
                    conn.Close();

                   //checkfactor == true;
                }
               


                //INSERT INTO Aghlam

                string insertaghlam = "INSERT INTO Aghlam(Factor_Id, Product_Id, Tedad) VALUES(@fac_id, @pro_id, @tedad)";
                conn.Open();

                SqlCommand comm4 = new SqlCommand(insertaghlam, conn);

                comm4.Parameters.AddWithValue("@fac_id", Convert.ToInt32(lblFactor.Text));
                comm4.Parameters.AddWithValue("@pro_id", Convert.ToInt64(txtProductCode.Text));
                comm4.Parameters.AddWithValue("@tedad", Convert.ToInt32(txtNumber.Text));

                comm4.ExecuteNonQuery();
                

                // بروز رسانی موجودی کالا
                string updatestock = "UPDATE Products SET Stock = Stock - @number  WHERE Product_Id = @pro_id ";
                comm4 = new SqlCommand(updatestock, conn);

                comm4.Parameters.AddWithValue("@number", Convert.ToInt32(txtNumber.Text));
                comm4.Parameters.AddWithValue("@pro_id", Convert.ToInt32(txtProductCode.Text));
                comm4.ExecuteNonQuery();

                conn.Close();


                txtNumber.Text = txtProductCode.Text = " ";
                txtProductCode.Focus();

                //نشون دادن سبد خرید
                
                ShowData();
                ShowFactor();
            }
        }

        private void txtProductCode_TextChanged(object sender, EventArgs e)
        {
            try
            {
                string selectProduct = "SELECT * FROM products WHERE Product_Id = @proId";

                conn.Open();

                SqlCommand comm2 = new SqlCommand(selectProduct, conn);
                comm2.Parameters.AddWithValue("@proId", txtProductCode.Text);

                SqlDataAdapter adapter2 = new SqlDataAdapter(comm2);
                DataTable DT2 = new DataTable();
                adapter2.Fill(DT2);

                lblKala.Text = DT2.Rows[0].ItemArray[1].ToString();
                lblPrice.Text = DT2.Rows[0].ItemArray[2].ToString();
                lblStock.Text = DT2.Rows[0].ItemArray[3].ToString();

                checkkala = true;


            }
            catch (Exception) 
            {
                lblStock.Text = lblPrice.Text = lblKala.Text = " ";
                checkkala = false;
            }
            conn.Close();
        }
    }

}