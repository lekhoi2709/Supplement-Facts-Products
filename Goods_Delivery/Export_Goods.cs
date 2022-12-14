using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Goods_Delivery
{
    public partial class Export_Goods : Form
    {
        List<ProductDetails> productDetailsList = new List<ProductDetails>();
        private string deliveryId = "";
        private string accountantName = "";

        public Export_Goods(string deliveryId, string accountantName)
        {
            InitializeComponent();
            this.deliveryId = deliveryId;
            this.accountantName = accountantName;
        }

        public String LabelID
        {
            get { return this.labelID.Text; }
            set { this.labelID.Text = value; }
        }

        private void Export_Goods_Load(object sender, EventArgs e)
        {
            dataGridView.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dataGridView.ReadOnly = true;

            SqlConnection conn = new SqlConnection();
            conn.ConnectionString = ConfigurationManager.ConnectionStrings["DBConnectionString"].ConnectionString;
            conn.Open();

            String sql = $"SELECT * FROM Products_Inventory";

            SqlCommand cmd = new SqlCommand(sql, conn);
            SqlDataAdapter adapter = new SqlDataAdapter(cmd);
            DataTable dataTable = new DataTable();
            adapter.Fill(dataTable);
            conn.Close();
            dataTable.Columns.Add("productStr", typeof(string), "productId + '_' + productName");
            cbBoxProductName.DataSource = dataTable;
            cbBoxProductName.DisplayMember = "productStr";
            cbBoxQuantity.SelectedText = "1";
        }

        private void AddProducts(String productName, String productId, int quantity, int price)
        {
            bool validate = productDetailsList.Any(id => string.Equals(id.ProductID, productId));
            if (!validate)
            {
                productDetailsList.Add(new ProductDetails(deliveryId, productName, productId, quantity, price, quantity * price));
                dataGridView.DataSource = productDetailsList.ToArray();
                MessageBox.Show("Insert Successful!");
            }
            else
            {
                MessageBox.Show("Insert Failed!");
            }
        }

        private void btnBack_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.OK;
        }

        private void Export_Goods_FormClosing(object sender, FormClosingEventArgs e)
        {
            try
            {
                if (e.CloseReason == CloseReason.UserClosing)
                {
                    if (MessageBox.Show("Are you sure to exit", "Goods Delivery Details", MessageBoxButtons.OKCancel, MessageBoxIcon.Information) == DialogResult.OK)
                    {
                        Environment.Exit(0);
                    }
                    else
                    {
                        e.Cancel = true;
                    }
                }
            }
            catch (Exception) { }
        }

        private void cbBoxProductName_SelectedIndexChanged(object sender, EventArgs e)
        {
            ComboBox cbBox = sender as ComboBox;
            string productStr = cbBox.GetItemText(cbBox.SelectedItem);
            string productName = "";
            if (productStr.Contains('_'))
            {
                productName = productStr.Split('_')[1];
            }
            cbBoxQuantity.Items.Clear();

            SqlConnection conn = new SqlConnection();
            conn.ConnectionString = ConfigurationManager.ConnectionStrings["DBConnectionString"].ConnectionString;
            conn.Open();

            String sql = $"SELECT quantity, price FROM Products_Inventory WHERE productName = '{productName}'";

            SqlCommand cmd = new SqlCommand(sql, conn);
            SqlDataAdapter adapter = new SqlDataAdapter(cmd);
            DataTable dataTable = new DataTable();
            adapter.Fill(dataTable);
            conn.Close();
            string quantityStr = "";
            for(int row = 0; row < dataTable.Rows.Count; row++)
            {
                labelPriceNumber.Text = dataTable.Rows[row]["price"].ToString();
                quantityStr = dataTable.Rows[row]["quantity"].ToString();
            }
            bool quantityRes = Int32.TryParse(quantityStr, out int quantity);
            if (quantityRes)
            {
                for (int count = 1; count <= quantity; count++)
                {
                    cbBoxQuantity.Items.Add(count);
                } 
            }
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            try
            {
                bool priceRes = Int32.TryParse(labelPriceNumber.Text, out int price);
                string quantityGetString = cbBoxQuantity.GetItemText(cbBoxQuantity.SelectedItem);
                bool quantityRes = Int32.TryParse(quantityGetString, out int quantity);

                string productStr = cbBoxProductName.GetItemText(cbBoxProductName.SelectedItem);
                string productId = productStr.Split('_')[0];
                string productName = productStr.Split('_')[1];

                if (priceRes && quantityRes)
                {
                    if (quantity > 0 || price > 0)
                    {
                        AddProducts(productName, productId, quantity, price);
                    }
                }
            }
            catch (Exception) { }
        }

        private void AddDeliveryNote()
        {
            SqlConnection conn = new SqlConnection();
            conn.ConnectionString = ConfigurationManager.ConnectionStrings["DBConnectionString"].ConnectionString;
            conn.Open();
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = conn;
            cmd.CommandText = @"INSERT INTO Goods_Delivery_Note VALUES (@deliveryId, @importDate, @accountantName, @totalPrice)";
            cmd.Parameters.AddWithValue("@deliveryId", deliveryId);
            cmd.Parameters.AddWithValue("@importDate", DateTime.Today.ToString("yyyy-MM-dd"));
            cmd.Parameters.AddWithValue("@accountantName", accountantName);
            cmd.Parameters.AddWithValue("@totalPrice", 0);

            cmd.ExecuteNonQuery();
            conn.Close();
        }

        private void UpdateDeliveryNote(double totalPrice)
        {
            SqlConnection conn = new SqlConnection();
            conn.ConnectionString = ConfigurationManager.ConnectionStrings["DBConnectionString"].ConnectionString;
            conn.Open();
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = conn;
            cmd.CommandText = $"UPDATE Goods_Delivery_Note SET totalPrice={totalPrice} WHERE deliveryId='{deliveryId}'";
            cmd.ExecuteNonQuery();
            conn.Close();
        }

        private void ConfirmReceipt(string productName, string productId, int quantity, int price)
        {
            double totalPrice = quantity * price;
            SqlConnection conn = new SqlConnection();
            conn.ConnectionString = ConfigurationManager.ConnectionStrings["DBConnectionString"].ConnectionString;
            conn.Open();

            SqlCommand cmd = new SqlCommand();
            cmd.Connection = conn;
            cmd.CommandText = @"INSERT INTO Goods_Delivery_Details VALUES (@deliveryId, @productName, @productId, @quantity, @price, @total)";
            cmd.Parameters.AddWithValue("@deliveryId", deliveryId);
            cmd.Parameters.AddWithValue("@productName", productName);
            cmd.Parameters.AddWithValue("@productId", productId);
            cmd.Parameters.AddWithValue("@quantity", quantity);
            cmd.Parameters.AddWithValue("@price", price);
            cmd.Parameters.AddWithValue("@total", totalPrice);

            cmd.ExecuteNonQuery();
            conn.Close();
        }

        private void btnConfirm_Click(object sender, EventArgs e)
        {
            try
            {
                if (MessageBox.Show("Are you sure to make an Good Delivery Note?", "Goods Delivery Details Confirmation", MessageBoxButtons.OKCancel, MessageBoxIcon.Information) == DialogResult.OK)
                {
                    bool flag = true;
                    double totalPrice = 0;
                    AddDeliveryNote();
                    if (!productDetailsList.Any())
                    {
                        MessageBox.Show("There is nothing to import");
                        flag = false;
                    }
                    else
                    {
                        foreach (ProductDetails productDetail in productDetailsList)
                        {
                            string productName = productDetail.ProductName;
                            string productId = productDetail.ProductID;
                            int quantity = productDetail.Quantity;
                            int price = productDetail.Price;
                            totalPrice += (quantity * price);
                            ConfirmReceipt(productName, productId, quantity, price);
                        }
                    }

                    if (flag)
                    {
                        UpdateDeliveryNote(totalPrice);
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
    }
}
