using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Aplikasi_Jadwal_ELEKTRO.Models;
using Aplikasi_Jadwal_ELEKTRO.Repositories;

namespace Aplikasi_Jadwal_ELEKTRO
{
    public partial class FormDosen : Form
    {
        private System.Windows.Forms.DataGridView DataGridDosen;

        private void InitializeComponent()
        {
            this.DataGridDosen = new System.Windows.Forms.DataGridView();
            this.label1 = new System.Windows.Forms.Label();
            this.btn_tambahdataDosen = new System.Windows.Forms.Button();
            this.btn_editDosen = new System.Windows.Forms.Button();
            this.btn_deleteDosen = new System.Windows.Forms.Button();
            this.btnRefreshDosen = new System.Windows.Forms.Button();
            this.btnLihatMatkul = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.DataGridDosen)).BeginInit();
            this.SuspendLayout();
            // 
            // DataGridDosen
            // 
            this.DataGridDosen.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.DataGridDosen.BackgroundColor = System.Drawing.Color.Silver;
            this.DataGridDosen.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.DataGridDosen.GridColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.DataGridDosen.Location = new System.Drawing.Point(47, 290);
            this.DataGridDosen.MultiSelect = false;
            this.DataGridDosen.Name = "DataGridDosen";
            this.DataGridDosen.ReadOnly = true;
            this.DataGridDosen.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.DataGridDosen.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.DataGridDosen.Size = new System.Drawing.Size(1443, 669);
            this.DataGridDosen.TabIndex = 1;
            this.DataGridDosen.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.DataGridDosen_CellContentClick);
            // 
            // label1
            // 
            this.label1.BackColor = System.Drawing.Color.Navy;
            this.label1.Font = new System.Drawing.Font("Book Antiqua", 27.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.SystemColors.ButtonFace;
            this.label1.Location = new System.Drawing.Point(353, 62);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(685, 157);
            this.label1.TabIndex = 14;
            this.label1.Text = "HALAMAN DOSEN";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.label1.Click += new System.EventHandler(this.Label1_Click);
            // 
            // btn_tambahdataDosen
            // 
            this.btn_tambahdataDosen.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btn_tambahdataDosen.ImageAlign = System.Drawing.ContentAlignment.BottomRight;
            this.btn_tambahdataDosen.Location = new System.Drawing.Point(47, 230);
            this.btn_tambahdataDosen.Name = "btn_tambahdataDosen";
            this.btn_tambahdataDosen.Size = new System.Drawing.Size(153, 44);
            this.btn_tambahdataDosen.TabIndex = 15;
            this.btn_tambahdataDosen.Text = "Tambah Data";
            this.btn_tambahdataDosen.UseVisualStyleBackColor = true;
            this.btn_tambahdataDosen.Click += new System.EventHandler(this.Btn_tambahdataDosen_Click);
            // 
            // btn_editDosen
            // 
            this.btn_editDosen.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btn_editDosen.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btn_editDosen.Location = new System.Drawing.Point(968, 242);
            this.btn_editDosen.Name = "btn_editDosen";
            this.btn_editDosen.Size = new System.Drawing.Size(121, 32);
            this.btn_editDosen.TabIndex = 17;
            this.btn_editDosen.Text = "Edit";
            this.btn_editDosen.UseVisualStyleBackColor = true;
            this.btn_editDosen.Click += new System.EventHandler(this.Btn_editDosen_Click);
            // 
            // btn_deleteDosen
            // 
            this.btn_deleteDosen.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btn_deleteDosen.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btn_deleteDosen.Location = new System.Drawing.Point(1095, 242);
            this.btn_deleteDosen.Name = "btn_deleteDosen";
            this.btn_deleteDosen.Size = new System.Drawing.Size(121, 32);
            this.btn_deleteDosen.TabIndex = 18;
            this.btn_deleteDosen.Text = "Delete";
            this.btn_deleteDosen.UseVisualStyleBackColor = true;
            this.btn_deleteDosen.Click += new System.EventHandler(this.Btn_deleteDosen_Click);
            // 
            // btnRefreshDosen
            // 
            this.btnRefreshDosen.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnRefreshDosen.ImageAlign = System.Drawing.ContentAlignment.BottomRight;
            this.btnRefreshDosen.Location = new System.Drawing.Point(1222, 242);
            this.btnRefreshDosen.Name = "btnRefreshDosen";
            this.btnRefreshDosen.Size = new System.Drawing.Size(121, 32);
            this.btnRefreshDosen.TabIndex = 19;
            this.btnRefreshDosen.Text = "Refresh";
            this.btnRefreshDosen.UseVisualStyleBackColor = true;
            this.btnRefreshDosen.Click += new System.EventHandler(this.BtnRefreshDosen_Click);
            // 
            // btnLihatMatkul
            // 
            this.btnLihatMatkul.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnLihatMatkul.ImageAlign = System.Drawing.ContentAlignment.BottomRight;
            this.btnLihatMatkul.Location = new System.Drawing.Point(1349, 243);
            this.btnLihatMatkul.Name = "btnLihatMatkul";
            this.btnLihatMatkul.Size = new System.Drawing.Size(141, 32);
            this.btnLihatMatkul.TabIndex = 20;
            this.btnLihatMatkul.Text = "Lihat Matkul";
            this.btnLihatMatkul.UseVisualStyleBackColor = true;
            this.btnLihatMatkul.Click += new System.EventHandler(this.button1_Click);
            // 
            // FormDosen
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Navy;
            this.ClientSize = new System.Drawing.Size(1540, 1002);
            this.Controls.Add(this.btnLihatMatkul);
            this.Controls.Add(this.btnRefreshDosen);
            this.Controls.Add(this.btn_deleteDosen);
            this.Controls.Add(this.btn_editDosen);
            this.Controls.Add(this.btn_tambahdataDosen);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.DataGridDosen);
            this.Name = "FormDosen";
            this.Text = "FormDosen";
            this.Load += new System.EventHandler(this.FormDosen_Load);
            ((System.ComponentModel.ISupportInitialize)(this.DataGridDosen)).EndInit();
            this.ResumeLayout(false);

        }

        private Label label1;
        private Button btn_tambahdataDosen;
        private Button btn_editDosen;
        private Button btn_deleteDosen;
        private Button btnRefreshDosen;
        private Button btnLihatMatkul;
    }
}
