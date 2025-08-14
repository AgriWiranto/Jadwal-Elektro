namespace Aplikasi_Jadwal_ELEKTRO
{
    partial class FormDosenMatkul
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
            this.cbDosen = new System.Windows.Forms.ComboBox();
            this.dgvMatkulDosen = new System.Windows.Forms.DataGridView();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.cbMatkul = new System.Windows.Forms.ComboBox();
            this.btnTambah = new System.Windows.Forms.Button();
            this.btnHapus = new System.Windows.Forms.Button();
            this.label4 = new System.Windows.Forms.Label();
            this.lblKodeMatkul = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.dgvMatkulDosen)).BeginInit();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.Font = new System.Drawing.Font("Book Antiqua", 27.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.Color.White;
            this.label1.Location = new System.Drawing.Point(-7, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(1531, 149);
            this.label1.TabIndex = 2;
            this.label1.Text = "DOSEN MATKUL";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // cbDosen
            // 
            this.cbDosen.FormattingEnabled = true;
            this.cbDosen.Location = new System.Drawing.Point(169, 238);
            this.cbDosen.Name = "cbDosen";
            this.cbDosen.Size = new System.Drawing.Size(393, 21);
            this.cbDosen.TabIndex = 3;
            this.cbDosen.SelectedIndexChanged += new System.EventHandler(this.cbDosen_SelectedIndexChanged);
            // 
            // dgvMatkulDosen
            // 
            this.dgvMatkulDosen.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvMatkulDosen.BackgroundColor = System.Drawing.SystemColors.ButtonHighlight;
            this.dgvMatkulDosen.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvMatkulDosen.Location = new System.Drawing.Point(77, 307);
            this.dgvMatkulDosen.MultiSelect = false;
            this.dgvMatkulDosen.Name = "dgvMatkulDosen";
            this.dgvMatkulDosen.ReadOnly = true;
            this.dgvMatkulDosen.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvMatkulDosen.Size = new System.Drawing.Size(1435, 364);
            this.dgvMatkulDosen.TabIndex = 4;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Times New Roman", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.ForeColor = System.Drawing.Color.White;
            this.label2.Location = new System.Drawing.Point(74, 234);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(74, 23);
            this.label2.TabIndex = 5;
            this.label2.Text = "Dosen :";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Times New Roman", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.ForeColor = System.Drawing.Color.White;
            this.label3.Location = new System.Drawing.Point(74, 721);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(125, 23);
            this.label3.TabIndex = 7;
            this.label3.Text = "Mata Kuliah :";
            // 
            // cbMatkul
            // 
            this.cbMatkul.FormattingEnabled = true;
            this.cbMatkul.Location = new System.Drawing.Point(218, 721);
            this.cbMatkul.Name = "cbMatkul";
            this.cbMatkul.Size = new System.Drawing.Size(393, 21);
            this.cbMatkul.TabIndex = 6;
            this.cbMatkul.SelectedIndexChanged += new System.EventHandler(this.cbMatkul_SelectedIndexChanged);
            // 
            // btnTambah
            // 
            this.btnTambah.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnTambah.ImageAlign = System.Drawing.ContentAlignment.BottomRight;
            this.btnTambah.Location = new System.Drawing.Point(662, 691);
            this.btnTambah.Name = "btnTambah";
            this.btnTambah.Size = new System.Drawing.Size(219, 44);
            this.btnTambah.TabIndex = 16;
            this.btnTambah.Text = "Tambah Mata Kuliah";
            this.btnTambah.UseVisualStyleBackColor = true;
            this.btnTambah.Click += new System.EventHandler(this.btnTambah_Click);
            // 
            // btnHapus
            // 
            this.btnHapus.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnHapus.ImageAlign = System.Drawing.ContentAlignment.BottomRight;
            this.btnHapus.Location = new System.Drawing.Point(887, 691);
            this.btnHapus.Name = "btnHapus";
            this.btnHapus.Size = new System.Drawing.Size(219, 44);
            this.btnHapus.TabIndex = 17;
            this.btnHapus.Text = "Hapus Mata Kuliah";
            this.btnHapus.UseVisualStyleBackColor = true;
            this.btnHapus.Click += new System.EventHandler(this.btnHapus_Click);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Times New Roman", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.ForeColor = System.Drawing.Color.White;
            this.label4.Location = new System.Drawing.Point(74, 682);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(126, 23);
            this.label4.TabIndex = 18;
            this.label4.Text = "Kode             :";
            // 
            // lblKodeMatkul
            // 
            this.lblKodeMatkul.AutoSize = true;
            this.lblKodeMatkul.Font = new System.Drawing.Font("Calibri", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblKodeMatkul.ForeColor = System.Drawing.Color.White;
            this.lblKodeMatkul.Location = new System.Drawing.Point(218, 683);
            this.lblKodeMatkul.Name = "lblKodeMatkul";
            this.lblKodeMatkul.Size = new System.Drawing.Size(0, 26);
            this.lblKodeMatkul.TabIndex = 19;
            // 
            // FormDosenMatkul
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoValidate = System.Windows.Forms.AutoValidate.EnableAllowFocusChange;
            this.BackColor = System.Drawing.Color.Navy;
            this.ClientSize = new System.Drawing.Size(1524, 963);
            this.Controls.Add(this.lblKodeMatkul);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.btnHapus);
            this.Controls.Add(this.btnTambah);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.cbMatkul);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.dgvMatkulDosen);
            this.Controls.Add(this.cbDosen);
            this.Controls.Add(this.label1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "FormDosenMatkul";
            this.Text = "FormDosenMatkul";
            this.Load += new System.EventHandler(this.FormDosenMatkul_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dgvMatkulDosen)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox cbDosen;
        private System.Windows.Forms.DataGridView dgvMatkulDosen;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.ComboBox cbMatkul;
        private System.Windows.Forms.Button btnTambah;
        private System.Windows.Forms.Button btnHapus;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label lblKodeMatkul;
    }
}