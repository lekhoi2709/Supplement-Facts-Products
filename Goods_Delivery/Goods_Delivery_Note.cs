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

namespace Goods_Delivery
{
    public partial class Goods_Delivery_Note : Form
    {
        private string accountantName = "";
        public Goods_Delivery_Note(string accountantName)
        {
            InitializeComponent();
            this.accountantName = accountantName;
        }

        private void Delivery_Note_Load()
        {
            SqlConnection conn = new SqlConnection();
            conn.ConnectionString = ConfigurationManager.ConnectionStrings["DBConnectionString"].ConnectionString;
            conn.Open();

            SqlCommand cmd = new SqlCommand($"SELECT * FROM Goods_Delivery_Note", conn);
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

        private void Goods_Delivery_Note_Load(object sender, EventArgs e)
        {
            Delivery_Note_Load();
            dataGridView1.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dataGridView1.ReadOnly = true;
        }

        private void Goods_Delivery_Note_FormClosing(object sender, FormClosingEventArgs e)
        {
            try
            {
                if (e.CloseReason == CloseReason.UserClosing)
                {
                    if (MessageBox.Show("Are you sure to exit", "Goods Delivery Note", MessageBoxButtons.OKCancel, MessageBoxIcon.Information) == DialogResult.OK)
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

        private void btnLogOut_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.OK;
        }

        private void dataGridView1_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            String deliveryId = dataGridView1.Rows[e.RowIndex].Cells[0].Value.ToString();
            String exportDate = dataGridView1.Rows[e.RowIndex].Cells[1].Value.ToString();
            String accountantName = dataGridView1.Rows[e.RowIndex].Cells[2].Value.ToString();

            Goods_Delivery_Details details = new Goods_Delivery_Details();
            details.LabelID = deliveryId;
            details.LabelDate = exportDate;
            details.LabelAccountant = accountantName;

            this.Hide();
            details.ShowDialog();
            this.Show();
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            try
            {
                string newId = "GD-001";
                for (int rows = 0; rows < dataGridView1.RowCount - 1; rows++)
                {
                    string deliveryId = dataGridView1.Rows[rows].Cells["deliveryId"].Value?.ToString();
                    int count = Convert.ToInt32(deliveryId.Split('-')[1]);
                    newId = String.Format("GD-{0:000}", count + 1);
                }

                Export_Goods export_Goods = new Export_Goods(newId, accountantName);
                export_Goods.LabelID = newId;
                this.Hide();
                export_Goods.ShowDialog();
                Delivery_Note_Load();
                this.Show();
            }
            catch (Exception) { }
        }

        private void Delivery_Note_Remove()
        {
            try
            {
                string deliveryId = dataGridView1.CurrentRow.Cells["deliveryId"].Value.ToString();
                if (MessageBox.Show("Are you sure to delete note " + deliveryId + " ?", "Goods Received Note Deletetion", MessageBoxButtons.OKCancel, MessageBoxIcon.Information) == DialogResult.OK)
                {
                    SqlConnection conn = new SqlConnection();
                    conn.ConnectionString = ConfigurationManager.ConnectionStrings["DBConnectionString"].ConnectionString;
                    conn.Open();

                    SqlCommand cmd = new SqlCommand($"DELETE FROM Goods_Delivery_Details WHERE deliveryId = '{deliveryId}'", conn);
                    cmd.ExecuteNonQuery();

                    cmd.CommandText = $"DELETE FROM Goods_Delivery_Note WHERE deliveryId = '{deliveryId}'";
                    cmd.ExecuteNonQuery();

                    MessageBox.Show("Deleted!");
                    conn.Close();
                    Delivery_Note_Load();
                }
            }
            catch (Exception)
            {
                MessageBox.Show("Error");
            }

        }

        private void deleteToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Delivery_Note_Remove();
        }
    }
}
