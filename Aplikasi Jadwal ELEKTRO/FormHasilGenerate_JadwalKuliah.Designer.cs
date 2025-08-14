namespace Aplikasi_Jadwal_ELEKTRO
{
    partial class FormHasilGenerate_JadwalKuliah
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
            this.dataGridViewHasilGenerate = new System.Windows.Forms.DataGridView();
            this.btnKembali = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewHasilGenerate)).BeginInit();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.Font = new System.Drawing.Font("Book Antiqua", 39.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(12, 99);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(1867, 147);
            this.label1.TabIndex = 0;
            this.label1.Text = "HASIL GENERATE JADWAL";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.label1.Click += new System.EventHandler(this.label1_Click);
            // 
            // dataGridViewHasilGenerate
            // 
            this.dataGridViewHasilGenerate.BackgroundColor = System.Drawing.Color.Silver;
            this.dataGridViewHasilGenerate.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridViewHasilGenerate.Location = new System.Drawing.Point(23, 406);
            this.dataGridViewHasilGenerate.Name = "dataGridViewHasilGenerate";
            this.dataGridViewHasilGenerate.ReadOnly = true;
            this.dataGridViewHasilGenerate.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dataGridViewHasilGenerate.Size = new System.Drawing.Size(1856, 563);
            this.dataGridViewHasilGenerate.TabIndex = 1;
            // 
            // btnKembali
            // 
            this.btnKembali.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnKembali.Location = new System.Drawing.Point(1748, 333);
            this.btnKembali.Name = "btnKembali";
            this.btnKembali.Size = new System.Drawing.Size(131, 43);
            this.btnKembali.TabIndex = 2;
            this.btnKembali.Text = "Kembali";
            this.btnKembali.UseVisualStyleBackColor = true;
            // 
            // FormHasilGenerate_JadwalKuliah
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Navy;
            this.ClientSize = new System.Drawing.Size(1904, 1041);
            this.Controls.Add(this.btnKembali);
            this.Controls.Add(this.dataGridViewHasilGenerate);
            this.Controls.Add(this.label1);
            this.Name = "FormHasilGenerate_JadwalKuliah";
            this.Text = "FormHasilGenerate_JadwalKuliah";
            this.Load += new System.EventHandler(this.FormHasilGenerate_JadwalKuliah_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewHasilGenerate)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.DataGridView dataGridViewHasilGenerate;
        private System.Windows.Forms.Button btnKembali;
    }
}