using System;
using System.Windows.Forms;

namespace Aplikasi_Jadwal_ELEKTRO
{
    partial class FormWaktu
    {
        private System.ComponentModel.IContainer components = null;
        private System.Windows.Forms.TabControl tabControlKelas;
        private System.Windows.Forms.Label lblStatus;
        private System.Windows.Forms.ProgressBar progressBar1;




        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
                components.Dispose();
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        private void InitializeComponent()
        {
            this.label1 = new System.Windows.Forms.Label();
            this.btnTambah = new System.Windows.Forms.Button();
            this.btnRefresh = new System.Windows.Forms.Button();
            this.btnReset = new System.Windows.Forms.Button();
            this.btnGenerate = new System.Windows.Forms.Button();
            this.dgvWaktu = new System.Windows.Forms.DataGridView();
            this.tabControlKelas = new System.Windows.Forms.TabControl();
            this.lblStatus = new System.Windows.Forms.Label();
            this.progressBar1 = new System.Windows.Forms.ProgressBar();
            ((System.ComponentModel.ISupportInitialize)(this.dgvWaktu)).BeginInit();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.Font = new System.Drawing.Font("Book Antiqua", 27.75F, System.Drawing.FontStyle.Bold);
            this.label1.ForeColor = System.Drawing.Color.White;
            this.label1.Location = new System.Drawing.Point(-3, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(1531, 149);
            this.label1.TabIndex = 1;
            this.label1.Text = "HALAMAN WAKTU";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.label1.Click += new System.EventHandler(this.label1_Click);
            // 
            // btnTambah
            // 
            this.btnTambah.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Bold);
            this.btnTambah.Location = new System.Drawing.Point(14, 176);
            this.btnTambah.Name = "btnTambah";
            this.btnTambah.Size = new System.Drawing.Size(155, 50);
            this.btnTambah.TabIndex = 2;
            this.btnTambah.Text = "Tambah Data";
            this.btnTambah.UseVisualStyleBackColor = true;
            this.btnTambah.Click += new System.EventHandler(this.btnTambah_Click);
            // 
            // btnRefresh
            // 
            this.btnRefresh.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Bold);
            this.btnRefresh.Location = new System.Drawing.Point(187, 176);
            this.btnRefresh.Name = "btnRefresh";
            this.btnRefresh.Size = new System.Drawing.Size(108, 50);
            this.btnRefresh.TabIndex = 3;
            this.btnRefresh.Text = "Hapus ";
            this.btnRefresh.UseVisualStyleBackColor = true;
            this.btnRefresh.Click += new System.EventHandler(this.btnHapusConfig_Click);
            // 
            // btnReset
            // 
            this.btnReset.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Bold);
            this.btnReset.Location = new System.Drawing.Point(1403, 176);
            this.btnReset.Name = "btnReset";
            this.btnReset.Size = new System.Drawing.Size(123, 50);
            this.btnReset.TabIndex = 4;
            this.btnReset.Text = "Reset Data";
            this.btnReset.UseVisualStyleBackColor = true;
            this.btnReset.Click += new System.EventHandler(this.btnReset_Click);
            // 
            // btnGenerate
            // 
            this.btnGenerate.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Bold);
            this.btnGenerate.Location = new System.Drawing.Point(1217, 176);
            this.btnGenerate.Name = "btnGenerate";
            this.btnGenerate.Size = new System.Drawing.Size(169, 50);
            this.btnGenerate.TabIndex = 5;
            this.btnGenerate.Text = "Generate Waktu";
            this.btnGenerate.UseVisualStyleBackColor = true;
            this.btnGenerate.Click += new System.EventHandler(this.btnGenerate_Click);
            // 
            // dgvWaktu
            // 
            this.dgvWaktu.AllowUserToAddRows = false;
            this.dgvWaktu.AllowUserToDeleteRows = false;
            this.dgvWaktu.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dgvWaktu.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvWaktu.BackgroundColor = System.Drawing.Color.White;
            this.dgvWaktu.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvWaktu.GridColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.dgvWaktu.Location = new System.Drawing.Point(14, 247);
            this.dgvWaktu.MultiSelect = false;
            this.dgvWaktu.Name = "dgvWaktu";
            this.dgvWaktu.ReadOnly = true;
            this.dgvWaktu.RowHeadersVisible = false;
            this.dgvWaktu.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvWaktu.Size = new System.Drawing.Size(1512, 398);
            this.dgvWaktu.TabIndex = 6;
            this.dgvWaktu.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvWaktu_CellContentClick);
            // 
            // tabControlKelas
            // 
            this.tabControlKelas.Location = new System.Drawing.Point(14, 245);
            this.tabControlKelas.Name = "tabControlKelas";
            this.tabControlKelas.SelectedIndex = 0;
            this.tabControlKelas.Size = new System.Drawing.Size(1512, 400);
            this.tabControlKelas.TabIndex = 7;
            // 
            // lblStatus
            // 
            this.lblStatus.AutoSize = true;
            this.lblStatus.Font = new System.Drawing.Font("Segoe UI", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblStatus.ForeColor = System.Drawing.Color.White;
            this.lblStatus.Location = new System.Drawing.Point(20, 840);
            this.lblStatus.Name = "lblStatus";
            this.lblStatus.Size = new System.Drawing.Size(270, 25);
            this.lblStatus.TabIndex = 8;
            this.lblStatus.Text = "TAMPILAN: Konfigurasi Awal";
            // 
            // progressBar1
            // 
            this.progressBar1.Location = new System.Drawing.Point(195, 651);
            this.progressBar1.Name = "progressBar1";
            this.progressBar1.Size = new System.Drawing.Size(1100, 23);
            this.progressBar1.Style = System.Windows.Forms.ProgressBarStyle.Continuous;
            this.progressBar1.TabIndex = 0;
            this.progressBar1.Visible = false;
            // 
            // FormWaktu
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Navy;
            this.ClientSize = new System.Drawing.Size(1540, 1002);
            this.Controls.Add(this.progressBar1);
            this.Controls.Add(this.dgvWaktu);
            this.Controls.Add(this.btnGenerate);
            this.Controls.Add(this.btnReset);
            this.Controls.Add(this.btnRefresh);
            this.Controls.Add(this.btnTambah);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.tabControlKelas);
            this.Controls.Add(this.lblStatus);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "FormWaktu";
            this.Text = "FormWaktu";
            this.Load += new System.EventHandler(this.FormWaktu_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dgvWaktu)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        private void dgvWaktu_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            //throw new NotImplementedException();
        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button btnTambah;
        private System.Windows.Forms.Button btnRefresh;
        private System.Windows.Forms.Button btnReset;
        private System.Windows.Forms.Button btnGenerate;
        private DataGridView dgvWaktu;
    }
}
