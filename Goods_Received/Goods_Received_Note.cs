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
using System.Web;
using System.Windows.Forms;

namespace Goods_Received
{
    public partial class Goods_Received_Note : Form
    {
        private String accountantName = "";
        public Goods_Received_Note(string accountantName)
        {
            InitializeComponent();
            this.accountantName = accountantName;
        }

        private void Receive_Note_Load()
        {
            SqlConnection conn = new SqlConnection();
            conn.ConnectionString = ConfigurationManager.ConnectionStrings["DBConnectionString"].ConnectionString;
            conn.Open();

            SqlCommand cmd = new SqlCommand($"SELECT * FROM Goods_Received_Note", conn);
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

        private void Goods_Receive_Note_Load(object sender, EventArgs e)
        {
            Receive_Note_Load();
            dataGridView1.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dataGridView1.ReadOnly = true;
        }

        private void Goods_Receive_Note_FormClosing(object sender, FormClosingEventArgs e)
        {
            try {
                if(e.CloseReason == CloseReason.UserClosing)
                {
                    if (MessageBox.Show("Are you sure to exit", "Goods Received Note", MessageBoxButtons.OKCancel, MessageBoxIcon.Information) == DialogResult.OK)
                    {
                        Environment.Exit(0);
                    }
                    else
                    {
                        e.Cancel = true;
                    }
                }
            } catch(Exception) { }
        }

        private void dataGridView1_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                String goodReceivedId = dataGridView1.Rows[e.RowIndex].Cells[0].Value.ToString();
                String importDate = dataGridView1.Rows[e.RowIndex].Cells[1].Value.ToString();
                String accountantName = dataGridView1.Rows[e.RowIndex].Cells[2].Value.ToString();

                Goods_Received_Details goods_Received_Details = new Goods_Received_Details();
                goods_Received_Details.LabelID = goodReceivedId;
                goods_Received_Details.LabelDate = importDate;
                goods_Received_Details.LabelAccountant = accountantName;

                this.Hide();
                goods_Received_Details.ShowDialog();
                this.Show();
            }
            catch (Exception)
            {

            }
        }

        private void btnLogOut_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.OK;
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            try
            {
                string newId = "GR-001";
                for (int rows = 0; rows < dataGridView1.RowCount - 1; rows++)
                { 
                    string goodReceivedId = dataGridView1.Rows[rows].Cells["goodReceivedId"].Value?.ToString();

                    if (goodReceivedId.StartsWith("GR"))
                    {
                        int count = Convert.ToInt32(goodReceivedId.Split('-')[1]);
                        newId = String.Format("GR-{0:000}", count+1);
                    }
                }

                Goods_Received_Form goods_Received_Form = new Goods_Received_Form(newId, accountantName);
                goods_Received_Form.LabelID = newId;
                this.Hide();
                goods_Received_Form.ShowDialog();
                Receive_Note_Load();
                this.Show();
            }
            catch (Exception) { }
        }

        private void Receive_Note_Remove()
        {
            try
            {
                string goodReceivedId = dataGridView1.CurrentRow.Cells["goodReceivedId"].Value.ToString();
                if (MessageBox.Show("Are you sure to delete note " + goodReceivedId + " ?", "Goods Received Note Deletetion", MessageBoxButtons.OKCancel, MessageBoxIcon.Information) == DialogResult.OK)
                {
                    SqlConnection conn = new SqlConnection();
                    conn.ConnectionString = ConfigurationManager.ConnectionStrings["DBConnectionString"].ConnectionString;
                    conn.Open();

                    SqlCommand cmd = new SqlCommand($"DELETE FROM Goods_Received_Details WHERE goodReceivedId = '{goodReceivedId}'", conn);
                    cmd.ExecuteNonQuery();
                    
                    cmd.CommandText = $"DELETE FROM Goods_Received_Note WHERE goodReceivedId = '{goodReceivedId}'";
                    cmd.ExecuteNonQuery();

                    MessageBox.Show("Deleted!");
                    conn.Close();
                    Receive_Note_Load();
                }
            }
            catch (Exception)
            {
                MessageBox.Show("Error");
            }
            
        }

        private void deleteToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Receive_Note_Remove();
        }
    }
}
