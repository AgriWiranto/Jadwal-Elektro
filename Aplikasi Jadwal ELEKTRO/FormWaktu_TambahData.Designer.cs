// FormWaktu_TambahData.Designer.cs — Revisi Final Dengan Label Text Sebelah Kiri
namespace Aplikasi_Jadwal_ELEKTRO
{
    partial class FormWaktu_TambahData
    {
        private System.ComponentModel.IContainer components = null;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        private void InitializeComponent()
        {
            this.cbTipeKelas = new System.Windows.Forms.ComboBox();
            this.tbDurasiPerSKS = new System.Windows.Forms.TextBox();
            this.tbJamMulai = new System.Windows.Forms.DateTimePicker();
            this.tbJamSelesai = new System.Windows.Forms.DateTimePicker();
            this.tbIstirahatSesi = new System.Windows.Forms.TextBox();
            this.btnSimpan = new System.Windows.Forms.Button();
            this.btnKembali = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.lblTipeKelas = new System.Windows.Forms.Label();
            this.lblJamMulai = new System.Windows.Forms.Label();
            this.lblDurasi = new System.Windows.Forms.Label();
            this.lblIstirahat = new System.Windows.Forms.Label();
            this.lblJamSelesai = new System.Windows.Forms.Label();
            this.SuspendLayout();

            // label1 (judul)
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Book Antiqua", 18F, System.Drawing.FontStyle.Bold);
            this.label1.Location = new System.Drawing.Point(160, 20);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(230, 28);
            this.label1.TabIndex = 0;
            this.label1.Text = "TAMBAH WAKTU";

            // lblTipeKelas
            this.lblTipeKelas.AutoSize = true;
            this.lblTipeKelas.Text = "Tipe Kelas";
            this.lblTipeKelas.Location = new System.Drawing.Point(80, 93);
            this.lblTipeKelas.Size = new System.Drawing.Size(60, 13);

            // cbTipeKelas
            this.cbTipeKelas.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbTipeKelas.Location = new System.Drawing.Point(200, 90);
            this.cbTipeKelas.Name = "cbTipeKelas";
            this.cbTipeKelas.Size = new System.Drawing.Size(200, 21);
            this.cbTipeKelas.TabIndex = 1;
            this.cbTipeKelas.SelectedIndexChanged += new System.EventHandler(this.cbTipeKelas_SelectedIndexChanged);

            // lblJamMulai
            this.lblJamMulai.AutoSize = true;
            this.lblJamMulai.Text = "Jam Mulai";
            this.lblJamMulai.Location = new System.Drawing.Point(80, 133);
            this.lblJamMulai.Size = new System.Drawing.Size(58, 13);

            // tbJamMulai
            this.tbJamMulai.Format = System.Windows.Forms.DateTimePickerFormat.Time;
            this.tbJamMulai.Location = new System.Drawing.Point(200, 130);
            this.tbJamMulai.Name = "tbJamMulai";
            this.tbJamMulai.ShowUpDown = true;
            this.tbJamMulai.Size = new System.Drawing.Size(200, 20);
            this.tbJamMulai.TabIndex = 2;
            this.tbJamMulai.ValueChanged += new System.EventHandler(this.tbJamMulai_ValueChanged);

            // lblDurasi
            this.lblDurasi.AutoSize = true;
            this.lblDurasi.Text = "Durasi (menit)";
            this.lblDurasi.Location = new System.Drawing.Point(80, 173);
            this.lblDurasi.Size = new System.Drawing.Size(77, 13);

            // tbDurasiPerSKS
            this.tbDurasiPerSKS.Location = new System.Drawing.Point(200, 170);
            this.tbDurasiPerSKS.Name = "tbDurasiPerSKS";
            this.tbDurasiPerSKS.Size = new System.Drawing.Size(200, 20);
            this.tbDurasiPerSKS.TabIndex = 3;
            this.tbDurasiPerSKS.TextChanged += new System.EventHandler(this.tbDurasiPerSKS_TextChanged);

            // lblIstirahat
            this.lblIstirahat.AutoSize = true;
            this.lblIstirahat.Text = "Istirahat Sesi";
            this.lblIstirahat.Location = new System.Drawing.Point(80, 213);
            this.lblIstirahat.Size = new System.Drawing.Size(75, 13);

            // tbIstirahatSesi
            this.tbIstirahatSesi.Location = new System.Drawing.Point(200, 210);
            this.tbIstirahatSesi.Name = "tbIstirahatSesi";
            this.tbIstirahatSesi.Size = new System.Drawing.Size(200, 20);
            this.tbIstirahatSesi.TabIndex = 4;

            // lblJamSelesai
            this.lblJamSelesai.AutoSize = true;
            this.lblJamSelesai.Text = "Jam Selesai";
            this.lblJamSelesai.Location = new System.Drawing.Point(80, 253);
            this.lblJamSelesai.Size = new System.Drawing.Size(64, 13);

            // tbJamSelesai
            this.tbJamSelesai.Format = System.Windows.Forms.DateTimePickerFormat.Time;
            this.tbJamSelesai.Location = new System.Drawing.Point(200, 250);
            this.tbJamSelesai.Name = "tbJamSelesai";
            this.tbJamSelesai.ShowUpDown = true;
            this.tbJamSelesai.Size = new System.Drawing.Size(200, 20);
            this.tbJamSelesai.TabIndex = 5;
            this.tbJamSelesai.Enabled = false;

            // btnSimpan
            this.btnSimpan.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
            this.btnSimpan.Location = new System.Drawing.Point(200, 290);
            this.btnSimpan.Name = "btnSimpan";
            this.btnSimpan.Size = new System.Drawing.Size(90, 30);
            this.btnSimpan.TabIndex = 6;
            this.btnSimpan.Text = "Simpan";
            this.btnSimpan.UseVisualStyleBackColor = true;
            this.btnSimpan.Click += new System.EventHandler(this.btnSimpan_Click);

            // btnKembali
            this.btnKembali.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
            this.btnKembali.Location = new System.Drawing.Point(310, 290);
            this.btnKembali.Name = "btnKembali";
            this.btnKembali.Size = new System.Drawing.Size(90, 30);
            this.btnKembali.TabIndex = 7;
            this.btnKembali.Text = "Kembali";
            this.btnKembali.UseVisualStyleBackColor = true;
            this.btnKembali.Click += new System.EventHandler(this.btnKembali_Click);

            // FormWaktu_TambahData
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(540, 360);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.lblTipeKelas);
            this.Controls.Add(this.lblJamMulai);
            this.Controls.Add(this.lblDurasi);
            this.Controls.Add(this.lblIstirahat);
            this.Controls.Add(this.lblJamSelesai);
            this.Controls.Add(this.cbTipeKelas);
            this.Controls.Add(this.tbDurasiPerSKS);
            this.Controls.Add(this.tbJamMulai);
            this.Controls.Add(this.tbJamSelesai);
            this.Controls.Add(this.tbIstirahatSesi);
            this.Controls.Add(this.btnSimpan);
            this.Controls.Add(this.btnKembali);
            this.Name = "FormWaktu_TambahData";
            this.Text = "FormWaktu_TambahData";
            this.Load += new System.EventHandler(this.FormWaktu_TambahData_Load);
            this.ResumeLayout(false);
            this.PerformLayout();
        }

        #endregion

        private System.Windows.Forms.ComboBox cbTipeKelas;
        private System.Windows.Forms.TextBox tbDurasiPerSKS;
        private System.Windows.Forms.DateTimePicker tbJamMulai;
        private System.Windows.Forms.DateTimePicker tbJamSelesai;
        private System.Windows.Forms.TextBox tbIstirahatSesi;
        private System.Windows.Forms.Button btnSimpan;
        private System.Windows.Forms.Button btnKembali;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label lblTipeKelas;
        private System.Windows.Forms.Label lblJamMulai;
        private System.Windows.Forms.Label lblDurasi;
        private System.Windows.Forms.Label lblIstirahat;
        private System.Windows.Forms.Label lblJamSelesai;
    }
}
