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

namespace Goods_Received
{
    public partial class Agent_Log_In : Form
    {
        public Agent_Log_In()
        {
            InitializeComponent();
        }

        private void Agent_Log_In_Load(object sender, EventArgs e)
        {
            this.ActiveControl = txtAgentName;
        }

        private void btnLogIn_Click(object sender, EventArgs e)
        {
            SqlConnection conn = new SqlConnection();
            conn.ConnectionString = ConfigurationManager.ConnectionStrings["DBConnectionString"].ConnectionString;
            conn.Open();

            String sql = $"SELECT * FROM Agents WHERE agentName='{txtAgentName.Text}' AND phoneNumber='{txtTelephone.Text}'";

            SqlCommand cmd = new SqlCommand(sql, conn);
            SqlDataAdapter adapter = new SqlDataAdapter(cmd);
            DataTable dataTable = new DataTable();
            adapter.Fill(dataTable);
            
            if(dataTable.Rows.Count > 0)
            {
                MessageBox.Show("Login Successfull!");
                Goods_Receive_Note goods_Receive_Note = new Goods_Receive_Note(txtAgentName.Text);
                this.Hide();
                goods_Receive_Note.ShowDialog();
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
