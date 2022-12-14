using System;
using System.Data;
using System.Windows.Forms;
using System.Data.SqlClient;
using System.Configuration;
using System.IO;
using System.Collections.Generic;
using System.Linq;

namespace Goods_Received
{
    public partial class Goods_Received_Form : Form
    {
        List<Add_Products_Details> products = new List<Add_Products_Details>();
        private string goodReceivedId = "";
        private string accountantName = "";

        public Goods_Received_Form(string goodReceivedId, string accountantName)
        {
            InitializeComponent();        
            this.goodReceivedId = goodReceivedId;
            this.accountantName = accountantName;
        }

        public String LabelID
        {
            get { return this.labelID.Text;  }
            set { this.labelID.Text = value; }
        }

        private void ConfirmReceipt(string productName, string productId, int quantity, int price)
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

        private void AddGoodNote()
        {
            SqlConnection conn = new SqlConnection();
            conn.ConnectionString = ConfigurationManager.ConnectionStrings["DBConnectionString"].ConnectionString;
            conn.Open();
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = conn;
            cmd.CommandText = @"INSERT INTO Goods_Received_Note VALUES (@goodReceivedId, @importDate, @accountantName, @totalPrice)";
            cmd.Parameters.AddWithValue("@goodReceivedId", goodReceivedId);
            cmd.Parameters.AddWithValue("@importDate", DateTime.Today.ToString("yyyy-MM-dd"));
            cmd.Parameters.AddWithValue("@accountantName", accountantName);
            cmd.Parameters.AddWithValue("@totalPrice", 0);

            cmd.ExecuteNonQuery();
            conn.Close();
        }

        private void AddProducts(String productName, String productId, int quantity, int price)
        { 
            products.Add(new Add_Products_Details(goodReceivedId, productName, productId, quantity, price, quantity * price));
            dataGridView.DataSource = products.ToArray();
        }

        private void Goods_Received_Form_Load(object sender, EventArgs e)
        {
            
            dataGridView.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dataGridView.ReadOnly = true;
        }

        private bool ProductNameValidate()
        {
            SqlConnection conn = new SqlConnection();
            conn.ConnectionString = ConfigurationManager.ConnectionStrings["DBConnectionString"].ConnectionString;
            conn.Open();

            SqlCommand cmd = new SqlCommand($"SELECT productName FROM Products_Inventory WHERE productId='{txtProductId.Text}'", conn);
            DataTable noteTable = new DataTable();
            SqlDataAdapter adapter = new SqlDataAdapter(cmd);
            adapter.Fill(noteTable);

            if (noteTable.Rows.Count > 0)
            {
                string productName = noteTable.Rows[0][0].ToString();
                if (string.Compare(productName, txtProductName.Text) == 0)
                {
                    conn.Close();
                    return true;
                }
                
                conn.Close();
                return false;
            }
            else
            {
                conn.Close();
                return true;
            }
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
                            bool containId = products.Any(product => product.ProductID == txtProductId.Text);
                            if (containId)
                            {
                                MessageBox.Show("Product Id is existed!");
                            }
                            else
                            {
                                bool flag = ProductNameValidate();
                                if(flag)
                                { 
                                    AddProducts(txtProductName.Text, txtProductId.Text, quantity, price);
                                    MessageBox.Show("Insert Successful!");
                                    ClearForm(this.Controls);
                                }
                                else
                                {
                                    MessageBox.Show("Product Name and ID do not match!");
                                }
                            }
                        }
                    }
                }
            }
            catch (Exception)
            {
                MessageBox.Show("Error!");
            } 
        }

        private void ClearForm(Control.ControlCollection control)
        {
            foreach (Control ctrl in control)
            {
                if (ctrl is TextBox)
                {
                    ctrl.Text = String.Empty;
                }
                else
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

        private void UpdateGoodNote(double totalPrice)
        {
            SqlConnection conn = new SqlConnection();
            conn.ConnectionString = ConfigurationManager.ConnectionStrings["DBConnectionString"].ConnectionString;
            conn.Open();
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = conn;
            cmd.CommandText = $"UPDATE Goods_Received_Note SET totalPrice={totalPrice} WHERE goodReceivedId='{goodReceivedId}'";
            cmd.ExecuteNonQuery();
            conn.Close();
        }

        private void btnConfirm_Click(object sender, EventArgs e)
        {
            try {
                if (MessageBox.Show("Are you sure to make an Good Received Note?", "Goods Received Details Confirmation", MessageBoxButtons.OKCancel, MessageBoxIcon.Information) == DialogResult.OK)
                {
                    bool flag = true;
                    double totalPrice = 0;
                    AddGoodNote();
                    if (!products.Any())
                    {
                        MessageBox.Show("There is nothing to import");
                        flag = false;
                    }
                    else
                    {
                        foreach (Add_Products_Details goodNote in products)
                        {
                            string productName = goodNote.ProductName;
                            string productId = goodNote.ProductID;
                            int quantity = goodNote.Quantity;
                            int price = goodNote.Price;
                            totalPrice += (quantity * price);
                            ConfirmReceipt(productName, productId, quantity, price);
                        }
                    }

                    if (flag)
                    {
                        UpdateGoodNote(totalPrice);
                        MessageBox.Show("Import Successful!");
                        this.DialogResult = DialogResult.OK;
                    }
                    else
                    {
                        MessageBox.Show("Import Failed!");
                    }
                }
            }
            catch (Exception)
            {
                MessageBox.Show("There is an error.");
            }
        }

        private void dataGridView_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            string productId = dataGridView.Rows[e.RowIndex].Cells[2].Value.ToString();
            bool containId = products.Any(product => product.ProductID == productId);

            if(MessageBox.Show("Do you want to DELETE product " + productId, "Delete Confirmation", MessageBoxButtons.OKCancel, MessageBoxIcon.Information) == DialogResult.OK)
            {
                if (containId)
                {
                    products.RemoveAll(item => item.ProductID == productId);
                    MessageBox.Show("Delete Successful!");
                    dataGridView.DataSource = products.ToArray();
                }

                else
                {
                    MessageBox.Show("Delete Failed!");
                }
            }

        }
    }
}
