using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Goods_Received
{
    public partial class Goods_Received_Details : Form
    {
        public Goods_Received_Details()
        {
            InitializeComponent();
        }

        public String LabelID
        {
            get { return this.labelID.Text; }
            set { this.labelID.Text = value; }
        }

        public String LabelDate
        {
            get { return this.labelDate.Text; }
            set { this.labelDate.Text = value; }
        }

        public String LabelAccountant
        {
            get { return this.labelAccoutant.Text; }
            set { this.labelAccoutant.Text = value; }
        }

        private void btnBack_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.OK;
        }

        private void Goods_Received_Details_FormClosing(object sender, FormClosingEventArgs e)
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
            catch (Exception) { }
        }

        public void ProductLoad()
        {
            String goodsReceivedId = LabelID;
            SqlConnection conn = new SqlConnection();
            conn.ConnectionString = ConfigurationManager.ConnectionStrings["DBConnectionString"].ConnectionString;
            conn.Open();

            SqlCommand cmd = new SqlCommand($"SELECT * FROM Goods_Received_Details WHERE goodReceivedId ='{goodsReceivedId}'", conn);
            DataTable noteTable = new DataTable();
            SqlDataAdapter adapter = new SqlDataAdapter(cmd);
            adapter.Fill(noteTable);

            if (noteTable.Rows.Count > 0)
            {
                dataGridView1.DataSource = noteTable;
            }
            else
            {
                MessageBox.Show("No Data");
            }
            conn.Close();
        }

        private void Goods_Received_Details_Load(object sender, EventArgs e)
        {
            ProductLoad();
            dataGridView1.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dataGridView1.ReadOnly = true;
        }
    }
}
