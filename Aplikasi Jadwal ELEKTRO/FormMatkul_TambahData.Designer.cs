namespace Aplikasi_Jadwal_ELEKTRO
{
    partial class FormMatkul_TambahData
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
            this.btnKembaliMK = new System.Windows.Forms.Button();
            this.tb_sks = new System.Windows.Forms.TextBox();
            this.tb_NamaMatkulMK = new System.Windows.Forms.TextBox();
            this.sks = new System.Windows.Forms.Label();
            this.nama_matkul = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.btnSimpanMK = new System.Windows.Forms.Button();
            this.lbltitleMK = new System.Windows.Forms.Label();
            this.tb_KodeMatkul = new System.Windows.Forms.TextBox();
            this.cmbRuangan = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // btnKembaliMK
            // 
            this.btnKembaliMK.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnKembaliMK.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnKembaliMK.Location = new System.Drawing.Point(410, 450);
            this.btnKembaliMK.Name = "btnKembaliMK";
            this.btnKembaliMK.Size = new System.Drawing.Size(116, 34);
            this.btnKembaliMK.TabIndex = 26;
            this.btnKembaliMK.Text = "Kembali";
            this.btnKembaliMK.UseVisualStyleBackColor = true;
            this.btnKembaliMK.Click += new System.EventHandler(this.btnKembaliMK_Click);
            // 
            // tb_sks
            // 
            this.tb_sks.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tb_sks.Location = new System.Drawing.Point(170, 204);
            this.tb_sks.Multiline = true;
            this.tb_sks.Name = "tb_sks";
            this.tb_sks.Size = new System.Drawing.Size(354, 32);
            this.tb_sks.TabIndex = 22;
            // 
            // tb_NamaMatkulMK
            // 
            this.tb_NamaMatkulMK.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tb_NamaMatkulMK.Location = new System.Drawing.Point(170, 146);
            this.tb_NamaMatkulMK.Multiline = true;
            this.tb_NamaMatkulMK.Name = "tb_NamaMatkulMK";
            this.tb_NamaMatkulMK.Size = new System.Drawing.Size(354, 32);
            this.tb_NamaMatkulMK.TabIndex = 21;
            this.tb_NamaMatkulMK.TextChanged += new System.EventHandler(this.tb_NamaMatkulMK_TextChanged);
            // 
            // sks
            // 
            this.sks.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.sks.AutoSize = true;
            this.sks.Font = new System.Drawing.Font("Book Antiqua", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.sks.Location = new System.Drawing.Point(25, 210);
            this.sks.Name = "sks";
            this.sks.Size = new System.Drawing.Size(37, 20);
            this.sks.TabIndex = 19;
            this.sks.Text = "SKS";
            // 
            // nama_matkul
            // 
            this.nama_matkul.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.nama_matkul.AutoSize = true;
            this.nama_matkul.Font = new System.Drawing.Font("Book Antiqua", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.nama_matkul.Location = new System.Drawing.Point(23, 152);
            this.nama_matkul.Name = "nama_matkul";
            this.nama_matkul.Size = new System.Drawing.Size(108, 20);
            this.nama_matkul.TabIndex = 18;
            this.nama_matkul.Text = "Nama Matkul";
            this.nama_matkul.Click += new System.EventHandler(this.label1_Click);
            // 
            // label2
            // 
            this.label2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Book Antiqua", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(23, 96);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(104, 20);
            this.label2.TabIndex = 16;
            this.label2.Text = "Kode Matkul";
            // 
            // btnSimpanMK
            // 
            this.btnSimpanMK.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnSimpanMK.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnSimpanMK.Location = new System.Drawing.Point(269, 450);
            this.btnSimpanMK.Name = "btnSimpanMK";
            this.btnSimpanMK.Size = new System.Drawing.Size(118, 34);
            this.btnSimpanMK.TabIndex = 15;
            this.btnSimpanMK.Text = "Simpan";
            this.btnSimpanMK.UseVisualStyleBackColor = true;
            this.btnSimpanMK.Click += new System.EventHandler(this.btnSimpanMK_Click);
            // 
            // lbltitleMK
            // 
            this.lbltitleMK.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.lbltitleMK.AutoSize = true;
            this.lbltitleMK.Font = new System.Drawing.Font("Book Antiqua", 18F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbltitleMK.Location = new System.Drawing.Point(130, 28);
            this.lbltitleMK.Name = "lbltitleMK";
            this.lbltitleMK.Size = new System.Drawing.Size(317, 28);
            this.lbltitleMK.TabIndex = 14;
            this.lbltitleMK.Text = "TAMBAH MATA KULIAH\r\n";
            this.lbltitleMK.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // tb_KodeMatkul
            // 
            this.tb_KodeMatkul.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tb_KodeMatkul.Location = new System.Drawing.Point(170, 90);
            this.tb_KodeMatkul.Multiline = true;
            this.tb_KodeMatkul.Name = "tb_KodeMatkul";
            this.tb_KodeMatkul.Size = new System.Drawing.Size(354, 32);
            this.tb_KodeMatkul.TabIndex = 27;
            // 
            // cmbRuangan
            // 
            this.cmbRuangan.FormattingEnabled = true;
            this.cmbRuangan.Location = new System.Drawing.Point(172, 262);
            this.cmbRuangan.Name = "cmbRuangan";
            this.cmbRuangan.Size = new System.Drawing.Size(352, 21);
            this.cmbRuangan.TabIndex = 30;
            // 
            // label1
            // 
            this.label1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Book Antiqua", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(25, 264);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(73, 20);
            this.label1.TabIndex = 31;
            this.label1.Text = "Ruangan";
            // 
            // FormMatkul_TambahData
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(551, 511);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.cmbRuangan);
            this.Controls.Add(this.tb_KodeMatkul);
            this.Controls.Add(this.btnKembaliMK);
            this.Controls.Add(this.tb_sks);
            this.Controls.Add(this.tb_NamaMatkulMK);
            this.Controls.Add(this.sks);
            this.Controls.Add(this.nama_matkul);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.btnSimpanMK);
            this.Controls.Add(this.lbltitleMK);
            this.Name = "FormMatkul_TambahData";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "FormMatkul_TambahData";
            this.Load += new System.EventHandler(this.FormMatkul_TambahData_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnKembaliMK;
        private System.Windows.Forms.TextBox tb_sks;
        private System.Windows.Forms.TextBox tb_NamaMatkulMK;
        private System.Windows.Forms.Label sks;
        private System.Windows.Forms.Label nama_matkul;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button btnSimpanMK;
        private System.Windows.Forms.Label lbltitleMK;
        private System.Windows.Forms.TextBox tb_KodeMatkul;
        private System.Windows.Forms.ComboBox cmbRuangan;
        private System.Windows.Forms.Label label1;
    }
}