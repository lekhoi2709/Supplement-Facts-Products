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

namespace Goods_Received
{
    public partial class Goods_Receive_Note : Form
    {
        private String agentName = "";
        public Goods_Receive_Note(string agentName)
        {
            InitializeComponent();
            this.agentName = agentName;
        }

        private void Receive_Note_Load()
        {
            SqlConnection conn = new SqlConnection();
            conn.ConnectionString = ConfigurationManager.ConnectionStrings["DBConnectionString"].ConnectionString;
            conn.Open();

            SqlCommand cmd = new SqlCommand($"SELECT * FROM Goods_Received_Note WHERE agentName = '{agentName}'", conn);
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

        private void Add_Goods_Received_Note(String goodReceivedId, String accountantName)
        {
            SqlConnection conn = new SqlConnection();
            conn.ConnectionString = ConfigurationManager.ConnectionStrings["DBConnectionString"].ConnectionString;
            conn.Open();

            SqlCommand cmd = new SqlCommand();
            cmd.Connection = conn;
            cmd.CommandText = @"INSERT INTO Goods_Received_Note VALUES (@goodReceivedId, @importDate, @agentName, @accountantName)";
            cmd.Parameters.AddWithValue("@goodReceivedId", goodReceivedId);
            cmd.Parameters.AddWithValue("@importDate", DateTime.Today.ToString("yyyy-MM-dd"));
            cmd.Parameters.AddWithValue("@agentName", agentName);
            cmd.Parameters.AddWithValue("@accountantName", accountantName);

            cmd.ExecuteNonQuery();
            conn.Close();
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
                if (goodReceivedId.StartsWith("GR"))
                {
                    Goods_Received_Form goods_Received_Form = new Goods_Received_Form(goodReceivedId);
                    this.Hide();
                    goods_Received_Form.ShowDialog();
                    this.Show();
                }
                if(goodReceivedId == "")
                {

                }
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
                if (String.IsNullOrEmpty(txtAccountantName.Text)){
                    MessageBox.Show("Accountant cannot be empty");
                }
                else if (String.IsNullOrEmpty(txtNoteId.Text)){
                    MessageBox.Show("Note ID cannot be empty");
                }
                else
                {
                    String accountantName = txtAccountantName.Text;
                    String noteId = txtNoteId.Text;
                
                    if (noteId.StartsWith("GR"))
                    {
                        Add_Goods_Received_Note(noteId, accountantName);
                        MessageBox.Show("Add successfull!");
                        Receive_Note_Load();
                    }
                    else
                    {
                        MessageBox.Show("Invalid format for Note ID!" + "\n" + "\n" + "(Note ID must start with GR)");
                    }
               
                }
            }
            catch (Exception) { }
        }
    }
}
