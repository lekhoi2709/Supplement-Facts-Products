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
using System.Configuration;

namespace Goods_Delivery
{
    public partial class Log_In : Form
    {
        public Log_In()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            this.ActiveControl = txtUserName;
        }

        private void btnLogIn_Click(object sender, EventArgs e)
        {
            SqlConnection conn = new SqlConnection();
            conn.ConnectionString = ConfigurationManager.ConnectionStrings["DBConnectionString"].ConnectionString;
            conn.Open();

            String sql = $"SELECT * FROM Accountants WHERE userName='{txtUserName.Text}' AND password='{txtPassword.Text}'";

            SqlCommand cmd = new SqlCommand(sql, conn);
            SqlDataAdapter adapter = new SqlDataAdapter(cmd);
            DataTable dataTable = new DataTable();
            adapter.Fill(dataTable);

            if (dataTable.Rows.Count > 0)
            {
                MessageBox.Show("Login Successfull!");
                Goods_Delivery_Note goods_Delivery_Note = new Goods_Delivery_Note(txtUserName.Text);
                this.Hide();
                goods_Delivery_Note.ShowDialog();
                this.Show();
            }
            else
            {
                MessageBox.Show("Invalid Login.");
            }

            conn.Close();
        }

        private void Agent_Log_In_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (e.CloseReason == CloseReason.UserClosing)
            {
                if (MessageBox.Show("Are you sure to exit", "Log In", MessageBoxButtons.OKCancel, MessageBoxIcon.Information) == DialogResult.OK)
                {
                    Environment.Exit(0);
                }
                else
                {
                    e.Cancel = true;
                }
            }
        }
    }
}
