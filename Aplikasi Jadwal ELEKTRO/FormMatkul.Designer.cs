namespace Aplikasi_Jadwal_ELEKTRO
{
    partial class FormMatkul
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
            this.MatkulTabel = new System.Windows.Forms.DataGridView();
            this.btn_tambahdataMatkul = new System.Windows.Forms.Button();
            this.btnRefreshMK = new System.Windows.Forms.Button();
            this.btn_editMatkul = new System.Windows.Forms.Button();
            this.btn_deleteMatkul = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.MatkulTabel)).BeginInit();
            this.SuspendLayout();
            // 
            // MatkulTabel
            // 
            this.MatkulTabel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.MatkulTabel.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.MatkulTabel.BackgroundColor = System.Drawing.Color.Silver;
            this.MatkulTabel.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.MatkulTabel.GridColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.MatkulTabel.Location = new System.Drawing.Point(25, 290);
            this.MatkulTabel.MultiSelect = false;
            this.MatkulTabel.Name = "MatkulTabel";
            this.MatkulTabel.ReadOnly = true;
            this.MatkulTabel.RowHeadersVisible = false;
            this.MatkulTabel.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.MatkulTabel.Size = new System.Drawing.Size(1509, 633);
            this.MatkulTabel.TabIndex = 6;
            this.MatkulTabel.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.MatkulTabel_CellContentClick);
            this.MatkulTabel.SelectionChanged += new System.EventHandler(this.MatkulTabel_SelectionChanged);
            // 
            // btn_tambahdataMatkul
            // 
            this.btn_tambahdataMatkul.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btn_tambahdataMatkul.ImageAlign = System.Drawing.ContentAlignment.BottomRight;
            this.btn_tambahdataMatkul.Location = new System.Drawing.Point(25, 219);
            this.btn_tambahdataMatkul.Name = "btn_tambahdataMatkul";
            this.btn_tambahdataMatkul.Size = new System.Drawing.Size(153, 45);
            this.btn_tambahdataMatkul.TabIndex = 5;
            this.btn_tambahdataMatkul.Text = "Tambah Data";
            this.btn_tambahdataMatkul.UseVisualStyleBackColor = true;
            this.btn_tambahdataMatkul.Click += new System.EventHandler(this.btn_tambahdataMatkul_Click);
            // 
            // btnRefreshMK
            // 
            this.btnRefreshMK.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnRefreshMK.ImageAlign = System.Drawing.ContentAlignment.BottomRight;
            this.btnRefreshMK.Location = new System.Drawing.Point(1421, 219);
            this.btnRefreshMK.Name = "btnRefreshMK";
            this.btnRefreshMK.Size = new System.Drawing.Size(113, 45);
            this.btnRefreshMK.TabIndex = 9;
            this.btnRefreshMK.Text = "Refresh";
            this.btnRefreshMK.UseVisualStyleBackColor = true;
            this.btnRefreshMK.Click += new System.EventHandler(this.btnRefreshMK_Click);
            // 
            // btn_editMatkul
            // 
            this.btn_editMatkul.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btn_editMatkul.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btn_editMatkul.Location = new System.Drawing.Point(1166, 218);
            this.btn_editMatkul.Name = "btn_editMatkul";
            this.btn_editMatkul.Size = new System.Drawing.Size(112, 45);
            this.btn_editMatkul.TabIndex = 8;
            this.btn_editMatkul.Text = "Edit";
            this.btn_editMatkul.UseVisualStyleBackColor = true;
            this.btn_editMatkul.Click += new System.EventHandler(this.btn_editMatkul_Click);
            // 
            // btn_deleteMatkul
            // 
            this.btn_deleteMatkul.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btn_deleteMatkul.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btn_deleteMatkul.Location = new System.Drawing.Point(1294, 219);
            this.btn_deleteMatkul.Name = "btn_deleteMatkul";
            this.btn_deleteMatkul.Size = new System.Drawing.Size(112, 44);
            this.btn_deleteMatkul.TabIndex = 7;
            this.btn_deleteMatkul.Text = "Delete";
            this.btn_deleteMatkul.UseVisualStyleBackColor = true;
            this.btn_deleteMatkul.Click += new System.EventHandler(this.btn_deleteMatkul_Click);
            // 
            // label1
            // 
            this.label1.Font = new System.Drawing.Font("Book Antiqua", 27.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.SystemColors.Control;
            this.label1.Location = new System.Drawing.Point(12, 32);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(1532, 165);
            this.label1.TabIndex = 10;
            this.label1.Text = "HALAMAN MATA KULIAH";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.label1.Click += new System.EventHandler(this.label1_Click);
            // 
            // FormMatkul
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Navy;
            this.ClientSize = new System.Drawing.Size(1556, 1041);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.btnRefreshMK);
            this.Controls.Add(this.btn_editMatkul);
            this.Controls.Add(this.btn_deleteMatkul);
            this.Controls.Add(this.MatkulTabel);
            this.Controls.Add(this.btn_tambahdataMatkul);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "FormMatkul";
            this.Text = "FormMatkul";
            this.Load += new System.EventHandler(this.FormMatkul_Load);
            ((System.ComponentModel.ISupportInitialize)(this.MatkulTabel)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.DataGridView MatkulTabel;
        private System.Windows.Forms.Button btn_tambahdataMatkul;
        private System.Windows.Forms.Button btnRefreshMK;
        private System.Windows.Forms.Button btn_editMatkul;
        private System.Windows.Forms.Button btn_deleteMatkul;
        private System.Windows.Forms.Label label1;
    }
}