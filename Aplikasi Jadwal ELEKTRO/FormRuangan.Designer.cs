namespace Aplikasi_Jadwal_ELEKTRO
{
    partial class FormRuangan
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
            this.label1 = new System.Windows.Forms.Label();
            this.btn_tambahdataRGN = new System.Windows.Forms.Button();
            this.RuanganTabel = new System.Windows.Forms.DataGridView();
            this.btnRefreshRGN = new System.Windows.Forms.Button();
            this.btn_editRGN = new System.Windows.Forms.Button();
            this.btn_deleteRGN = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.RuanganTabel)).BeginInit();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.Font = new System.Drawing.Font("Book Antiqua", 27.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.Color.White;
            this.label1.Location = new System.Drawing.Point(12, 23);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(1531, 149);
            this.label1.TabIndex = 0;
            this.label1.Text = "HALAMAN INPUT RUANGAN";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // btn_tambahdataRGN
            // 
            this.btn_tambahdataRGN.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btn_tambahdataRGN.Location = new System.Drawing.Point(20, 261);
            this.btn_tambahdataRGN.Name = "btn_tambahdataRGN";
            this.btn_tambahdataRGN.Size = new System.Drawing.Size(155, 50);
            this.btn_tambahdataRGN.TabIndex = 2;
            this.btn_tambahdataRGN.Text = "Tambah Data";
            this.btn_tambahdataRGN.UseVisualStyleBackColor = true;
            this.btn_tambahdataRGN.Click += new System.EventHandler(this.btn_tambahdataRGN_Click);
            // 
            // RuanganTabel
            // 
            this.RuanganTabel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.RuanganTabel.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.RuanganTabel.BackgroundColor = System.Drawing.Color.Silver;
            this.RuanganTabel.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.RuanganTabel.GridColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.RuanganTabel.Location = new System.Drawing.Point(20, 332);
            this.RuanganTabel.MultiSelect = false;
            this.RuanganTabel.Name = "RuanganTabel";
            this.RuanganTabel.ReadOnly = true;
            this.RuanganTabel.RowHeadersVisible = false;
            this.RuanganTabel.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.RuanganTabel.Size = new System.Drawing.Size(1512, 580);
            this.RuanganTabel.TabIndex = 3;
            // 
            // btnRefreshRGN
            // 
            this.btnRefreshRGN.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnRefreshRGN.ImageAlign = System.Drawing.ContentAlignment.BottomRight;
            this.btnRefreshRGN.Location = new System.Drawing.Point(1424, 261);
            this.btnRefreshRGN.Name = "btnRefreshRGN";
            this.btnRefreshRGN.Size = new System.Drawing.Size(108, 48);
            this.btnRefreshRGN.TabIndex = 12;
            this.btnRefreshRGN.Text = "Refresh";
            this.btnRefreshRGN.UseVisualStyleBackColor = true;
            this.btnRefreshRGN.Click += new System.EventHandler(this.btnRefreshRGN_Click);
            // 
            // btn_editRGN
            // 
            this.btn_editRGN.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btn_editRGN.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btn_editRGN.Location = new System.Drawing.Point(1168, 261);
            this.btn_editRGN.Name = "btn_editRGN";
            this.btn_editRGN.Size = new System.Drawing.Size(108, 48);
            this.btn_editRGN.TabIndex = 11;
            this.btn_editRGN.Text = "Edit";
            this.btn_editRGN.UseVisualStyleBackColor = true;
            this.btn_editRGN.Click += new System.EventHandler(this.btn_editRGN_Click);
            // 
            // btn_deleteRGN
            // 
            this.btn_deleteRGN.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btn_deleteRGN.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btn_deleteRGN.Location = new System.Drawing.Point(1296, 261);
            this.btn_deleteRGN.Name = "btn_deleteRGN";
            this.btn_deleteRGN.Size = new System.Drawing.Size(108, 48);
            this.btn_deleteRGN.TabIndex = 10;
            this.btn_deleteRGN.Text = "Delete";
            this.btn_deleteRGN.UseVisualStyleBackColor = true;
            this.btn_deleteRGN.Click += new System.EventHandler(this.btn_deleteRGN_Click);
            // 
            // FormRuangan
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Navy;
            this.ClientSize = new System.Drawing.Size(1556, 1041);
            this.Controls.Add(this.btnRefreshRGN);
            this.Controls.Add(this.btn_editRGN);
            this.Controls.Add(this.btn_deleteRGN);
            this.Controls.Add(this.RuanganTabel);
            this.Controls.Add(this.btn_tambahdataRGN);
            this.Controls.Add(this.label1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "FormRuangan";
            this.Text = "FormRuangan";
            this.Load += new System.EventHandler(this.FormRuangan_Load);
            ((System.ComponentModel.ISupportInitialize)(this.RuanganTabel)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button btn_tambahdataRGN;
        private System.Windows.Forms.DataGridView RuanganTabel;
        private System.Windows.Forms.Button btnRefreshRGN;
        private System.Windows.Forms.Button btn_editRGN;
        private System.Windows.Forms.Button btn_deleteRGN;
    }
}