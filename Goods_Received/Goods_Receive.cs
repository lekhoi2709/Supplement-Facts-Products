using System;
using System.Data;
using System.Windows.Forms;
using System.Data.SqlClient;
using System.Configuration;
using System.IO;

namespace Goods_Received
{
    public partial class Goods_Received_Form : Form
    {
        private String goodReceivedId = "";
        public Goods_Received_Form(String goodReceivedId)
        {
            InitializeComponent();
            this.goodReceivedId = goodReceivedId;  
        }

        private void ProductLoad()
        {
            SqlConnection conn = new SqlConnection();
            conn.ConnectionString = ConfigurationManager.ConnectionStrings["DBConnectionString"].ConnectionString;
            conn.Open();

            SqlCommand cmd = new SqlCommand($"SELECT * FROM Goods_Received_Details WHERE goodReceivedId='{goodReceivedId}'", conn);
            DataTable products = new DataTable();
            SqlDataAdapter adapter = new SqlDataAdapter(cmd);
            adapter.Fill(products);

            if (products.Rows.Count > 0)
            {
                dataGridView.DataSource = products;
            }
            else
            {
                MessageBox.Show("No Data");
            }
            conn.Close();
        }

        private void AddProducts(int quantity, int price, String productName, String productId)
        {
            double totalPrice = quantity * price;
            SqlConnection conn = new SqlConnection();
            conn.ConnectionString = ConfigurationManager.ConnectionStrings["DBConnectionString"].ConnectionString;
            conn.Open();

            SqlCommand cmd = new SqlCommand();
            cmd.Connection = conn;
            cmd.CommandText = @"INSERT INTO Goods_Received_Details VALUES (@goodReceivedId, @productName, @productId, @quantity, @price, @total)";
            cmd.Parameters.AddWithValue("@goodReceivedId", goodReceivedId);
            cmd.Parameters.AddWithValue("@productName", productName);
            cmd.Parameters.AddWithValue("@productId", productId);
            cmd.Parameters.AddWithValue("@quantity", quantity);
            cmd.Parameters.AddWithValue("@price", price);
            cmd.Parameters.AddWithValue("@total", totalPrice);

            cmd.ExecuteNonQuery();
            conn.Close();
            
        }

        private void Goods_Received_Form_Load(object sender, EventArgs e)
        {
            ProductLoad();
            dataGridView.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dataGridView.ReadOnly = true;
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            try
            {
                bool quantityRes = int.TryParse(txtQuantity.Text, out int quantity);
                bool priceRes = int.TryParse(txtPrice.Text, out int price);

                if (quantityRes && priceRes)
                {
                    
                    if(quantity <= 0)
                    {
                        MessageBox.Show("Quantity must higher than 0");
                    } 

                    else if(price <= 0)
                    {
                        MessageBox.Show("Price must higher than 0");
                    }

                    else
                    {
                        if (string.IsNullOrEmpty(txtProductName.Text) ||
                            string.IsNullOrEmpty(txtProductId.Text) ||
                            string.IsNullOrEmpty(txtQuantity.Text) ||
                            string.IsNullOrEmpty(txtPrice.Text))
                        {
                            MessageBox.Show("Please make sure that you've filled all the form.");
                        }

                        else if (!txtProductId.Text.StartsWith("P"))
                        {
                            MessageBox.Show("Product ID must be started with P");
                        }

                        else
                        {
                            AddProducts(quantity, price, txtProductName.Text, txtProductId.Text);
                            MessageBox.Show("Insert Successful!");
                            ProductLoad();
                            ClearForm(this.Controls);  
                        }
                    }
                }
            }
            catch (Exception)
            {
                MessageBox.Show("Error");
            }
        }

        private void ClearForm(Control.ControlCollection control)
        {
            foreach(Control ctrl in control)
            {
                if(ctrl is TextBox)
                {
                    ctrl.Text = String.Empty;
                } else
                {
                    ClearForm(ctrl.Controls);
                }
            }
        }

        private void Goods_Received_Form_FormClosing(object sender, FormClosingEventArgs e)
        {
            try
            {
                if (e.CloseReason == CloseReason.UserClosing)
                {
                    if (MessageBox.Show("Are you sure to exit", "Goods Received Details", MessageBoxButtons.OKCancel, MessageBoxIcon.Information) == DialogResult.OK)
                    {
                        Environment.Exit(0);
                    }
                    else
                    {
                        e.Cancel = true;
                    }
                }
            }
            catch (Exception)
            {

            }
        }

        private void btnBack_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.OK;
        }
    }
}
