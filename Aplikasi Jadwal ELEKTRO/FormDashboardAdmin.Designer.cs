namespace Aplikasi_Jadwal_ELEKTRO
{
    partial class FormDashboardAdmin
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormDashboardAdmin));
            this.DASHBOARD = new System.Windows.Forms.Label();
            this.btnGenerate = new System.Windows.Forms.Button();
            this.button3 = new System.Windows.Forms.Button();
            this.button4 = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.button5 = new System.Windows.Forms.Button();
            this.tabControlKelas = new System.Windows.Forms.TabControl();
            this.btnCekKonsistensi = new System.Windows.Forms.Button();
            this.btnPerbaikiOtomatis = new System.Windows.Forms.Button();
            this.btnCancel = new System.Windows.Forms.Button();
            this.progressBar1 = new System.Windows.Forms.ProgressBar();
            this.lblProgress = new System.Windows.Forms.Label();
            this.btnExportPdf = new System.Windows.Forms.Button();
            this.btnExportExcel = new System.Windows.Forms.Button();
            this.btnGenerateWaktu = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // DASHBOARD
            // 
            this.DASHBOARD.Font = new System.Drawing.Font("Book Antiqua", 27.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.DASHBOARD.ForeColor = System.Drawing.SystemColors.Control;
            this.DASHBOARD.Location = new System.Drawing.Point(12, 21);
            this.DASHBOARD.Name = "DASHBOARD";
            this.DASHBOARD.Size = new System.Drawing.Size(1503, 115);
            this.DASHBOARD.TabIndex = 0;
            this.DASHBOARD.Text = "HALAMAN DASHBOARD";
            this.DASHBOARD.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.DASHBOARD.Click += new System.EventHandler(this.DASHBOARD_Click);
            // 
            // btnGenerate
            // 
            this.btnGenerate.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.btnGenerate.Font = new System.Drawing.Font("Book Antiqua", 24F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnGenerate.Location = new System.Drawing.Point(30, 177);
            this.btnGenerate.Name = "btnGenerate";
            this.btnGenerate.Size = new System.Drawing.Size(329, 80);
            this.btnGenerate.TabIndex = 2;
            this.btnGenerate.Text = "GENERATE ";
            this.btnGenerate.UseVisualStyleBackColor = true;
            this.btnGenerate.Click += new System.EventHandler(this.button1_Click);
            // 
            // button3
            // 
            this.button3.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("button3.BackgroundImage")));
            this.button3.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.button3.Location = new System.Drawing.Point(44, 13);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(53, 53);
            this.button3.TabIndex = 4;
            this.button3.UseVisualStyleBackColor = true;
            // 
            // button4
            // 
            this.button4.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("button4.BackgroundImage")));
            this.button4.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.button4.Location = new System.Drawing.Point(139, 13);
            this.button4.Name = "button4";
            this.button4.Size = new System.Drawing.Size(53, 53);
            this.button4.TabIndex = 5;
            this.button4.UseVisualStyleBackColor = true;
            // 
            // label2
            // 
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(23, 73);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(98, 25);
            this.label2.TabIndex = 7;
            this.label2.Text = "Open File";
            this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label3
            // 
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(127, 70);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(85, 31);
            this.label3.TabIndex = 8;
            this.label3.Text = "Save File";
            this.label3.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // textBox1
            // 
            this.textBox1.BackColor = System.Drawing.Color.LightGray;
            this.textBox1.Font = new System.Drawing.Font("Microsoft Sans Serif", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.textBox1.Location = new System.Drawing.Point(1031, 123);
            this.textBox1.Multiline = true;
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(429, 52);
            this.textBox1.TabIndex = 9;
            // 
            // button5
            // 
            this.button5.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("button5.BackgroundImage")));
            this.button5.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.button5.Location = new System.Drawing.Point(1466, 123);
            this.button5.Name = "button5";
            this.button5.Size = new System.Drawing.Size(49, 52);
            this.button5.TabIndex = 10;
            this.button5.UseVisualStyleBackColor = true;
            // 
            // tabControlKelas
            // 
            this.tabControlKelas.Location = new System.Drawing.Point(44, 389);
            this.tabControlKelas.Name = "tabControlKelas";
            this.tabControlKelas.SelectedIndex = 0;
            this.tabControlKelas.Size = new System.Drawing.Size(1471, 521);
            this.tabControlKelas.TabIndex = 20;
            // 
            // btnCekKonsistensi
            // 
            this.btnCekKonsistensi.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnCekKonsistensi.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnCekKonsistensi.Location = new System.Drawing.Point(913, 246);
            this.btnCekKonsistensi.Name = "btnCekKonsistensi";
            this.btnCekKonsistensi.Size = new System.Drawing.Size(288, 52);
            this.btnCekKonsistensi.TabIndex = 21;
            this.btnCekKonsistensi.Text = "Cek Konsistensi Jadwal";
            this.btnCekKonsistensi.UseVisualStyleBackColor = true;
            this.btnCekKonsistensi.Click += new System.EventHandler(this.btnCekKonsistensi_Click);
            // 
            // btnPerbaikiOtomatis
            // 
            this.btnPerbaikiOtomatis.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnPerbaikiOtomatis.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnPerbaikiOtomatis.Location = new System.Drawing.Point(1227, 246);
            this.btnPerbaikiOtomatis.Name = "btnPerbaikiOtomatis";
            this.btnPerbaikiOtomatis.Size = new System.Drawing.Size(288, 52);
            this.btnPerbaikiOtomatis.TabIndex = 22;
            this.btnPerbaikiOtomatis.Text = "Perbaiki Otomatis";
            this.btnPerbaikiOtomatis.UseVisualStyleBackColor = true;
            this.btnPerbaikiOtomatis.Click += new System.EventHandler(this.btnPerbaikiOtomatis_Click);
            // 
            // btnCancel
            // 
            this.btnCancel.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.btnCancel.Font = new System.Drawing.Font("Book Antiqua", 24F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnCancel.Location = new System.Drawing.Point(30, 277);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(329, 80);
            this.btnCancel.TabIndex = 23;
            this.btnCancel.Text = "CANCEL";
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // progressBar1
            // 
            this.progressBar1.Location = new System.Drawing.Point(248, 952);
            this.progressBar1.Name = "progressBar1";
            this.progressBar1.Size = new System.Drawing.Size(953, 10);
            this.progressBar1.Style = System.Windows.Forms.ProgressBarStyle.Marquee;
            this.progressBar1.TabIndex = 24;
            // 
            // lblProgress
            // 
            this.lblProgress.AutoSize = true;
            this.lblProgress.ForeColor = System.Drawing.SystemColors.ButtonHighlight;
            this.lblProgress.Location = new System.Drawing.Point(707, 925);
            this.lblProgress.Name = "lblProgress";
            this.lblProgress.Size = new System.Drawing.Size(35, 13);
            this.lblProgress.TabIndex = 25;
            this.lblProgress.Text = "label1";
            // 
            // btnExportPdf
            // 
            this.btnExportPdf.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnExportPdf.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnExportPdf.Location = new System.Drawing.Point(913, 322);
            this.btnExportPdf.Name = "btnExportPdf";
            this.btnExportPdf.Size = new System.Drawing.Size(288, 52);
            this.btnExportPdf.TabIndex = 26;
            this.btnExportPdf.Text = "Eksport ke PDF";
            this.btnExportPdf.UseVisualStyleBackColor = true;
            this.btnExportPdf.Click += new System.EventHandler(this.btnExportPdf_Click);
            // 
            // btnExportExcel
            // 
            this.btnExportExcel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnExportExcel.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnExportExcel.Location = new System.Drawing.Point(1227, 322);
            this.btnExportExcel.Name = "btnExportExcel";
            this.btnExportExcel.Size = new System.Drawing.Size(288, 52);
            this.btnExportExcel.TabIndex = 27;
            this.btnExportExcel.Text = "Eksport ke Excel";
            this.btnExportExcel.UseVisualStyleBackColor = true;
            this.btnExportExcel.Click += new System.EventHandler(this.btnExportExcel_Click);
            // 
            // btnGenerateWaktu
            // 
            this.btnGenerateWaktu.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnGenerateWaktu.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnGenerateWaktu.Location = new System.Drawing.Point(1286, 925);
            this.btnGenerateWaktu.Name = "btnGenerateWaktu";
            this.btnGenerateWaktu.Size = new System.Drawing.Size(229, 52);
            this.btnGenerateWaktu.TabIndex = 28;
            this.btnGenerateWaktu.Text = "Generate Waktu";
            this.btnGenerateWaktu.UseVisualStyleBackColor = true;
            this.btnGenerateWaktu.Click += new System.EventHandler(this.button1_Click_1);
            // 
            // FormDashboardAdmin
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Navy;
            this.ClientSize = new System.Drawing.Size(1540, 1002);
            this.Controls.Add(this.btnGenerateWaktu);
            this.Controls.Add(this.btnExportExcel);
            this.Controls.Add(this.btnExportPdf);
            this.Controls.Add(this.lblProgress);
            this.Controls.Add(this.progressBar1);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnPerbaikiOtomatis);
            this.Controls.Add(this.btnCekKonsistensi);
            this.Controls.Add(this.tabControlKelas);
            this.Controls.Add(this.button5);
            this.Controls.Add(this.textBox1);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.button4);
            this.Controls.Add(this.button3);
            this.Controls.Add(this.btnGenerate);
            this.Controls.Add(this.DASHBOARD);
            this.Cursor = System.Windows.Forms.Cursors.Default;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "FormDashboardAdmin";
            this.Text = "FormDashboardAdmin";
            this.Load += new System.EventHandler(this.FormDashboardAdmin_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label DASHBOARD;
        private System.Windows.Forms.Button btnGenerate;
        private System.Windows.Forms.Button button3;
        private System.Windows.Forms.Button button4;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.Button button5;
        private System.Windows.Forms.TabControl tabControlKelas;
        private System.Windows.Forms.Button btnCekKonsistensi;
        private System.Windows.Forms.Button btnPerbaikiOtomatis;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.ProgressBar progressBar1;
        private System.Windows.Forms.Label lblProgress;
        private System.Windows.Forms.Button btnExportPdf;
        private System.Windows.Forms.Button btnExportExcel;
        private System.Windows.Forms.Button btnGenerateWaktu;
    }
}