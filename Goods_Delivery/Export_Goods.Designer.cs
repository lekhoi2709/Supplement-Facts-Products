namespace Goods_Delivery
{
    partial class Export_Goods
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.labelID = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.btnConfirm = new System.Windows.Forms.Button();
            this.btnBack = new System.Windows.Forms.Button();
            this.btnAdd = new System.Windows.Forms.Button();
            this.label5 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.dataGridView = new System.Windows.Forms.DataGridView();
            this.cbBoxProductName = new System.Windows.Forms.ComboBox();
            this.labelPriceNumber = new System.Windows.Forms.Label();
            this.cbBoxQuantity = new System.Windows.Forms.ComboBox();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView)).BeginInit();
            this.SuspendLayout();
            // 
            // labelID
            // 
            this.labelID.AutoSize = true;
            this.labelID.Font = new System.Drawing.Font("Microsoft Sans Serif", 8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelID.Location = new System.Drawing.Point(95, 464);
            this.labelID.Name = "labelID";
            this.labelID.Size = new System.Drawing.Size(52, 17);
            this.labelID.TabIndex = 34;
            this.labelID.Text = "label7";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(12, 465);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(77, 16);
            this.label6.TabIndex = 33;
            this.label6.Text = "Receipt Id : ";
            // 
            // btnConfirm
            // 
            this.btnConfirm.Location = new System.Drawing.Point(476, 394);
            this.btnConfirm.Margin = new System.Windows.Forms.Padding(4);
            this.btnConfirm.Name = "btnConfirm";
            this.btnConfirm.Size = new System.Drawing.Size(368, 55);
            this.btnConfirm.TabIndex = 32;
            this.btnConfirm.Text = "Confirm";
            this.btnConfirm.UseVisualStyleBackColor = true;
            this.btnConfirm.Click += new System.EventHandler(this.btnConfirm_Click);
            // 
            // btnBack
            // 
            this.btnBack.Location = new System.Drawing.Point(664, 333);
            this.btnBack.Margin = new System.Windows.Forms.Padding(4);
            this.btnBack.Name = "btnBack";
            this.btnBack.Size = new System.Drawing.Size(180, 55);
            this.btnBack.TabIndex = 31;
            this.btnBack.Text = "Back";
            this.btnBack.UseVisualStyleBackColor = true;
            this.btnBack.Click += new System.EventHandler(this.btnBack_Click);
            // 
            // btnAdd
            // 
            this.btnAdd.Location = new System.Drawing.Point(476, 332);
            this.btnAdd.Margin = new System.Windows.Forms.Padding(4);
            this.btnAdd.Name = "btnAdd";
            this.btnAdd.Size = new System.Drawing.Size(180, 55);
            this.btnAdd.TabIndex = 29;
            this.btnAdd.Text = "Add";
            this.btnAdd.UseVisualStyleBackColor = true;
            this.btnAdd.Click += new System.EventHandler(this.btnAdd_Click);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(412, 403);
            this.label5.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(36, 16);
            this.label5.TabIndex = 28;
            this.label5.Text = "VND";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(13, 403);
            this.label4.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(44, 16);
            this.label4.TabIndex = 26;
            this.label4.Text = "Price: ";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(13, 366);
            this.label3.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(61, 16);
            this.label3.TabIndex = 24;
            this.label3.Text = "Quantity: ";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(10, 330);
            this.label1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(99, 16);
            this.label1.TabIndex = 20;
            this.label1.Text = "Product Name: ";
            // 
            // dataGridView
            // 
            this.dataGridView.ColumnHeadersHeight = 29;
            this.dataGridView.Location = new System.Drawing.Point(13, 13);
            this.dataGridView.Margin = new System.Windows.Forms.Padding(4);
            this.dataGridView.Name = "dataGridView";
            this.dataGridView.RowHeadersWidth = 51;
            this.dataGridView.Size = new System.Drawing.Size(831, 293);
            this.dataGridView.TabIndex = 30;
            // 
            // cbBoxProductName
            // 
            this.cbBoxProductName.FormattingEnabled = true;
            this.cbBoxProductName.Location = new System.Drawing.Point(126, 327);
            this.cbBoxProductName.Name = "cbBoxProductName";
            this.cbBoxProductName.Size = new System.Drawing.Size(322, 24);
            this.cbBoxProductName.TabIndex = 35;
            this.cbBoxProductName.SelectedIndexChanged += new System.EventHandler(this.cbBoxProductName_SelectedIndexChanged);
            // 
            // labelPriceNumber
            // 
            this.labelPriceNumber.AutoSize = true;
            this.labelPriceNumber.Location = new System.Drawing.Point(123, 403);
            this.labelPriceNumber.Name = "labelPriceNumber";
            this.labelPriceNumber.Size = new System.Drawing.Size(14, 16);
            this.labelPriceNumber.TabIndex = 37;
            this.labelPriceNumber.Text = "0";
            // 
            // cbBoxQuantity
            // 
            this.cbBoxQuantity.FormattingEnabled = true;
            this.cbBoxQuantity.Location = new System.Drawing.Point(126, 363);
            this.cbBoxQuantity.Name = "cbBoxQuantity";
            this.cbBoxQuantity.Size = new System.Drawing.Size(100, 24);
            this.cbBoxQuantity.TabIndex = 39;
            // 
            // Export_Goods
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(858, 490);
            this.Controls.Add(this.cbBoxQuantity);
            this.Controls.Add(this.labelPriceNumber);
            this.Controls.Add(this.cbBoxProductName);
            this.Controls.Add(this.labelID);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.btnConfirm);
            this.Controls.Add(this.btnBack);
            this.Controls.Add(this.btnAdd);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.dataGridView);
            this.Name = "Export_Goods";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Goods Delivery Details";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Export_Goods_FormClosing);
            this.Load += new System.EventHandler(this.Export_Goods_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label labelID;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Button btnConfirm;
        private System.Windows.Forms.Button btnBack;
        private System.Windows.Forms.Button btnAdd;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.DataGridView dataGridView;
        private System.Windows.Forms.ComboBox cbBoxProductName;
        private System.Windows.Forms.Label labelPriceNumber;
        private System.Windows.Forms.ComboBox cbBoxQuantity;
    }
}