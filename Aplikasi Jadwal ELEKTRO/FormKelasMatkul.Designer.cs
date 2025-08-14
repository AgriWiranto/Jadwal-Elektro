namespace Aplikasi_Jadwal_ELEKTRO
{
    partial class FormKelasMatkul
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
            this.HapusMatkulUntukKelas = new System.Windows.Forms.Button();
            this.TambahMatkulUntukKelas = new System.Windows.Forms.Button();
            this.label3 = new System.Windows.Forms.Label();
            this.cmbMatkul = new System.Windows.Forms.ComboBox();
            this.label2 = new System.Windows.Forms.Label();
            this.dgvKelasMatkul = new System.Windows.Forms.DataGridView();
            this.cmbKelas = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.lblKodeMatkul = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.dgvKelasMatkul)).BeginInit();
            this.SuspendLayout();
            // 
            // HapusMatkulUntukKelas
            // 
            this.HapusMatkulUntukKelas.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.HapusMatkulUntukKelas.ImageAlign = System.Drawing.ContentAlignment.BottomRight;
            this.HapusMatkulUntukKelas.Location = new System.Drawing.Point(883, 781);
            this.HapusMatkulUntukKelas.Name = "HapusMatkulUntukKelas";
            this.HapusMatkulUntukKelas.Size = new System.Drawing.Size(219, 44);
            this.HapusMatkulUntukKelas.TabIndex = 25;
            this.HapusMatkulUntukKelas.Text = "Hapus Mata Kuliah";
            this.HapusMatkulUntukKelas.UseVisualStyleBackColor = true;
            this.HapusMatkulUntukKelas.Click += new System.EventHandler(this.HapusMatkulUntukKelas_Click);
            // 
            // TambahMatkulUntukKelas
            // 
            this.TambahMatkulUntukKelas.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.TambahMatkulUntukKelas.ImageAlign = System.Drawing.ContentAlignment.BottomRight;
            this.TambahMatkulUntukKelas.Location = new System.Drawing.Point(658, 781);
            this.TambahMatkulUntukKelas.Name = "TambahMatkulUntukKelas";
            this.TambahMatkulUntukKelas.Size = new System.Drawing.Size(219, 44);
            this.TambahMatkulUntukKelas.TabIndex = 24;
            this.TambahMatkulUntukKelas.Text = "Tambah Mata Kuliah";
            this.TambahMatkulUntukKelas.UseVisualStyleBackColor = true;
            this.TambahMatkulUntukKelas.Click += new System.EventHandler(this.TambahMatkulUntukKelas_Click);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Times New Roman", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.ForeColor = System.Drawing.Color.White;
            this.label3.Location = new System.Drawing.Point(70, 810);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(125, 23);
            this.label3.TabIndex = 23;
            this.label3.Text = "Mata Kuliah :";
            // 
            // cmbMatkul
            // 
            this.cmbMatkul.FormattingEnabled = true;
            this.cmbMatkul.Location = new System.Drawing.Point(214, 810);
            this.cmbMatkul.Name = "cmbMatkul";
            this.cmbMatkul.Size = new System.Drawing.Size(393, 21);
            this.cmbMatkul.TabIndex = 22;
            this.cmbMatkul.SelectedIndexChanged += new System.EventHandler(this.cmbMatkul_SelectedIndexChanged);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Times New Roman", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.ForeColor = System.Drawing.Color.White;
            this.label2.Location = new System.Drawing.Point(70, 324);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(68, 23);
            this.label2.TabIndex = 21;
            this.label2.Text = "Kelas :";
            // 
            // dgvKelasMatkul
            // 
            this.dgvKelasMatkul.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvKelasMatkul.BackgroundColor = System.Drawing.SystemColors.ButtonHighlight;
            this.dgvKelasMatkul.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvKelasMatkul.Location = new System.Drawing.Point(73, 397);
            this.dgvKelasMatkul.MultiSelect = false;
            this.dgvKelasMatkul.Name = "dgvKelasMatkul";
            this.dgvKelasMatkul.ReadOnly = true;
            this.dgvKelasMatkul.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvKelasMatkul.Size = new System.Drawing.Size(1435, 364);
            this.dgvKelasMatkul.TabIndex = 20;
            // 
            // cmbKelas
            // 
            this.cmbKelas.FormattingEnabled = true;
            this.cmbKelas.Location = new System.Drawing.Point(165, 328);
            this.cmbKelas.Name = "cmbKelas";
            this.cmbKelas.Size = new System.Drawing.Size(393, 21);
            this.cmbKelas.TabIndex = 19;
            this.cmbKelas.SelectedIndexChanged += new System.EventHandler(this.cmbKelas_SelectedIndexChanged);
            // 
            // label1
            // 
            this.label1.Font = new System.Drawing.Font("Book Antiqua", 27.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.Color.White;
            this.label1.Location = new System.Drawing.Point(-11, 99);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(1531, 149);
            this.label1.TabIndex = 18;
            this.label1.Text = "KELAS MATKUL";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lblKodeMatkul
            // 
            this.lblKodeMatkul.AutoSize = true;
            this.lblKodeMatkul.Font = new System.Drawing.Font("Calibri", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblKodeMatkul.ForeColor = System.Drawing.Color.White;
            this.lblKodeMatkul.Location = new System.Drawing.Point(216, 775);
            this.lblKodeMatkul.Name = "lblKodeMatkul";
            this.lblKodeMatkul.Size = new System.Drawing.Size(0, 26);
            this.lblKodeMatkul.TabIndex = 27;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Times New Roman", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.ForeColor = System.Drawing.Color.White;
            this.label4.Location = new System.Drawing.Point(70, 775);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(126, 23);
            this.label4.TabIndex = 26;
            this.label4.Text = "Kode             :";
            // 
            // FormKelasMatkul
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Navy;
            this.ClientSize = new System.Drawing.Size(1508, 924);
            this.Controls.Add(this.lblKodeMatkul);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.HapusMatkulUntukKelas);
            this.Controls.Add(this.TambahMatkulUntukKelas);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.cmbMatkul);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.dgvKelasMatkul);
            this.Controls.Add(this.cmbKelas);
            this.Controls.Add(this.label1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "FormKelasMatkul";
            this.Text = "FormKelasMatkul";
            this.Load += new System.EventHandler(this.FormKelasMatkul_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dgvKelasMatkul)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button HapusMatkulUntukKelas;
        private System.Windows.Forms.Button TambahMatkulUntukKelas;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.ComboBox cmbMatkul;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.DataGridView dgvKelasMatkul;
        private System.Windows.Forms.ComboBox cmbKelas;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label lblKodeMatkul;
        private System.Windows.Forms.Label label4;
    }
}